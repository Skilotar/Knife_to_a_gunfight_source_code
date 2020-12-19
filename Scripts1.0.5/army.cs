using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ItemAPI;
using UnityEngine;
using Dungeonator;

namespace Knives
{
    public class SkeletonArmy : PlayerItem
    {
        public static void Register()
        {  // The name of the item
            string itemName = "Book of Necromancy";
            //not my item
            //Refers to an embedded png in the project. Make sure to embed your resources!
            string resourceName = "Knives/Resources/Bloody_napkin";

            //Create new GameObject
            GameObject obj = new GameObject();

            //Add a ActiveItem component to the object
            var item = obj.AddComponent<SkeletonArmy>();

            //Adds a tk2dSprite component to the object and adds your texture to the item sprite collection
            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);

            //Ammonomicon entry variables
            string shortDesc = "Make them run.";
            string longDesc = "Strikes fear into those who come too close.\n\n" +
                "you usually cant get a threatening aura just from an item, but anything is possible in The Gungeon.";

            //Adds the item to the gungeon item list, the ammonomicon, the loot table, etc.
            //"example_pool" here is the item pool. In the console you'd type "give example_pool:sweating_bullets"
            ItemBuilder.SetupItem(item, shortDesc, longDesc, "ans");

            //Set the cooldown type and duration of the cooldown
            ItemBuilder.SetCooldownType(item, ItemBuilder.CooldownType.PerRoom, 2f);


            //Set some other fields
            item.consumable = false;
            item.quality = PickupObject.ItemQuality.B;


        }
        public override void Update()
        {
            base.Update();
            safebullets();
            safebullets();
        }
      


        public AIActor buddy1;
        public AIActor buddy2;
        public AIActor buddy3;
        protected override void DoEffect(PlayerController owner)
        {
           

            for (int i = 0; i <= 3; i++)
            {
                try
                {
                    AIActor orLoadByGuid = EnemyDatabase.GetOrLoadByGuid("21dd14e5ca2a4a388adab5b11b69a1e1");
                    IntVector2? intVector = new IntVector2?(owner.CurrentRoom.GetRandomVisibleClearSpot(1, 1));
                    bool flag = intVector != null;
                    if (flag)
                    {
                        if (i == 1)
                        {
                            AIActor one = AIActor.Spawn(orLoadByGuid.aiActor, intVector.Value, GameManager.Instance.Dungeon.data.GetAbsoluteRoomFromPosition(intVector.Value), true, AIActor.AwakenAnimationType.Default, true);
                            
                            one.IgnoreForRoomClear = true;
                            one.CanTargetEnemies = true;
                            one.CanTargetPlayers = false;
                            one.IsHarmlessEnemy = true;
                            one.gameObject.AddComponent<KillOnRoomClear>();
                            buddy1 = one;
                        }
                        if (i == 2)
                        {
                            AIActor two = AIActor.Spawn(orLoadByGuid.aiActor, intVector.Value, GameManager.Instance.Dungeon.data.GetAbsoluteRoomFromPosition(intVector.Value), true, AIActor.AwakenAnimationType.Default, true);
                            
                            two.IgnoreForRoomClear = true;  
                            two.CanTargetEnemies = true;
                            two.CanTargetPlayers = false;
                            two.IsHarmlessEnemy = true;
                            two.gameObject.AddComponent<KillOnRoomClear>();
                            buddy2 = two;
                        }
                        if (i == 3)
                        {
                            AIActor three = AIActor.Spawn(orLoadByGuid.aiActor, intVector.Value, GameManager.Instance.Dungeon.data.GetAbsoluteRoomFromPosition(intVector.Value), true, AIActor.AwakenAnimationType.Default, true);
                            three.IgnoreForRoomClear = true;
                            three.CanTargetPlayers = false;
                            three.CanTargetEnemies = true;
                            three.IsHarmlessEnemy = true;
                            three.gameObject.AddComponent<KillOnRoomClear>();
                            buddy3 = three;
                        }
                    }
                }
                catch (Exception ex)
                {
                    ETGModConsole.Log(ex.Message, false);
                }
            }



        }

        public void safebullets()
        { //call safebullets from the update method
            foreach (var projectile in GetbuddyBullets())
            {
                projectile.collidesWithPlayer = false;
                projectile.UpdateCollisionMask();

            }
            
        }

        public List<Projectile> GetbuddyBullets()
        {   // buddy should be the aiactor you want to get projectiles from as a public variable like below
            List<Projectile> list = new List<Projectile>();
            var allProjectiles = StaticReferenceManager.AllProjectiles;
            for (int i = 0; i < allProjectiles.Count; i++)
            {
                Projectile projectile = allProjectiles[i];

                if (projectile.Owner == buddy1 || projectile.Owner == buddy2 || projectile.Owner == buddy3)
                {
                    list.Add(projectile);
                }

            }
            return list;
        }
       
        public override void Pickup(PlayerController player)
        {
            base.Pickup(player);
            Tools.Print($"Player picked up {this.DisplayName}");
        }
    }
}