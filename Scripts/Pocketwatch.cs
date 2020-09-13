using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using ItemAPI;

namespace Knives
{
    class pocketwatch :PassiveItem
    {
        
        public static void Register()
        {
            //The name of the item
            string itemName = "Sundial Pocketwatch";

            //Refers to an embedded png in the project. Make sure to embed your resources! Google it
            string resourceName = "Knives/Resources/sundial pocketwatch";

            //Create new GameObject
            GameObject obj = new GameObject(itemName);

            //Add a PassiveItem component to the object
            var item = obj.AddComponent<pocketwatch>();

            //Adds a sprite component to the object and adds your texture to the item sprite collection
            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);

            //Ammonomicon entry variables
            string shortDesc = "It is aproximatly 12 0'clock";
            string longDesc = "These pocketwatches were all the rage on the long forgotten western floor of the gungeon.\n\n" +
                "A small amount of sepia dust still remains on the watch";

            //Adds the item to the gungeon item list, the ammonomicon, the loot table, etc.
            //Do this after ItemBuilder.AddSpriteToObject!
            ItemBuilder.SetupItem(item, shortDesc, longDesc, "ski");

            //Adds the actual passive effect to the item


            ItemBuilder.AddPassiveStatModifier(item, PlayerStats.StatType.Accuracy, -.8f, StatModifier.ModifyMethod.ADDITIVE);
            ItemBuilder.AddPassiveStatModifier(item, PlayerStats.StatType.Damage, .1f, StatModifier.ModifyMethod.ADDITIVE);



            //Set the rarity of the item
            item.quality = PickupObject.ItemQuality.B;

        }

        public override void Pickup(PlayerController player)
        {
            base.Pickup(player);
            RadialSlowInterface test = new RadialSlowInterface();
            test.RadialSlowHoldTime = 9999999999999f;
            test.RadialSlowOutTime = 1f;
            test.RadialSlowTimeModifier = 1f;
            test.DoesSepia = true;
            
            test.RadialSlowInTime = 4f;
            test.DoRadialSlow(player.CenterPosition, player.CurrentRoom);
            
            
           
        }

        
    }
}

