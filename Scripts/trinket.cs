using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;
using ItemAPI;
using JetBrains.Annotations;

namespace Knives
{
    class trinket :OnKillEnemyItem
    {
        public static void Register()
        {
            string itemName = "Trinket of Kaliber"; //The name of the item
            string resourceName = "Knives/Resources/trinket_of_kaliber"; //Refers to an embedded png in the project. Make sure to embed your resources!

            GameObject obj = new GameObject();
            var item = obj.AddComponent<trinket>();
            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);

            string shortDesc = "A Reminder";
            string longDesc = "This small medalion is worn to remind the user who's domain they are in and becons them to remain pure in the eyes of Kaliber\n\n";
            ;

            ItemBuilder.SetupItem(item, shortDesc, longDesc, "ski");
            item.activationStyle = 0;
            item.chanceOfActivating = 1f;
            item.quality = PickupObject.ItemQuality.B;

        }

        public override void Pickup(PlayerController player)
        {
            base.Pickup(player);
        }
        float kali_points = 0;
        float actual = 0;

        protected override void Update()
        {
            base.Update();
           
        }

        private void crusade(PlayerController player)
        {
            this.OnKilledEnemy(player);
            {
                kali_points++;
                float curse = this.Owner.stats.GetStatValue(PlayerStats.StatType.Curse);
                actual = (kali_points * .01f) / curse;
               
            }

            RemoveStat(PlayerStats.StatType.Damage);
            AddStat(PlayerStats.StatType.Damage, actual );
           
            this.Owner.stats.RecalculateStats(Owner, true);

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
    }
}
