using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using ItemAPI;

namespace Knives
{
    class smooth_criminal : PassiveItem
    {
        public static void Register()
        {
            string itemName = "Smooth Criminal"; //The name of the item
            string resourceName = "Knives/Resources/smooth_criminal"; //Refers to an embedded png in the project. Make sure to embed your resources!

            GameObject obj = new GameObject();
            var item = obj.AddComponent<Super_fly>();
            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);

            string shortDesc = "Hit By! Struck By!";
            string longDesc = "A Smooth Criminal!\n\n" +
            "Nothing fills you with more joy than a successful heist.";
               

            ItemBuilder.SetupItem(item, shortDesc, longDesc, "ski");

            item.quality = PickupObject.ItemQuality.B;

        }

        public override void Pickup(PlayerController player)
        {
            base.Pickup(player);
        }

        protected override void Update()
        {
            base.Update();
            stealing();
        }
        public int robberpoints = 0;
        public void stealing()
        {   
            if (this.Owner.IsThief == true)
            {
                robberpoints++;
                this.Owner.IsThief = false;
                ItemBuilder.AddPassiveStatModifier(this, PlayerStats.StatType.Damage, robberpoints*.15f , StatModifier.ModifyMethod.ADDITIVE);
                this.Owner.stats.RecalculateStats(Owner, true);
            }
        }
    }
}
