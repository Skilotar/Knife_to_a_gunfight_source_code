using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using ItemAPI; 

namespace Knives
{
    class sandvich : PlayerItem
    {
       
        public static void Register()
        {
            //The name of the item
            string itemName = "Sandvich";

            //Refers to an embedded png in the project. Make sure to embed your resources! Google it
            string resourceName = "Knives/Resources/sandvich";

            //Create new GameObject
            GameObject obj = new GameObject(itemName);

            //Add a PassiveItem component to the object
            var item = obj.AddComponent<sandvich>();

            //Adds a sprite component to the object and adds your texture to the item sprite collection
            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);

            //Ammonomicon entry variables
            string shortDesc = "Perfect fuel for killing";
            string longDesc = "Stand still and *Om nOm Nom Naugm Nau* \n\n" +
                "";

            //Adds the item to the gungeon item list, the ammonomicon, the loot table, etc.
            //Do this after ItemBuilder.AddSpriteToObject!
            ItemBuilder.SetupItem(item, shortDesc, longDesc, "ski");

            //Adds the actual passive effect to the item
            //PlayerController owner = item.LastOwner as PlayerController;

            ItemBuilder.SetCooldownType(item, ItemBuilder.CooldownType.Damage, 700f);
            
            //Set the rarity of the item
         
            

            item.quality = PickupObject.ItemQuality.A;
            item.numberOfUses = 1;
        }

        //applies damage on last use
        protected override void DoEffect(PlayerController user)
        {
            float dura = 5f;
            

            StartCoroutine(ItemBuilder.HandleDuration(this, dura, user, EndEffect));
            ItemBuilder.AddPassiveStatModifier(this, PlayerStats.StatType.MovementSpeed, -7f, StatModifier.ModifyMethod.ADDITIVE);
            this.LastOwner.stats.RecalculateStats(LastOwner, true);
            user.healthHaver.ApplyHealing(.5f);

        }
        protected void EndEffect(PlayerController user)
        {

            user.healthHaver.ApplyHealing(.5f);
            user.healthHaver.ApplyHealing(.5f);
          
           
           

            ItemBuilder.AddPassiveStatModifier(this, PlayerStats.StatType.MovementSpeed, 7f, StatModifier.ModifyMethod.ADDITIVE);
            this.LastOwner.stats.RecalculateStats(LastOwner, true);
           
        }
    }
}
