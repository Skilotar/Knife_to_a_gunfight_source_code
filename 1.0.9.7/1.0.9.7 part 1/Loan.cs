using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using ItemAPI;
using MultiplayerBasicExample;

namespace Knives
{
    class loan : PassiveItem
    {

        public static void Register()
        {
            string itemName = "Devilish Loan Note";

            string resourceName = "Knives/Resources/loan";

            GameObject obj = new GameObject(itemName);

            var item = obj.AddComponent<loan>();

            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);

            //Ammonomicon entry variables
            string shortDesc = "Already payed off";
            string longDesc = "A sinister looking bank note signed with your own blood. The deal was simple, money now, soul later.\n\nWithout a soul the gundead no longer see you as human and you will not gain from dropped casings.\n\n\n - Knife_to_a_Gunfight";

            //Adds the item to the gungeon item list, the ammonomicon, the loot table, etc.
            //Do this after ItemBuilder.AddSpriteToObject!
            ItemBuilder.SetupItem(item, shortDesc, longDesc, "ski");

            //Adds the actual passive effect to the item


            ItemBuilder.AddPassiveStatModifier(item, PlayerStats.StatType.Curse, 2f, StatModifier.ModifyMethod.ADDITIVE);
            ItemBuilder.AddPassiveStatModifier(item, PlayerStats.StatType.MoneyMultiplierFromEnemies, -667f, StatModifier.ModifyMethod.ADDITIVE);
            item.CanBeDropped = false;

            item.IgnoredByRat = true;


            item.quality = PickupObject.ItemQuality.A;


        }
        public override void Pickup(PlayerController player)
        {
            player.GiveItem("50_casing");
            player.GiveItem("50_casing");
            player.GiveItem("50_casing");
            player.GiveItem("50_casing");
            player.GiveItem("50_casing");
            player.GiveItem("50_casing");
            player.GiveItem("50_casing");
            player.GiveItem("50_casing");
            player.GiveItem("50_casing");
            player.GiveItem("50_casing");
            player.GiveItem("50_casing");
            player.GiveItem("50_casing");
            player.GiveItem("50_casing");
            player.GiveItem("50_casing");
            player.GiveItem("50_casing");
            player.GiveItem("50_casing");
            player.GiveItem("50_casing");
            player.GiveItem("50_casing");
            player.GiveItem("50_casing");
            player.GiveItem("50_casing");
            player.GiveItem("50_casing");
            player.GiveItem("50_casing");
            player.GiveItem("50_casing");
            
            

            base.Pickup(player);
            
        }
        public int max_case = 1300;
        protected override void Update()
        {   //currency limiter
            base.Update();
            if(this.Owner != null)
            {
                if (this.Owner.carriedConsumables.Currency > max_case)
                {
                    this.Owner.carriedConsumables.Currency = max_case;
                }

                if (this.Owner.carriedConsumables.Currency < max_case)
                {
                    max_case = this.Owner.carriedConsumables.Currency;
                }
            }
           
        }
    }
}
