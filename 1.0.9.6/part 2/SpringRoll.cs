using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using ItemAPI;


namespace Knives
{
    class Spring_roll :PassiveItem
    {


        public static void Register()
        {
            //The name of the item
            string itemName = "Spring Roll";

            //Refers to an embedded png in the project. Make sure to embed your resources! Google it
            string resourceName = "Knives/Resources/Spring_roll";

            //Create new GameObject
            GameObject obj = new GameObject(itemName);

            //Add a PassiveItem component to the object
            var item = obj.AddComponent<Spring_roll>();

            //Adds a sprite component to the object and adds your texture to the item sprite collection
            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);

            //Ammonomicon entry variables
            string shortDesc = "Awful";
            string longDesc = "This roll contains a literal spring. A coil of metal wraps around the seaweed wrap squeazing the roll tighter. \n\n" +
            "The crunchy taste is, unsuprisingly, an awful experience. At least you now have a spring in your step." +
            "\n\n\n - Knife_to_a_Gunfight";

            //Adds the item to the gungeon item list, the ammonomicon, the loot table, etc.
            //Do this after ItemBuilder.AddSpriteToObject!
            ItemBuilder.SetupItem(item, shortDesc, longDesc, "ski");

            //Adds the actual passive effect to the item

            ItemBuilder.AddPassiveStatModifier(item, PlayerStats.StatType.DodgeRollSpeedMultiplier, .25f, StatModifier.ModifyMethod.ADDITIVE);
            ItemBuilder.AddPassiveStatModifier(item, PlayerStats.StatType.MovementSpeed, 1f, StatModifier.ModifyMethod.ADDITIVE);

            //Set the rarity of the item
            item.quality = PickupObject.ItemQuality.C;


        }


    }






}
