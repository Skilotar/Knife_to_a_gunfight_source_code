using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gungeon;
using UnityEngine;
using ItemAPI;
using HutongGames.PlayMaker.Actions;
using MultiplayerBasicExample;
using MonoMod.Utils;
using MonoMod;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Knives
{
    public class vodoo_kit : PlayerItem
    {
        //Call this method from the Start() method of your ETGModule extension
        public static void Register()
        {
            //The name of the item
            string itemName = "Grateful dead";

            //Refers to an embedded png in the project. Make sure to embed your resources! Google it
            string resourceName = "Knives/Resources/reanimation_kit";

            //Create new GameObject
            GameObject obj = new GameObject(itemName);

            //Add a PassiveItem component to the object
            var item = obj.AddComponent<vodoo_kit>();

            //Adds a sprite component to the object and adds your texture to the item sprite collection
            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);

            //Ammonomicon entry variables
            string shortDesc = "Your job here isn't quite over";
            string longDesc =

                "A simple reanimation kit, created by a couple of failure medical students that could only \"Heal\" things by smashing a fairy in a bottle over the patients head.\n" +
                "They used this to cheat on operation exams as most of their patients would die. This kit can be used to reanimate any corpses left on an open battlefield into a much ghastlier version. " +
                "These regundead are grateful for your aid and will attack enemies of the room, however they are not entirely harmless." +
                "\n\n\n - Knife_to_a_Gunfight";

            //Adds the item to the gungeon item list, the ammonomicon, the loot table, etc.
            //Do this after ItemBuilder.AddSpriteToObject!
            ItemBuilder.SetupItem(item, shortDesc, longDesc, "ski");

            //Adds the actual passive effect to the item
            //PlayerController owner = item.LastOwner as PlayerController;
            ItemBuilder.SetCooldownType(item, ItemBuilder.CooldownType.Damage, 200f);


            //Set the rarity of the item

            item.consumable = false;
            item.quality = PickupObject.ItemQuality.B;
        }

       
        protected override void DoEffect(PlayerController user)
        {
            rezroom(this.LastOwner);
        }

       
        private System.Random rng = new System.Random();
        public void rezroom(PlayerController player)
        {   
            
            for (int i = 0; i < StaticReferenceManager.AllCorpses.Count; i++)
            {
                GameObject gameObject = StaticReferenceManager.AllCorpses[i];
                if (gameObject && gameObject.GetComponent<tk2dBaseSprite>() && gameObject.transform.position.GetAbsoluteRoom() == player.CurrentRoom)
                {
                    int spawntype = rng.Next(1, 5);
                    if(spawntype == 1)
                    {   //tombstoner
                        string guid;
                        if (player)
                        {
                            guid = "cf27dd464a504a428d87a8b2560ad40a";
                        }
                        else
                        {
                            guid = "cf27dd464a504a428d87a8b2560ad40a";
                        }

                        AIActor orLoadByGuid = EnemyDatabase.GetOrLoadByGuid(guid);
                        IntVector2? intVector = new IntVector2?(player.CurrentRoom.GetRandomVisibleClearSpot(2, 2));
                        AIActor aiactor = AIActor.Spawn(orLoadByGuid.aiActor, intVector.Value, GameManager.Instance.Dungeon.data.GetAbsoluteRoomFromPosition(intVector.Value), true, AIActor.AwakenAnimationType.Default, true);
                        aiactor.CanTargetEnemies = true;
                        aiactor.CanTargetPlayers = false;
                        PhysicsEngine.Instance.RegisterOverlappingGhostCollisionExceptions(aiactor.specRigidbody, null, false);
                        aiactor.gameObject.AddComponent<KillOnRoomClear>();
                        aiactor.IsHarmlessEnemy = true;
                        aiactor.CompanionOwner = player;
                        aiactor.CanDropCurrency = false;
                        aiactor.IsNormalEnemy = true;

                        aiactor.isPassable = true;
                        aiactor.IgnoreForRoomClear = true;
                        aiactor.reinforceType = AIActor.ReinforceType.Instant;
                        aiactor.HandleReinforcementFallIntoRoom(0f);
                    }
                    if (spawntype == 2)
                    {    //skullmet
                        string guid;
                        if (player)
                        {
                            guid = "95ec774b5a75467a9ab05fa230c0c143";
                        }
                        else
                        {
                            guid = "95ec774b5a75467a9ab05fa230c0c143";
                        }

                        AIActor orLoadByGuid = EnemyDatabase.GetOrLoadByGuid(guid);
                        IntVector2? intVector = new IntVector2?(player.CurrentRoom.GetRandomVisibleClearSpot(2, 2));
                        AIActor aiactor = AIActor.Spawn(orLoadByGuid.aiActor, intVector.Value, GameManager.Instance.Dungeon.data.GetAbsoluteRoomFromPosition(intVector.Value), true, AIActor.AwakenAnimationType.Default, true);
                        aiactor.CanTargetEnemies = true;
                        aiactor.CanTargetPlayers = false;
                        PhysicsEngine.Instance.RegisterOverlappingGhostCollisionExceptions(aiactor.specRigidbody, null, false);
                        aiactor.gameObject.AddComponent<KillOnRoomClear>();
                        aiactor.IsHarmlessEnemy = true;
                        aiactor.CompanionOwner = player;
                        aiactor.CanDropCurrency = false;
                        aiactor.IsNormalEnemy = true;

                        aiactor.isPassable = true;
                        aiactor.IgnoreForRoomClear = true;
                        aiactor.reinforceType = AIActor.ReinforceType.Instant;
                        aiactor.HandleReinforcementFallIntoRoom(0f);
                    }
                    if (spawntype == 3)
                    {   //skullet
                        string guid;
                        if (player)
                        {
                            guid = "336190e29e8a4f75ab7486595b700d4a";
                        }
                        else
                        {
                            guid = "336190e29e8a4f75ab7486595b700d4a";
                        }

                        AIActor orLoadByGuid = EnemyDatabase.GetOrLoadByGuid(guid);
                        IntVector2? intVector = new IntVector2?(player.CurrentRoom.GetRandomVisibleClearSpot(2, 2));
                        AIActor aiactor = AIActor.Spawn(orLoadByGuid.aiActor, intVector.Value, GameManager.Instance.Dungeon.data.GetAbsoluteRoomFromPosition(intVector.Value), true, AIActor.AwakenAnimationType.Default, true);
                        aiactor.CanTargetEnemies = true;
                        aiactor.CanTargetPlayers = false;
                        PhysicsEngine.Instance.RegisterOverlappingGhostCollisionExceptions(aiactor.specRigidbody, null, false);
                        aiactor.gameObject.AddComponent<KillOnRoomClear>();
                        aiactor.IsHarmlessEnemy = true;
                        aiactor.CompanionOwner = player;
                        aiactor.CanDropCurrency = false;
                        aiactor.IsNormalEnemy = true;

                        aiactor.isPassable = true;
                        aiactor.IgnoreForRoomClear = true;
                        aiactor.reinforceType = AIActor.ReinforceType.Instant;
                        aiactor.HandleReinforcementFallIntoRoom(0f);
                    }
                    if (spawntype == 4)
                    {    //skulet
                        string guid;
                        if (player)
                        {
                            guid = "336190e29e8a4f75ab7486595b700d4a";
                        }
                        else
                        {
                            guid = "336190e29e8a4f75ab7486595b700d4a";
                        }

                        AIActor orLoadByGuid = EnemyDatabase.GetOrLoadByGuid(guid);
                        IntVector2? intVector = new IntVector2?(player.CurrentRoom.GetRandomVisibleClearSpot(2, 2));
                        AIActor aiactor = AIActor.Spawn(orLoadByGuid.aiActor, intVector.Value, GameManager.Instance.Dungeon.data.GetAbsoluteRoomFromPosition(intVector.Value), true, AIActor.AwakenAnimationType.Default, true);
                        aiactor.CanTargetEnemies = true;
                        aiactor.CanTargetPlayers = false;
                        PhysicsEngine.Instance.RegisterOverlappingGhostCollisionExceptions(aiactor.specRigidbody, null, false);
                        aiactor.gameObject.AddComponent<KillOnRoomClear>();
                        aiactor.IsHarmlessEnemy = true;
                        aiactor.CompanionOwner = player;
                        aiactor.CanDropCurrency = false;
                        aiactor.IsNormalEnemy = true;

                        aiactor.isPassable = true;
                        aiactor.IgnoreForRoomClear = true;
                        aiactor.reinforceType = AIActor.ReinforceType.Instant;
                        aiactor.HandleReinforcementFallIntoRoom(0f);
                    }
                    if (spawntype == 5)
                    {   //hollowpoint
                        string guid;
                        if (player)
                        {
                            guid = "4db03291a12144d69fe940d5a01de376";
                        }
                        else
                        {
                            guid = "4db03291a12144d69fe940d5a01de376";
                        }

                        AIActor orLoadByGuid = EnemyDatabase.GetOrLoadByGuid(guid);
                        IntVector2? intVector = new IntVector2?(player.CurrentRoom.GetRandomVisibleClearSpot(2, 2));
                        AIActor aiactor = AIActor.Spawn(orLoadByGuid.aiActor, intVector.Value, GameManager.Instance.Dungeon.data.GetAbsoluteRoomFromPosition(intVector.Value), true, AIActor.AwakenAnimationType.Default, true);
                        aiactor.CanTargetEnemies = true;
                        aiactor.CanTargetPlayers = false;
                        PhysicsEngine.Instance.RegisterOverlappingGhostCollisionExceptions(aiactor.specRigidbody, null, false);
                        aiactor.gameObject.AddComponent<KillOnRoomClear>();
                        aiactor.IsHarmlessEnemy = true;
                        aiactor.CompanionOwner = player;
                        aiactor.CanDropCurrency = false;
                        aiactor.IsNormalEnemy = true;

                        aiactor.isPassable = true;
                        aiactor.IgnoreForRoomClear = true;
                        aiactor.reinforceType = AIActor.ReinforceType.Instant;
                        aiactor.HandleReinforcementFallIntoRoom(0f);
                    }

                }

            }
        }
           
       
       

    }
}
