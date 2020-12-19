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

    public class jumper : GunBehaviour
    {


        public static void Add()
        {
            // Get yourself a new gun "base" first.
            // Let's just call it "Basic Gun", and use "jpxfrd" for all sprites and as "codename" All sprites must begin with the same word as the codename. For example, your firing sprite would be named "jpxfrd_fire_001".
            Gun gun = ETGMod.Databases.Items.NewGun("Rocket Jumper", "jump");
            // "kp:basic_gun determines how you spawn in your gun through the console. You can change this command to whatever you want, as long as it follows the "name:itemname" template.
            Game.Items.Rename("outdated_gun_mods:rocket_jumper", "ski:rocket_jumper");
            gun.gameObject.AddComponent<jumper>();

            //These two lines determines the description of your gun, ".SetShortDescription" being the description that appears when you pick up the gun and ".SetLongDescription" being the description in the Ammonomicon entry. 
            gun.SetShortDescription("Deals no damage");
            gun.SetLongDescription("Filled with plungers?!");
            // This is required, unless you want to use the sprites of the base gun.
            
            // That means, "sprites/Ammonomicon Encounter Icon Collection/defaultsprite.png" in your mod .zip. You can see an example of this with inside the mod folder.
            gun.SetupSprite(null, "jump_idle_001", 8);
            // ETGMod automatically checks which animations are available.
            // The numbers next to "shootAnimation" determine the animation fps. You can also tweak the animation fps of the reload animation and idle animation using this method.
            gun.SetAnimationFPS(gun.shootAnimation, 24);
            // Every modded gun has base projectile it works with that is borrowed from other guns in the game. 
            // The gun names are the names from the JSON dump! While most are the same, some guns named completely different things. If you need help finding gun names, ask a modder on the Gungeon discord.

            gun.AddProjectileModuleFrom("ak-47", true, false);
            //I would reccomend replacing the line above with 'gun.AddProjectileModuleFrom(PickupObjectDatabase.GetById(id) as Gun, true, false);'
            //'id' in that code I just gave you should be the numerical Id of the gun you want your projectiles to be based off of.

            gun.DefaultModule.ammoCost = 1;
            gun.DefaultModule.shootStyle = ProjectileModule.ShootStyle.SemiAutomatic;
            gun.DefaultModule.sequenceStyle = ProjectileModule.ProjectileSequenceStyle.Random;
            gun.reloadTime = 1.0f;
            gun.DefaultModule.cooldownTime = 2f;
            gun.DefaultModule.numberOfShotsInClip = 4;
            gun.InfiniteAmmo = true;
            gun.gunHandedness = GunHandedness.TwoHanded;
            gun.gunClass = GunClass.EXPLOSIVE;

            gun.quality = PickupObject.ItemQuality.D;


            Gun gun3 = PickupObjectDatabase.GetById(27) as Gun; //This line isn't doing anything in the code
            Projectile projectile = UnityEngine.Object.Instantiate<Projectile>(gun3.DefaultModule.projectiles[0]);
            projectile.gameObject.SetActive(false);
            FakePrefab.MarkAsFakePrefab(projectile.gameObject);
            UnityEngine.Object.DontDestroyOnLoad(projectile);
            gun.DefaultModule.projectiles[0] = projectile;
            gun.reflectDuringReload = true;
            projectile.baseData.range = 0f;
            projectile.baseData.damage = 0f;
            projectile.baseData.speed = .001f;

            ETGMod.Databases.Items.Add(gun, null, "ANY");

        }

        public override void OnPostFired(PlayerController player, Gun gun)
        {
            //This determines what sound you want to play when you fire a gun.
            //Sounds names are based on the Gungeon sound dump, which can be found at EnterTheGungeon/Etg_Data/StreamingAssets/Audio/GeneratedSoundBanks/Windows/sfx.txt
            gun.PreventNormalFireAudio = true;
            DoSafeExplosion(player.unadjustedAimPoint);
        }
        public void DoSafeExplosion(Vector3 position)
        {

            ExplosionData defaultSmallExplosionData2 = GameManager.Instance.Dungeon.sharedSettingsPrefab.DefaultSmallExplosionData;
            this.smallPlayerSafeExplosion.effect = defaultSmallExplosionData2.effect;
            this.smallPlayerSafeExplosion.ignoreList = defaultSmallExplosionData2.ignoreList;
            this.smallPlayerSafeExplosion.ss = defaultSmallExplosionData2.ss;
            Exploder.Explode(position, this.smallPlayerSafeExplosion, Vector2.zero, null, false, CoreDamageTypes.None, false);

        }
        private ExplosionData smallPlayerSafeExplosion = new ExplosionData
        {
            damageRadius = 2.5f,
            damageToPlayer = 0f,
            doDamage = false,
            damage = 0f,
            doDestroyProjectiles = false,
            
   
            doForce = true,
            debrisForce = 10f,
            preventPlayerForce = true,
            explosionDelay = 0.1f,
            usesComprehensiveDelay = false,
            doScreenShake = false,
            playDefaultSFX = true
        };
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

                if (gun.IsReloading)
                {


                }

            }
        }

        public int count = 0;
        public override void OnAutoReload(PlayerController player, Gun gun)
        {
            AkSoundEngine.PostEvent("Play_WPN_burninghand_shot_01", base.gameObject);
            if (this.gun.CurrentOwner)
            {

            }
        }

        

    }
}
