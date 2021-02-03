
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Brave;
using Brave.BulletScript;
using Gungeon;
using HutongGames.PlayMaker.Actions;
using ItemAPI;
using UnityEngine;

namespace Knives
{
    class Catalyzer : AdvancedGunBehaviour
    {
        public static void Add()
        {
            // Get yourself a new gun "base" first.
            // Let's just call it "Basic Gun", and use "jpxfrd" for all sprites and as "codename" All sprites must begin with the same word as the codename. For example, your firing sprite would be named "jpxfrd_fire_001".
            Gun gun = ETGMod.Databases.Items.NewGun("Catalyzer", "Cat");
            // "kp:basic_gun determines how you spawn in your gun through the console. You can change this command to whatever you want, as long as it follows the "name:itemname" template.
            Game.Items.Rename("outdated_gun_mods:catalyzer", "ski:catalyzer");
            gun.gameObject.AddComponent<Catalyzer>();
            //These two lines determines the description of your gun, ".SetShortDescription" being the description that appears when you pick up the gun and ".SetLongDescription" being the description in the Ammonomicon entry. 
            gun.SetShortDescription("Activation Energy Required");

            gun.SetLongDescription("This Gun's bullets are made of chemicals on the brink of a reaction.\n\n" +
                "They are brimming with terrifying potential. The charge shot of the gun holds enough potential to catalyze the reaction but any damage will do.");
            // This is required, unless you want to use the sprites of the base gun.
            // That, by default, is the pea shooter.
            // SetupSprite sets up the default gun sprite for the ammonomicon and the "gun get" popup.
            // WARNING: Add a copy of your default sprite to Ammonomicon Encounter Icon Collection!
            // That means, "sprites/Ammonomicon Encounter Icon Collection/defaultsprite.png" in your mod .zip. You can see an example of this with inside the mod folder.
            gun.SetupSprite(null, "Cat_idle_001", 8);
            // ETGMod automatically checks which animations are available.
            // The numbers next to "shootAnimation" determine the animation fps. You can also tweak the animation fps of the reload animation and idle animation using this method.
            gun.SetAnimationFPS(gun.shootAnimation, 2);
            // Every modded gun has base projectile it works with that is borrowed from other guns in the game. 
            // The gun names are the names from the JSON dump! While most are the same, some guns named completely different things. If you need help finding gun names, ask a modder on the Gungeon discord.
            gun.AddProjectileModuleFrom(PickupObjectDatabase.GetById(181) as Gun, true, false);

            // Here we just take the default projectile module and change its settings how we want it to be.
            gun.DefaultModule.ammoCost = 1;
            gun.DefaultModule.angleVariance = 0;
            gun.gunClass = GunClass.SILLY;
            gun.gunHandedness = GunHandedness.OneHanded;
            
            gun.DefaultModule.shootStyle = ProjectileModule.ShootStyle.Charged;
            gun.DefaultModule.sequenceStyle = ProjectileModule.ProjectileSequenceStyle.Random;
            gun.reloadTime = .5f;
            
            gun.DefaultModule.cooldownTime = .001f;
            gun.DefaultModule.numberOfShotsInClip = 3;
            gun.SetBaseMaxAmmo(900);

            gun.quality = PickupObject.ItemQuality.C;
            //gun.encounterTrackable.EncounterGuid = "One Herring Twitch! And Kableewy!";



            Projectile projectile = UnityEngine.Object.Instantiate<Projectile>(gun.DefaultModule.projectiles[0]);
            projectile.gameObject.SetActive(false);
            FakePrefab.MarkAsFakePrefab(projectile.gameObject);
            UnityEngine.Object.DontDestroyOnLoad(projectile);
            gun.DefaultModule.projectiles[0] = projectile;
            projectile.baseData.damage = 0f;
            projectile.baseData.speed *= 1f;
            projectile.baseData.range = 1003f;
            projectile.HasDefaultTint = true;
            projectile.DefaultTintColor = UnityEngine.Color.cyan;
            projectile.transform.parent = gun.barrelOffset;
            UnchangeableRangeController ranger = projectile.gameObject.AddComponent<UnchangeableRangeController>();
          
           
           





            Projectile projectile2 = UnityEngine.Object.Instantiate<Projectile>(gun.DefaultModule.projectiles[0]);
            projectile2.gameObject.SetActive(false);
            FakePrefab.MarkAsFakePrefab(projectile2.gameObject);
            UnityEngine.Object.DontDestroyOnLoad(projectile2);
            gun.DefaultModule.projectiles[0] = projectile2;
            projectile2.baseData.damage = 5f;
            projectile2.baseData.speed *= 1f;
            projectile2.baseData.range = 1004f;
            projectile.HasDefaultTint = true;
            projectile.DefaultTintColor = UnityEngine.Color.cyan;
            projectile2.AdditionalScaleMultiplier = 2f;
            projectile2.transform.parent = gun.barrelOffset;
            UnchangeableRangeController ranger2 = projectile2.gameObject.AddComponent<UnchangeableRangeController>();


            ProjectileModule.ChargeProjectile item = new ProjectileModule.ChargeProjectile
            {
                Projectile = projectile,
                ChargeTime = 0f
            };
            ProjectileModule.ChargeProjectile item2 = new ProjectileModule.ChargeProjectile
            {
                Projectile = projectile2,
                ChargeTime = .75f
            };
            gun.DefaultModule.chargeProjectiles = new List<ProjectileModule.ChargeProjectile>
            {
                item,
                item2
            };
            

            ETGMod.Databases.Items.Add(gun, null, "ANY");

        }


        protected override void OnPickedUpByPlayer(PlayerController player)
        {
            
            base.OnPickedUpByPlayer(player);
        }
        public override void OnPostFired(PlayerController player, Gun gun)
        { 
        }

        public override void PostProcessProjectile(Projectile projectile)
        {   //uncharged
            if (projectile.baseData.range == 1003f && projectile.gameObject.GetComponent<UnchangeableRangeController>() != null)
            {
                projectile.OnHitEnemy += this.hitenemyhandleruncharge;
                //PierceProjModifier stab = projectile.GetComponent<PierceProjModifier>();
                //stab.MaxBossImpacts = 999;
                //stab.penetration = 999;



            }
            //charged
            if (projectile.baseData.range == 1004f && projectile.gameObject.GetComponent<UnchangeableRangeController>() != null)
            {
              
            }

            base.PostProcessProjectile(projectile);
        }

        public void hitenemyhandleruncharge(Projectile proj, SpeculativeRigidbody body, bool yes)
        {
            if (yes)
            {
                //BecomeOrbitProjectileModifier become = proj.gameObject.AddComponent<BecomeOrbitProjectileModifier>();
                OrbitProjectileMotionModule orbiter = proj.gameObject.GetComponent<OrbitProjectileMotionModule>();
                orbiter.lifespan = 99999f;
                orbiter.MaxRadius = 3;
                orbiter.MinRadius = 3;
                
                orbiter.alternateOrbitTarget = body;
              
                /// WHY YOU NO SPIN AUUUUUUUUUUUGHHH

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


    }
}

