﻿using System;

using Dungeonator;
using Gungeon;
using ItemAPI;
using UnityEngine;

namespace Knives
{
	// Token: 0x0200000B RID: 11
	class CopperChariot2 : AdvancedGunBehaviour
	{
		// Token: 0x06000051 RID: 81 RVA: 0x00003DB8 File Offset: 0x00001FB8
		public static void Add()
		{
			try
			{
				Gun gun = ETGMod.Databases.Items.NewGun("Copper Chariot", "chariot");
				Game.Items.Rename("outdated_gun_mods:copper_chariot", "ski:copper_chariot");
				gun.gameObject.AddComponent<CopperChariot2>();
				GunExt.SetShortDescription(gun, "Blinding Speed!");
				GunExt.SetLongDescription(gun, "The ghost of a gun nut guards the wielder of this blade. While the blade itself is too weak to be used your new astral companion will be happy to provide assistance.\n\n__________________________________");
				GunExt.SetupSprite(gun, null, "chariot_idle_001", 8);
				GunExt.SetAnimationFPS(gun, gun.shootAnimation, 8);
				GunExt.SetAnimationFPS(gun, gun.reloadAnimation, 8);
				GunExt.AddProjectileModuleFrom(gun, "AK-47", true, true);
				gun.DefaultModule.ammoCost = 1;
				gun.DefaultModule.angleVariance = 0f;
				gun.gunClass = (GunClass.SILLY);
				gun.reloadTime = 0.5f;
				gun.DefaultModule.shootStyle = 0;
				gun.DefaultModule.sequenceStyle = 0;
				gun.DefaultModule.cooldownTime = 1.5f;
				gun.DefaultModule.numberOfShotsInClip = 20;
				gun.InfiniteAmmo = true;
				gun.SetBaseMaxAmmo(1000);
				gun.quality = (PickupObject.ItemQuality.B);
				gun.encounterTrackable.EncounterGuid = "hora hora hora hora!";
				Projectile projectile = UnityEngine.Object.Instantiate<Projectile>(gun.DefaultModule.projectiles[0]);
				projectile.gameObject.SetActive(false);
				ItemAPI.FakePrefab.MarkAsFakePrefab(projectile.gameObject);
				UnityEngine.Object.DontDestroyOnLoad(projectile);
				gun.DefaultModule.projectiles[0] = projectile;
				projectile.baseData.damage = 1f;
				projectile.baseData.speed *= 1f;
				projectile.baseData.range = 0.001f;
				projectile.baseData.force = 0f;
				projectile.pierceMinorBreakables = true;
				projectile.transform.parent = gun.barrelOffset;
				ETGMod.Databases.Items.Add(gun, null, "ANY");
			}
			catch (Exception e)
			{
				Tools.Print("Copper Add", "FFFFFF", true);
				Tools.PrintException(e);
			}
		}

        protected override void OnPickedUpByPlayer(PlayerController player)
        {
         
			try
			{
				base.OnPickup(player);
				player.GunChanged += this.OnGunChanged;
			}
			catch (Exception e)
			{
				Tools.Print("Copper OnPickup", "FFFFFF", true);
				Tools.PrintException(e);
			}
		}

        protected override void OnPostDroppedByPlayer(PlayerController player)

		{
			try
			{
				player.GunChanged -= this.OnGunChanged;
				base.OnPostDrop(player);
			}
			catch (Exception e)
			{
				Tools.Print("Copper OnPostDrop", "FFFFFF", true);
				Tools.PrintException(e);
			}
		}

		// Token: 0x06000392 RID: 914 RVA: 0x000261A4 File Offset: 0x000243A4
		private void OnGunChanged(Gun oldGun, Gun newGun, bool arg3)
		{
			try
			{
				this.RemoveNutOnGunSwitchOut(newGun);
			}
			catch (Exception e)
			{
				Tools.Print("Copper OnGunChanged", "FFFFFF", true);
				Tools.PrintException(e);
			}
		}

		private void RemoveNutOnGunSwitchOut(Gun current)
		{
			try
			{
				bool flag2 = (current != this.gun && this.canSpawn == 1); // to remove the nut on weapon switch
				if (flag2)
				{
					this.KillNut();
				}
			}
			catch (Exception e)
			{
				Tools.Print("Copper RemoveNutOnGunSwitchOut", "FFFFFF", true);
				Tools.PrintException(e);
			}
		}

