using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dungeonator;
using System.Collections;
using Gungeon;
using MonoMod;
using UnityEngine;
using ItemAPI;
using System.CodeDom;

namespace Knives.Scripts
{
    class SealedScythe : AdvancedGunBehaviour
    {

        public static void Add()
        {
            // Get yourself a new gun "base" first.
            // Let's just call it "Basic Gun", and use "jpxfrd" for all sprites and as "codename" All sprites must begin with the same word as the codename. For example, your firing sprite would be named "jpxfrd_fire_001".
            Gun gun = ETGMod.Databases.Items.NewGun("Boomerang Scythe", "stab");
            // "kp:basic_gun determines how you spawn in your gun through the console. You can change this command to whatever you want, as long as it follows the "name:itemname" template.
            Game.Items.Rename("outdated_gun_mods:boomerang_scythe", "ski:boomerang_scythe");
            gun.gameObject.AddComponent<SealedScythe>();
            //These two lines determines the description of your gun, ".SetShortDescription" being the description that appears when you pick up the gun and ".SetLongDescription" being the description in the Ammonomicon entry. 
            gun.SetShortDescription("Right back at ya!");
            gun.SetLongDescription("A high tech australian hunting device. A loosely fitted boomerang is fitted to the top of this scythe but don't worry it never breaks entirely." +
                "___________________________________________");
            gun.SetupSprite(null, "stab_idle_001", 8);
            gun.SetAnimationFPS(gun.shootAnimation, 24);
            gun.SetAnimationFPS(gun.reloadAnimation, 24);

            GunExt.AddProjectileModuleFrom(gun, PickupObjectDatabase.GetById(541) as Gun, true, false);
            gun.DefaultModule.ammoCost = 1;
            gun.DefaultModule.angleVariance = 0f;
            gun.DefaultModule.shootStyle = ProjectileModule.ShootStyle.Charged;
            gun.DefaultModule.sequenceStyle = ProjectileModule.ProjectileSequenceStyle.Random;
            gun.DefaultModule.preventFiringDuringCharge = true;
            Gun gun3 = (Gun)ETGMod.Databases.Items["wonderboy"];
            gun.muzzleFlashEffects = gun3.muzzleFlashEffects;
            gun.reloadTime = 1f;
            gun.DefaultModule.cooldownTime = .0f;
            gun.DefaultModule.numberOfShotsInClip = 5;
            gun.InfiniteAmmo = true;
            gun.quality = PickupObject.ItemQuality.B;

            gun.encounterTrackable.EncounterGuid = "stab stab stab";
            //This block of code helps clone our projectile. Basically it makes it so things like Shadow Clone and Hip Holster keep the stats/sprite of your custom gun's projectiles.
            Projectile projectile = UnityEngine.Object.Instantiate<Projectile>(gun.DefaultModule.projectiles[0]);

            projectile.gameObject.SetActive(false);
            FakePrefab.MarkAsFakePrefab(projectile.gameObject);
            UnityEngine.Object.DontDestroyOnLoad(projectile);
            gun.DefaultModule.projectiles[0] = projectile;

            projectile.baseData.damage = 11f;
            projectile.baseData.speed = 1f;
            projectile.baseData.range = 1f;

            projectile.transform.parent = gun.barrelOffset;




            ETGMod.Databases.Items.Add(gun, null, "ANY");

        }
        
        public override void OnFinishAttack(PlayerController player, Gun gun)
        {
            BoomerangProjectile booomer = new BoomerangProjectile();
            booomer.StunDuration = 2;
            booomer.UsesMouseAimPoint = true;
            //booomer.sprite.SetSprite("Knives/Resources/long_roll_boots");
            booomer.Start();
           
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
           

          
            base.Update();
        }

        public override void OnReloadPressed(PlayerController player, Gun gun, bool bSOMETHING)
        {
            if (gun.IsReloading && this.HasReloaded)
            {
                HasReloaded = false;
                AkSoundEngine.PostEvent("Stop_WPN_All", base.gameObject);
                base.OnReloadPressed(player, gun, bSOMETHING);
               
            }
        }

    }
}

