
using Gungeon;

using UnityEngine;
using ItemAPI;
using Kvant;

namespace Knives
{

    class fourth_wall_breaker : AdvancedGunBehaviour
    {
        public float dura = 5;
        public static void Add()
        {
            // Get yourself a new gun "base" first.
            // Let's just call it "Basic Gun", and use "jpxfrd" for all sprites and as "codename" All sprites must begin with the same word as the codename. For example, your firing sprite would be named "jpxfrd_fire_001".
            Gun gun = ETGMod.Databases.Items.NewGun("World shatter", "break");
            // "kp:basic_gun determines how you spawn in your gun through the console. You can change this command to whatever you want, as long as it follows the "name:itemname" template.
            Game.Items.Rename("outdated_gun_mods:world_shatter", "ski:world_shatter");
            gun.gameObject.AddComponent<fourth_wall_breaker>();
            //These two lines determines the description of your gun, ".SetShortDescription" being the description that appears when you pick up the gun and ".SetLongDescription" being the description in the Ammonomicon entry. 
            gun.SetShortDescription("Are you sure you want to quit?");
            gun.SetLongDescription("A legendary shotgun fortold to be able to shatter the foundation of the gungeon itself. The power lies inside the third barrel that kills things more deader than ever before. You personally shouldn't fire it, but maybe you could get something else to?" +
                "\n\n\n - Knife_to_a_Gunfight");
            // This is required, unless you want to use the sprites of the base gun.
            // That, by default, is the pea shooter.
            // SetupSprite sets up the default gun sprite for the ammonomicon and the "gun get" popup.
            // WARNING: Add a copy of your default sprite to Ammonomicon Encounter Icon Collection!
            // That means, "sprites/Ammonomicon Encounter Icon Collection/defaultsprite.png" in your mod .zip. You can see an example of this with inside the mod folder.
            gun.SetupSprite(null, "break_idle_001", 8);
            // ETGMod automatically checks which animations are available.
            // The numbers next to "shootAnimation" determine the animation fps. You can also tweak the animation fps of the reload animation and idle animation using this method.
            gun.SetAnimationFPS(gun.shootAnimation, 24);
            // Every modded gun has base projectile it works with that is borrowed from other guns in the game. 
            // The gun names are the names from the JSON dump! While most are the same, some guns named completely different things. If you need help finding gun names, ask a modder on the Gungeon discord.
            gun.AddProjectileModuleFrom("gold_double_barrel_shotgun", true, true);
            
            // Here we just take the default projectile module and change its settings how we want it to be.
            gun.DefaultModule.ammoCost = 1;
            gun.DefaultModule.angleVariance = 0f;
            gun.directionlessScreenShake = true;
            gun.DefaultModule.shootStyle = ProjectileModule.ShootStyle.SemiAutomatic;  
            gun.DefaultModule.sequenceStyle = ProjectileModule.ProjectileSequenceStyle.Random;
            gun.reloadTime = 1.2f;
            gun.DefaultModule.cooldownTime = 3f;
            gun.DefaultModule.numberOfShotsInClip = 1;
            gun.SetBaseMaxAmmo(1);
            // Here we just set the quality of the gun and the "EncounterGuid", which is used by Gungeon to identify the gun.
            gun.quality = PickupObject.ItemQuality.S;
            
            gun.encounterTrackable.EncounterGuid = "fourth wall break";
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
            projectile.baseData.damage *= 999999999f;
            projectile.baseData.speed *= 1f;
            projectile.transform.parent = gun.barrelOffset;
            projectile.AppliesBleed = true;
            projectile.AppliesFire = true;
            projectile.AppliesStun = true;
            
            ETGMod.Databases.Items.Add(gun, null, "ANY");

        }
        protected override void OnPickup(GameActor owner)
        {
            if (this.Player != null && this.Player.CharacterUsesRandomGuns)
            {
                this.gun.LoseAmmo(1);
                this.Player.ChangeToRandomGun();
            }
            base.OnPickup(owner);
        }

      

        public override void OnPostFired(PlayerController player, Gun gun)
        {
            //This determines what sound you want to play when you fire a gun.
            //Sounds names are based on the Gungeon sound dump, which can be found at EnterTheGungeon/Etg_Data/StreamingAssets/Audio/GeneratedSoundBanks/Windows/sfx.txt
            gun.PreventNormalFireAudio = true;
            AkSoundEngine.PostEvent("Play_WPN_golddoublebarrelshotgun_shot_01", gameObject);
            AkSoundEngine.PostEvent("Play_WPN_golddoublebarrelshotgun_shot_01", gameObject);
            
            if(player.CharacterUsesRandomGuns == false)
            {
                
             
                



               // GameObject TunnelInstancetunneleffect = (GameObject)UnityEngine.Object.Instantiate(BraveResources.Load("Global Prefabs/ModTunnel_02", ".prefab"), new Vector3(-100f, -700f, 0f), Quaternion.identity);
               //TunnelThatCanKillThePast TunnelController2 = TunnelInstancetunneleffect.GetComponent<TunnelThatCanKillThePast>();
               //TunnelController2.Shatter();

                AkSoundEngine.PostEvent("Play_ENV_time_shatter_01", GameManager.Instance.gameObject);
                Application.Quit();
            }
            
            
        }
        private GameObject DoShatterVFX(PlayerController user)
        {
            this.ShatterVFX = (GameObject)ResourceCache.Acquire("Global VFX/VFX_shatter_mask");
            GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.ShatterVFX);
            gameObject.GetComponent<tk2dBaseSprite>().PlaceAtLocalPositionByAnchor(user.specRigidbody.UnitBottomCenter + new Vector2(0f, -1f), tk2dBaseSprite.Anchor.LowerCenter);
            gameObject.transform.position = gameObject.transform.position.Quantize(0.0625f);
            gameObject.GetComponent<tk2dBaseSprite>().UpdateZDepth();
            return gameObject;
        }

        private bool HasReloaded;
        //This block of code allows us to change the reload sounds.
        protected override void Update()
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
            }
            if (this.Player != null && this.Player.CharacterUsesRandomGuns)
            {
                this.gun.LoseAmmo(1);
                this.Player.ChangeToRandomGun();
            }
            base.Update();
        }

        public override void OnReloadPressed(PlayerController player, Gun gun, bool bSOMETHING)
        {
            if (gun.IsReloading && this.HasReloaded)
            {
                HasReloaded = false;
                AkSoundEngine.PostEvent("Stop_WPN_All", base.gameObject);
                base.OnReloadPressed(player, gun, bSOMETHING);
                AkSoundEngine.PostEvent("Play_WPN_SAA_reload_01", base.gameObject);

            }
        }

        private GameObject ShatterVFX;
       
    }
}