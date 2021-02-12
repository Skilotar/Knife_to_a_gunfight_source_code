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
    class Spear : AdvancedGunBehaviour
    {
        public static void Add()
        {
            // Get yourself a new gun "base" first.
            // Let's just call it "Basic Gun", and use "jpxfrd" for all sprites and as "codename" All sprites must begin with the same word as the codename. For example, your firing sprite would be named "jpxfrd_fire_001".
            Gun gun = ETGMod.Databases.Items.NewGun("Tribal Spear", "Nat_spr");
            // "kp:basic_gun determines how you spawn in your gun through the console. You can change this command to whatever you want, as long as it follows the "name:itemname" template.
            Game.Items.Rename("outdated_gun_mods:tribal_spear", "sts:tribal_spear");
            gun.gameObject.AddComponent<Spear>();
            //These two lines determines the description of your gun, ".SetShortDescription" being the description that appears when you pick up the gun and ".SetLongDescription" being the description in the Ammonomicon entry. 
            gun.SetShortDescription("True to form");
            gun.SetLongDescription("\n\nAn ancient spear etched with markings of old and stained with the blood of pirates. Though its history is bleak its purpose is valiant. \n\n" +
                "Now you take it to arms once again to save the tribe just as your elders once did.");
            // This is required, unless you want to use the sprites of the base gun.
            // That, by default, is the pea shooter.
            // SetupSprite sets up the default gun sprite for the ammonomicon and the "gun get" popup.
            // WARNING: Add a copy of your default sprite to Ammonomicon Encounter Icon Collection!
            // That means, "sprites/Ammonomicon Encounter Icon Collection/defaultsprite.png" in your mod .zip. You can see an example of this with inside the mod folder.
            gun.SetupSprite(null, "Nat_spr_idle_001", 8);
            // ETGMod automatically checks which animations are available.
            // The numbers next to "shootAnimation" determine the animation fps. You can also tweak the animation fps of the reload animation and idle animation using this method.
            gun.SetAnimationFPS(gun.shootAnimation, 2);
            // Every modded gun has base projectile it works with that is borrowed from other guns in the game. 
            // The gun names are the names from the JSON dump! While most are the same, some guns named completely different things. If you need help finding gun names, ask a modder on the Gungeon discord.
            gun.AddProjectileModuleFrom(PickupObjectDatabase.GetById(481) as Gun, true, false);

            // Here we just take the default projectile module and change its settings how we want it to be.
            gun.DefaultModule.ammoCost = 1;
            gun.DefaultModule.angleVariance = 0;
            gun.gunClass = GunClass.SILLY;
            gun.gunHandedness = GunHandedness.TwoHanded;

            gun.DefaultModule.shootStyle = ProjectileModule.ShootStyle.Charged;
            gun.DefaultModule.sequenceStyle = ProjectileModule.ProjectileSequenceStyle.Random;
            gun.reloadTime = 0f;

            gun.DefaultModule.cooldownTime = .5f;
            gun.InfiniteAmmo = true;
            gun.quality = PickupObject.ItemQuality.C;
           
           // gun.DefaultModule.ammoType = GameUIAmmoType.AmmoType.;
            gun.DefaultModule.numberOfShotsInClip = 1;
            //gun.encounterTrackable.EncounterGuid = "small pokey stick";



            Projectile projectile = UnityEngine.Object.Instantiate<Projectile>(gun.DefaultModule.projectiles[0]);
            projectile.gameObject.SetActive(false);
            FakePrefab.MarkAsFakePrefab(projectile.gameObject);
            UnityEngine.Object.DontDestroyOnLoad(projectile);
            gun.DefaultModule.projectiles[0] = projectile;
            projectile.baseData.damage = 0f;
            projectile.baseData.speed *= 1f;
            projectile.baseData.range = 4f;
            projectile.AdditionalScaleMultiplier = .0001f;
            projectile.transform.parent = gun.barrelOffset;

            projectile.gameObject.AddComponent<UnchangeableRangeController>();
           

            Projectile projectile2 = UnityEngine.Object.Instantiate<Projectile>(gun.DefaultModule.projectiles[0]);
            projectile2.gameObject.SetActive(false);
            FakePrefab.MarkAsFakePrefab(projectile2.gameObject);
            UnityEngine.Object.DontDestroyOnLoad(projectile2);
            gun.DefaultModule.projectiles[0] = projectile2;
            projectile2.baseData.damage = 5f;
            projectile2.baseData.speed *= .5f;
            projectile2.baseData.range = 7f;
            projectile2.AdditionalScaleMultiplier = .0001f;
            
            
            projectile2.transform.parent = gun.barrelOffset;
            projectile2.gameObject.AddComponent<UnchangeableRangeController>();
            
            ProjectileModule.ChargeProjectile item = new ProjectileModule.ChargeProjectile
            {
                Projectile = projectile,
                ChargeTime = 0f
            };
            ProjectileModule.ChargeProjectile item2 = new ProjectileModule.ChargeProjectile
            {
                Projectile = projectile2,
                ChargeTime = 1f
            };
           
            gun.DefaultModule.chargeProjectiles = new List<ProjectileModule.ChargeProjectile>
            {
                item,
                item2,
                
            };

            ETGMod.Databases.Items.Add(gun, null, "ANY");

        }



        public string playerName;
        protected override void OnPickedUpByPlayer(PlayerController player)
        {
           
            if (player.IsPrimaryPlayer)
            {
                playerName = "primaryplayer";
            }
            else
            {
                playerName = "secondaryplayer";
            }
            base.OnPickedUpByPlayer(player);
        }

        public override void PostProcessProjectile(Projectile projectile)
        {
            if (projectile.baseData.range == 4f && projectile.gameObject.GetComponent<UnchangeableRangeController>())
            {
                PlayerController player = this.Owner as PlayerController;
                Vector2 vector = player.unadjustedAimPoint.XY() - player.CenterPosition;
                float zRotation = BraveMathCollege.Atan2Degrees(vector);
                player.StartCoroutine(this.HandleSwing(player, vector, 15f, 2.25f));
                this.gun.CurrentOwner.StartCoroutine(HandleDash(this.gun.CurrentOwner as PlayerController, 1));

            }
            if (projectile.baseData.range == 7f && projectile.gameObject.GetComponent<UnchangeableRangeController>())
            {
                PlayerController player = this.Owner as PlayerController;
                Vector2 vector = player.unadjustedAimPoint.XY() - player.CenterPosition;
                float zRotation = BraveMathCollege.Atan2Degrees(vector);
                player.StartCoroutine(this.HandleSwing(player, vector, 25f, 2.25f));
                this.gun.CurrentOwner.StartCoroutine(HandleDash(this.gun.CurrentOwner as PlayerController, 2));

            }
            base.PostProcessProjectile(projectile);
        
        }

        public SpeculativeRigidbody last_hit;
        private IEnumerator HandleSwing(PlayerController user, Vector2 aimVec, float rayDamage, float rayLength)
        {
            float elapsed = 0f;
            while (elapsed < 1)
            {
                elapsed += BraveTime.DeltaTime;
                SpeculativeRigidbody hitRigidbody = this.IterativeRaycast(user.CenterPosition, aimVec, rayLength, int.MaxValue, user.specRigidbody);
                if (hitRigidbody && hitRigidbody.aiActor && hitRigidbody.aiActor.IsNormalEnemy)
                {
                    if (user.IsPrimaryPlayer)
                    {
                        if (last_hit != hitRigidbody)
                        {
                            hitRigidbody.aiActor.healthHaver.ApplyDamage(rayDamage, aimVec, "primaryplayer", CoreDamageTypes.None, DamageCategory.Normal, false, null, false);
                            last_hit = hitRigidbody;
                        }
                        
                    }
                    else
                    {
                        if (last_hit != hitRigidbody)
                        {
                            hitRigidbody.aiActor.healthHaver.ApplyDamage(rayDamage, aimVec, "secondaryplayer", CoreDamageTypes.None, DamageCategory.Normal, false, null, false);
                            last_hit = hitRigidbody;
                        }

                    }
                    
                }
               

                yield return null;
            }
            yield break;
        }

        
        protected SpeculativeRigidbody IterativeRaycast(Vector2 rayOrigin, Vector2 rayDirection, float rayDistance, int collisionMask, SpeculativeRigidbody ignoreRigidbody)
        {
            int num = 0;
            RaycastResult raycastResult;
            while (PhysicsEngine.Instance.Raycast(rayOrigin, rayDirection, rayDistance, out raycastResult, true, true, collisionMask, new CollisionLayer?(CollisionLayer.Projectile), false, null, ignoreRigidbody))
            {
                num++;
                SpeculativeRigidbody speculativeRigidbody = raycastResult.SpeculativeRigidbody;
                if (num < 3 && speculativeRigidbody != null)
                {
                    MinorBreakable component = speculativeRigidbody.GetComponent<MinorBreakable>();
                    if (component != null)
                    {
                        component.Break(rayDirection.normalized * 3f);
                        RaycastResult.Pool.Free(ref raycastResult);
                        continue;
                    }
                }
                RaycastResult.Pool.Free(ref raycastResult);
                return speculativeRigidbody;
            }
            return null;
        }



        public float duration = .30f;
        public float adjSpeed = 7f;
        public IEnumerator HandleDash(PlayerController user, int dashtype)
        {
            if (dashtype == 1)
            {
                duration = .1f;
                adjSpeed = 20f;
            }
            if (dashtype == 2)
            {
                duration = .20f;
                adjSpeed = 20;
            }
            float elapsed = -BraveTime.DeltaTime;
            float angle = user.CurrentGun.CurrentAngle;
            while (elapsed < duration)
            {
                elapsed += BraveTime.DeltaTime;
                this.gun.CurrentOwner.specRigidbody.Velocity = BraveMathCollege.DegreesToVector(angle).normalized * adjSpeed;
                yield return null;
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


