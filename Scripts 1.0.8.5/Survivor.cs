using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using UnityEngine;
using ItemAPI;

namespace Knives
{
    class Survivor : PassiveItem
    {
        public static void Register()
        {
            //The name of the item
            string itemName = "Survivor";

            //Refers to an embedded png in the project. Make sure to embed your resources! Google it
            string resourceName = "Knives/Resources/I_will_survive";

            //Create new GameObject
            GameObject obj = new GameObject(itemName);

            //Add a PassiveItem component to the object
            var item = obj.AddComponent<Survivor>();

            //Adds a sprite component to the object and adds your texture to the item sprite collection
            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);

            //Ammonomicon entry variables
            string shortDesc = "I will survive";
            string longDesc = "You've been through bullet-hell and survived. At this point as long as you keep trying it feels like nothing can kill you.\n\n" +
                "Increases enemy bullet speed and gives chance to ignore damage." +
                "\n\n\n - Knife_to_a_Gunfight";

            //Adds the item to the gungeon item list, the ammonomicon, the loot table, etc.
            //Do this after ItemBuilder.AddSpriteToObject!
            ItemBuilder.SetupItem(item, shortDesc, longDesc, "ski");

            //Adds the actual passive effect to the item
            ItemBuilder.AddPassiveStatModifier(item, PlayerStats.StatType.Coolness, 1f, StatModifier.ModifyMethod.ADDITIVE);
            ItemBuilder.AddPassiveStatModifier(item, PlayerStats.StatType.EnemyProjectileSpeedMultiplier, .20f, StatModifier.ModifyMethod.ADDITIVE);
            //Set the rarity of the item

            item.quality = PickupObject.ItemQuality.A;

        }
        public override void Pickup(PlayerController player)
        {
            base.Pickup(player);
        }

        protected override void Update()
        {
            base.Update();
            healthchecker();
        }
        public float lastknownhealth;
        public void healthchecker()
        {
            float health = this.Owner.healthHaver.GetCurrentHealth();
            System.Random rng = new System.Random();
            int rand = rng.Next(1, 3);
            if (health < lastknownhealth)
            {
               
                
                if (rand == 1)
                {
                    Grit();
                }

            }

            lastknownhealth = health;

        }

        public void Grit()
        {
            EffectDoer();
            this.Owner.healthHaver.ApplyHealing(.5f);
        }
        private IEnumerable EffectDoer()
        {
            StartVFX(this.Owner);
            yield return new WaitForSecondsRealtime(1);
            StopVFX(this.Owner);
        }
        private void StartVFX(PlayerController user)
        {
            Material outline = SpriteOutlineManager.GetOutlineMaterial(user.sprite);
            outline.SetColor("_OverrideColor", new Color(63f, 236f, 165f));
        }


        private void StopVFX(PlayerController user)
        {
            Material outline = SpriteOutlineManager.GetOutlineMaterial(user.sprite);
            outline.SetColor("_OverrideColor", new Color(0f, 0f, 0f));
        }
    }
}
