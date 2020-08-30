using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using ItemAPI;
using MonoMod;
using Gungeon;

namespace Knives
{
    class Empty_Collection :PlayerItem
    {
        
            public static void Register()
            {
            //The name of the item
            string itemName = "Empty Record Collection";

            //Refers to an embedded png in the project. Make sure to embed your resources! Google it
            string resourceName = "Knives/Resources/Empty_record_collection";

            //Create new GameObject
            GameObject obj = new GameObject(itemName);

            //Add a PassiveItem component to the object
            var item = obj.AddComponent<Empty_Collection>();

            //Adds a sprite component to the object and adds your texture to the item sprite collection
            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);

            //Ammonomicon entry variables
            string shortDesc = "It's Empty";
            string longDesc = "It's far lighter now but its got nothing in it...\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n" +
            "Or is it? Maybe you should take another look.";

            //Adds the item to the gungeon item list, the ammonomicon, the loot table, etc.
            //Do this after ItemBuilder.AddSpriteToObject!
            ItemBuilder.SetupItem(item, shortDesc, longDesc, "ski");

            //Adds the actual passive effect to the item
                //PlayerController owner = item.LastOwner as PlayerController;



            //Set the rarity of the item
            item.AddToSubShop(ItemBuilder.ShopType.Trorc, .01f);
            item.consumable = true;
            item.quality = PickupObject.ItemQuality.D;
            }
            //gives the user a little hope
            protected override void DoEffect(PlayerController user)
            {

            AkSoundEngine.PostEvent("Play_WPN_gun_empty_01", base.gameObject);
                
            user.GiveItem("ski:fly_friend");

            }
            
    }
}