		private void KillNut()
		{
			try
			{
				if (this.nut && this.nut.healthHaver.IsAlive && canSpawn == 1)
				{
					this.nut.EraseFromExistence(true);
					this.canSpawn = 0;
				}
			}
			catch (Exception e)
			{
				Tools.Print("Copper KillNut", "FFFFFF", true);
				Tools.PrintException(e);
			}
		}



		// Token: 0x06000052 RID: 82 RVA: 0x00003F98 File Offset: 0x00002198
		public override void OnPostFired(PlayerController player, Gun gun)
		{
			try
			{
				gun.PreventNormalFireAudio = false;
				bool flag = this.combat == 1 && this.canSpawn == 0;
				if (flag)
				{
					this.KnightPlacer(player);
					this.canSpawn = 1;
				}
				if (nut.specRigidbody.UnitCenter != null && HasSynergy)
				{
					Gun gunnut = PickupObjectDatabase.GetById(380) as Gun;
					Gun currentGun = nut.CurrentGun;
					GameObject gameObject = gunnut.ObjectToInstantiateOnReload.gameObject;
					GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(gameObject, nut.sprite.WorldCenter, Quaternion.identity);
					SingleSpawnableGunPlacedObject @interface = gameObject2.GetInterface<SingleSpawnableGunPlacedObject>();
					BreakableShieldController component = gameObject2.GetComponent<BreakableShieldController>();
					bool flag3 = gameObject2;
					if (flag3)
					{

						component.maxDuration = 1.7f;

						@interface.Initialize(currentGun);
						component.Initialize(currentGun);


					}
				}
			}
			catch (Exception e)
			{
				Tools.Print("Copper OnPostFired", "FFFFFF", true);
				Tools.PrintException(e);
			}
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00003FD8 File Offset: 0x000021D8
		protected override void Update()
		{
			try
			{
				bool flag = this.gun.CurrentOwner;
				if (flag)
				{
					bool flag2 = !this.gun.PreventNormalFireAudio;
					if (flag2)
					{
						this.gun.PreventNormalFireAudio = true;
					}
					bool flag3 = !this.gun.IsReloading && !this.HasReloaded;
					if (flag3)
					{
						this.HasReloaded = true;
					}
					this.CombatChecker();
				}
				bool flag4 = !this.gun.CurrentOwner && canSpawn == 1;
				if (flag4)
				{
					this.KillNut();
				}
				PlayerController player = (PlayerController)gun.CurrentOwner;
				bool synergy = player.HasPickupID(545);
				if (synergy)
				{
					HasSynergy = true;
					nut.MovementSpeed = 6.4f;
				}
				else
				{
					HasSynergy = false;
					nut.MovementSpeed = 6.1f;
				}


				base.Update();
			}
			catch (Exception e)
			{
				Tools.Print("Copper Update", "FFFFFF", true);
				Tools.PrintException(e);
			}
		}

		// Token: 0x06000054 RID: 84 RVA: 0x000040CC File Offset: 0x000022CC
		public void CombatChecker()
		{
			try
			{
				PlayerController playerController = (PlayerController)this.gun.CurrentOwner;
				bool isInCombat = playerController.IsInCombat;
				if (isInCombat)
				{
					this.combat = 1;
				}
				else
				{
					this.combat = 0;
					this.canSpawn = 0;
				}
			}
			catch (Exception e)
			{
				Tools.Print("Copper CombatCheck", "FFFFFF", true);
				Tools.PrintException(e);
			}
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00004110 File Offset: 0x00002310
		public override void OnReloadPressed(PlayerController player, Gun gun, bool bSOMETHING)
		{
			try
			{
				bool flag = gun.IsReloading && this.HasReloaded && this.nut;
				if (flag)
				{
					this.HasReloaded = false;
					AkSoundEngine.PostEvent("Stop_WPN_All", base.gameObject);
					base.OnReloadPressed(player, gun, bSOMETHING);
					AkSoundEngine.PostEvent("Play_WPN_SAA_reload_01", base.gameObject);
					Tools.Print("OnReloadPressed kill !", "FFFFFF", true);
					this.KillNut();
				}
			}
			catch (Exception e)
			{
				Tools.Print("Copper OnReloadPressed", "FFFFFF", true);
				Tools.PrintException(e);
			}
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00004194 File Offset: 0x00002394
		public void KnightPlacer(PlayerController owner)
		{
			try
			{
				AIActor orLoadByGuid = EnemyDatabase.GetOrLoadByGuid("ec8ea75b557d4e7b8ceeaacdf6f8238c");
				IntVector2 intVector = Vector2Extensions.ToIntVector2(owner.unadjustedAimPoint, (VectorConversions)2);
				RoomHandler absoluteRoomFromPosition = GameManager.Instance.Dungeon.data.GetAbsoluteRoomFromPosition(intVector);
				bool flag = absoluteRoomFromPosition != null && absoluteRoomFromPosition == owner.CurrentRoom && owner.IsInCombat;
				if (flag)
				{
					AIActor aiactor = AIActor.Spawn(orLoadByGuid.aiActor, owner.CenterPosition, absoluteRoomFromPosition, true, (AIActor.AwakenAnimationType)2, true);
					PhysicsEngine.Instance.RegisterOverlappingGhostCollisionExceptions(aiactor.specRigidbody, null, false);
					aiactor.CanTargetEnemies = true;
					aiactor.CanTargetPlayers = false;
					aiactor.IsHarmlessEnemy = true;
					aiactor.CanDropCurrency = false;
					aiactor.IgnoreForRoomClear = true;
					aiactor.MovementSpeed = 5.95f;
					aiactor.CompanionOwner = owner;
					aiactor.IsBuffEnemy = true;
					aiactor.isPassable = true;
					aiactor.gameObject.AddComponent<KillOnRoomClear>();
					aiactor.reinforceType = (AIActor.ReinforceType)2;
					aiactor.HandleReinforcementFallIntoRoom(0.1f);

					//added this so the player doesn't collide with the nut when dodge rolling, he goes through companions
					aiactor.gameObject.AddComponent<CompanionController>();
					CompanionController component = aiactor.gameObject.GetComponent<CompanionController>();
					
					component.Initialize(owner);
					
					

					//aiactor.specRigidbody.AddCollisionLayerIgnoreOverride(CollisionMask.LayerToMask(CollisionLayer.EnemyHitBox, CollisionLayer.EnemyCollider, CollisionLayer.PlayerHitBox,
					//CollisionLayer.Projectile, CollisionLayer.PlayerCollider, CollisionLayer.PlayerBlocker, CollisionLayer.BeamBlocker));

					//aiactor.specRigidbody.AddCollisionLayerIgnoreOverride(CollisionMask.LayerToMask(CollisionLayer.BulletBlocker, CollisionLayer.BulletBreakable, CollisionLayer.Trap));

					this.nut = aiactor;
					MindControlEffect orAddComponent = GameObjectExtensions.GetOrAddComponent<MindControlEffect>(aiactor.gameObject);
					orAddComponent.owner = (this.gun.CurrentOwner as PlayerController);



					// to make the nut invincible against the player effects or any other damage effects
					if (aiactor.healthHaver != null)
					{
						aiactor.healthHaver.PreventAllDamage = true;
					}

					// to prevent the attacks of the nut from damaging the player
					if (aiactor.bulletBank != null)
					{
						AIBulletBank bulletBank = aiactor.bulletBank;
						bulletBank.OnProjectileCreated = (Action<Projectile>)Delegate.Combine(bulletBank.OnProjectileCreated, new Action<Projectile>(CopperChariot.OnPostProcessProjectile));
					}
					if (aiactor.aiShooter != null)
					{
						AIShooter aiShooter = aiactor.aiShooter;
						aiShooter.PostProcessProjectile = (Action<Projectile>)Delegate.Combine(aiShooter.PostProcessProjectile, new Action<Projectile>(CopperChariot.OnPostProcessProjectile));
					}
				}
			}
			catch (Exception e)
			{
				Tools.Print("Copper KnightPlacer", "FFFFFF", true);
				Tools.PrintException(e);
			}
		}

		public static void OnPostProcessProjectile(Projectile proj)
		{
			try
			{
				proj.baseData.damage = 3f;
				proj.collidesWithPlayer = false;
				proj.UpdateCollisionMask();
			}
			catch (Exception e)
			{
				Tools.Print("Copper OnPostProcessProjectile", "FFFFFF", true);
				Tools.PrintException(e);
			}
		}
		private bool HasSynergy;

		// Token: 0x04000015 RID: 21
		private bool HasReloaded;

		// Token: 0x04000016 RID: 22
		public int canSpawn = 0;

		// Token: 0x04000017 RID: 23
		public int combat = 0;

		// Token: 0x04000018 RID: 24
		private AIActor nut;
	}
}
