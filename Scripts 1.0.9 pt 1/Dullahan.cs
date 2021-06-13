using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gungeon;
using UnityEngine;
using Dungeonator;
using ItemAPI;
using System.Collections;

namespace Knives
{
    class Dullahan_curse : PlayerItem
    {
        public static void Register()
        {
            //The name of the item
            string itemName = "Dullahan's Curse";

            //Refers to an embedded png in the project. Make sure to embed your resources! Google it
            string resourceName = "Knives/Resources/Dullahan_curse";

            //Create new GameObject
            GameObject obj = new GameObject(itemName);

            //Add a PassiveItem component to the object
            var item = obj.AddComponent<Dullahan_curse>();

            //Adds a sprite component to the object and adds your texture to the item sprite collection
            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);

            //Ammonomicon entry variables
            string shortDesc = "Just Dead Cels";
            string longDesc = "Your body can now stay alive through sheer power of hatred.\n\n" +
                "_____________________________________________________\n" +
                "First use spawns a head that can cause effects on contact. The second use destroys it." +
                "\n\n\n - Knife_to_a_Gunfight";

            //Adds the item to the gungeon item list, the ammonomicon, the loot table, etc.
            //Do this after ItemBuilder.AddSpriteToObject!
            ItemBuilder.SetupItem(item, shortDesc, longDesc, "ski");

            //Adds the actual passive effect to the item
            //PlayerController owner = item.LastOwner as PlayerController;
            ItemBuilder.SetCooldownType(item, ItemBuilder.CooldownType.Timed, 30f);


            //Set the rarity of the item
            item.CanBeDropped = false;
            item.UsesNumberOfUsesBeforeCooldown = true;
            item.numberOfUses = 2;
            item.consumable = false;
            item.quality = PickupObject.ItemQuality.B;
        }


        public AIActor host;
        public int CamToggle = 0;
        public int modifierController = 0;
        

        protected override void DoEffect(PlayerController player)
        {
            if (numberOfUses == 2 && host == null)
            {
                //head
                IntVector2 aim = Vector2Extensions.ToIntVector2(player.unadjustedAimPoint, VectorConversions.Round);
                RoomHandler room = GameManager.Instance.Dungeon.data.GetAbsoluteRoomFromPosition(aim);
                AIActor orLoadByGuid = EnemyDatabase.GetOrLoadByGuid("c2f902b7cbe745efb3db4399927eab34");


                AIActor aiActor = AIActor.Spawn(orLoadByGuid.aiActor, player.CenterPosition, room, true, AIActor.AwakenAnimationType.Spawn, true);
                PhysicsEngine.Instance.RegisterOverlappingGhostCollisionExceptions(aiActor.specRigidbody, null, false);
                aiActor.CanTargetEnemies = true;
                aiActor.CanTargetPlayers = false;
               
                aiActor.IgnoreForRoomClear = true;
                aiActor.MovementSpeed = 10f;
                aiActor.CompanionOwner = player;
               

               
                aiActor.reinforceType = AIActor.ReinforceType.Instant;
                aiActor.HandleReinforcementFallIntoRoom(.1f);
                host = aiActor;
                
                MindControlEffect mindControl = aiActor.gameObject.GetOrAddComponent<MindControlEffect>();
                mindControl.owner = player;

               

               

                player.IsEthereal = true;
                player.SetIsStealthed(true, "cause they just are");
                PassiveItem.IncrementFlag(player, typeof(LiveAmmoItem));
                if (modifierController == 0)
                {
                    player.MovementModifiers += this.NoMotionModifier;
                    player.IsStationary = true;
                    modifierController++;
                }
                player.CurrentStoneGunTimer = 9999999f;
                this.LastOwner.stats.RecalculateStats(LastOwner, true);

                CamToggle = 1;
              

            }
            if (numberOfUses == 1)
            {



                player.CurrentStoneGunTimer = 1f;

                player.MovementModifiers -= this.NoMotionModifier;
                player.IsStationary = false;
                modifierController = 0;
                player.IsEthereal = false;

                if (host != null)
                {
                    host.healthHaver.ApplyDamage(99999, Vector2.zero, "unbound", CoreDamageTypes.Magic, DamageCategory.Unstoppable, true, null, false);
                    host = null;
                }

                player.SetIsStealthed(false, "cause they just are");
                
                
                CamToggle = 0;
                GameManager.Instance.MainCameraController.StartTrackingPlayer();
                GameManager.Instance.MainCameraController.SetManualControl(false, true);
                

            }

        }
       
