
 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using UnityEngine;
using ItemAPI;

namespace Knives
{
    class HotelCaliforniaSpecial : PlayerItem
    {
        // Only real use is for Monk
        public static void Register()
        {

            string itemName = "The Hotel California Special";

            string resourceName = "Knives/Resources/SpiceInjection";

            GameObject obj = new GameObject(itemName);

            var item = obj.AddComponent<HotelCaliforniaSpecial>();

            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);

            //Ammonomicon entry variables
            string shortDesc = "Since 1969";
            string longDesc = "The special treat of an infamous hotel; like the gungeon many have entered its depths, but few ever leave. \n\n" +
                "This vial of special hotel secret chemicals was captured by a private eye looking into the mysterious missing persons cases surrounding the hotel. \n\n" +
                "The hotel seemed to pride itself that no one ever wanted to leave once they had tasted the hotel california special. \n\n" +
                "After chemical analysis the vial was found to contain a pure extract of pumpkin spice." +
                "\n\n\n - Knife_to_a_Gunfight";

            //Adds the item to the gungeon item list, the ammonomicon, the loot table, etc.
            //Do this after ItemBuilder.AddSpriteToObject!
            ItemBuilder.SetupItem(item, shortDesc, longDesc, "ski");

            //Adds the actual passive effect to the item
            //PlayerController owner = item.LastOwner as PlayerController;




            // item.AddToSubShop(ItemBuilder.ShopType.Flynt, .01f);
            ItemBuilder.SetCooldownType(item, ItemBuilder.CooldownType.Damage, 300);
            item.UsesNumberOfUsesBeforeCooldown = true;
            item.numberOfUses = 5;
            item.quality = PickupObject.ItemQuality.B;
        }
        public Projectile projectile1 = null;
        public override void Update()
        {
            if(this.LastOwner != null)
            {
                projectile1 = this.LastOwner.CurrentGun.projectile;
            }
           
            base.Update();
        }

        protected override void DoEffect(PlayerController user )
        {
            
            user.StartCoroutine(HandleDash(user, projectile1));

        }

        public IEnumerator HandleDash(PlayerController user, Projectile projectile)
        {
            float duration = .30f;
            float adjSpeed = 30;
            float elapsed = -BraveTime.DeltaTime;
            float angle = user.CurrentGun.CurrentAngle;
            while (elapsed < duration)
            {
                elapsed += BraveTime.DeltaTime;
                this.LastOwner.specRigidbody.Velocity = BraveMathCollege.DegreesToVector(angle).normalized * adjSpeed;
                yield return null;
            }

        }
    }




}