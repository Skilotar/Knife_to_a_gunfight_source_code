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
    public class Dizzyring :PassiveItem
    { 
        public static void Register()
        {
            string itemName = "Dizzy Ring";

            string resourceName = "Knives/Resources/dizzy_ring";

            GameObject obj = new GameObject(itemName);
            
            var item = obj.AddComponent<Dizzyring>();

            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);

            //Ammonomicon entry variables
            string shortDesc = "Disorienting";
            string longDesc = "This ring was once owned by the Rollin Ronin, a student of Sir Manuel that went mad with rolling power! and also dizziness... mostly dizziness.\n\n" +
                "Feel how this ring becons you to tumble, but be warned excessive rolling can lead to head issues.";

            //Adds the item to the gungeon item list, the ammonomicon, the loot table, etc.
            //Do this after ItemBuilder.AddSpriteToObject!
            ItemBuilder.SetupItem(item, shortDesc, longDesc, "ski");

            //Adds the actual passive effect to the item
            ItemBuilder.AddPassiveStatModifier(item, PlayerStats.StatType.DodgeRollDistanceMultiplier, .6f, StatModifier.ModifyMethod.ADDITIVE);
            ItemBuilder.AddPassiveStatModifier(item, PlayerStats.StatType.DodgeRollSpeedMultiplier, .2f, StatModifier.ModifyMethod.ADDITIVE);
            ItemBuilder.AddPassiveStatModifier(item, PlayerStats.StatType.DodgeRollDamage, 50f, StatModifier.ModifyMethod.ADDITIVE);
           
            ItemBuilder.AddPassiveStatModifier(item, PlayerStats.StatType.ReloadSpeed, 1.75f, StatModifier.ModifyMethod.ADDITIVE);
            
           
            item.quality = PickupObject.ItemQuality.B;

           
        }

       
     }
 }

        
