﻿ using System;
using System.Collections;
using Gungeon;
using MonoMod;
using UnityEngine;
using ItemAPI;
using Dungeonator;
using System.Collections.Generic;
using MultiplayerBasicExample;

namespace Knives
{

    class Succ : AdvancedGunBehaviour
    {
        public static void Add()
        {
            // Get yourself a new gun "base" first.
            // Let's just call it "Basic Gun", and use "jpxfrd" for all sprites and as "codename" All sprites must begin with the same word as the codename. For example, your firing sprite would be named "jpxfrd_fire_001".
            Gun gun = ETGMod.Databases.Items.NewGun("Holstergeist 5000", "Succ");
            // "kp:basic_gun determines how you spawn in your gun through the console. You can change this command to whatever you want, as long as it follows the "name:itemname" template.
            Game.Items.Rename("outdated_gun_mods:holstergeist_5000", "ski:holstergeist_5000");
            gun.gameObject.AddComponent<Succ>();
            //These two lines determines the description of your gun, ".SetShortDescription" being the description that appears when you pick up the gun and ".SetLongDescription" being the description in the Ammonomicon entry. 
            gun.SetShortDescription("Some Dust Bunnys Worst Nightmare");
            gun.SetLongDescription("Ashes to ashes... \n" +
                "Dust to dust... \n" +
                "To more dust.. \n" +
                "Where did all this dust come from?\n" +
                "Capture up to 5 enemies in your bag and fire them upon reloading. " +
                "Reload can be done at full ammo. " +
                "Enemies loaded Over 5 get deleted. " +
                "Some enemies shoot with a delay because of how they spawn in. " +
                "Enemies with more health will put up more of a fight when you capture them. \n" +
                "Now you too can hit a dude with another dude!" +
                "\n\n\n - Knife_to_a_Gunfight");
            // This is required, unless you want to use the sprites of the base gun.
            // That, by default, is the pea shooter.
            // SetupSprite sets up the default gun sprite for the ammonomicon and the "gun get" popup.
            // WARNING: Add a copy of your default sprite to Ammonomicon Encounter Icon Collection!
            // That means, "sprites/Ammonomicon Encounter Icon Collection/defaultsprite.png" in your mod .zip. You can see an example of this with inside the mod folder.
            gun.SetupSprite(null, "Succ_idle_001", 8);
            // ETGMod automatically checks which animations are available.
            // The numbers next to "shootAnimation" determine the animation fps. You can also tweak the animation fps of the reload animation and idle animation using this method.
            gun.SetAnimationFPS(gun.shootAnimation, 10);
            gun.SetAnimationFPS(gun.reloadAnimation, 8);
            // Every modded gun has base projectile it works with that is borrowed from other guns in the game. 
            // The gun names are the names from the JSON dump! While most are the same, some guns named completely different things. If you need help finding gun names, ask a modder on the Gungeon discord.
            gun.AddProjectileModuleFrom(PickupObjectDatabase.GetByEncounterName("Hegemony Rifle") as Gun, true, false);
            // Here we just take the default projectile module and change its settings how we want it to be.
            gun.DefaultModule.ammoCost = 2;
            gun.DefaultModule.shootStyle = ProjectileModule.ShootStyle.Automatic;
            gun.DefaultModule.sequenceStyle = ProjectileModule.ProjectileSequenceStyle.Random;
            gun.reloadTime = 2f;
            gun.DefaultModule.cooldownTime = .05f;
            gun.DefaultModule.numberOfShotsInClip = 60;
            gun.SetBaseMaxAmmo(2000);
            gun.CurrentAmmo = 2000;
            gun.DefaultModule.ammoType = GameUIAmmoType.AmmoType.CUSTOM;
            gun.DefaultModule.customAmmoType = "red_beam";
            gun.muzzleFlashEffects = null;
            gun.usesContinuousFireAnimation = true;
            // Here we just set the quality of the gun and the "EncounterGuid", which is used by Gungeon to identify the gun.
            gun.quality = PickupObject.ItemQuality.A;
            gun.encounterTrackable.EncounterGuid = "https://www.youtube.com/watch?v=GKRA7-ZqxSM";
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
            projectile.baseData.damage = 0f;
            projectile.baseData.speed *= 1f;
            projectile.baseData.force = -2f;
            projectile.transform.parent = gun.barrelOffset;



            ETGMod.Databases.Items.Add(gun, null, "ANY");

        }
        public bool isSUCC = false;
        public override void OnPostFired(PlayerController player, Gun gun)
        {
            if (!isSUCC)
            {
                AkSoundEngine.PostEvent("Play_vac_sfx", base.gameObject);
                AkSoundEngine.PostEvent("Play_vac_sfx", base.gameObject);
                isSUCC = true;
            }

            gun.PreventNormalFireAudio = true;

        }
        public override void OnFinishAttack(PlayerController player, Gun gun)
        {
            if (isSUCC)
            {
                AkSoundEngine.PostEvent("Stop_vac_sfx", base.gameObject);
                AkSoundEngine.PostEvent("Stop_vac_sfx", base.gameObject);
                isSUCC = false;
            }
            base.OnFinishAttack(player, gun);
        }
        private bool HasReloaded;
        public override void PostProcessProjectile(Projectile projectile)
        {   // outer pull
            ProjectileSlashingBehaviour slasher1 = projectile.gameObject.GetOrAddComponent<ProjectileSlashingBehaviour>();
            slasher1.SlashRange = 15f;
            slasher1.SlashDimensions = 30;
            slasher1.SlashVFX.type = VFXPoolType.None;
            slasher1.DoSound = false;
            // innner pull 
            PlayerController player = (PlayerController)this.gun.CurrentOwner;
            Projectile projectile1 = ((Gun)ETGMod.Databases.Items[3]).DefaultModule.projectiles[0];
            GameObject gameObject = SpawnManager.SpawnProjectile(projectile1.gameObject, player.CurrentGun.sprite.WorldCenter, Quaternion.Euler(0f, 0f, (player.CurrentGun == null) ? 0f : player.CurrentGun.CurrentAngle), true);
            Projectile component = gameObject.GetComponent<Projectile>();
            component.baseData.force = -9;
            component.baseData.damage = .5f;
            component.Owner = player;
            ProjectileSlashingBehaviour slasher2 = component.gameObject.GetOrAddComponent<ProjectileSlashingBehaviour>();
            slasher2.SlashRange = 15f;
            slasher2.SlashDimensions = 5;
            slasher2.SlashVFX.type = VFXPoolType.None;
            slasher2.DoSound = false;
            //inner repel
            Projectile projectile2 = ((Gun)ETGMod.Databases.Items[3]).DefaultModule.projectiles[0];
            GameObject gameObject2 = SpawnManager.SpawnProjectile(projectile2.gameObject, player.CurrentGun.sprite.WorldCenter, Quaternion.Euler(0f, 0f, (player.CurrentGun == null) ? 0f : player.CurrentGun.CurrentAngle), true);
            Projectile component2 = gameObject2.GetComponent<Projectile>();
            component2.baseData.force = 9;
            component2.StunApplyChance = 100f;
            component2.AppliesStun = true;
            component2.AppliedStunDuration = .5f;
            component2.baseData.damage = 1f;
            component2.Owner = player;
            ProjectileSlashingBehaviour slasher3 = component2.gameObject.GetOrAddComponent<ProjectileSlashingBehaviour>();
            slasher3.SlashRange = 2.2f;
            slasher3.SlashDimensions = 20;
            slasher3.SlashVFX.type = VFXPoolType.None;
            slasher3.DoSound = false;


            Vector2 vector = player.unadjustedAimPoint.XY() - player.CenterPosition;
            
            player.StartCoroutine(this.HandleSwing(player, vector, .2f, 3f));



            base.PostProcessProjectile(projectile);
        }

