using System;
using System.Collections;
using System.Collections.Generic;
using Gungeon;
using ItemAPI;
using UnityEngine;

namespace Knives
{	// EXAMPLE CODE I USED BY SPECIAL API_________________________________________________________
	// Token: 0x02000021 RID: 33
	internal class BigGunController : GunBehaviour
	{
		// Token: 0x06000116 RID: 278 RVA: 0x0000C598 File Offset: 0x0000A798
		public static void Init()
		{
			Gun gun = ETGMod.Databases.Items.NewGun("Big Gun", "big_gun");
			Game.Items.Rename("outdated_gun_mods:big_gun", "spapi:big_gun");
			gun.gameObject.AddComponent<BigGunController>();
			GunExt.SetShortDescription(gun, "A Gun That's Big");
			GunExt.SetLongDescription(gun, "A big gun.\n\nIn comparison with the Magnum, this one is bigger.");
			GunExt.SetupSprite(gun, null, "big_gun_idle_001", 8);
			GunExt.SetAnimationFPS(gun, gun.shootAnimation, 16);
			GunExt.AddProjectileModuleFrom(gun, PickupObjectDatabase.GetById(601) as Gun, true, false);
			gun.DefaultModule.shootStyle = ProjectileModule.ShootStyle.SemiAutomatic;
			gun.DefaultModule.angleVariance = 0f;
			gun.DefaultModule.ammoType = GameUIAmmoType.AmmoType.MEDIUM_BULLET;
			Projectile projectile = UnityEngine.Object.Instantiate<Projectile>((PickupObjectDatabase.GetById(601) as Gun).DefaultModule.projectiles[0]);
			projectile.gameObject.SetActive(false);
			FakePrefab.MarkAsFakePrefab(projectile.gameObject);
			UnityEngine.Object.DontDestroyOnLoad(projectile);
			gun.DefaultModule.projectiles[0] = projectile;
			projectile.transform.parent = gun.barrelOffset;
			projectile.baseData.damage *= 2f;
			projectile.baseData.speed *= 2f;
			projectile.name = "BigGun_Projectile";
			gun.DefaultModule.cooldownTime = 0.15f;
			gun.DefaultModule.numberOfShotsInClip = 3;
			gun.reloadTime = 2.1f;
			gun.SetBaseMaxAmmo(60);
			gun.quality = PickupObject.ItemQuality.S;
			gun.barrelOffset.transform.localPosition = new Vector3(1.9f, 1f, 0f);
			gun.muzzleFlashEffects.type = VFXPoolType.None;
			gun.encounterTrackable.EncounterGuid = "oh_look_this_gun_is_big";
			gun.gunClass = GunClass.PISTOL;
			ETGMod.Databases.Items.Add(gun, null, "ANY");
			
		}

		// Token: 0x06000117 RID: 279 RVA: 0x0000C7CC File Offset: 0x0000A9CC
		private void LateUpdate()
		{
			bool flag = this.gun && this.gun.IsReloading && this.gun.CurrentOwner is PlayerController;
			if (flag)
			{
				PlayerController playerController = this.gun.CurrentOwner as PlayerController;
				bool flag2 = playerController.CurrentRoom != null;
				if (flag2)
				{
					playerController.CurrentRoom.ApplyActionToNearbyEnemies(playerController.CenterPosition, 8f, new Action<AIActor, float>(this.ProcessEnemy));
				}
			}
		}

		// Token: 0x06000118 RID: 280 RVA: 0x0000C854 File Offset: 0x0000AA54
		private void ProcessEnemy(AIActor target, float distance)
		{
			for (int i = 0; i < this.TargetEnemies.Count; i++)
			{
				bool flag = target.EnemyGuid == this.TargetEnemies[i];
				if (flag)
				{
					GameManager.Instance.Dungeon.StartCoroutine(this.HandleEnemySuck(target));
					target.EraseFromExistence(true);
					break;
				}
			}
		}

		// Token: 0x06000119 RID: 281 RVA: 0x0000C8BB File Offset: 0x0000AABB
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

		// Token: 0x0600011A RID: 282 RVA: 0x0000C8D4 File Offset: 0x0000AAD4
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

		// Token: 0x04000090 RID: 144
		public List<string> TargetEnemies = new List<string>
		{
			"db35531e66ce41cbb81d507a34366dfe",
			"01972dee89fc4404a5c408d50007dad5",
			"70216cae6c1346309d86d4a0b4603045",
			"88b6b6a93d4b4234a67844ef4728382c",
			"df7fb62405dc4697b7721862c7b6b3cd",
			"47bdfec22e8e4568a619130a267eab5b",
			"3cadf10c489b461f9fb8814abc1a09c1",
			"8bb5578fba374e8aae8e10b754e61d62",
			"e5cffcfabfae489da61062ea20539887",
			"1a78cfb776f54641b832e92c44021cf2",
			"d4a9836f8ab14f3fadd0f597438b1f1f",
			"5f3abc2d561b4b9c9e72b879c6f10c7e",
			"844657ad68894a4facb1b8e1aef1abf9",
			"2feb50a6a40f4f50982e89fd276f6f15"
		};
	}
}

