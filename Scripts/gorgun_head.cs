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
    class gorgun_head : PlayerItem
    {
        public static void Register()
        {
            //The name of the item
            string itemName = "gorgun's head";

            //Refers to an embedded png in the project. Make sure to embed your resources! Google it
            string resourceName = "Knives/Resources/gorgun's_head";

            //Create new GameObject
            GameObject obj = new GameObject(itemName);

            //Add a PassiveItem component to the object
            var item = obj.AddComponent<gorgun_head>();

            //Adds a sprite component to the object and adds your texture to the item sprite collection
            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);

            //Ammonomicon entry variables
            string shortDesc = "An Icey stare";
            string longDesc =

                "Much of its power has left it and a foul stench lingers on it.\n\n" +
                "Though its life has left it a chill still runs up your spine when you look in its eyes. ";

            //Adds the item to the gungeon item list, the ammonomicon, the loot table, etc.
            //Do this after ItemBuilder.AddSpriteToObject!
            ItemBuilder.SetupItem(item, shortDesc, longDesc, "ski");

            //Adds the actual passive effect to the item
            //PlayerController owner = item.LastOwner as PlayerController;

            ItemBuilder.AddPassiveStatModifier(item, PlayerStats.StatType.Curse, 1f, StatModifier.ModifyMethod.ADDITIVE);
            ItemBuilder.SetCooldownType(item, ItemBuilder.CooldownType.Damage, 400f);
            //Set the rarity of the item


            
            
            item.quality = PickupObject.ItemQuality.B;
        }
        protected override void DoEffect(PlayerController user)
        {
            
        }

        protected void EndEffect(PlayerController user)
        {

        }


    }
}