        private IEnumerator HandleSwing(PlayerController user, Vector2 aimVec, float rayDamage, float rayLength)
        {
            float elapsed = 0f;
            while (elapsed < 1)
            {
                elapsed += BraveTime.DeltaTime;
                SpeculativeRigidbody hitRigidbody = this.IterativeRaycast(user.CenterPosition, aimVec, rayLength, int.MaxValue, user.specRigidbody);
                if (hitRigidbody && hitRigidbody.aiActor && hitRigidbody.aiActor.IsNormalEnemy)
                {
                    ProcessEnemy(hitRigidbody.aiActor);
                    hitRigidbody.aiActor.behaviorSpeculator.Stun(.5f, true);
                }

                yield return null;
            }
            yield break;
        }

        protected SpeculativeRigidbody IterativeRaycast(Vector2 rayOrigin, Vector2 rayDirection, float rayDistance, int collisionMask, SpeculativeRigidbody ignoreRigidbody)
        {
            int num = 0;
            RaycastResult raycastResult;
            while (PhysicsEngine.Instance.Raycast(rayOrigin, rayDirection, rayDistance, out raycastResult, true, true, collisionMask, new CollisionLayer?(CollisionLayer.BulletBlocker), false, null, ignoreRigidbody))
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
        public bool wastrue = false;
        public System.Random rng = new System.Random();
        private void ProcessEnemy(AIActor target)
        {
            PlayerController player = this.gun.CurrentOwner as PlayerController;
            for (int i = 0; i < this.TargetEnemiesBLK.Count; i++)
            {
                bool not_in = target.EnemyGuid == this.TargetEnemiesBLK[i];
               
                if (not_in)
                {
                    wastrue = true;
                    break;
                   
                }
            }
            bool health_check = target.CompanionOwner != player && !target.healthHaver.IsBoss && (target.healthHaver.GetCurrentHealth() <= target.healthHaver.GetMaxHealth() * .75);
            if (wastrue != true && health_check)
            {
                succ.Add(target.EnemyGuid.ToString());
                int sfx = rng.Next(1, 3);
                if(sfx == 1)
                {
                    AkSoundEngine.PostEvent("Play_pwoomp_01", base.gameObject);
                    AkSoundEngine.PostEvent("Play_pwoomp_01", base.gameObject);
                    AkSoundEngine.PostEvent("Play_pwoomp_01", base.gameObject);
                    AkSoundEngine.PostEvent("Play_pwoomp_01", base.gameObject);
                    AkSoundEngine.PostEvent("Play_pwoomp_01", base.gameObject);
                    AkSoundEngine.PostEvent("Play_pwoomp_01", base.gameObject);

                }
                if (sfx == 2)
                {
                    AkSoundEngine.PostEvent("Play_pwoomp_02", base.gameObject);
                    AkSoundEngine.PostEvent("Play_pwoomp_02", base.gameObject);
                    AkSoundEngine.PostEvent("Play_pwoomp_02", base.gameObject);
                    AkSoundEngine.PostEvent("Play_pwoomp_02", base.gameObject);
                    AkSoundEngine.PostEvent("Play_pwoomp_02", base.gameObject);
                    AkSoundEngine.PostEvent("Play_pwoomp_02", base.gameObject);
                }
                if (sfx == 3)
                {
                    AkSoundEngine.PostEvent("Play_pwoomp_03", base.gameObject);
                    AkSoundEngine.PostEvent("Play_pwoomp_03", base.gameObject);
                    AkSoundEngine.PostEvent("Play_pwoomp_03", base.gameObject);
                    AkSoundEngine.PostEvent("Play_pwoomp_03", base.gameObject);
                    AkSoundEngine.PostEvent("Play_pwoomp_03", base.gameObject);
                    AkSoundEngine.PostEvent("Play_pwoomp_03", base.gameObject);
                }
                GameManager.Instance.Dungeon.StartCoroutine(this.HandleEnemySuck(target));
                target.EraseFromExistence(true);
            }
            
           


        }


        private IEnumerator HandleEnemySuck(AIActor target)
        {
            Transform copySprite = this.CreateEmptySprite(target);
            Vector3 startPosition = copySprite.transform.position;
            float elapsed = 0f;
            float duration = 0.5f;
            while (elapsed < duration)
            {
                elapsed += BraveTime.DeltaTime;
                bool flag = this.gun && copySprite;
                if (flag)
                {
                    Vector3 position = this.gun.PrimaryHandAttachPoint.position;
                    float t = elapsed / duration * (elapsed / duration);
                    copySprite.position = Vector3.Lerp(startPosition, position, t);
                    copySprite.rotation = Quaternion.Euler(0f, 0f, 360f * BraveTime.DeltaTime) * copySprite.rotation;
                    copySprite.localScale = Vector3.Lerp(Vector3.one, new Vector3(0.1f, 0.1f, 0.1f), t);
                    position = default(Vector3);
                }
                yield return null;
            }
            bool flag2 = copySprite;
            if (flag2)
            {
                UnityEngine.Object.Destroy(copySprite.gameObject);
            }
            bool flag3 = this.gun;
            if (flag3)
            {
                this.gun.GainAmmo(1);
            }
            yield break;
        }


        private Transform CreateEmptySprite(AIActor target)
        {
            GameObject gameObject = new GameObject("suck image");
            gameObject.layer = target.gameObject.layer;
            tk2dSprite tk2dSprite = gameObject.AddComponent<tk2dSprite>();
            gameObject.transform.parent = SpawnManager.Instance.VFX;
            tk2dSprite.SetSprite(target.sprite.Collection, target.sprite.spriteId);
            tk2dSprite.transform.position = target.sprite.transform.position;
            GameObject gameObject2 = new GameObject("image parent");
            gameObject2.transform.position = tk2dSprite.WorldCenter;
            tk2dSprite.transform.parent = gameObject2.transform;
            bool flag = target.optionalPalette != null;
            if (flag)
            {
                tk2dSprite.renderer.material.SetTexture("_PaletteTex", target.optionalPalette);
            }
            return gameObject2.transform;
        }

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

                if (succ.Count > 5)
                {
                    succ.RemoveAt(0);

                }

               
            }

        }
        
       
        public override void OnReloadPressed(PlayerController player, Gun gun, bool bSOMETHING)
        {

            HasReloaded = false;
            AkSoundEngine.PostEvent("Stop_WPN_All", base.gameObject);
            base.OnReloadPressed(player, gun, bSOMETHING);
            // spit
            PlayerController owner = (PlayerController)this.gun.CurrentOwner;
            if (succ[0] != null)
            {
                AIActor orLoadByGuid = EnemyDatabase.GetOrLoadByGuid(succ[0]);
                IntVector2 intVector = Vector2Extensions.ToIntVector2(owner.unadjustedAimPoint, (VectorConversions)2);
                RoomHandler absoluteRoomFromPosition = GameManager.Instance.Dungeon.data.GetAbsoluteRoomFromPosition(intVector);
                bool flag = absoluteRoomFromPosition != null && absoluteRoomFromPosition == owner.CurrentRoom && owner.IsInCombat;
                if (flag)
                {
                   
                    AIActor aiactor = AIActor.Spawn(orLoadByGuid.aiActor, gun.sprite.WorldBottomRight, absoluteRoomFromPosition, true, (AIActor.AwakenAnimationType.Spawn), true);
                    PhysicsEngine.Instance.RegisterOverlappingGhostCollisionExceptions(aiactor.specRigidbody, null, false);
                    aiactor.CanTargetEnemies = false;
                    aiactor.CanTargetPlayers = true;
                    aiactor.IsHarmlessEnemy = true;
                    aiactor.CanDropCurrency = true;
                    aiactor.IgnoreForRoomClear = false;
                    aiactor.IsBuffEnemy = true;
                    aiactor.isPassable = true;
                    aiactor.reinforceType = (AIActor.ReinforceType.Instant);
                    aiactor.HandleReinforcementFallIntoRoom(-1f);
                    aiactor.specRigidbody.AddCollisionLayerIgnoreOverride(CollisionMask.LayerToMask(CollisionLayer.PlayerHitBox, CollisionLayer.PlayerCollider));
                    

                    Projectile projectile1 = ((Gun)ETGMod.Databases.Items[541]).DefaultModule.chargeProjectiles[0].Projectile;
                    GameObject gameObject = SpawnManager.SpawnProjectile(projectile1.gameObject, player.CurrentGun.sprite.WorldCenter, Quaternion.Euler(0f, 0f, (player.CurrentGun == null) ? 0f : player.CurrentGun.CurrentAngle), true);
                    Projectile component = gameObject.GetComponent<Projectile>();
                    component.baseData.damage = 150f;
                    component.baseData.force = 800f;
                    component.baseData.speed *= 1f;

                    component.Owner = player;
                    
                    HandlePostProcessProjectile(component);
                    succ.RemoveAt(0);
                    StartCoroutine(wait());
                    

                }
            }


        }

