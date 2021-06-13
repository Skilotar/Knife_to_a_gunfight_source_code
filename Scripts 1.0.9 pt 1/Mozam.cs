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
	class Mozam : AdvancedGunBehaviour
	{
		public static void Add()
		{
			Gun gun = ETGMod.Databases.Items.NewGun("Mozambique", "Mozam");
			Game.Items.Rename("outdated_gun_mods:mozambique", "ski:mozambique");
			gun.gameObject.AddComponent<Mozam>();
			gun.SetShortDescription("Good enough");
			gun.SetLongDescription("One to the head and two to the chest, a solid shotgun pistol. What it lacks in raw power it makes up for in adaptability. " +
				"Sporting a multitude of hop-ups and combos with other munitions. That's enough briefing cadet. Oscar Mike ladies!\n\n- Knife_to_a_gunfight");
			gun.SetupSprite(null, "Mozam_idle_001", 1);
			GunExt.SetAnimationFPS(gun, gun.shootAnimation, 12);
			GunExt.SetAnimationFPS(gun, gun.reloadAnimation, 14);
			GunExt.SetAnimationFPS(gun, gun.idleAnimation, 1);
			for (int i = 0; i < 3; i++)
			{
				gun.AddProjectileModuleFrom(PickupObjectDatabase.GetById(157) as Gun, true, false);
				gun.gunSwitchGroup = (PickupObjectDatabase.GetById(157) as Gun).gunSwitchGroup;

			}
			foreach (ProjectileModule projectileModule in gun.Volley.projectiles)
			{
				projectileModule.ammoCost = 1;
				projectileModule.shootStyle = ProjectileModule.ShootStyle.Automatic;
				projectileModule.sequenceStyle = ProjectileModule.ProjectileSequenceStyle.Random;
				projectileModule.cooldownTime = .4f;
				projectileModule.angleVariance = 6f;
				projectileModule.numberOfShotsInClip = 4;
				Projectile projectile = UnityEngine.Object.Instantiate<Projectile>(projectileModule.projectiles[0]);
				projectile.gameObject.SetActive(false);
				projectileModule.projectiles[0] = projectile;
				projectile.baseData.damage = 3f;
				projectile.AdditionalScaleMultiplier = .5f;
				FakePrefab.MarkAsFakePrefab(projectile.gameObject);
				UnityEngine.Object.DontDestroyOnLoad(projectile);
				gun.DefaultModule.ammoType = GameUIAmmoType.AmmoType.BLUE_SHOTGUN;
				gun.DefaultModule.projectiles[0] = projectile;
				bool flag = projectileModule != gun.DefaultModule;
				
			}
			gun.reloadTime = 1.5f;
			gun.SetBaseMaxAmmo(800);
			gun.muzzleFlashEffects = (PickupObjectDatabase.GetById(157) as Gun).muzzleFlashEffects;
			gun.quality = PickupObject.ItemQuality.D;
			gun.encounterTrackable.EncounterGuid = "Three buckshot on a single trigger pull.";
			ETGMod.Databases.Items.Add(gun, null, "ANY");
			

		}

		public System.Random rand = new System.Random();
		public override void PostProcessProjectile(Projectile projectile)
		{
			PlayerController owner = this.gun.CurrentOwner as PlayerController;
			//hammerpoint
			bool hammer = owner.PlayerHasActiveSynergy("Hop-up: Hammer Point Rounds");

			if (hammer)
			{
				projectile.baseData.damage = 15f;
			}
			else
			{
				projectile.baseData.damage = 3f;
				
			}

			bool shatter = owner.PlayerHasActiveSynergy("Hop-up: Shattering Tier Lists");

			if (shatter)
			{
				int rng = rand.Next(1, 20);
				if(rng == 1)
                {
					Projectile shatterer = ((Gun)ETGMod.Databases.Items[157]).DefaultModule.projectiles[0];
					GameObject gameObject = SpawnManager.SpawnProjectile(shatterer.gameObject, owner.specRigidbody.UnitCenter, Quaternion.Euler(0f, 0f, (owner.CurrentGun == null) ? 0f : owner.CurrentGun.CurrentAngle));
					Projectile component = gameObject.GetComponent<Projectile>();
					component.Owner = owner;
					component.baseData.damage = 9999f;
					component.ignoreDamageCaps = true;
					

					
				}
			}
			

		}

		
        public override void OnReload(PlayerController player, Gun gun)
        {
            base.OnReload(player, gun);
       
       
			PlayerController owner = this.gun.CurrentOwner as PlayerController;
			bool throws = owner.PlayerHasActiveSynergy("Hop-up: Throw Away Joke");
			if (throws)
			{
				if(this.gun.ClipShotsRemaining == 0)
                {
					player.CurrentGun.MoveBulletsIntoClip(1);
					GameObject gameObject = SpawnManager.SpawnProjectile("ThrownGunProjectile", owner.specRigidbody.UnitCenter, Quaternion.Euler(0f, 0f, (owner.CurrentGun == null) ? 0f : owner.CurrentGun.CurrentAngle));
					Projectile component = gameObject.GetComponent<Projectile>();
					component.Owner = owner;
					
					
					component.angularVelocity = 50;
					
				}
				
			}
		}

		protected override void Update()
		{
			base.Update();
			PlayerController owner = this.gun.CurrentOwner as PlayerController;
			//April Fools
			bool fools = owner.PlayerHasActiveSynergy("Hop-up: April Fools");

			if (fools)
			{
				foreach (ProjectileModule projectile in gun.Volley.projectiles)
				{
					projectile.numberOfShotsInClip = 9;
					projectile.cooldownTime = .15f;
					
				}

				gun.SetBaseMaxAmmo(2000);
			}
			else
			{
				foreach (ProjectileModule projectile in gun.Volley.projectiles)
				{
					projectile.numberOfShotsInClip = 4;
					projectile.cooldownTime = .4f;
					
				}

			}

			


		}


		private void OnDestroy()
		{
			
		}
		
	}
}





