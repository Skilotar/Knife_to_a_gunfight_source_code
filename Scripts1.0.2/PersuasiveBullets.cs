using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using ItemAPI;

namespace Knives
{
    class persuasive_bullets :PassiveItem
    {
        public static void Register()
        {
            //The name of the item
            string itemName = "99 blobuloons";

            //Refers to an embedded png in the project. Make sure to embed your resources! Google it
            string resourceName = "Knives/Resources/99_blob_balloons";

            //Create new GameObject
            GameObject obj = new GameObject(itemName);

            //Add a PassiveItem component to the object
            var item = obj.AddComponent<Luft_balloons>();

            //Adds a sprite component to the object and adds your texture to the item sprite collection
            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);

            //Ammonomicon entry variables
            string shortDesc = "Just following orders";
            string longDesc =

                "These bullets are adorned with a beautiful captain's hat. The enemies of the gungeon are all well aquainted with miliary heirarchy and are happy to do what they are told ";                

            //Adds the item to the gungeon item list, the ammonomicon, the loot table, etc.
            //Do this after ItemBuilder.AddSpriteToObject!
            ItemBuilder.SetupItem(item, shortDesc, longDesc, "ski");

            //Adds the actual passive effect to the item
            //PlayerController owner = item.LastOwner as PlayerController;
            


            //Set the rarity of the item

            item.consumable = false;
            item.quality = PickupObject.ItemQuality.A;
        }
        public override void Pickup(PlayerController player)
        {
            player.PostProcessProjectile += this.PostProcessProjectile;
            base.Pickup(player);
        }
        private System.Random rng = new System.Random();
        private void PostProcessProjectile(Projectile SourceProjectile, float chance)
        {
            int rpg = rng.Next(1, 10);
            if (rpg == 1)
            {
                Projectile projectile = ((Gun)ETGMod.Databases.Items[0]).DefaultModule.projectiles[0];
                Vector3 vector = this.Owner.unadjustedAimPoint - this.Owner.LockedApproximateSpriteCenter;
                Vector3 vector2 = this.Owner.specRigidbody.UnitCenter;
                GameObject gameObject = SpawnManager.SpawnProjectile(projectile.gameObject, this.Owner.sprite.WorldCenter, Quaternion.Euler(0f, 0f, (this.Owner.CurrentGun == null) ? 0f : this.Owner.CurrentGun.CurrentAngle), true);
                Projectile component = gameObject.GetComponent<Projectile>();
                bool flag = component != null;
                if (flag)
                {
                    component.Owner = this.Owner;
                    component.Shooter = this.Owner.specRigidbody;
                }
            }
            
            }
        }

    }

