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
            ItemBuilder.SetCooldownType(item, ItemBuilder.CooldownType.Timed, 20f);
            //Adds the actual passive effect to the item

            //Set the rarity of the item
            item.quality = PickupObject.ItemQuality.C;
            item.numberOfUses = 1;
            item.UsesNumberOfUsesBeforeCooldown = true;

        }
        public int go = 1;
        public float fail = 0;
        protected override void DoEffect(PlayerController user)
        {
            go = 1;
            fail = 0;
            StartCoroutine(ItemBuilder.HandleDuration(this, 45f, user, EndEffect));
            Start();
        }
        protected override void Start()
        {
          if (go == 1)
            {
                AkSoundEngine.PostEvent("Play_ENM_shells_gather_01", base.gameObject);
                if (this.LastOwner.IsDodgeRolling == true)
                {
                    fail = 1;
                    AkSoundEngine.PostEvent("Play_OBJ_metronome_fail_01", base.gameObject);
                }
                if( this.LastOwner.IsCheezen == true)
                {
                    fail = 1;
                    AkSoundEngine.PostEvent("Play_OBJ_metronome_fail_01", base.gameObject);
                }
                if (this.LastOwner.IsOnFire == true)
                {
                    fail = 1;
                    AkSoundEngine.PostEvent("Play_OBJ_metronome_fail_01", base.gameObject);
                }
                if (this.LastOwner.CurrentPoisonMeterValue > 0)
                {
                    fail = 1;
                    AkSoundEngine.PostEvent("Play_OBJ_metronome_fail_01", base.gameObject);
                }
                Start();
            }
            
        }
        private void EndEffect(PlayerController user)
        {
            go = 0;
            if (fail != 1)
            {
                user.healthHaver.ApplyHealing(1.5f);
            }
        }

    }
}