        public IEnumerator wait()
        {
            yield return new WaitForSeconds(.5f);

            PlayerController player = (PlayerController)this.gun.CurrentOwner;
            Projectile projectile1 = ((Gun)ETGMod.Databases.Items[541]).DefaultModule.chargeProjectiles[0].Projectile;
            GameObject gameObject = SpawnManager.SpawnProjectile(projectile1.gameObject, player.CurrentGun.sprite.WorldCenter, Quaternion.Euler(0f, 0f, (player.CurrentGun == null) ? 0f : player.CurrentGun.CurrentAngle), true);
            Projectile component = gameObject.GetComponent<Projectile>();
            component.baseData.damage = 150f;
            component.baseData.force = 800f;
            component.baseData.speed *= 1f;

            component.Owner = player;
            HandlePostProcessProjectile(component);


            yield return new WaitForSeconds(.5f);
           
            Projectile projectile2 = ((Gun)ETGMod.Databases.Items[541]).DefaultModule.chargeProjectiles[0].Projectile;
            GameObject gameObject2 = SpawnManager.SpawnProjectile(projectile2.gameObject, player.CurrentGun.sprite.WorldCenter, Quaternion.Euler(0f, 0f, (player.CurrentGun == null) ? 0f : player.CurrentGun.CurrentAngle), true);
            Projectile component2 = gameObject2.GetComponent<Projectile>();
            component2.baseData.damage = 150f;
            component2.baseData.force = 800f;
            component2.baseData.speed *= 1f;

            component2.Owner = player;
            HandlePostProcessProjectile(component);

        }

