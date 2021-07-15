using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

using ItemAPI;
using Dungeonator;
using System.Reflection;
using Random = System.Random;
using FullSerializer;
using System.Collections;
using Gungeon;
using MonoMod.RuntimeDetour;
using MonoMod;

namespace Knives
{
	class punt : AdvancedGunBehaviour
	{
		public static void Add()
		{
			Gun gun = ETGMod.Databases.Items.NewGun("Half Gauge Shotgun", "half_gauge");
			Game.Items.Rename("outdated_gun_mods:half_gauge_shotgun", "ski:half_gauge_shotgun");
			gun.gameObject.AddComponent<punt>();
			gun.SetShortDescription("Screw that general direction!");
			gun.SetLongDescription("Invented by McGee Larvey Oddballed this gun was designed to aid the vision impared in their duck hunting adventures. " +
				"While the idea was good at heart McGee failed to comprehend the full weight of his actions until his sight damaged brother shot out the entire side of his barn. " +
				"But at least he hit something... " +
				"\n" +
				"The knockback of this gun alone is enough to rip it clean out of your hands upon firing" +
				"" +
				"\n\n- Knife_to_a_gunfight");
			gun.SetupSprite(null, "half_gauge_idle_001", 1);
			gun.SetAnimationFPS(gun.shootAnimation, 16);
			gun.SetAnimationFPS( gun.reloadAnimation, 14);
			gun.SetAnimationFPS(gun.idleAnimation, 1);
			
			System.Random angle = new System.Random();
			for (int i = 0; i <= 100; i++)
			{
				gun.AddProjectileModuleFrom(PickupObjectDatabase.GetById(15) as Gun, true, false);
				gun.gunSwitchGroup = (PickupObjectDatabase.GetById(15) as Gun).gunSwitchGroup;

			}
			foreach (ProjectileModule projectileModule in gun.Volley.projectiles)
			{
				projectileModule.ammoCost = 1;
				projectileModule.shootStyle = ProjectileModule.ShootStyle.Automatic;
				projectileModule.sequenceStyle = ProjectileModule.ProjectileSequenceStyle.Random;
				projectileModule.cooldownTime = 1f;
				int angleint = angle.Next(1, 40);
				projectileModule.angleVariance = angleint;
				projectileModule.numberOfShotsInClip = 1;
				Projectile projectile = UnityEngine.Object.Instantiate<Projectile>(projectileModule.projectiles[0]);
				projectile.gameObject.SetActive(false);
				projectileModule.projectiles[0] = projectile;
				projectile.baseData.range = 20f;
				projectile.baseData.speed = angleint + 20;
				projectile.baseData.damage = 2f;
				projectile.AdditionalScaleMultiplier = .3f;
				FakePrefab.MarkAsFakePrefab(projectile.gameObject);
				UnityEngine.Object.DontDestroyOnLoad(projectile);
				projectile.AppliesKnockbackToPlayer = true;
				projectile.PlayerKnockbackForce = 3f;
				gun.DefaultModule.ammoType = GameUIAmmoType.AmmoType.BLUE_SHOTGUN;
				gun.DefaultModule.projectiles[0] = projectile;
				bool flag = projectileModule != gun.DefaultModule;
				gun.PreventNormalFireAudio = true;
			}
			
			gun.PreventNormalFireAudio = true;
			gun.reloadTime = 1.5f;
			gun.DefaultModule.cooldownTime = .20f;
		
			gun.SetBaseMaxAmmo(3000);
			gun.CurrentAmmo = 3000;
			gun.muzzleFlashEffects = null;
			gun.quality = PickupObject.ItemQuality.B;
			gun.encounterTrackable.EncounterGuid = "I can hit the broad side of the barn";
			ETGMod.Databases.Items.Add(gun, null, "ANY");


		}
        protected override void OnPickedUpByPlayer(PlayerController player)
        {
			this.gun.HasBeenPickedUp = true;
			
            base.OnPickedUpByPlayer(player);
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
			}
		}

		public override void PostProcessProjectile(Projectile projectile)
		{


		}

		public override void OnFinishAttack(PlayerController player, Gun gun)
		{
			AkSoundEngine.PostEvent("Play_half_gauge_fire", base.gameObject);

			bool footing = player.PlayerHasActiveSynergy("Iron Grip");

			if (footing == false)
			{
				StartCoroutine(DelayedDrop(player, gun));
			}
			base.OnFinishAttack(player, gun);
		}

		public IEnumerator DelayedDrop(PlayerController player, Gun gun)
        {
			yield return new WaitForSeconds(.25f);
			player.ForceDropGun(gun);
        }
		bool HasReloaded;
		public override void OnReloadPressed(PlayerController player, Gun gun, bool bSOMETHING)
		{
			if (gun.IsReloading && this.HasReloaded)
			{

				AkSoundEngine.PostEvent("Stop_WPN_All", base.gameObject);
				base.OnReloadPressed(player, gun, bSOMETHING);

			}

		}



	}
}