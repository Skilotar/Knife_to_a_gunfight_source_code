using System;
using ItemAPI;
using UnityEngine;

namespace Knives
{   //this code is by Nevernamed
	// Token: 0x02000192 RID: 402
	public class PlayerVelocityBuffProjectileModifier : MonoBehaviour
	{
		// Token: 0x0600086B RID: 2155 RVA: 0x0006760C File Offset: 0x0006580C
		public PlayerVelocityBuffProjectileModifier()
		{
			
			
		}

		// Token: 0x0600086C RID: 2156 RVA: 0x0006765B File Offset: 0x0006585B
		private void Start()
		{
			this.Projectile = base.GetComponent<Projectile>();
			this.parentOwner = this.Projectile.ProjectilePlayerOwner();
		}

		// Token: 0x0600086D RID: 2157 RVA: 0x0006767C File Offset: 0x0006587C
		private void Update()
		{
			if(Projectile != null && !this.hasbeenupdated)
            {
				Upgradeprojectile(this.Projectile);
            }
		}

		public void Upgradeprojectile(Projectile projectile)
        {
			if(projectile != null)
            {
				//ETGModConsole.Log("\n");
				//ETGModConsole.Log(this.parentOwner.Velocity.magnitude.ToString());
				
				projectile.Speed *= 1 + ((float)this.parentOwner.Velocity.magnitude);
				projectile.UpdateSpeed();
				projectile.baseData.damage *= 1 + ((float)this.parentOwner.Velocity.magnitude / 3.5f); ;
				
				this.hasbeenupdated = true;
			}
        }


		public bool hasbeenupdated = false;
		// Token: 0x040004C7 RID: 1223
		private Projectile Projectile;

		// Token: 0x040004C8 RID: 1224
		private PlayerController parentOwner;

		public float ProjSpeedMult;

		public float ProjDamageMult;

		public float ProjSizeMult;
	}
}
