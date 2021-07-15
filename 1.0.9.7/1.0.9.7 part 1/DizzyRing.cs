using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using ItemAPI;
using MonoMod;
using Gungeon;


namespace Knives
{
    public class Dizzyring :PassiveItem
    { 
        public static void Register()
        {
            string itemName = "Dizzy Ring";

            string resourceName = "Knives/Resources/dizzy_ring";

            GameObject obj = new GameObject(itemName);
            
            var item = obj.AddComponent<Dizzyring>();

            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);

            //Ammonomicon entry variables
            string shortDesc = "Disorienting";
            string longDesc = "This ring was once owned by the Rollin Ronin, a student of Sir Manuel that went mad with rolling power! and also dizziness... mostly dizziness.\n\n" +
                "Feel how this ring becons you to tumble, but be warned excessive rolling can lead to head issues." +
                "\n\n\n -Knife_to_a_Gunfight";

            //Adds the item to the gungeon item list, the ammonomicon, the loot table, etc.
            //Do this after ItemBuilder.AddSpriteToObject!
            ItemBuilder.SetupItem(item, shortDesc, longDesc, "ski");

            //Adds the actual passive effect to the item
            ItemBuilder.AddPassiveStatModifier(item, PlayerStats.StatType.DodgeRollDistanceMultiplier, .6f, StatModifier.ModifyMethod.ADDITIVE);
            ItemBuilder.AddPassiveStatModifier(item, PlayerStats.StatType.DodgeRollSpeedMultiplier, .1f, StatModifier.ModifyMethod.ADDITIVE);
            ItemBuilder.AddPassiveStatModifier(item, PlayerStats.StatType.DodgeRollDamage, 14f, StatModifier.ModifyMethod.ADDITIVE);
           
            ItemBuilder.AddPassiveStatModifier(item, PlayerStats.StatType.ReloadSpeed, .5f, StatModifier.ModifyMethod.ADDITIVE);
            
           
            item.quality = PickupObject.ItemQuality.B;

           
        }
        public override void Pickup(PlayerController player)
        {
            IncrementFlag(player, typeof(LiveAmmoItem));
            player.OnNewFloorLoaded += this.OnLoadedFloor;
            base.Pickup(player);

        }
       
        
        public void OnLoadedFloor(PlayerController player) 
        {
            
            
            RemoveStat(PlayerStats.StatType.DodgeRollDamage);
            AddStat(PlayerStats.StatType.DodgeRollDamage, (GameManager.Instance.GetLastLoadedLevelDefinition().enemyHealthMultiplier - 1) * 10  + 14f);
            
            this.Owner.stats.RecalculateStats(Owner, true);
            
        }

        public override DebrisObject Drop(PlayerController player)
        {
            DecrementFlag(player, typeof(LiveAmmoItem));
            player.OnNewFloorLoaded -= this.OnLoadedFloor;
            return base.Drop(player);
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

        
