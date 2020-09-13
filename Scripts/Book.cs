using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using ItemAPI;
using MonoMod;
using Gungeon;

namespace Knives
{ 
    class book : OnDamagedPassiveItem
    {

        public static void Register()
        {
            
            string itemName = "Book of book";

           
            string resourceName = "Knives/Resources/book";

            
            GameObject obj = new GameObject(itemName);

            
            var item = obj.AddComponent<book>();

            
            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);

            //Ammonomicon entry variables
            string shortDesc = "Book";
            string longDesc = "An colection of encyclopedias compressed with a powerjack into a singular book. Its heavy, thats all its good for... its not like your gonna take the time to read it anyway. Maybe it can block some bullets. ";

            //Adds the item to the gungeon item list, the ammonomicon, the loot table, etc.
            //Do this after ItemBuilder.AddSpriteToObject!
            ItemBuilder.SetupItem(item, shortDesc, longDesc, "ski");

            //Adds the actual passive effect to the item
            ItemBuilder.AddPassiveStatModifier(item, PlayerStats.StatType.MovementSpeed, -1f, StatModifier.ModifyMethod.ADDITIVE);
            ItemBuilder.AddPassiveStatModifier(item, PlayerStats.StatType.DodgeRollDistanceMultiplier, -.1f, StatModifier.ModifyMethod.ADDITIVE);
            ItemBuilder.AddPassiveStatModifier(item, PlayerStats.StatType.DodgeRollSpeedMultiplier, -.2f, StatModifier.ModifyMethod.ADDITIVE);
            item.ArmorToGainOnInitialPickup = 8;
            item.CanBeDropped = false;
            
            item.IgnoredByRat = true;
            

            
            item.quality = PickupObject.ItemQuality.B;


        }
       

    }
}