        private void HandlePostProcessProjectile(Projectile targetProjectile)
        {
            targetProjectile.OnHitEnemy = (Action<Projectile, SpeculativeRigidbody, bool>)Delegate.Combine(targetProjectile.OnHitEnemy, new Action<Projectile, SpeculativeRigidbody, bool>(this.HandleHitEnemy));
        }
        private void HandleHitEnemy(Projectile sourceProjectile, SpeculativeRigidbody hitRigidbody, bool fatal)
        {
            if (fatal && hitRigidbody)
            {
                if (sourceProjectile)
                {
                    sourceProjectile.baseData.force = 0f;
                }
                AIActor aiActor = hitRigidbody.aiActor;
                KnockbackDoer knockbackDoer = hitRigidbody.knockbackDoer;
                aiActor.CompanionOwner = null;
                hitRigidbody.AddCollisionLayerOverride(CollisionMask.LayerToMask(CollisionLayer.EnemyHitBox));
                hitRigidbody.OnPreRigidbodyCollision = (SpeculativeRigidbody.OnPreRigidbodyCollisionDelegate)Delegate.Combine(hitRigidbody.OnPreRigidbodyCollision, new SpeculativeRigidbody.OnPreRigidbodyCollisionDelegate(this.HandleHitEnemyHitEnemy));

                float num = -1f;
                AIActor nearestEnemyInDirection = aiActor.ParentRoom.GetNearestEnemyInDirection(aiActor.CenterPosition, sourceProjectile.Direction, 50, out num, true);
                Vector2 direction = sourceProjectile.Direction;
                if (nearestEnemyInDirection)
                {
                    direction = nearestEnemyInDirection.CenterPosition - aiActor.CenterPosition;
                }
                PlayerController player = (PlayerController)this.gun.CurrentOwner;
                Vector2 vector = player.unadjustedAimPoint.XY() - player.CenterPosition;
                hitRigidbody.aiActor.knockbackDoer.ApplyKnockback(vector, 800, true);


            }
        }


