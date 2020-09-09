using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using ItemAPI;
using Gungeon;

using HutongGames.PlayMaker.Actions;

namespace Knives
{ 
    public class Dragun_force : PlayerItem
    {
        public static void Register()
        {
            //The name of the item
            string itemName = "Led Maiden";

            //Refers to an embedded png in the project. Make sure to embed your resources! Google it
            string resourceName = "Knives/Resources/Dragunforce";

            //Create new GameObject
            GameObject obj = new GameObject(itemName);

            //Add a PassiveItem component to the object
            var item = obj.AddComponent<Dragun_force>();

            //Adds a sprite component to the object and adds your texture to the item sprite collection
            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);

            //Ammonomicon entry variables
            string shortDesc = "Fire and flames";
            string longDesc =

                "A song created by the dragun itself to help get it in the mood for burning literaly EVERYTHING it sees";

            //Adds the item to the gungeon item list, the ammonomicon, the loot table, etc.
            //Do this after ItemBuilder.AddSpriteToObject!
            ItemBuilder.SetupItem(item, shortDesc, longDesc, "ski");

            //Adds the actual passive effect to the item
            //PlayerController owner = item.LastOwner as PlayerController;
            ItemBuilder.SetCooldownType(item, ItemBuilder.CooldownType.Damage, 450f);


            //Set the rarity of the item

            item.consumable = false;
            item.quality = PickupObject.ItemQuality.A;
        }
        protected override void DoEffect(PlayerController user)
        {

            
            float dura = 20f;
            StartCoroutine(ItemBuilder.HandleDuration(this, dura, user, EndEffect));


        }


        protected void EndEffect(PlayerController user)
        {
           

        }

    }






}

