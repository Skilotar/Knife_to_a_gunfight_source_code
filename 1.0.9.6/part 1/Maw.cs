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
    class Maw : AdvancedGunBehaviour
    {


        public static void Add()
        {
            // Get yourself a new gun "base" first.
            // Let's just call it "Basic Gun", and use "jpxfrd" for all sprites and as "codename" All sprites must begin with the same word as the codename. For example, your firing sprite would be named "jpxfrd_fire_001".
            Gun gun = ETGMod.Databases.Items.NewGun("Maw of Vog", "Maw");
            // "kp:basic_gun determines how you spawn in your gun through the console. You can change this command to whatever you want, as long as it follows the "name:itemname" template.
            Game.Items.Rename("outdated_gun_mods:maw_of_vog", "ski:maw_of_vog");
            gun.gameObject.AddComponent<Maw>();
            //These two lines determines the description of your gun, ".SetShortDescription" being the description that appears when you pick up the gun and ".SetLongDescription" being the description in the Ammonomicon entry. 
            gun.SetShortDescription("Through the fire and flames");
            gun.SetLongDescription("This blade was forged with the blessing of the bullet hell's canine gaurdian, Vog. Vog's firey hate is embued into it causing it to sometimes lash against enemies and its user with fire. The blade is very much alive acting as a cub of vog normaly would, reacting to danger and reveling in a good hunt." +
                "\n\n\n - Knife_to_a_Gunfight"); ;


            gun.SetupSprite(null, "Maw_idle_001", 8);
            

            gun.SetAnimationFPS(gun.shootAnimation, 12);
            gun.SetAnimationFPS(gun.reloadAnimation, 9);

            gun.AddProjectileModuleFrom(PickupObjectDatabase.GetByEncounterName("Hegemony Rifle") as Gun, true, false);
           

            gun.DefaultModule.ammoCost = 1;
            gun.DefaultModule.angleVariance = 0;
            gun.gunClass = GunClass.SILLY;
            gun.gunHandedness = GunHandedness.OneHanded;

            gun.DefaultModule.shootStyle = ProjectileModule.ShootStyle.Charged;
            gun.DefaultModule.sequenceStyle = ProjectileModule.ProjectileSequenceStyle.Random;

            gun.reloadTime = 1f;
            gun.DefaultModule.numberOfShotsInClip = 5;
            gun.DefaultModule.cooldownTime = .5f;



            gun.InfiniteAmmo = true;
            gun.quality = PickupObject.ItemQuality.B;
            gun.encounterTrackable.EncounterGuid = "BURN BABY BURN!";

            //swipe
            Projectile projectile = UnityEngine.Object.Instantiate<Projectile>(gun.DefaultModule.projectiles[0]);
            gun.barrelOffset.transform.localPosition = new Vector3(1f, .5f, 0f);
            gun.DefaultModule.ammoType = GameUIAmmoType.AmmoType.SKULL;
           
            gun.muzzleFlashEffects = null;



            projectile.gameObject.SetActive(false);
            FakePrefab.MarkAsFakePrefab(projectile.gameObject);
            UnityEngine.Object.DontDestroyOnLoad(projectile);
            gun.DefaultModule.projectiles[0] = projectile;
            projectile.baseData.damage = 8f;
            projectile.baseData.speed = 3f;
            projectile.baseData.range = 3f;
            projectile.baseData.force = 5;
            
            projectile.AppliesFire = true;
            projectile.FireApplyChance = 100f;



            //Hot HOT HOT 
            Projectile projectile2 = UnityEngine.Object.Instantiate<Projectile>(gun.DefaultModule.projectiles[0]);

            projectile2.gameObject.SetActive(false);
            FakePrefab.MarkAsFakePrefab(projectile2.gameObject);
            UnityEngine.Object.DontDestroyOnLoad(projectile2);
            gun.DefaultModule.projectiles[0] = projectile2;
            projectile2.baseData.damage = 15f;
            projectile2.baseData.speed = 50f;
            projectile2.baseData.range = 10f;
            projectile.AppliesFire = true;
            projectile.FireApplyChance = 100f;
            projectile2.name = "shelling";



            ProjectileModule.ChargeProjectile item = new ProjectileModule.ChargeProjectile
            {
                Projectile = projectile,
                ChargeTime = 0f,
                AmmoCost = 0,


            };
            ProjectileModule.ChargeProjectile item2 = new ProjectileModule.ChargeProjectile
            {
                Projectile = projectile2,
                ChargeTime = 1f,


            };
            gun.DefaultModule.chargeProjectiles = new List<ProjectileModule.ChargeProjectile>
            {
                item,
                item2,
                
            };


            tk2dSpriteAnimationClip fireClip2 = gun.sprite.spriteAnimator.GetClipByName("Maw_fire");
            float[] offsetsX2 = new float[] { .1f, .4f, .3f, .2f ,.0f};
            float[] offsetsY2 = new float[] { 1.2f, .5f , .3f, .1f, -.3f};
            
            for (int i = 0; i < offsetsX2.Length && i < offsetsY2.Length && i < fireClip2.frames.Length; i++)
            {
                int id = fireClip2.frames[i].spriteId;
                fireClip2.frames[i].spriteCollection.spriteDefinitions[id].position0.x += offsetsX2[i];
                fireClip2.frames[i].spriteCollection.spriteDefinitions[id].position0.y += offsetsY2[i];
                fireClip2.frames[i].spriteCollection.spriteDefinitions[id].position1.x += offsetsX2[i];
                fireClip2.frames[i].spriteCollection.spriteDefinitions[id].position1.y += offsetsY2[i];
                fireClip2.frames[i].spriteCollection.spriteDefinitions[id].position2.x += offsetsX2[i];
                fireClip2.frames[i].spriteCollection.spriteDefinitions[id].position2.y += offsetsY2[i];
                fireClip2.frames[i].spriteCollection.spriteDefinitions[id].position3.x += offsetsX2[i];
                fireClip2.frames[i].spriteCollection.spriteDefinitions[id].position3.y += offsetsY2[i];
            }
            


            projectile.transform.parent = gun.barrelOffset;
            ETGMod.Databases.Items.Add(gun, null, "ANY");

        }



        protected override void OnPickedUpByPlayer(PlayerController player)
        {
            player.SetResistance(EffectResistanceType.Fire, .01f);
            base.OnPickedUpByPlayer(player);
        }
        public override void OnPostFired(PlayerController player, Gun gun)
        {

            gun.PreventNormalFireAudio = true;
        }
        public override void PostProcessProjectile(Projectile projectile)
        {

            if (projectile.GetCachedBaseDamage == 8f)
            {
                //slash
                
                PlayerController player = (PlayerController)this.gun.CurrentOwner;
                Vector2 vector = player.unadjustedAimPoint.XY() - player.CenterPosition;
                ProjectileSlashingBehaviour slasher = projectile.gameObject.AddComponent<ProjectileSlashingBehaviour>();
                slasher.SlashDimensions = 90;
                slasher.SlashRange = 3f;
                slasher.playerKnockback = 0;
                
                slasher.InteractMode = SlashDoer.ProjInteractMode.IGNORE;



            }
            if (projectile.GetCachedBaseDamage == 15f)
            {

                PlayerController player = (PlayerController)this.gun.CurrentOwner;
                Vector2 vector = player.unadjustedAimPoint.XY() - player.CenterPosition;
                ProjectileSlashingBehaviour slasher = projectile.gameObject.AddComponent<ProjectileSlashingBehaviour>();
                slasher.SlashDimensions = 90;
                slasher.SlashRange = 3f;
                slasher.DestroyBaseAfterFirstSlash = false;
                slasher.playerKnockback = 0;
                
                slasher.InteractMode = SlashDoer.ProjInteractMode.IGNORE;

                StartCoroutine(RingOfFire());

            }

            base.PostProcessProjectile(projectile);
        }

        public IEnumerator RingOfFire()
        {

            PlayerController player = (PlayerController)this.gun.CurrentOwner;

            yield return new WaitForSecondsRealtime(.2f);
            AkSoundEngine.PostEvent("Play_Hell_s_bell", base.gameObject);
            for (int i = 0; i < 36; i++)
            {
                
                Projectile projectile2 = ((Gun)ETGMod.Databases.Items[125]).DefaultModule.projectiles[0];
                GameObject gameObject = SpawnManager.SpawnProjectile(projectile2.gameObject, player.CurrentGun.sprite.WorldCenter, Quaternion.Euler(0f, 0f, (player.CurrentGun == null) ? 0f : player.CurrentGun.CurrentAngle - (i * 10)), true);
                Projectile component = gameObject.GetComponent<Projectile>();
                bool flag = component != null;
                if (flag)
                {
                    
                    
                    
                    component.baseData.range *= .5f;
                    
                    component.Owner = player;
                    component.Shooter = player.specRigidbody;
                    component.baseData.damage = 2.5f;
                    
                    
                }
            }
            player.IsOnFire = true;
            player.IncreaseFire(.01f);

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
                PlayerController player = (PlayerController)this.gun.CurrentOwner;
                if (player.CurrentFireMeterValue > 0)
                {
                    gun.DefaultModule.cooldownTime = .2f;
                    gun.reloadTime = .5f;
                    
                }
                else
                {
                    gun.DefaultModule.cooldownTime = .5f;
                    gun.reloadTime = 1f;

                }
            }


        }

        protected override void OnPostDroppedByPlayer(PlayerController player)
        {
            player.SetResistance(EffectResistanceType.Fire, 1f);
            base.OnPostDroppedByPlayer(player);
        }
        public override void OnReloadPressed(PlayerController player, Gun gun, bool bSOMETHING)
        {
            if (gun.IsReloading && this.HasReloaded)
            {
                HasReloaded = false;
                AkSoundEngine.PostEvent("Stop_WPN_All", base.gameObject);
                base.OnReloadPressed(player, gun, bSOMETHING);
                AkSoundEngine.PostEvent("Play_WPN_SAA_reload_01", base.gameObject);
                player.IncreaseFire(.1f);

            }
        }


    }
}

