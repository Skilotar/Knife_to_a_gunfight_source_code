using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;
using ItemAPI;

namespace Knives
{
    class Old_Computer :PassiveItem
    {
        public static void Register()
        {
            string itemName = "Old Computer"; //The name of the item
            string resourceName = "Knives/Resources/solitaire"; //Refers to an embedded png in the project. Make sure to embed your resources!

            GameObject obj = new GameObject();
            var item = obj.AddComponent<Super_fly>();
            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);

            string shortDesc = "A series of tubes!";
            string longDesc = "A computer that seems to predate even the robot's base technology. It works like a charm... if you consider being stuck on only being able to open solitaire working like a charm.\n\n";
            ;

            ItemBuilder.SetupItem(item, shortDesc, longDesc, "ski");

            item.quality = PickupObject.ItemQuality.D;

        }

        public override void Pickup(PlayerController player)
        {
            base.Pickup(player);
        }

        protected override void Update()
        {
            base.Update();
            fall();
        }


        public void fall()
        {
            if( this.Owner.IsDodgeRolling == true)
            {
                AkSoundEngine.PostEvent("Play_WPN_radgun_noice_01", base.gameObject);
            }
        }
       
      
    
    }
}
