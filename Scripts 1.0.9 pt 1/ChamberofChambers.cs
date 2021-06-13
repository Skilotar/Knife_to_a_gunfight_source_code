
   using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HutongGames.PlayMaker.Actions;
using ItemAPI;
using UnityEngine;

namespace Knives
{
    class ChamberofChambers : PassiveItem
    {

        //Call this method from the Start() method of your ETGModule extension
        public static void Register()
        {
            //The name of the item
            string itemName = "Chamber of Chambers";

            //Refers to an embedded png in the project. Make sure to embed your resources! Google it
            string resourceName = "Knives/Resources/Chamber of Chambers";

            //Create new GameObject
            GameObject obj = new GameObject(itemName);

            //Add a PassiveItem component to the object
            var item = obj.AddComponent<ChamberofChambers>();

            //Adds a sprite component to the object and adds your texture to the item sprite collection
            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);

            //Ammonomicon entry variables
            string shortDesc = "5 out of 6 Chambers recommend";
            string longDesc = "A strange invention created by the Duke of the chamber. This chamber has 6 smaller chambers inlayed into itself. \n\nWith this you can instantly reload 6 times before having to refill the whole chamber. " +
                "\n\nIf a quick shootout is what you're looking for this is the chamber for you. Just make sure you finish it in 6 clips or its gonna be one heck of a reload." +
                "\n\n\n - Knife_to_a_Gunfight";

            //Adds the item to the gungeon item list, the ammonomicon, the loot table, etc.
            //Do this after ItemBuilder.AddSpriteToObject!
            ItemBuilder.SetupItem(item, shortDesc, longDesc, "ski");

            //Adds the actual passive effect to the item





            //Set the rarity of the item
            item.CanBeDropped = false;
            item.quality = PickupObject.ItemQuality.B;


        }
        //trying to make the code spawn extra jammed lords
        public override void Pickup(PlayerController player)
        {

            player.OnReloadedGun = (Action<PlayerController, Gun>)Delegate.Combine(player.OnReloadedGun, new Action<PlayerController, Gun>(this.reloadometer));
            
            base.Pickup(player);
        }
        public int token = 6;
        public int Suffer = 0;
        public int toggle = 1;

        private void reloadometer(PlayerController user, Gun gun)
        {
            
            if (token > 0)
            {
                
                token--;
                Suffer = 0;
            }
            else
            {
                user.CurrentStoneGunTimer = 3f;
                token = 6;
                Suffer = 1;
            }
            if (Suffer != 1)
            {
                RemoveStat(PlayerStats.StatType.ReloadSpeed);
                AddStat(PlayerStats.StatType.ReloadSpeed, -1);
                this.Owner.stats.RecalculateStats(Owner, true);
            }
            if (Suffer == 1)
            {
                RemoveStat(PlayerStats.StatType.ReloadSpeed);
                AddStat(PlayerStats.StatType.ReloadSpeed, 6f);
                this.Owner.stats.RecalculateStats(Owner, true);
            }

        }
        protected override void Update()
        {
            
            base.Update();
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

