using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using ItemAPI;
using MonoMod;
using Gungeon;
using System.Reflection;
using HutongGames.PlayMaker.Actions;

namespace Knives
{
    class Long_roll_boots : PassiveItem
    {

        public static void Register()
        {
            string itemName = "Long Roll Boots";

            string resourceName = "Knives/Resources/long_roll_boots";

            GameObject obj = new GameObject(itemName);

            var item = obj.AddComponent<Long_roll_boots>();

            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);

            //Ammonomicon entry variables
            string shortDesc = "*beewwww*";
            string longDesc = "An apeture science prototype for the long fall boots. This early model attempted to negate falling injuries by slowing the timestream of the person jumping. \n\n Turns out slowing time doesnt slow momentum. The subjects still splatted... just really slowly.\n\n" +
                "Oh well all in the name of science I guess. The boots are sticky from prior testing, but try not to let that get to you. Good luck and happy rolling.";

            //Adds the item to the gungeon item list, the ammonomicon, the loot table, etc.
            //Do this after ItemBuilder.AddSpriteToObject!
            ItemBuilder.SetupItem(item, shortDesc, longDesc, "ski");

            //Adds the actual passive effect to the item
           
            ItemBuilder.AddPassiveStatModifier(item, PlayerStats.StatType.DodgeRollDistanceMultiplier, 1.5f, StatModifier.ModifyMethod.ADDITIVE);
            ItemBuilder.AddPassiveStatModifier(item, PlayerStats.StatType.DodgeRollSpeedMultiplier, -.5f, StatModifier.ModifyMethod.ADDITIVE);
            ItemBuilder.AddPassiveStatModifier(item, PlayerStats.StatType.MovementSpeed, 1f, StatModifier.ModifyMethod.ADDITIVE);



            item.quality = PickupObject.ItemQuality.C;
            item.RespawnsIfPitfall = true;
            item.IgnoredByRat = true;

        }
        
    }
}
