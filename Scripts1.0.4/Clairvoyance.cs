using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using Gungeon;
using Dungeonator;
using UnityEngine;
using ItemAPI;
using System.Reflection;
using MultiplayerBasicExample;

namespace Knives
{ 
    class Clairvoyance : PassiveItem
    {

        //Call this method from the Start() method of your ETGModule extension
        public static void Register()
        {
            //The name of the item
            string itemName = "Clairvoyance";

            //Refers to an embedded png in the project. Make sure to embed your resources! Google it
            string resourceName = "Knives/Resources/Clairvoyance";

            //Create new GameObject
            GameObject obj = new GameObject(itemName);

            //Add a PassiveItem component to the object
            var item = obj.AddComponent<Clairvoyance>();

            //Adds a sprite component to the object and adds your texture to the item sprite collection
            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);

            //Ammonomicon entry variables
            string shortDesc = "The Next thing you'll say is... ";
            string longDesc = "Your destiny pulls you towards the correct outcome. You can feel what is coming through devine wisdom ";

            //Adds the item to the gungeon item list, the ammonomicon, the loot table, etc.
            //Do this after ItemBuilder.AddSpriteToObject!
            ItemBuilder.SetupItem(item, shortDesc, longDesc, "ski");

            //Adds the actual passive effect to the item
            //ItemBuilder.SetCooldownType(item, ItemBuilder.CooldownType.None, 1f);
            

            //Set the rarity of the item
            item.AddToSubShop(ItemBuilder.ShopType.OldRed, .01f);
            item.quality = PickupObject.ItemQuality.C;
           
        }


        public override void Pickup(PlayerController player)
        {
            base.Pickup(player);
            ETGModConsole.Log(bossname);
            bossname = null;
        }

        string bossname = null;
        public static void Helper()
        {
            string bossname = null;

            GameManager ted = new GameManager();
            if (ted.IsLoadingLevel)
            {
                if (bossname == null)
                {
                    bossname = BossManager.PriorFloorSelectedBossRoom.name;
                    ETGModConsole.Log(bossname);
                    bossname = null;
                }
                else
                {
                    Helper();
                }
            }
            else
            {
                Helper();
            }
          
        }


        




    }

}