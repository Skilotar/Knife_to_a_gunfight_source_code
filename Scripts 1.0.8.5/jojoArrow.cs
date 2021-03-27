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
    class jojo_arrow :PlayerItem   
    {
        public static void Register()
        {
            string itemName = "Arrow of Destiny";

            string resourceName = "Knives/Resources/Arrow_of_the_chosen";

            GameObject obj = new GameObject(itemName);

            var item = obj.AddComponent<jojo_arrow>();

            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);

            //Ammonomicon entry variables
            string shortDesc = "Your trial awaits";
            string longDesc =

                "This arrow graints great power to those of strong will or solid destiny. A sacrifice of unknown magnitude must be given to test the user.\n\n" +
                "If the user survives they will be granted great power" +
                "\n\n\n - Knife_to_a_Gunfight";

            //Adds the item to the gungeon item list, the ammonomicon, the loot table, etc.
            //Do this after ItemBuilder.AddSpriteToObject!
            ItemBuilder.SetupItem(item, shortDesc, longDesc, "ski");

            //Adds the actual passive effect to the item
       
            ItemBuilder.AddPassiveStatModifier(item, PlayerStats.StatType.Curse, 2f, StatModifier.ModifyMethod.ADDITIVE);




            item.consumable = true;
            item.quality = PickupObject.ItemQuality.A;
        }
        protected override void DoEffect(PlayerController user)
        {
            System.Random rand = new System.Random();
            int hurt = rand.Next(1, 5);
            var health = user.healthHaver;
            user.GiveItem("ski:fate's_blessing");

            health.ApplyDamage(hurt, Vector2.zero, "Failed Fate's Challenge!", CoreDamageTypes.Magic, DamageCategory.Unstoppable, true);
        }
        protected void EndEffect(PlayerController user)
        {
            
        }

    }
}
