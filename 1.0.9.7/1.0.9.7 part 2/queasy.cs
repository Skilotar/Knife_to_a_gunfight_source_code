using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using UnityEngine;
using ItemAPI;

namespace Knives
{
    class Queasy : PassiveItem
    {

        public static void Register()
        {
            //The name of the item
            string itemName = "Queasy Reload Bullets";

            //Refers to an embedded png in the project. Make sure to embed your resources! Google it
            string resourceName = "Knives/Resources/queasy-reload-bullets";

            //Create new GameObject
            GameObject obj = new GameObject(itemName);

            //Add a PassiveItem component to the object
            var item = obj.AddComponent<Queasy>();

            //Adds a sprite component to the object and adds your texture to the item sprite collection
            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);

            //Ammonomicon entry variables
            string shortDesc = "Rollin rollin rollin";
            string longDesc =
                "These bullets are made for reloading more effectivly however they require many more rolls than their later counterparts, this results in the user being quite nausious afterwards." +
                "\n\n\n - Knife_to_a_Gunfight";

            //Adds the item to the gungeon item list, the ammonomicon, the loot table, etc.
            //Do this after ItemBuilder.AddSpriteToObject!
            ItemBuilder.SetupItem(item, shortDesc, longDesc, "ski");

           

            item.quality = PickupObject.ItemQuality.C;
        }

        public override void Pickup(PlayerController player)
        {
            player.OnPreDodgeRoll += this.OnPreDodgeRoll;
            base.Pickup(player);
        }

        public System.Random rand = new System.Random();
        public bool cancelToggle = false;
        private void OnPreDodgeRoll(PlayerController player)
        {
            if(this.Owner.CurrentGun == PickupObjectDatabase.GetByEncounterName("TaurenTails") as Gun)
            {
                int randomizer = rand.Next(1, 30);
                if (randomizer == 1)
                {
                    if (player.CurrentGun.ClipCapacity != player.CurrentGun.ClipShotsRemaining && !player.CurrentGun.IsReloading)
                    {
                        AkSoundEngine.PostEvent("Play_ITM_Crisis_Stone_Reload_01", base.gameObject);
                        player.CurrentGun.MoveBulletsIntoClip(player.CurrentGun.ClipCapacity - player.CurrentGun.ClipShotsRemaining);

                    }

                }
            }
            else
            {
                int randomizer = rand.Next(1, 10);
                if (randomizer == 1)
                {
                    if (player.CurrentGun.ClipCapacity != player.CurrentGun.ClipShotsRemaining && !player.CurrentGun.IsReloading)
                    {
                        AkSoundEngine.PostEvent("Play_ITM_Crisis_Stone_Reload_01", base.gameObject);
                        player.CurrentGun.MoveBulletsIntoClip(player.CurrentGun.ClipCapacity - player.CurrentGun.ClipShotsRemaining);

                    }

                }
            }
        }

    
    

        public override DebrisObject Drop(PlayerController player)
        {
            player.OnPreDodgeRoll -= this.OnPreDodgeRoll;
            return base.Drop(player);
        }

        protected override void Update()
        {
           
            base.Update();
        }
       

    }
}
