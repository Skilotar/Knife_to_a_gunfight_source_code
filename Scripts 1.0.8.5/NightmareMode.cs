using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HutongGames.PlayMaker.Actions;
using ItemAPI;
using Dungeonator;
using UnityEngine;

namespace Knives
{
    class nightmare_mode :PassiveItem
    {

        //Call this method from the Start() method of your ETGModule extension
        public static void Register()
        {
            //The name of the item
            string itemName = "Nightmare mode";

            //Refers to an embedded png in the project. Make sure to embed your resources! Google it
            string resourceName = "Knives/Resources/nightmare_mode";

            //Create new GameObject
            GameObject obj = new GameObject(itemName);

            //Add a PassiveItem component to the object
            var item = obj.AddComponent<nightmare_mode>();

            //Adds a sprite component to the object and adds your texture to the item sprite collection
            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);

            //Ammonomicon entry variables
            string shortDesc = "Nightmare!Nightmare!NIGHTMARE!";
            string longDesc = "Why would you pick this up?! Its literally the worst! Its cursed to heck and back and now that you can read this guess what you can't remove it!\n\n" +
                "Anyways uh... heres some damage up. You'll be needing it where you're going!" +
                "\n\n\n - Knife_to_a_Gunfight";

            //Adds the item to the gungeon item list, the ammonomicon, the loot table, etc.
            //Do this after ItemBuilder.AddSpriteToObject!
            ItemBuilder.SetupItem(item, shortDesc, longDesc, "ski");

            //Adds the actual passive effect to the item


            ItemBuilder.AddPassiveStatModifier(item, PlayerStats.StatType.Curse, 40f, StatModifier.ModifyMethod.ADDITIVE);

            ItemBuilder.AddPassiveStatModifier(item, PlayerStats.StatType.Damage, 4f, StatModifier.ModifyMethod.ADDITIVE);


            //Set the rarity of the item
           
            item.quality = PickupObject.ItemQuality.D;
            item.CanBeDropped = false;
           
        }
        //trying to make the code spawn extra jammed lords
        public override void Pickup(PlayerController player)
        {
            player.OnEnteredCombat = (Action)Delegate.Combine(player.OnEnteredCombat, new Action(this.SpawnGrim));





            base.Pickup(player);
        }
       
        
        private void SpawnGrim()
        {
           
            string guid;
            if (this.Owner)
            {
                guid = "0d3f7c641557426fbac8596b61c9fb45";
            }
            else
            {
                guid = "0d3f7c641557426fbac8596b61c9fb45";
            }
            PlayerController owner = base.Owner;
            AIActor orLoadByGuid = EnemyDatabase.GetOrLoadByGuid(guid);
            IntVector2? intVector = new IntVector2?(base.Owner.CurrentRoom.GetRandomVisibleClearSpot(2, 2));
            AIActor aiactor = AIActor.Spawn(orLoadByGuid.aiActor, intVector.Value, GameManager.Instance.Dungeon.data.GetAbsoluteRoomFromPosition(intVector.Value), true, AIActor.AwakenAnimationType.Default, true);
            aiactor.CanTargetEnemies = false;
            aiactor.CanTargetPlayers = true;
            PhysicsEngine.Instance.RegisterOverlappingGhostCollisionExceptions(aiactor.specRigidbody, null, false);
            aiactor.gameObject.AddComponent<KillOnRoomClear>();
            aiactor.IsHarmlessEnemy = false;
            aiactor.IgnoreForRoomClear = true;
            aiactor.MovementSpeed = .5f;
            
            aiactor.reinforceType = AIActor.ReinforceType.Instant;
            aiactor.HandleReinforcementFallIntoRoom(0f);
        }
        protected override void Update()
        {
            RoomHandler currentRoom = this.Owner.CurrentRoom;
            foreach (AIActor aiactor in currentRoom.GetActiveEnemies(RoomHandler.ActiveEnemyType.All))
            { if (aiactor.EnemyGuid == "0d3f7c641557426fbac8596b61c9fb45")
                {
                    aiactor.MovementSpeed = .5f;
                  
                }
            }
                base.Update();
        }
    }
 }
        
