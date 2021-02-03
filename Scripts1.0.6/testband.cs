using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gungeon;
using UnityEngine;
using Dungeonator;
using ItemAPI;
using System.Collections;
using HutongGames.PlayMaker.Actions;
using MultiplayerBasicExample;
using MonoMod.Utils;
using MonoMod;
using System.Runtime.CompilerServices;

namespace Knives
{
    public class testBand : PlayerItem
    {
        //Call this method from the Start() method of your ETGModule extension
        public static void Register()
        {
            //The name of the item
            string itemName = "test Band";

            //Refers to an embedded png in the project. Make sure to embed your resources! Google it
            string resourceName = "Knives/Resources/MindSwapHeadband";

            //Create new GameObject
            GameObject obj = new GameObject(itemName);

            //Add a PassiveItem component to the object
            var item = obj.AddComponent<testBand>();

            //Adds a sprite component to the object and adds your texture to the item sprite collection
            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);

            //Ammonomicon entry variables
            string shortDesc = "give em da rock";
            string longDesc =

                "";

            //Adds the item to the gungeon item list, the ammonomicon, the loot table, etc.
            //Do this after ItemBuilder.AddSpriteToObject!
            ItemBuilder.SetupItem(item, shortDesc, longDesc, "ski");

            //Adds the actual passive effect to the item
            //PlayerController owner = item.LastOwner as PlayerController;
            ItemBuilder.SetCooldownType(item, ItemBuilder.CooldownType.Damage, 1f);


            //Set the rarity of the item
            item.CanBeDropped = false;
            item.UsesNumberOfUsesBeforeCooldown = true;
            item.numberOfUses = 5;
            item.consumable = false;
            item.quality = PickupObject.ItemQuality.B;
        }


        public AIActor host;
        public int BallToggle = 0;
        public PlayerOrbital ball;

        protected override void DoEffect(PlayerController player)
        {
           
                Vector2 vector = player.unadjustedAimPoint.XY() - player.CenterPosition;
                float zRotation = BraveMathCollege.Atan2Degrees(vector);
                player.StartCoroutine(this.HandleSwing(player, vector, 1f, 20f));






        }
       
        private IEnumerator HandleSwing(PlayerController user, Vector2 aimVec, float rayDamage, float rayLength)
        {
            float elapsed = 0f;
            while (elapsed < .1)
            {
                elapsed += BraveTime.DeltaTime;
                SpeculativeRigidbody hitRigidbody = this.IterativeRaycast(user.CenterPosition, aimVec, rayLength, int.MaxValue, user.specRigidbody);
                if (hitRigidbody && hitRigidbody.aiActor && !hitRigidbody.aiActor.healthHaver.IsBoss && hitRigidbody.aiActor.IsNormalEnemy)
                {
                    hitRigidbody.aiActor.healthHaver.ApplyDamage(rayDamage, aimVec, "Hero's Sword", CoreDamageTypes.None, DamageCategory.Normal, false, null, false);

                   

                    host = hitRigidbody.aiActor;
                   
                    host.CompanionOwner = user;
                 
                    
                    BallToggle = 1;


                }
                yield return null;
            }
            yield break;
        }

        private void HandleMotionCAT(AIActor host, PlayerOrbital ball)
        {
           ball.transform.position = host.CenterPosition;
            ball.aiActor.OverrideTarget = host.specRigidbody;
        }

        protected SpeculativeRigidbody IterativeRaycast(Vector2 rayOrigin, Vector2 rayDirection, float rayDistance, int collisionMask, SpeculativeRigidbody ignoreRigidbody)
        {
            int num = 0;
            RaycastResult raycastResult;
            while (PhysicsEngine.Instance.Raycast(rayOrigin, rayDirection, rayDistance, out raycastResult, true, true, collisionMask, new CollisionLayer?(CollisionLayer.Projectile), false, null, ignoreRigidbody))
            {
                num++;
                SpeculativeRigidbody speculativeRigidbody = raycastResult.SpeculativeRigidbody;
                if (num < 3 && speculativeRigidbody != null)
                {
                    MinorBreakable component = speculativeRigidbody.GetComponent<MinorBreakable>();
                    if (component != null)
                    {
                        component.Break(rayDirection.normalized * 3f);
                        RaycastResult.Pool.Free(ref raycastResult);
                        continue;
                    }
                }
                RaycastResult.Pool.Free(ref raycastResult);
                return speculativeRigidbody;
            }
            return null;
        }


        public override void Update()
        {
           if(BallToggle == 1)
            {
                HandleMotionCAT(host, ball);

            }

            base.Update();
        }
       
    }
       
        




}

