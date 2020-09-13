using System;
using System.Collections;
using System.Collections.Generic;
using Gungeon;
using MonoMod;
using UnityEngine;
using ItemAPI;
using Brave;
using Dungeonator;
using HutongGames.PlayMaker.Actions;
using MultiplayerBasicExample;
using System.Reflection;
using System.Xml.Serialization;

namespace Knives
{

    
    class CopperChariot :GunBehaviour
    {  

        public static void Add()
        {
            // Get yourself a new gun "base" first.
            // Let's just call it "Basic Gun", and use "jpxfrd" for all sprites and as "codename" All sprites must begin with the same word as the codename. For example, your firing sprite would be named "jpxfrd_fire_001".
            Gun gun = ETGMod.Databases.Items.NewGun("Copper Chariot", "chariot");
            // "kp:basic_gun determines how you spawn in your gun through the console. You can change this command to whatever you want, as long as it follows the "name:itemname" template.
            Game.Items.Rename("outdated_gun_mods:copper_chariot", "ski:copper_chariot");
            gun.gameObject.AddComponent<CopperChariot>();

            //These two lines determines the description of your gun, ".SetShortDescription" being the description that appears when you pick up the gun and ".SetLongDescription" being the description in the Ammonomicon entry. 
            gun.SetShortDescription("Blinding Speed!");
            gun.SetLongDescription("The ghost of a gun nut gaurds the weilder of this blade. While the blade itself is too weak to be used your new astral companion will be happy to provide assistance.\n\n" +
                "__________________________________\n\n" +
                "Copper Chariot can hurt you but you will not sustain damage from the hit. However, if you are at .5 hp it will kill you.");
            // This is required, unless you want to use the sprites of the base gun.
            // That, by default, is the pea shooter.
            // SetupSprite sets up the default gun sprite for the ammonomicon and the "gun get" popup.
            // WARNING: Add a copy of your default sprite to Ammonomicon Encounter Icon Collection!
            // That means, "sprites/Ammonomicon Encounter Icon Collection/defaultsprite.png" in your mod .zip. You can see an example of this with inside the mod folder.
            gun.SetupSprite(null, "chariot_idle_001", 8);
            // ETGMod automatically checks which animations are available.
            // The numbers next to "shootAnimation" determine the animation fps. You can also tweak the animation fps of the reload animation and idle animation using this method.
            gun.SetAnimationFPS(gun.shootAnimation, 8);
            gun.SetAnimationFPS(gun.reloadAnimation, 8);
            // Every modded gun has base projectile it works with that is borrowed from other guns in the game. 
            // The gun names are the names from the JSON dump! While most are the same, some guns named completely different things. If you need help finding gun names, ask a modder on the Gungeon discord.

            GunExt.AddProjectileModuleFrom(gun, "AK-47", true, true);
            
            gun.DefaultModule.ammoCost = 1;
            gun.DefaultModule.angleVariance = 0f;
            gun.gunClass = GunClass.SILLY;
            gun.reloadTime = .5f;
            gun.DefaultModule.shootStyle = ProjectileModule.ShootStyle.SemiAutomatic;
            gun.DefaultModule.sequenceStyle = ProjectileModule.ProjectileSequenceStyle.Random;
            gun.DefaultModule.cooldownTime = 1.5f;
            gun.DefaultModule.numberOfShotsInClip = 20;
            gun.InfiniteAmmo = true;
            gun.SetBaseMaxAmmo(1000);
            // Here we just set the quality of the gun and the "EncounterGuid", which is used by Gungeon to identify the gun.
            gun.quality = PickupObject.ItemQuality.B;
            gun.encounterTrackable.EncounterGuid = "hora hora hora hora!";
            //This block of code helps clone our projectile. Basically it makes it so things like Shadow Clone and Hip Holster keep the stats/sprite of your custom gun's projectiles.
            Projectile projectile = UnityEngine.Object.Instantiate<Projectile>(gun.DefaultModule.projectiles[0]);
            projectile.gameObject.SetActive(false);
            FakePrefab.MarkAsFakePrefab(projectile.gameObject);
            UnityEngine.Object.DontDestroyOnLoad(projectile);
            gun.DefaultModule.projectiles[0] = projectile;
            //projectile.baseData allows you to modify the base properties of your projectile module.
            //In our case, our gun uses modified projectiles from the ak-47.
            //Setting static values for a custom gun's projectile stats prevents them from scaling with player stats and bullet modifiers (damage, shotspeed, knockback)
            //You have to multiply the value of the original projectile you're using instead so they scale accordingly. For example if the projectile you're using as a base has 10 damage and you want it to be 6 you use this
            //In our case, our projectile has a base damage of 5.5, so we multiply it by 1.1 so it does 10% more damage from the ak-47.
            projectile.baseData.damage = 1f;
            projectile.baseData.speed *= 1f;
            projectile.baseData.range = .001f;
            projectile.baseData.force = 0f;

            projectile.pierceMinorBreakables = true;

            projectile.transform.parent = gun.barrelOffset;


            ETGMod.Databases.Items.Add(gun, null, "ANY");

        }

