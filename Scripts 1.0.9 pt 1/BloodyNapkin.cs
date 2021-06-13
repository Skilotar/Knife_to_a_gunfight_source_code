using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using ItemAPI;
using MultiplayerBasicExample;
using System.Xml.Schema;

namespace Knives
{ 
    class BloodyNapkin :PlayerItem
    {
        public static void Register()
        {
            //The name of the item
            string itemName = "Bloody Napkin";

            //Refers to an embedded png in the project. Make sure to embed your resources! Google it
            string resourceName = "Knives/Resources/Bloody_napkin";

            //Create new GameObject
            GameObject obj = new GameObject(itemName);

            //Add a PassiveItem component to the object
            var item = obj.AddComponent<BloodyNapkin>();

            //Adds a sprite component to the object and adds your texture to the item sprite collection
            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);

            //Ammonomicon entry variables
            string shortDesc = "Ecnalubma";
            string longDesc = "Well its a good thing that I brought this napkin. I knew that this would happen. Why does this always happen?!\n\n" +
                "Help me out I can't seem to get this window open. Nevermind now its open. I think my hand is broken!\n\n" +
                "Look at that now you're all patched up there. Almost like it never happened. Almost like what never happened??\n\n" +
                "Exactly.\n\n" +
                "______________________________\n\n" +
                "Freezes you, Fires you back in time, resets health. You should find somewhere to hide before using this... " +
                "\n\n\n - Knife_to_a_Gunfight";

           
            ItemBuilder.SetupItem(item, shortDesc, longDesc, "ski");

            //Adds the actual passive effect to the item


            ItemBuilder.SetCooldownType(item, ItemBuilder.CooldownType.Damage, 800f);
           
            //Set the rarity of the item
            
            item.quality = PickupObject.ItemQuality.C;

        }
        public override void Pickup(PlayerController player)
        {
            player.OnEnteredCombat = (Action)Delegate.Combine(player.OnEnteredCombat, new Action(this.detectHealth));


            base.Pickup(player);
        }
        public float twoBack = 0;
        public float lastRoom = 0;
        public float thisRoom = 0;
        public void detectHealth()
        {
           
            twoBack = lastRoom;
            lastRoom = thisRoom;
            thisRoom = this.LastOwner.healthHaver.GetCurrentHealth();

        }
        public void Healuser(PlayerController player)
        {
            if (twoBack == 0)
            {
                if(lastRoom == thisRoom)
                {
                    player.healthHaver.ApplyHealing(1);
                }
                
                if (lastRoom > thisRoom)
                {
                    float reset = lastRoom - thisRoom;
                    player.healthHaver.ApplyHealing(reset);
                }
            }
            else
            {
                if (twoBack > thisRoom)
                {
                    float reset = twoBack - thisRoom;
                    player.healthHaver.ApplyHealing(reset);
                }

            }
           
        }
        protected override void DoEffect(PlayerController user)
        {   //freezes player, makes invincible, spawns gripmaster and heals.
            float dura = 2.5f;
            user.MovementModifiers += NoMotionModifier;
            user.IsStationary = true;
            SpawnParamedic(user);
            StartCoroutine(ItemBuilder.HandleDuration(this, dura, user, EndEffect));

        }
       

        public void SpawnParamedic(PlayerController player)
        {
            string guid;
            if (player)
            {
                guid = "22fc2c2c45fb47cf9fb5f7b043a70122";
            }
            else
            {
                guid = "22fc2c2c45fb47cf9fb5f7b043a70122";
            }
            
            AIActor orLoadByGuid = EnemyDatabase.GetOrLoadByGuid(guid);
            IntVector2? intVector = new IntVector2?(player.CurrentRoom.GetRandomVisibleClearSpot(2, 2));
            AIActor aiactor = AIActor.Spawn(orLoadByGuid.aiActor, player.AimCenter, GameManager.Instance.Dungeon.data.GetAbsoluteRoomFromPosition(intVector.Value), true, AIActor.AwakenAnimationType.Spawn, true);
            aiactor.CanTargetEnemies = false;
            aiactor.CanTargetPlayers = true;
            PhysicsEngine.Instance.RegisterOverlappingGhostCollisionExceptions(aiactor.specRigidbody, null, false);
            aiactor.LocalTimeScale = 40;
            aiactor.IsHarmlessEnemy = false;
            aiactor.IgnoreForRoomClear = true;
            aiactor.reinforceType = AIActor.ReinforceType.Instant;
            aiactor.HandleReinforcementFallIntoRoom(0f);
            
        }

        public override void Update()
        {
           

            base.Update();
        }

        private void NoMotionModifier(ref Vector2 voluntaryVel, ref Vector2 involuntaryVel)
        {
            voluntaryVel = Vector2.zero;
        }
        
        
        protected void EndEffect(PlayerController user)
        {
            Healuser(user);
            
            
            user.MovementModifiers -= NoMotionModifier;
            user.IsStationary = false;
        }
    }
}
