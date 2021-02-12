using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using ItemAPI;
using MonoMod;
using Gungeon;
using System.Runtime;

namespace Knives
{
    class bandaids : PlayerItem
    { //every time i try it crashes T-T
        public static void Register()
        {
            //The name of the item
            string itemName = "nonstick bandaids";

            //Refers to an embedded png in the project. Make sure to embed your resources! Google it
            string resourceName = "Knives/Resources/bandaids";

            //Create new GameObject
            GameObject obj = new GameObject(itemName);

            //Add a PassiveItem component to the object
            var item = obj.AddComponent<bandaids>();

            //Adds a sprite component to the object and adds your texture to the item sprite collection
            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);

            //Ammonomicon entry variables
            string shortDesc = "Questionably useful";
            string longDesc = "Originally designed to be a no-pain solution for taking off bandaids. As one might assume a bandaid who's sole purpose is to come off easily didn't sell well.\n\n" +
                "Fortunaly for your bullethole ridden self they are still in fact bandaids so they will help heal you if you can manage to keep them stuck to you.\n\n" +
                "For optimal healing avoid sudden movements, taking damage, dangerous liquids , and last but most obviously... dying.";

            //Adds the item to the gungeon item list, the ammonomicon, the loot table, etc.
            //Do this after ItemBuilder.AddSpriteToObject!
            ItemBuilder.SetupItem(item, shortDesc, longDesc, "ski");
            ItemBuilder.SetCooldownType(item, ItemBuilder.CooldownType.PerRoom, 8f);
            //Adds the actual passive effect to the item

            //Set the rarity of the item
            item.quality = PickupObject.ItemQuality.C;
            item.numberOfUses = 1;
            item.UsesNumberOfUsesBeforeCooldown = true;

        }
        bool toggle = false;
        bool hasFailed = false;
        int RoomsTillEnd = 4;
        protected override void DoEffect(PlayerController user)
        {
            RoomsTillEnd = 4;
            user.OnPreDodgeRoll += OnPreDodgeRoll;
            user.OnReceivedDamage += OnRecivedDamage;
            user.OnEnteredCombat += OnEnteredCombat;
            hasFailed = false;
            toggle = true;
        }
        private void OnEnteredCombat()
        {
            RoomsTillEnd--;
        }
        private void OnRecivedDamage(PlayerController user)
        {
            this.fail();
            toggle = false;
        }
        private void OnPreDodgeRoll(PlayerController player)
        {
            this.fail();
            toggle = false;

        }

        public override void Update()
        {
            if (toggle)
            {
                if (this.LastOwner.IsOnFire)
                {
                    this.fail();
                    toggle = false;

                }

                //if (this.LastOwner.CurrentPoisonMeterValue > 0)
                {
                  //this.fail();
                    //toggle = false;

                }

                if(RoomsTillEnd == 0)
                {
                    endingEffect(this.LastOwner);
                    RoomsTillEnd = 4;
                }
            }
            base.Update();
        }

        public void fail()
        {
            hasFailed = true;
        }

        public void endingEffect(PlayerController user)
        {
            toggle = false;
            user.OnPreDodgeRoll -= OnPreDodgeRoll;
            user.OnReceivedDamage -= OnRecivedDamage;
            user.OnEnteredCombat -= OnEnteredCombat;
            if (!hasFailed)
            {
                user.healthHaver.ApplyHealing(.5f);

            }
            else
            {
                // ha ha 
            }
        }
    }
}
