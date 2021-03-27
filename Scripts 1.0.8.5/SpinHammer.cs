﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Gungeon;
using ItemAPI;

namespace Knives
{
    class SpinHammer : GunBehaviour
    {
        public static void Add()
        {
            // Get yourself a new gun "base" first.
            // Let's just call it "Basic Gun", and use "jpxfrd" for all sprites and as "codename" All sprites must begin with the same word as the codename. For example, your firing sprite would be named "jpxfrd_fire_001".
            Gun gun = ETGMod.Databases.Items.NewGun("Gyro Hammer", "gyro");
            // "kp:basic_gun determines how you spawn in your gun through the console. You can change this command to whatever you want, as long as it follows the "name:itemname" template.
            Game.Items.Rename("outdated_gun_mods:gyro_hammer", "ski:gyro_hammer");
            gun.gameObject.AddComponent<SpinHammer>();
            //These two lines determines the description of your gun, ".SetShortDescription" being the description that appears when you pick up the gun and ".SetLongDescription" being the description in the Ammonomicon entry. 
            gun.SetShortDescription("Don't Let Go");
            gun.SetLongDescription("This hammer is equiped with gyro functions that always keep its user on their feet. But be warry the gyroscope is old and goes haywire when the aim point is close to the player. The closer you aim the faster you spin!");
            // This is required, unless you want to use the sprites of the base gun.
            // That, by default, is the pea shooter.
            // SetupSprite sets up the default gun sprite for the ammonomicon and the "gun get" popup.
            // WARNING: Add a copy of your default sprite to Ammonomicon Encounter Icon Collection!
            // That means, "sprites/Ammonomicon Encounter Icon Collection/defaultsprite.png" in your mod .zip. You can see an example of this with inside the mod folder.
            gun.SetupSprite(null, "gyro_idle_001", 8);
            // ETGMod automatically checks which animations are available.
            // The numbers next to "shootAnimation" determine the animation fps. You can also tweak the animation fps of the reload animation and idle animation using this method.
            gun.SetAnimationFPS(gun.shootAnimation, 24);
            // Every modded gun has base projectile it works with that is borrowed from other guns in the game. 
            // The gun names are the names from the JSON dump! While most are the same, some guns named completely different things. If you need help finding gun names, ask a modder on the Gungeon discord.
            gun.AddProjectileModuleFrom("Ak-47", true, true);

            // Here we just take the default projectile module and change its settings how we want it to be.
            gun.DefaultModule.ammoCost = 1;
            gun.DefaultModule.angleVariance = 0f;


            gun.DefaultModule.shootStyle = ProjectileModule.ShootStyle.Automatic;
            gun.DefaultModule.sequenceStyle = ProjectileModule.ProjectileSequenceStyle.Random;

            
            gun.DefaultModule.cooldownTime = 0f;
            gun.DefaultModule.numberOfShotsInClip = int.MaxValue;

            gun.InfiniteAmmo = true;
            // Here we just set the quality of the gun and the "EncounterGuid", which is used by Gungeon to identify the gun.
            gun.quality = PickupObject.ItemQuality.A;
            gun.encounterTrackable.EncounterGuid = "SPEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEN";
            //This block of code helps clone our projectile. Basically it makes it so things like Shadow Clone and Hip Holster keep the stats/sprite of your custom gun's projectiles.
            Projectile projectile = UnityEngine.Object.Instantiate<Projectile>(gun.DefaultModule.projectiles[0]);
            projectile.gameObject.SetActive(false);
            FakePrefab.MarkAsFakePrefab(projectile.gameObject);
            UnityEngine.Object.DontDestroyOnLoad(projectile);
            gun.DefaultModule.projectiles[0] = projectile;
            projectile.baseData.force = 7;
            projectile.baseData.damage = 3f;
            projectile.baseData.speed *= 1f;
            projectile.baseData.range = .01f;
            
            
            projectile.transform.parent = gun.barrelOffset;
            gun.barrelOffset.transform.localPosition = new Vector3(2.75f,0f, 0f);

            ETGMod.Databases.Items.Add(gun, null, "ANY");

        }
        public float distance = 2.75f;
        public int counter = 0;
        public override void OnPostFired(PlayerController player, Gun gun)
        {
            gun.PreventNormalFireAudio = true;
            if (counter == 0)
            {
                gun.barrelOffset.transform.localPosition = new Vector3(distance, 1f, 0f);
            }
            if (counter == 1)
            {
                gun.barrelOffset.transform.localPosition = new Vector3(distance, .5f, 0f);
            }
            if (counter == 2)
            {
                gun.barrelOffset.transform.localPosition = new Vector3(distance, 0f, 0f);
            }
            if (counter == 3)
            {
                gun.barrelOffset.transform.localPosition = new Vector3(distance, -.5f, 0f);
            }
            if (counter == 4)
            {
                gun.barrelOffset.transform.localPosition = new Vector3(distance, -1f, 0f);
                counter = 0;
            }
            counter++;
        }
        public override void OnFinishAttack(PlayerController player, Gun gun)
        {
            this.gun.barrelOffset.transform.localPosition = new Vector3(.3f, .3f, 0f);
            counter = 0;
            base.OnFinishAttack(player, gun);
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

            }
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

