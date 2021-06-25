
using Gungeon;
using System.Collections.Generic;
using UnityEngine;
using ItemAPI;
using Kvant;

namespace Knives
{

    class fourth_wall_breaker : AdvancedGunBehaviour
    {
       
        public static void Add()
        {
            // Get yourself a new gun "base" first.
            // Let's just call it "Basic Gun", and use "jpxfrd" for all sprites and as "codename" All sprites must begin with the same word as the codename. For example, your firing sprite would be named "jpxfrd_fire_001".
            Gun gun = ETGMod.Databases.Items.NewGun("World shatter", "break");
            // "kp:basic_gun determines how you spawn in your gun through the console. You can change this command to whatever you want, as long as it follows the "name:itemname" template.
            Game.Items.Rename("outdated_gun_mods:world_shatter", "ski:world_shatter");
            gun.gameObject.AddComponent<fourth_wall_breaker>();
            //These two lines determines the description of your gun, ".SetShortDescription" being the description that appears when you pick up the gun and ".SetLongDescription" being the description in the Ammonomicon entry. 
            gun.SetShortDescription("Chained beast");
            gun.SetLongDescription("A legendary shotgun fortold to be able to shatter the foundation of the gungeon itself. Weapon of the mightly Kinrir, this gun has been cursed to supress its natural World ending power." +
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
            gun.SetAnimationFPS(gun.reloadAnimation, 5);
            // Every modded gun has base projectile it works with that is borrowed from other guns in the game. 
            // The gun names are the names from the JSON dump! While most are the same, some guns named completely different things. If you need help finding gun names, ask a modder on the Gungeon discord.
            gun.AddProjectileModuleFrom("gold_double_barrel_shotgun", true, true);
            
            // Here we just take the default projectile module and change its settings how we want it to be.
            
            // Here we just set the quality of the gun and the "EncounterGuid", which is used by Gungeon to identify the gun.
            gun.quality = PickupObject.ItemQuality.S;
            
            gun.encounterTrackable.EncounterGuid = "This gun has been a thorn in my side";
            //This block of code helps clone our projectile. Basically it makes it so things like Shadow Clone and Hip Holster keep the stats/sprite of your custom gun's projectiles.

           
            gun.DefaultModule.ammoCost = 1;
            gun.DefaultModule.angleVariance = 0f;
            gun.directionlessScreenShake = true;
            gun.DefaultModule.shootStyle = ProjectileModule.ShootStyle.Charged;
            gun.DefaultModule.sequenceStyle = ProjectileModule.ProjectileSequenceStyle.Random;
            
            gun.reloadTime = 2f;
            gun.DefaultModule.cooldownTime = 0f;
            gun.DefaultModule.numberOfShotsInClip = 3;
            gun.SetBaseMaxAmmo(75);

            Projectile projectile = UnityEngine.Object.Instantiate<Projectile>(gun.DefaultModule.projectiles[0]);
            gun.barrelOffset.transform.localPosition = new Vector3(3f, .5f, 0f);
            gun.DefaultModule.ammoType = GameUIAmmoType.AmmoType.SHOTGUN;
            


            projectile.gameObject.SetActive(false);
            FakePrefab.MarkAsFakePrefab(projectile.gameObject);
            UnityEngine.Object.DontDestroyOnLoad(projectile);
            gun.DefaultModule.projectiles[0] = projectile;
            projectile.baseData.damage = 8f;
            



            //shelling
            Projectile projectile2 = UnityEngine.Object.Instantiate<Projectile>(gun.DefaultModule.projectiles[0]);

            projectile2.gameObject.SetActive(false);
            FakePrefab.MarkAsFakePrefab(projectile2.gameObject);
            UnityEngine.Object.DontDestroyOnLoad(projectile2);
            gun.DefaultModule.projectiles[0] = projectile2;
            projectile2.baseData.damage = 9f;
            projectile2.BossDamageMultiplier = 3;




            //wyvernstake
            Projectile projectile3 = UnityEngine.Object.Instantiate<Projectile>(gun.DefaultModule.projectiles[0]);

            projectile3.gameObject.SetActive(false);
            FakePrefab.MarkAsFakePrefab(projectile3.gameObject);
            UnityEngine.Object.DontDestroyOnLoad(projectile3);
            gun.DefaultModule.projectiles[0] = projectile3;
            projectile3.baseData.damage = 10f;
            projectile3.BossDamageMultiplier = 6;
           

            
            ProjectileModule.ChargeProjectile item = new ProjectileModule.ChargeProjectile
            {
                Projectile = projectile,
                ChargeTime = .2f,
                AmmoCost = 0,


            };
            ProjectileModule.ChargeProjectile item2 = new ProjectileModule.ChargeProjectile
            {
                Projectile = projectile2,
                ChargeTime = 1f,
                AmmoCost = 0,


            };
            ProjectileModule.ChargeProjectile item3 = new ProjectileModule.ChargeProjectile
            {
                Projectile = projectile3,
                ChargeTime = 1.7f,
                AmmoCost = 0,


            };
            gun.DefaultModule.chargeProjectiles = new List<ProjectileModule.ChargeProjectile>
            {
                item,
                item2,
                item3
            };
            ETGMod.Databases.Items.Add(gun, null, "ANY");

        }
        protected override void OnPickup(GameActor owner)
        {
           
            base.OnPickup(owner);
        }
        System.Random rng = new System.Random();
        public override void PostProcessProjectile(Projectile projectile)
        {
            PlayerController player = (PlayerController)this.gun.CurrentOwner;
            int snd = rng.Next(1, 2);
            if(snd == 1)
            {
                AkSoundEngine.PostEvent("Play_shatter_fire_001", base.gameObject);
            }
            else
            {
                AkSoundEngine.PostEvent("Play_shatter_fire_002", base.gameObject);
            }
            if (projectile.GetCachedBaseDamage == 8f)
            {
                for (int i = 0; i < 8; i++)
                {
                    int vary = rng.Next(-12, 12);
                    Projectile projectile2 = ((Gun)ETGMod.Databases.Items[93]).DefaultModule.projectiles[0];
                    GameObject gameObject = SpawnManager.SpawnProjectile(projectile2.gameObject, this.gun.CurrentOwner.CurrentGun.sprite.WorldCenter, Quaternion.Euler(0f, 0f, (this.gun.CurrentOwner.CurrentGun == null) ? 0f : this.gun.CurrentOwner.CurrentGun.CurrentAngle + vary), true);
                    Projectile component = gameObject.GetComponent<Projectile>();
                    bool flag = component != null;
                    if (flag)
                    {
                        
                        component.Owner = this.gun.CurrentOwner;
                        component.Shooter = this.gun.CurrentOwner.specRigidbody;
                        component.baseData.damage = 8.1f;
                        component.baseData.range *= .6f;
                        if (vary >= 0)
                        {
                            component.baseData.speed *= (float)(1 + .02 * vary);
                        }
                        else
                        {
                            component.baseData.speed *= (float)(1 - .02 * vary);
                        }
                        player.DoPostProcessProjectile(component);
                    }
                }

            }
            if (projectile.GetCachedBaseDamage == 9f)
            {

                for (int i = 0; i < 10; i++)
                {
                    int vary = rng.Next(-6, 6);
                    Projectile projectile2 = ((Gun)ETGMod.Databases.Items[93]).DefaultModule.projectiles[0];
                    GameObject gameObject = SpawnManager.SpawnProjectile(projectile2.gameObject, this.gun.CurrentOwner.CurrentGun.sprite.WorldCenter, Quaternion.Euler(0f, 0f, (this.gun.CurrentOwner.CurrentGun == null) ? 0f : this.gun.CurrentOwner.CurrentGun.CurrentAngle + vary), true);
                    Projectile component = gameObject.GetComponent<Projectile>();
                    bool flag = component != null;
                    if (flag)
                    {

                        component.Owner = this.gun.CurrentOwner;
                        component.Shooter = this.gun.CurrentOwner.specRigidbody;
                        component.baseData.damage = 9.1f;
                        if (vary >= 0)
                        {
                            component.baseData.speed *= (float)(1 + .02 * vary);
                        }
                        else
                        {
                            component.baseData.speed *= (float)(1 - .02 * vary);
                        }
                        player.DoPostProcessProjectile(component);
                        component.baseData.range *= .8f;
                    }
                }
            }
            if (projectile.GetCachedBaseDamage == 10f)
            {
                for (int i = 0; i < 13; i++)
                {
                    int vary = rng.Next(-2, 2);
                    Projectile projectile2 = ((Gun)ETGMod.Databases.Items[93]).DefaultModule.projectiles[0];
                    GameObject gameObject = SpawnManager.SpawnProjectile(projectile2.gameObject, this.gun.CurrentOwner.CurrentGun.sprite.WorldCenter, Quaternion.Euler(0f, 0f, (this.gun.CurrentOwner.CurrentGun == null) ? 0f : this.gun.CurrentOwner.CurrentGun.CurrentAngle + vary), true);
                    Projectile component = gameObject.GetComponent<Projectile>();
                    bool flag = component != null;
                    if (flag)
                    {

                        component.Owner = this.gun.CurrentOwner;
                        component.Shooter = this.gun.CurrentOwner.specRigidbody;
                        component.baseData.damage = 10.1f;
                        if(vary >=0)
                        {
                            component.baseData.speed *= (float)(1 + .02 * vary);
                        }
                        else
                        {
                            component.baseData.speed *= (float)(1 - .02 * vary);
                        }
                        player.DoPostProcessProjectile(component);
                        component.baseData.range *= 1.6f;
                    }
                }
            }


            base.PostProcessProjectile(projectile);
        }

        public float multiplier = 0;
        public override void OnPostFired(PlayerController player, Gun gun)
        {
            
            gun.PreventNormalFireAudio = true;
            
            
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
                AkSoundEngine.PostEvent("Play_shatter_reload", base.gameObject);

            }
        }

     
       
    }
}