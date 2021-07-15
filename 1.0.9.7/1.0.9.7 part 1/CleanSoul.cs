using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using ItemAPI;

namespace Knives
{
    class clean_soul :PassiveItem
    {
        public static void Register()
        {
            string itemName = "Mournful Soul";

            string resourceName = "Knives/Resources/self_cleansing_soul";

            GameObject obj = new GameObject(itemName);

            var item = obj.AddComponent<clean_soul>();

            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);

            //Ammonomicon entry variables
            string shortDesc = "Kaliber Forgive Me.";
            string longDesc = "Should we sin so that grace may abound? No. But if we must maybe Kaliber can forgive.\n\n" +
                "Removes curse over time. Hurts to gain curse." +
                "\n\n\n - Knife_to_a_Gunfight";

            //Adds the item to the gungeon item list, the ammonomicon, the loot table, etc.
            //Do this after ItemBuilder.AddSpriteToObject!
            ItemBuilder.SetupItem(item, shortDesc, longDesc, "ski");

            //Adds the actual passive effect to the item
            
            
            item.CanBeDropped = false;

            


            
            item.quality = PickupObject.ItemQuality.B;


        }
        public override void Pickup(PlayerController player)
        {
            base.Pickup(player);
        }

        protected override void Update()
        {
            base.Update();
            if (this.Owner != null)
            {
                cursechecker();
                AutoCleanse();
            }
            
        }
        public float token;
        public float lastknowncurse;
        public void cursechecker()
        { // detects if curse goes up
            float curse = this.Owner.stats.GetStatValue(PlayerStats.StatType.Curse);

            if (curse > lastknowncurse)
            {

                Penance();

            }
            lastknowncurse = curse;
            
        }
        public void Penance()
        {// damages for curse gain Unless the gain is the reset
            if (token == 1)
            {
                this.Owner.healthHaver.ApplyDamage(.5f, Vector2.zero, "Forced Penance", CoreDamageTypes.Void, DamageCategory.Unstoppable, true, null, true);
            }
            token = 1;
        }
        private System.Random rng = new System.Random();
        public void AutoCleanse()
        {
            int mrclean = rng.Next(1, 300);
            if ( mrclean == 1)
            {
                if(lastknowncurse > 0)
                {
                    ItemBuilder.AddPassiveStatModifier(this, PlayerStats.StatType.Curse, -1f, StatModifier.ModifyMethod.ADDITIVE);
                    this.Owner.stats.RecalculateStats(Owner, true);
                    AkSoundEngine.PostEvent("Play_NPC_magic_blessing_01", base.gameObject);
                }
                
                if ( lastknowncurse < 0)
                {
                    token = 0;
                    ItemBuilder.AddPassiveStatModifier(this, PlayerStats.StatType.Curse, -lastknowncurse, StatModifier.ModifyMethod.ADDITIVE);
                    this.Owner.stats.RecalculateStats(Owner, true);
                }
                           
             }

        }
    }
}
