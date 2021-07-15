using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using ItemAPI;
using Brave.BulletScript;

namespace Knives
{
    class Danger_dance :PassiveItem
    {
        public static void Register()
        {
            string itemName = "Risky Headband";

            string resourceName = "Knives/Resources/Danger_dance";

            GameObject obj = new GameObject(itemName);

            var item = obj.AddComponent<Danger_dance>();

            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);

            //Ammonomicon entry variables
            string shortDesc = "Flirting with death";
            string longDesc = "Dancing trough the danger makes your aura very cool. Only the best of the best can weave through the bullet hellfire like you do." +
                "Taking damage or dodgerolling will make take away from the coolness of your prior dodging." +
                "\n\n\n - Knife_to_a_Gunfight";

            //Adds the item to the gungeon item list, the ammonomicon, the loot table, etc.
            //Do this after ItemBuilder.AddSpriteToObject!
            ItemBuilder.SetupItem(item, shortDesc, longDesc, "ski");

            //Adds the actual passive effect to the item




            ItemBuilder.AddPassiveStatModifier(item, PlayerStats.StatType.EnemyProjectileSpeedMultiplier, -.3f, StatModifier.ModifyMethod.ADDITIVE);


            item.quality = PickupObject.ItemQuality.D;


        }

        public float lastknownhealth;
        public int stack = 0;
        public int counter = 0;
        public float health;
        public Vector2 standing;
        public Vector2 bullet;
      
        public override void Pickup(PlayerController player)
        {
            player.OnPreDodgeRoll += this.OnPreDodgeRoll;
            base.Pickup(player);
        }

        protected override void Update()
        {
            base.Update();
            if (this.Owner != null)
            {

            
                proximity(this.Owner);
                
                if (stack < 0)
                {
                    stack = 0;
                }
            
                health = this.Owner.healthHaver.GetCurrentHealth();

                if (health < lastknownhealth)
                {

                    stack = stack - 5;

                }
                lastknownhealth = health;
            
                RemoveStat(PlayerStats.StatType.Coolness);
                AddStat(PlayerStats.StatType.Coolness, stack * .25f);
                this.Owner.stats.RecalculateStats(Owner, true);
            }
        }
        
        private void OnPreDodgeRoll(PlayerController player)
        {
            stack = stack - 1;
        }
        public void proximity(PlayerController player)
        {   // gets and compares distance to every bullet checking id they are witin 1.55 meters and awards stacks
            standing = player.CenterPosition;
            foreach (var projectile in GetBullets())
            {
                float health = this.Owner.healthHaver.GetCurrentHealth();
                if (health > 0.0)
                {
                    bullet = (Vector2)projectile.LastPosition;

                    float radius = 1.5f;
                    if (Vector2.Distance(bullet, standing) < radius)
                    {
                        counter++;
                       
                    }
                    else
                    {
                       
                    }
                } 
            }

            if (counter >= 200)
            {
                counter = 0;
                stack = stack + 2;
                AkSoundEngine.PostEvent("Play_WPN_radgun_cool_01", base.gameObject);
                
                
            }

        }
        private static List<Projectile> GetBullets()
        {
            List<Projectile> list = new List<Projectile>();
            var allProjectiles = StaticReferenceManager.AllProjectiles;
            for (int i = 0; i < allProjectiles.Count; i++)
            {
                Projectile projectile = allProjectiles[i];
                if (projectile && projectile.sprite && !projectile.ImmuneToBlanks && !projectile.ImmuneToSustainedBlanks)
                {
                    if (projectile.Owner != null)
                    {
                        if (projectile.isFakeBullet || projectile.Owner is AIActor || (projectile.Shooter != null && projectile.Shooter.aiActor != null) || projectile.ForcePlayerBlankable)
                        {
                            list.Add(projectile);
                        }
                    }
                }
            }
            return list;
        }
        private void AddStat(PlayerStats.StatType statType, float amount, StatModifier.ModifyMethod method = StatModifier.ModifyMethod.ADDITIVE)
        {
            StatModifier modifier = new StatModifier();
            modifier.amount = amount;
            modifier.statToBoost = statType;
            modifier.modifyType = method;

            foreach (var m in passiveStatModifiers)
            {
                if (m.statToBoost == statType) return; //don't add duplicates
            }

            if (this.passiveStatModifiers == null)
                this.passiveStatModifiers = new StatModifier[] { modifier };
            else
                this.passiveStatModifiers = this.passiveStatModifiers.Concat(new StatModifier[] { modifier }).ToArray();
        }


        //Removes a stat
        private void RemoveStat(PlayerStats.StatType statType)
        {
            var newModifiers = new List<StatModifier>();
            for (int i = 0; i < passiveStatModifiers.Length; i++)
            {
                if (passiveStatModifiers[i].statToBoost != statType)
                    newModifiers.Add(passiveStatModifiers[i]);
            }
            this.passiveStatModifiers = newModifiers.ToArray();
        }

        public override DebrisObject Drop(PlayerController player)
        {
            player.OnPreDodgeRoll -= this.OnPreDodgeRoll;
            return base.Drop(player);
        }
      
    }


}
