using ItemAPI;
using System;
using System.Collections;
using System.Collections.Generic;
using Gungeon;
using MonoMod;
using Dungeonator;
using UnityEngine;

namespace ExampleMod
{
    public class XaccoGun : GunBehaviour
    {

        public static void Add()
        {

            Gun gun = ETGMod.Databases.Items.NewGun("The Scourgue", "xacco_gun");
            Game.Items.Rename("outdated_gun_mods:the_scourgue", "xacco:the_scourgue");
            gun.gameObject.AddComponent<XaccoGun>();
            gun.SetShortDescription("Abyssal reaper");
            gun.SetLongDescription("The pride of the captain       Emits a lethal light");
            gun.SetupSprite(null, "xacco_gun_idle_001", 8);
            gun.SetAnimationFPS(gun.shootAnimation, 24);

            gun.AddProjectileModuleFrom("ak-47", true, false);
            //I would reccomend replacing the line above with 'gun.AddProjectileModuleFrom(PickupObjectDatabase.GetById(id) as Gun, true, false);'
            //'id' in that code I just gave you should be the numerical Id of the gun you want your projectiles to be based off of.

            gun.DefaultModule.ammoCost = 1;
            gun.DefaultModule.shootStyle = ProjectileModule.ShootStyle.SemiAutomatic;
            gun.DefaultModule.sequenceStyle = ProjectileModule.ProjectileSequenceStyle.Random;
            gun.reloadTime = 1.0f;
            gun.DefaultModule.cooldownTime = 0.1f;
            gun.DefaultModule.numberOfShotsInClip = 14;
            gun.SetBaseMaxAmmo(250);
            gun.barrelOffset.transform.localPosition = new Vector3(1.70f, 0.97f, 1.70f);

            gun.quality = PickupObject.ItemQuality.D;


            Gun gun3 = PickupObjectDatabase.GetById(27) as Gun; //This line isn't doing anything in the code
            Projectile projectile = UnityEngine.Object.Instantiate<Projectile>(gun3.DefaultModule.projectiles[0]);
            projectile.gameObject.SetActive(false);
            FakePrefab.MarkAsFakePrefab(projectile.gameObject);
            UnityEngine.Object.DontDestroyOnLoad(projectile);
            gun.DefaultModule.projectiles[0] = projectile;
            gun.reflectDuringReload = true;
            
            projectile.baseData.damage = 3.57f;
            projectile.baseData.speed *= 4f;

            ETGMod.Databases.Items.Add(gun, null, "ANY");

        }

        public override void OnPostFired(PlayerController player, Gun gun)
        {
            //This determines what sound you want to play when you fire a gun.
            //Sounds names are based on the Gungeon sound dump, which can be found at EnterTheGungeon/Etg_Data/StreamingAssets/Audio/GeneratedSoundBanks/Windows/sfx.txt
            gun.PreventNormalFireAudio = true;
            AkSoundEngine.PostEvent("Play_WPN_blasphemy_shot_01", gameObject);
        }
        private bool HasReloaded;
        //This block of code allows us to change the reload sounds.
        protected void Update()
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

                if (gun.IsReloading)
                {
                    foreach (var projectile in GetBullets())
                    {
                        float radius = 3;
                        Vector2 Standing = this.gun.CurrentOwner.CenterPosition;
                        if (Vector2.Distance(projectile.LastPosition, Standing) <= radius)
                        {
                            PassiveReflectItem.ReflectBullet(projectile, true, this.gun.CurrentOwner, 15f, 1f, 1f, 0f);
                        }
                    }
                }
                
            }
        }

        
        public override void OnAutoReload(PlayerController player, Gun gun)
        {
            AkSoundEngine.PostEvent("Play_WPN_burninghand_shot_01", base.gameObject);
            if (this.gun.CurrentOwner)
            {
               
            }
        }


        private List<Projectile> GetBullets()
        {
            
            List<Projectile> list = new List<Projectile>();
            var allProjectiles = StaticReferenceManager.AllProjectiles;
            for (int i = 0; i < allProjectiles.Count; i++)
            {
                Projectile projectile = allProjectiles[i];
                if (projectile && projectile.sprite && !projectile.ImmuneToBlanks && !projectile.ImmuneToSustainedBlanks)
                {
                    if (projectile.Owner != null)
                    {
                        if (projectile.isFakeBullet || projectile.Owner is AIActor || (projectile.Shooter != null && projectile.Shooter.aiActor != null) || projectile.ForcePlayerBlankable)
                        {
                            
                                list.Add(projectile);
                            
                        }
                    }
                }
            }
            return list;
        }

    }
}