        public override void OnPostFired(PlayerController player, Gun gun)
        {
            //This determines what sound you want to play when you fire a gun.
            //Sounds names are based on the Gungeon sound dump, which can be found at EnterTheGungeon/Etg_Data/StreamingAssets/Audio/GeneratedSoundBanks/Windows/sfx.txt
            gun.PreventNormalFireAudio = false;

            if (combat == 1 && canSpawn == 0)
            {
                knightPlacer(player);
                canSpawn = 1;
            }
        }


        private bool HasReloaded;
        //This block of code allows us to change the reload sounds.
        protected void Update()
        {
            if (gun.CurrentOwner)
            {

                if (!gun.PreventNormalFireAudio)
                {
                    this.gun.PreventNormalFireAudio = true;
                }
                if (!gun.IsReloading && !HasReloaded)
                {
                    this.HasReloaded = true;
                }
                combatchecker();
            }
            if(this.gun.CurrentOwner != true)
            {
                nut.healthHaver.ApplyDamage(200f, Vector2.zero, "killed", CoreDamageTypes.Magic, DamageCategory.Unstoppable, true, null, true);
                canSpawn = 0;
            }
            if(nut.HasDamagedPlayer)
            {
                this.gun.CurrentOwner.healthHaver.ApplyHealing(.5f);
                nut.HasDamagedPlayer = false;
            }
           
        }
        public int canSpawn = 0;
        public int combat = 0;

        
        public void combatchecker()
        {
            PlayerController player = (PlayerController)this.gun.CurrentOwner;
            if (player.IsInCombat)
            {
                combat = 1;
            }
            else
            {
                combat = 0;
                canSpawn = 0;
            }
        }

        public override void OnReloadPressed(PlayerController player, Gun gun, bool bSOMETHING)
        {
            if (gun.IsReloading && this.HasReloaded)
            {
                HasReloaded = false;
                AkSoundEngine.PostEvent("Stop_WPN_All", base.gameObject);
                base.OnReloadPressed(player, gun, bSOMETHING);
                AkSoundEngine.PostEvent("Play_WPN_SAA_reload_01", base.gameObject);
                nut.healthHaver.ApplyDamage(200f, Vector2.zero, "killed", CoreDamageTypes.Magic, DamageCategory.Unstoppable, true, null, true);
                canSpawn = 0;
            }
        }
        AIActor nut;
        public void knightPlacer(PlayerController owner)
        {
            AIActor orLoadByGuid = EnemyDatabase.GetOrLoadByGuid("ec8ea75b557d4e7b8ceeaacdf6f8238c");


            IntVector2 aim = Vector2Extensions.ToIntVector2(owner.unadjustedAimPoint, VectorConversions.Round);
            RoomHandler room = GameManager.Instance.Dungeon.data.GetAbsoluteRoomFromPosition(aim);
            if (room != null && room == owner.CurrentRoom && owner.IsInCombat)
            {
                AIActor aiActor = AIActor.Spawn(orLoadByGuid.aiActor, owner.CenterPosition, room, true, AIActor.AwakenAnimationType.Spawn, true);
                PhysicsEngine.Instance.RegisterOverlappingGhostCollisionExceptions(aiActor.specRigidbody, null, false);
                aiActor.CanTargetEnemies = true;
                aiActor.CanTargetPlayers = false;
                aiActor.IsHarmlessEnemy = true;
                aiActor.CanDropCurrency = false;
                aiActor.IgnoreForRoomClear = true;
                aiActor.MovementSpeed = 5.95f;
                aiActor.CompanionOwner = owner;
                aiActor.IsBuffEnemy = true;
                aiActor.isPassable = true;
                
                aiActor.gameObject.AddComponent<KillOnRoomClear>();
                aiActor.reinforceType = AIActor.ReinforceType.Instant;
                aiActor.HandleReinforcementFallIntoRoom(.1f);
                nut = aiActor;

                MindControlEffect orAddComponent = aiActor.gameObject.GetOrAddComponent<MindControlEffect>();
                orAddComponent.owner = (this.gun.CurrentOwner as PlayerController);
            }

        }
       
    }
}

