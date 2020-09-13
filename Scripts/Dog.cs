using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;
using ItemAPI;

namespace Knives
{
    class dog :PlayerItem
    {
        public static void Register()
        {
            string itemName = "Dog Problem";

            string resourceName = "Knives/Resources/dog";

            GameObject obj = new GameObject(itemName);

            var item = obj.AddComponent<dog>();

            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);

            //Ammonomicon entry variables
            string shortDesc = "You are carrying too many dogs";
            string longDesc = "yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip";

            //Adds the item to the gungeon item list, the ammonomicon, the loot table, etc.
            //Do this after ItemBuilder.AddSpriteToObject!
            ItemBuilder.SetupItem(item, shortDesc, longDesc, "ski");

            ItemBuilder.SetCooldownType(item, ItemBuilder.CooldownType.PerRoom, 3f);



            item.consumable = false;
            item.quality = PickupObject.ItemQuality.S;
        }
        private System.Random rng = new System.Random();
        protected override void DoEffect(PlayerController user)
        {

            float dura = 1f;
            StartCoroutine(ItemBuilder.HandleDuration(this, dura, user, EndEffect));
            int dog = rng.Next(1, 20);
            if (dog == 1)
            {
                user.GiveItem("wolf");
            }
            if (dog != 1)
            {
                user.GiveItem("dog");
            }
            if (hasSynergy)
            {
                if (dog == 1)
                {
                    user.GiveItem("wolf");
                }
                if (dog != 1)
                {
                    user.GiveItem("dog");
                }
            }
        }


        protected void EndEffect(PlayerController user)
        {
           


        }
        private bool hasSynergy = false;
        public override void Update()
        {
            bool flag = this.LastOwner;
            if (flag)
            {
                bool flag2 = this.LastOwner.HasPickupID(PickupObjectDatabase.GetByEncounterName("Turtle Problem").PickupObjectId);
                if (flag2)
                {
                    this.hasSynergy = true;
                }
                else
                {
                    this.hasSynergy = false;
                }


                base.Update();
            }
        }
    }
}
