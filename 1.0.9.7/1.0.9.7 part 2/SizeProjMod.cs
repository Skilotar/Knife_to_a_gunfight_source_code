﻿using System;
using ItemAPI;
using UnityEngine;

namespace Knives
{  
	public class EnemySizeDamageScaleModifier : MonoBehaviour
	{
		
		public EnemySizeDamageScaleModifier()
		{


		}

	
		private void Start()
		{
			this.Projectile = base.GetComponent<Projectile>();
			this.parentOwner = this.Projectile.ProjectilePlayerOwner();
		}

		
		private void Update()
		{
			if (Projectile != null && !this.hasbeenupdated)
			{
				Upgradeprojectile(this.Projectile);
			}
		}

		public void Upgradeprojectile(Projectile projectile)
		{
			if (projectile != null)
			{

				projectile.OnHitEnemy = (Action<Projectile, SpeculativeRigidbody, bool>)Delegate.Combine(projectile.OnHitEnemy, new Action<Projectile, SpeculativeRigidbody, bool>(OnHitEnemy));
				this.hasbeenupdated = true;
			}
		}

		public void OnHitEnemy(Projectile proj, SpeculativeRigidbody body, bool yes)
		{
            if (!this.hasbeenBuffed)
            {
				float X = body.HitboxPixelCollider.Dimensions.x;

				float Y = body.HitboxPixelCollider.Dimensions.y;
				float A = X * Y;

				float percent = A / 275; // 275 is the area of a bulletkin's hitbox setting them as a standard
				
				if(percent > .5) //size based scaling off of the area of the hitbox
                {
					Projectile.baseData.damage *= percent;
				}
                else // reduction for smaller enemies only goes to 50%
                {
					Projectile.baseData.damage *= .5f;
				}
				
				hasbeenBuffed = true;
				beamComp.ForceChargeTimer(0);
			}
			
			

		}


		public bool hasbeenBuffed = false;

		public bool hasbeenupdated = false;
		
		private Projectile Projectile;

		
		private PlayerController parentOwner;

		public BasicBeamController beamComp;

		public Gun gun;
	}
}
