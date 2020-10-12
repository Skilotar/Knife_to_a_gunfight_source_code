using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using ItemAPI;

namespace Knives
{ 
    class OceanMan :PassiveItem
    {

        public static void Register()
        {

            string itemName = "Ocean Man";


            string resourceName = "Knives/Resources/ocean_man";


            GameObject obj = new GameObject(itemName);


            var item = obj.AddComponent<OceanMan>();


            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);

            //Ammonomicon entry variables
            string shortDesc = "Take me by the hand";
            string longDesc = "The planet of the gungeon is filled with bloody waters and ugly sealife. You remember the old days of a land you understand.";

            //Adds the item to the gungeon item list, the ammonomicon, the loot table, etc.
            //Do this after ItemBuilder.AddSpriteToObject!
            ItemBuilder.SetupItem(item, shortDesc, longDesc, "ski");

            //Adds the actual passive effect to the item






            item.quality = PickupObject.ItemQuality.B;


        }
        protected override void Update()
        {
           // if(this.Owner.CenterPosition == WaterTile.
            base.Update();

        }

        //this attempts to fire a rocket projectile currently broken
       



      
       
    }
}

