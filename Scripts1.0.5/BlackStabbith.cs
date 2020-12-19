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

namespace Knives
{
    class BlackStabbith :AdvancedGunBehaviour
    {
        public static void Add()
        {
            // Get yourself a new gun "base" first.
            // Let's just call it "Basic Gun", and use "jpxfrd" for all sprites and as "codename" All sprites must begin with the same word as the codename. For example, your firing sprite would be named "jpxfrd_fire_001".
            Gun gun = ETGMod.Databases.Items.NewGun("Black Stabbith", "stab");
            // "kp:basic_gun determines how you spawn in your gun through the console. You can change this command to whatever you want, as long as it follows the "name:itemname" template.
            Game.Items.Rename("outdated_gun_mods:black_stabbith", "ski:black_stabbith");
            gun.gameObject.AddComponent<BlackStabbith>();
            //These two lines determines the description of your gun, ".SetShortDescription" being the description that appears when you pick up the gun and ".SetLongDescription" being the description in the Ammonomicon entry. 
            gun.SetShortDescription("Judge jury and exocutioner");
            gun.SetLongDescription("This blade of blades was weilded by a terrifying vigilanty of justice. This style of blades is typically reserved for executions however this blade takes its duty one step further. If the convicted manages to escape the initial blow a flurry of blades will manifest to cripple their escape. " +
                "___________________________________________");
            gun.SetupSprite(null, "stab_idle_001", 8);
            gun.SetAnimationFPS(gun.shootAnimation, 50);
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




        public override void OnPostFired(PlayerController player, Gun gun)
        {



        }
      
        public override void OnFinishAttack(PlayerController player, Gun gun)
        {
            throwknives(player);
            AkSoundEngine.PostEvent("Stop_WPN_All", base.gameObject);

            stab = 0;
        }
        public int stab = 0;
        
        
        
        public void throwknives(PlayerController player)
        {
            
            if (stab == 1) 
            { 
                Projectile projectile = ((Gun)ETGMod.Databases.Items[377]).DefaultModule.projectiles[0];
               
                
               
                GameObject gameObject = SpawnManager.SpawnProjectile(projectile.gameObject, player.sprite.WorldCenter, Quaternion.Euler(0f, 0f, (player.CurrentGun == null) ? 0f : player.CurrentGun.CurrentAngle + 10), true);
                Projectile component = gameObject.GetComponent<Projectile>();

                bool flag = component != null;
                if (flag)
                {
                    component.Owner = player;
                    component.Shooter = player.specRigidbody;
                    component.baseData.speed *= 0;
                    component.baseData.damage = 0;
                    component.angularVelocity = 0f;
                    component.AdditionalScaleMultiplier = 1.5f;
                }
                
               
                GameObject gameObject2 = SpawnManager.SpawnProjectile(projectile.gameObject, player.sprite.WorldCenter, Quaternion.Euler(0f, 0f, (player.CurrentGun == null) ? 0f : player.CurrentGun.CurrentAngle + 15), true);
                Projectile component2 = gameObject2.GetComponent<Projectile>();

                bool flag2 = component != null;
                if (flag2)
                {
                    component2.Owner = player;
                    component2.Shooter = player.specRigidbody;
                    component2.baseData.speed *= 0;
                    component2.baseData.damage = 0;
                    component2.angularVelocity = 0f;
                    component2.AdditionalScaleMultiplier = 1.5f;
                }

                GameObject gameObject3 = SpawnManager.SpawnProjectile(projectile.gameObject, player.sprite.WorldCenter, Quaternion.Euler(0f, 0f, (player.CurrentGun == null) ? 0f : player.CurrentGun.CurrentAngle), true);
                Projectile component3 = gameObject3.GetComponent<Projectile>();

                bool flag3 = component != null;
                if (flag3)
                {
                    component3.Owner = player;
                    component3.Shooter = player.specRigidbody;
                    component3.baseData.speed *= 0;
                    component3.baseData.damage = 0;
                    component3.angularVelocity = 0f;
                    component3.AdditionalScaleMultiplier = 1.5f;
                }

                GameObject gameObject4 = SpawnManager.SpawnProjectile(projectile.gameObject, player.sprite.WorldCenter, Quaternion.Euler(0f, 0f, (player.CurrentGun == null) ? 0f : player.CurrentGun.CurrentAngle - 15), true);
                Projectile component4 = gameObject4.GetComponent<Projectile>();

                bool flag4 = component != null;
                if (flag2)
                {
                    component4.Owner = player;
                    component4.Shooter = player.specRigidbody;
                    component4.baseData.speed *= 0;
                    component4.baseData.damage = 0;
                    component4.angularVelocity = 0f;
                    component4.AdditionalScaleMultiplier = 1.5f;
                }

                GameObject gameObject5 = SpawnManager.SpawnProjectile(projectile.gameObject, player.sprite.WorldCenter, Quaternion.Euler(0f, 0f, (player.CurrentGun == null) ? 0f : player.CurrentGun.CurrentAngle - 10), true);
                Projectile component5 = gameObject5.GetComponent<Projectile>();

                bool flag5 = component != null;
                if (flag5)
                {
                    component5.Owner = player;
                    component5.Shooter = player.specRigidbody;
                    component5.baseData.speed *= 0;
                    component5.baseData.damage = 0;
                    component5.angularVelocity = 0f;
                    component5.AdditionalScaleMultiplier = 1.5f;
                }
            }   
        }
       
        private List<Projectile> GetBullets()
        {
            PlayerController player = this.gun.CurrentOwner as PlayerController;
            List<Projectile> list = new List<Projectile>();
            var allProjectiles = StaticReferenceManager.AllProjectiles;
            for (int i = 0; i < allProjectiles.Count; i++)
            {
                Projectile projectile = allProjectiles[i];
                if (projectile && projectile.sprite && !projectile.ImmuneToBlanks && !projectile.ImmuneToSustainedBlanks)
                {
                    if (projectile.Owner != null)
                    {
                        if (player)
                        {
                                list.Add(projectile);
                        }
                    }
                }
            }
            return list;
        }
        public int multiplier;
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
            if (this.gun.HasChargedProjectileReady)
            {
                stab = 1;
            }

            if (this.gun.HasChargedProjectileReady)
            {
                multiplier++;
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
                foreach( var projectile in GetBullets())
                {
                    projectile.baseData.speed = 12;
                    projectile.baseData.damage = 15f;
                    
                    projectile.Update();
                    projectile.UpdateSpeed();
                }
            }
        }

    }
}