        public void TargetNumber1(AIActor host, PlayerController user)
        {
            if (host != null && user.IsInCombat)
            {
                RoomHandler room = user.CurrentRoom;
                foreach (var enemy in room.GetActiveEnemies(RoomHandler.ActiveEnemyType.All))
                {
                    if (enemy != host && host != null)
                    {
                        enemy.OverrideTarget = host.specRigidbody;

                    }

                }
            }
        }
      
        private void NoMotionModifier(ref Vector2 voluntaryVel, ref Vector2 involuntaryVel)
        {
            voluntaryVel = Vector2.zero;
        }

        public override void Update()
        {   // deathcontroller
            if (host != null && host.healthHaver.GetCurrentHealth() == 0)
            {
                this.LastOwner.CurrentStoneGunTimer = 1f;

                this.LastOwner.MovementModifiers -= this.NoMotionModifier;
                this.LastOwner.IsStationary = false;
                modifierController = 0;
                this.LastOwner.IsEthereal = false;


                this.LastOwner.SetIsStealthed(false, "cause they just are");
                PassiveItem.DecrementFlag(this.LastOwner, typeof(LiveAmmoItem));
                this.LastOwner.stats.RecalculateStats(LastOwner, true);
                CamToggle = 0;
                GameManager.Instance.MainCameraController.StartTrackingPlayer();
                GameManager.Instance.MainCameraController.SetManualControl(false, true);
               
                
            }

            if (host != null && CamToggle == 1)
            {
                // in head state
                CameraController m_camera = GameManager.Instance.MainCameraController;
                m_camera.StopTrackingPlayer();
                m_camera.SetManualControl(true, false);
                m_camera.OverridePosition = host.CenterPosition;
                TargetNumber1(host, this.LastOwner);
                RoomHandler room = this.LastOwner.CurrentRoom;
                proximity(this.LastOwner);
                foreach (var enemy in room.GetActiveEnemies(RoomHandler.ActiveEnemyType.All))
                {
                    System.Random rng = new System.Random();
                    int chanceToFire = rng.Next(1, 80);

                    if (chanceToFire == 1 && enemy != host)
                    {
                        enemy.aiShooter.ShootAtTarget();
                    }
                }
                //poison
                List<AIActor> activeEnemies = base.LastOwner.CurrentRoom.GetActiveEnemies(RoomHandler.ActiveEnemyType.All);
                Vector2 centerPosition = host.specRigidbody.UnitCenter;
                foreach (AIActor aiactor in activeEnemies)
                {
                    bool flag3 = Vector2.Distance(aiactor.CenterPosition, centerPosition) < 1.5f && aiactor.healthHaver.GetMaxHealth() > 0f && aiactor != null && aiactor.specRigidbody != null && base.LastOwner != null;
                    
                    if (flag3 && aiactor != host)
                    {
                        aiactor.ApplyEffect(Game.Items["irradiated_lead"].GetComponent<BulletStatusEffectItem>().HealthModifierEffect, 4f, null);
                        aiactor.ApplyEffect(Game.Items["hot_lead"].GetComponent<BulletStatusEffectItem>().HealthModifierEffect, 4f, null);
                        aiactor.ApplyEffect(Game.Items["battery_bullets"].GetComponent<BulletStatusEffectItem>().HealthModifierEffect, 4f, null);
                        

                    }
                }

            }



            base.Update();
        }

   
        public Vector2 standing;
        public Vector2 bullet;
        public void proximity(PlayerController player)
        {   // gets and compares distance to every bullet checking id they are witin 1.55 meters and awards stacks
            standing = host.CenterPosition;
            foreach (var projectile in GetBullets())
            {
                float health = host.healthHaver.GetCurrentHealth();
                if (health > 0.0)
                {
                    bullet = (Vector2)projectile.LastPosition;

                    float radius = .5f;
                    if (Vector2.Distance(bullet, standing) < radius)
                    {
                        host.healthHaver.ApplyDamage(1f, Vector2.zero, "YES");

                    }


                }
            }



        }
        public List<Projectile> GetBullets()
        {
            List<Projectile> list = new List<Projectile>();
            var allProjectiles = StaticReferenceManager.AllProjectiles;
            for (int i = 0; i < allProjectiles.Count; i++)
            {
                Projectile projectile = allProjectiles[i];
                if (projectile && projectile.sprite && !projectile.ImmuneToBlanks && !projectile.ImmuneToSustainedBlanks)
                {
                    if (projectile.Owner != null && projectile.Owner != host)
                    {


                        list.Add(projectile);

                    }
                }
            }
            return list;
        }
       
    }



}