        private void HandleHitEnemyHitEnemy(SpeculativeRigidbody myRigidbody, PixelCollider myPixelCollider, SpeculativeRigidbody otherRigidbody, PixelCollider otherPixelCollider)
        {
            if (otherRigidbody && otherRigidbody.aiActor && myRigidbody && myRigidbody.healthHaver)
            {
                AIActor aiActor = otherRigidbody.aiActor;
                myRigidbody.OnPreRigidbodyCollision = (SpeculativeRigidbody.OnPreRigidbodyCollisionDelegate)Delegate.Remove(myRigidbody.OnPreRigidbodyCollision, new SpeculativeRigidbody.OnPreRigidbodyCollisionDelegate(this.HandleHitEnemyHitEnemy));


                if (aiActor.healthHaver.IsBoss)
                {
                    aiActor.healthHaver.ApplyDamage(myRigidbody.healthHaver.GetMaxHealth() * 1f, myRigidbody.Velocity, "Pinball", CoreDamageTypes.None, DamageCategory.Normal, false, null, true);
                }
                else
                {
                    aiActor.healthHaver.ApplyDamage(myRigidbody.healthHaver.GetMaxHealth() * 2f, myRigidbody.Velocity, "Pinball", CoreDamageTypes.None, DamageCategory.Normal, false, null, false);
                }


            }
        }
        public List<string> succ = new List<string>
        { };
        public List<string> TargetEnemies = new List<string>
        {
            "db35531e66ce41cbb81d507a34366dfe",
            "6b7ef9e5d05b4f96b04f05ef4a0d1b18",
            "128db2f0781141bcb505d8f00f9e4d47",
            "70216cae6c1346309d86d4a0b4603045",
            "4d37ce3d666b4ddda8039929225b7ede",
            "42be66373a3d4d89b91a35c9ff8adfec",
            "01972dee89fc4404a5c408d50007dad5",
            "88b6b6a93d4b4234a67844ef4728382c",
            "05891b158cd542b1a5f3df30fb67a7ff",
            "0239c0680f9f467dbe5c4aab7dd1eca6",
            "4db03291a12144d69fe940d5a01de376",
            "af84951206324e349e1f13f9b7b60c1a",
            "042edb1dfb614dc385d5ad1b010f2ee3",
            "b5503988e3684e8fa78274dd0dda0bf5",
            "06a82532447247f9ada1940d079a31a7",
            "df7fb62405dc4697b7721862c7b6b3cd",
            "7b0b1b6d9ce7405b86b75ce648025dd6",
            "ffdc8680bdaa487f8f31995539f74265",
            "b54d89f9e802455cbb2b8a96a31e8259",
            "864ea5a6a9324efc95a0dd2407f42810",
            "c1757107b9a44f0c823a707adeb4ae7e",
            "c4fba8def15e47b297865b18e36cbef8",
            "b08ec82bef6940328c7ecd9ffc6bd16c",
            "b5e699a0abb94666bda567ab23bd91c4",
            "2752019b770f473193b08b4005dc781f",
            "9b2cf2949a894599917d4d391a0b7394",
            "249db525a9464e5282d02162c88e0357",
            "022d7c822bc146b58fe3b0287568aaa2",
            "f155fd2759764f4a9217db29dd21b7eb",
            "336190e29e8a4f75ab7486595b700d4a",
            "5288e86d20184fa69c91ceb642d31474",
            "95ec774b5a75467a9ab05fa230c0c143",
            "2feb50a6a40f4f50982e89fd276f6f15",
            "2d4f8b5404614e7d8b235006acde427a",
            "21dd14e5ca2a4a388adab5b11b69a1e1",
            "c0ff3744760c4a2eb0bb52ac162056e6",
            "b4666cb6ef4f4b038ba8924fd8adf38f",
            "6f22935656c54ccfb89fca30ad663a64",
            "a400523e535f41ac80a43ff6b06dc0bf",
            "7ec3e8146f634c559a7d58b19191cd43",
            "c2f902b7cbe745efb3db4399927eab34",
            "ac986dabc5a24adab11d48a4bccf4cb1",
            "48d74b9c65f44b888a94f9e093554977",
            "3cadf10c489b461f9fb8814abc1a09c1",
            "e61cab252cfb435db9172adc96ded75f",
            "fe3fe59d867347839824d5d9ae87f244",
            "b8103805af174924b578c98e95313074",
            "ec8ea75b557d4e7b8ceeaacdf6f8238c",
            "ed37fa13e0fa4fcf8239643957c51293",
            "6e972cd3b11e4b429b888b488e308551",
            "ccf6d241dad64d989cbcaca2a8477f01",
            "0b547ac6b6fc4d68876a241a88f5ca6a",
            "37340393f97f41b2822bc02d14654172",
            "31a3ea0c54a745e182e22ea54844a82d",
            "c5b11bfc065d417b9c4d03a5e385fe2c",
            "43426a2e39584871b287ac31df04b544",
            "9d50684ce2c044e880878e86dbada919",
            "9b4fb8a2a60a457f90dcf285d34143ac",
            "f905765488874846b7ff257ff81d6d0c",
            "eed5addcc15148179f300cc0d9ee7f94",
            "5f3abc2d561b4b9c9e72b879c6f10c7e",
            "044a9f39712f456597b9762893fbc19c",
            "33b212b856b74ff09252bf4f2e8b8c57",
            "3f2026dc3712490289c4658a2ba4a24b",
            "e5cffcfabfae489da61062ea20539887",
            "b1540990a4f1480bbcb3bea70d67f60d",
            "56f5a0f2c1fc4bc78875aea617ee31ac",
            "56fb939a434140308b8f257f0f447829",
            "d8a445ea4d944cc1b55a40f22821ae69",
            "a9cc6a4e9b3d46ea871e70a03c9f77d4",
            "556e9f2a10f9411cb9dbfd61e0e0f1e1",
            "c5a0fd2774b64287bf11127ca59dd8b4",
            "b67ffe82c66742d1985e5888fd8e6a03",
            "d9632631a18849539333a92332895ebd",
            "1898f6fe1ee0408e886aaf05c23cc216",
            "abd816b0bcbf4035b95837ca931169df",
            "07d06d2b23cc48fe9f95454c839cb361",
            "78eca975263d4482a4bfa4c07b32e252",
            "cf27dd464a504a428d87a8b2560ad40a",
            "d1c9781fdac54d9e8498ed89210a0238",
            "1a78cfb776f54641b832e92c44021cf2",
            "1bd8e49f93614e76b140077ff2e33f2b",
            "d4a9836f8ab14f3fadd0f597438b1f1f",
            "116d09c26e624bca8cca09fc69c714b3",
            "0ff278534abb4fbaaa65d3f638003648",
            "1a4872dafdb34fd29fe8ac90bd2cea67",
            "383175a55879441d90933b5c4e60cf6f",
            "72d2f44431da43b8a3bae7d8a114a46d",
            "3f6d6b0c4a7c4690807435c7b37c35a5",
            "cd4a4b7f612a4ba9a720b9f97c52f38c",
            "8bb5578fba374e8aae8e10b754e61d62",
            "98ea2fe181ab4323ab6e9981955a9bca",
            "062b9b64371e46e195de17b6f10e47c8",
            "d5a7b95774cd41f080e517bea07bf495",
            "c182a5cb704d460d9d099a47af49c913",
            "206405acad4d4c33aac6717d184dc8d4",
            "7f665bd7151347e298e4d366f8818284",
            "e21ac9492110493baef6df02a2682a0d",
            "02a14dec58ab45fb8aacde7aacd25b01",
            "47bdfec22e8e4568a619130a267eab5b",
            "78a8ee40dff2477e9c2134f6990ef297",
            "566ecca5f3b04945ac6ce1f26dedbf4f",
            "1398aaccb26d42f3b998c367b7800b85",
            "9044d8e4431f490196ba697927a4e3d4",
            "40bf8b0d97794a26b8c440425ec8cac1",
            "3590db6c4eac474a93299a908cb77ab2",
            "8b4a938cdbc64e64822e841e482ba3d2",
            "b1770e0f1c744d9d887cc16122882b4f",
            "c0260c286c8d4538a697c5bf24976ccf",
            "3e98ccecf7334ff2800188c417e67c15",
            "ba657723b2904aa79f9e51bce7d23872",
            "19b420dec96d4e9ea4aebc3398c0ba7a",
            "463d16121f884984abe759de38418e48",
            "844657ad68894a4facb1b8e1aef1abf9",
            "f38686671d524feda75261e469f30e0b",
            "a446c626b56d4166915a4e29869737fd",
            "22fc2c2c45fb47cf9fb5f7b043a70122",
            "98ca70157c364750a60f5e0084f9d3e2",
            "1bc2a07ef87741be90c37096910843ab",
            "475c20c1fd474dfbad54954e7cba29c1",
            "45192ff6d6cb43ed8f1a874ab6bef316",
            "be0683affb0e41bbb699cb7125fdded6",
            "eeb33c3a5a8e4eaaaaf39a743e8767bc",
            "ba928393c8ed47819c2c5f593100a5bc",
            "4538456236f64ea79f483784370bc62f",
            "d4dd2b2bbda64cc9bcec534b4e920518",
            "8a9e9bedac014a829a48735da6daf3da",
            "cf2b7021eac44e3f95af07db9a7c442c",
            "c50a862d19fc4d30baeba54795e8cb93",
            "bb73eeeb9e584fbeaf35877ec176de28",
            "db97e486ef02425280129e1e27c33118",
            "edc61b105ddd4ce18302b82efdc47178",
            "39de9bd6a863451a97906d949c103538",
            "98fdf153a4dd4d51bf0bafe43f3c77ff",
            "70058857b0a641a888ac4389bd10f455",


        };

