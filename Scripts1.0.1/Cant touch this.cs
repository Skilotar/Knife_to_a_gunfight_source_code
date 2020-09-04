using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using ItemAPI;

namespace Knives
{
    class Cant_touch_this : PassiveItem
    {
        public static void Register()
        {
            string itemName = "Can't touch this";

            string resourceName = "Knives/Resources/cant_touch_this";

            GameObject obj = new GameObject(itemName);

            var item = obj.AddComponent<Cant_touch_this>();

            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);

            //Ammonomicon entry variables
            string shortDesc = "Hammer time";
            string longDesc = "You randomly become so cool nothing wants to hurt you.";

            //Adds the item to the gungeon item list, the ammonomicon, the loot table, etc.
            //Do this after ItemBuilder.AddSpriteToObject!
            ItemBuilder.SetupItem(item, shortDesc, longDesc, "ski");

            //Adds the actual passive effect to the item


            

           


            item.quality = PickupObject.ItemQuality.C;
            

        }
        
        public override void Pickup(PlayerController player)
        {
            base.Pickup(player);
            
        }
        public int counter = 0;
        public bool currentlyEtherial = false;
        protected override void Update()
        {
            base.Update();
            if (this.Owner)
            {
                groovy(this.Owner);
            }
           
        }
        private System.Random rng = new System.Random();
        // groovy uses counter as a basic frame/update based timer 20 updates = 1 second
        protected void groovy(PlayerController player)
        {
            int etherial = rng.Next(1, 500);
            if (etherial == 1 && !this.currentlyEtherial)
            {
                player.IsEthereal = true;
                AkSoundEngine.PostEvent("Play_WPN_radgun_cool_01", base.gameObject);
                currentlyEtherial = true;
                StartVFX(player);
                
            }
            if (currentlyEtherial && counter <= 60)
            {
                counter++;
            }
            if (currentlyEtherial && counter >= 60)
            {
                this.Owner.IsEthereal = false;
                currentlyEtherial = false;
                counter = 0;
                StopVFX(player);
            }
        }
        private void StartVFX(PlayerController user)
        {
            Material outline = SpriteOutlineManager.GetOutlineMaterial(user.sprite);
            outline.SetColor("_OverrideColor", new Color(63f, 236f, 165f));
        }

       
        private void StopVFX(PlayerController user)
        {
            Material outline = SpriteOutlineManager.GetOutlineMaterial(user.sprite);
            outline.SetColor("_OverrideColor", new Color(0f, 0f, 0f));
        }
    }
}

