using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dungeonator;
using UnityEngine;
using ItemAPI;
using Gungeon;
using System.Collections;

namespace Knives
{
    class Cursor : AdvancedGunBehaviour
    {
        public static void Add()
        {
            // Get yourself a new gun "base" first.
            // Let's just call it "Basic Gun", and use "jpxfrd" for all sprites and as "codename" All sprites must begin with the same word as the codename. For example, your firing sprite would be named "jpxfrd_fire_001".
            Gun gun = ETGMod.Databases.Items.NewGun("Curseor", "click");
            // "kp:basic_gun determines how you spawn in your gun through the console. You can change this command to whatever you want, as long as it follows the "name:itemname" template.
            Game.Items.Rename("outdated_gun_mods:curseor", "ski:curseor");
            gun.gameObject.AddComponent<Cursor>();
            //These two lines determines the description of your gun, ".SetShortDescription" being the description that appears when you pick up the gun and ".SetLongDescription" being the description in the Ammonomicon entry. 
            gun.SetShortDescription("Click and Drag");
            gun.SetLongDescription("Click an enemy and drag them into pits or other enemies");
            gun.SetupSprite(null, "ball_idle_001", 8);
            gun.SetAnimationFPS(gun.shootAnimation, 24);
            gun.SetAnimationFPS(gun.reloadAnimation, 24);

            gun.AddProjectileModuleFrom("Camera_Gun", true, true);
            gun.PreventNormalFireAudio = true;

            gun.gunHandedness = GunHandedness.OneHanded;
            gun.DefaultModule.ammoCost = 1;
            gun.DefaultModule.angleVariance = 0f;
            gun.DefaultModule.shootStyle = ProjectileModule.ShootStyle.Automatic;
            gun.DefaultModule.sequenceStyle = ProjectileModule.ProjectileSequenceStyle.Random;
           

            gun.reloadTime = .5f;
            gun.DefaultModule.cooldownTime = .01f;
            gun.DefaultModule.numberOfShotsInClip = int.MaxValue;
            gun.SetBaseMaxAmmo(1000000000);
            gun.quality = PickupObject.ItemQuality.S;

            gun.encounterTrackable.EncounterGuid = "there is no guid... keep scrolling";
            //This block of code helps clone our projectile. Basically it makes it so things like Shadow Clone and Hip Holster keep the stats/sprite of your custom gun's projectiles.
            Projectile projectile = UnityEngine.Object.Instantiate<Projectile>(gun.DefaultModule.projectiles[0]);

            projectile.gameObject.SetActive(false);
            FakePrefab.MarkAsFakePrefab(projectile.gameObject);
            UnityEngine.Object.DontDestroyOnLoad(projectile);
            gun.DefaultModule.projectiles[0] = projectile;

            projectile.baseData.damage = 0f;
            projectile.baseData.speed = 100000f;
            projectile.baseData.range = .25f;
            projectile.AdditionalScaleMultiplier = .00001f;
            gun.barrelOffset.transform.localPosition = new Vector3(-.30f, 0, 0);
            projectile.transform.parent = gun.barrelOffset;




            ETGMod.Databases.Items.Add(gun, null, "ANY");

        }

       
        protected override void OnPickedUpByPlayer(PlayerController player)
        {
            
            base.OnPickedUpByPlayer(player);
        }
            
        
        public override void OnPostFired(PlayerController player, Gun gun)
        {
            RoomHandler room = player.CurrentRoom;
            if (!room.HasActiveEnemies(RoomHandler.ActiveEnemyType.All)) return;
            foreach (var enemy in room.GetActiveEnemies(RoomHandler.ActiveEnemyType.All))
            {
               if(Vector2.Distance(enemy.specRigidbody.UnitCenter, player.unadjustedAimPoint) <= 2)
               {
                    float angle = Vector2.Angle(enemy.specRigidbody.UnitCenter, player.unadjustedAimPoint);
                    float speed = Vector2.Distance(enemy.specRigidbody.UnitCenter, player.unadjustedAimPoint) * 3;
                    enemy.specRigidbody.Velocity = BraveMathCollege.DegreesToVector(angle).normalized * speed;

               }
            }
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
        public void hatportal()
        {
          
        }

        public override void OnReloadPressed(PlayerController player, Gun gun, bool bSOMETHING)
        {
            if (gun.IsReloading && this.HasReloaded)
            {
                AkSoundEngine.PostEvent("Stop_WPN_All", base.gameObject);
                HasReloaded = false;

                base.OnReloadPressed(player, gun, bSOMETHING);
               

                
            }
        }

    }
}