        public List<string> TargetEnemiesBLK = new List<string>
        {
            "ec6b674e0acd4553b47ee94493d66422",
            "4b992de5b4274168a8878ef9bf7ea36b",
            "fa76c8cfdf1c4a88b55173666b4bc7fb",
            "998807b57e454f00a63d67883fcf90d6",
            "e667fdd01f1e43349c03a18e5b79e579",
            "fc809bd43a4d41738a62d7565456622c",
            "8817ab9de885424d9ba83455ead5ffef",
            "ffca09398635467da3b1f4a54bcfda80",
            "ea40fcc863d34b0088f490f4e57f8913",
            "c00390483f394a849c36143eb878998f",
            "5729c8b5ffa7415bb3d01205663a33ef",
            "3a077fa5872d462196bb9a3cb1af02a3",
            "f3b04a067a65492f8b279130323b41f0",
            "465da2bb086a4a88a803f79fe3a27677",
            "2ebf8ef6728648089babb507dec4edb7",
            "6c43fddfd401456c916089fdd1c99b1c",
            "56a430ddce6f4a71b9757ba3b2e52b1a",
            "d8d651e3484f471ba8a2daa4bf535ce6",
            "abfb454340294a0992f4173d6e5898a8",
            "9189f46c47564ed588b9108965f975c9",
            "5e0af7f7d9de4755a68d2fd3bbc15df4 ",
            "538669d3b2cd4edca2e3699812bcf2b6",
            "c2f902b7cbe745efb3db4399927eab34",
            "c07ef60ae32b404f99e294a6f9acba75",
            "7ee0a3fbb3dc417db4c3073ba23e1be8",
            "c367f00240a64d5d9f3c26484dc35833",
            "8d441ad4e9924d91b6070d5b3438d066",
            "1b5810fafbec445d89921a4efb4e42b7",
            "70058857b0a641a888ac4389bd10f455 ",
            "dc3cd41623d447aeba77c77c99598426",
            "a9cc6a4e9b3d46ea871e70a03c9f77d4",
            "8b913eea3d174184be1af362d441910d",
            "1ccdace06ebd42dc984d46cb1f0db6cf",
            "d375913a61d1465f8e4ffcf4894e4427",
            "b98b10fca77d469e80fb45f3c5badec5",
            "0d3f7c641557426fbac8596b61c9fb45",
            "68a238ed6a82467ea85474c595c49c6e",
            "cd88c3ce60c442e9aa5b3904d31652bc",
            "575a37abca8d414d89c4e251dd606050",
            "b98b10fca77d469e80fb45f3c5badec5",
            "7c5d5f09911e49b78ae644d2b50ff3bf",
            "41ba74c517534f02a62f2e2028395c58",
            "705e9081261446039e1ed9ff16905d04",
            "76bc43539fc24648bff4568c75c686d1",
            "fe51c83b41ce4a46b42f54ab5f31e6d0",
            "ededff1deaf3430eaf8321d0c6b2bd80",
            "6450d20137994881aff0ddd13e3d40c8",
            "d8fd592b184b4ac9a3be217bc70912a2 ",
            "88f037c3f93b4362a040a87b30770407",
            "5695e8ffa77c4d099b4d9bd9536ff35e",
            "3f11bbbc439c4086a180eb0fb9990cb4 ",
            "a38e9dca103c4e4fa4bf478cf9a2f2de",
            "c6c8e59d0f5d41969c74e802c9d67d07",
            "5fa8c86a65234b538cd022f726af2aea",
            "8b0dd96e2fe74ec7bebc1bc689c0008a",
            "78a8ee40dff2477e9c2134f6990ef297",
            "880bbe4ce1014740ba6b4e2ea521e49d",
            "7bd9c670f35b4b8d84280f52a5cc47f6",
            "2ccaa1b7ae10457396a1796decda9cf6",
            "39dca963ae2b4688b016089d926308ab",
            "11a14dbd807e432985a89f69b5f9b31e ",
            "86237c6482754cd29819c239403a2de7",
            "ad35abc5a3bf451581c3530417d89f2c",
            "640238ba85dd4e94b3d6f68888e6ecb8",
            "e9fa6544000942a79ad05b6e4afb62db",
            "ebf2314289ff4a4ead7ea7ef363a0a2e",
            "ab4a779d6e8f429baafa4bf9e5dca3a9",
            "9216803e9c894002a4b931d7ea9c6bdf",
            "cc9c41aa8c194e17b44ac45f993dd212",
            "45f5291a60724067bd3ccde50f65ac22",
            "41ab10778daf4d3692e2bc4b370ab037",
            "2976522ec560460c889d11bb54fbe758",
            "6f9c28403d3248c188c391f5e40774c5",
            "e456b66ed3664a4cb590eab3a8ff3814",
            "6868795625bd46f3ae3e4377adce288b",
            "4d164ba3f62648809a4a82c90fc22cae",
            "05b8afe0b6cc4fffa9dc6036fa24c8ec",
            
        };
    }
}

