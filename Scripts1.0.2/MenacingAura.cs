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
    class menacing_aura : PassiveItem
    {
 
        public static void Register()
        {
            //The name of the item
            string itemName = "Menacing Aura";

            //Refers to an embedded png in the project. Make sure to embed your resources! Google it
            string resourceName = "Knives/Resources/menacing_stare";

            //Create new GameObject
            GameObject obj = new GameObject(itemName);

            //Add a PassiveItem component to the object
            var item = obj.AddComponent<Spring_roll>();

            //Adds a sprite component to the object and adds your texture to the item sprite collection
            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);

            //Ammonomicon entry variables
            string shortDesc = "Hes just standing there. MENACINGLY!";
            string longDesc = "WIP\n\n Strike a pose and channel your menacing aura to unnerve all who oppose you. Normally standing still on a battlefeild is a terrible strategy, but it works great if everyone is too afraid to shoot you.";

            //Adds the item to the gungeon item list, the ammonomicon, the loot table, etc.
            //Do this after ItemBuilder.AddSpriteToObject!
            ItemBuilder.SetupItem(item, shortDesc, longDesc, "ski");

            //Adds the actual passive effect to the item



            //Set the rarity of the item
            item.quality = PickupObject.ItemQuality.C;
            

        }


        public override void Pickup(PlayerController player)
        {

            base.Pickup(player);
            if (player.GetComponent<menacingBehaviour>() != null)
            {
                Destroy(player.GetComponent<menacingBehaviour>());
            }
            player.gameObject.AddComponent<menacingBehaviour>().parent = this;
            
        }
        
        public class menacingBehaviour : BraveBehaviour
        {

            public menacing_aura parent;

            public FleePlayerData fleeData;
            public void FixedUpdate(PlayerController user)
            {

                if (user.CurrentInputState == PlayerInputState.NoMovement)
                {
                    HandleFear(user, true);
                }
                else
                {
                    HandleFear(user, false);
                }


                if (fleeData == null || fleeData.Player != user)
                {
                    fleeData = new FleePlayerData();
                    fleeData.Player = user;
                    fleeData.StartDistance *= 2;
                }
               FixedUpdate(user);

            }
            private void HandleFear(PlayerController user, bool active)
            {
                

                RoomHandler room = user.CurrentRoom;
                if (!room.HasActiveEnemies(RoomHandler.ActiveEnemyType.All)) return;

                if (active)
                {
                    foreach (var enemy in room.GetActiveEnemies(RoomHandler.ActiveEnemyType.All))
                    {
                        if (enemy.behaviorSpeculator != null)
                        {
                            enemy.behaviorSpeculator.FleePlayerData = this.fleeData;
                            FleePlayerData fleePlayerData = new FleePlayerData();
                        }
                    }

                }
                else
                {
                    foreach (var enemy in room.GetActiveEnemies(RoomHandler.ActiveEnemyType.All))
                    {
                        if (enemy.behaviorSpeculator != null && enemy.behaviorSpeculator.FleePlayerData != null)
                            enemy.behaviorSpeculator.FleePlayerData.Player = null;
                    }
                }
            }
        }
    }
}
