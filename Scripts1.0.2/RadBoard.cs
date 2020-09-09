using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using ItemAPI;
using MonoMod.RuntimeDetour;

namespace Knives
{
    class rad_board : PlayerItem
    {
        public static void Register()
        {
            //The name of the item
            string itemName = "Rad board";

            //Refers to an embedded png in the project. Make sure to embed your resources! Google it
            string resourceName = "Knives/Resources/rad_board";

            //Create new GameObject
            GameObject obj = new GameObject(itemName);

            //Add a PassiveItem component to the object
            var item = obj.AddComponent<rad_board>();

            //Adds a sprite component to the object and adds your texture to the item sprite collection
            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);

            //Ammonomicon entry variables
            string shortDesc = "Sick Flips!";
            string longDesc = "A skake board that has had its fair share of use and of new coats of paint. Its usefulness in the gungeon is questionable, but you sure look cool jumping over bullets.";

            //Adds the item to the gungeon item list, the ammonomicon, the loot table, etc.
            //Do this after ItemBuilder.AddSpriteToObject!
            ItemBuilder.SetupItem(item, shortDesc, longDesc, "ski");

            //Adds the actual passive effect to the item

            
            ItemBuilder.SetCooldownType(item, ItemBuilder.CooldownType.Damage, 300f);
            ItemBuilder.AddPassiveStatModifier(item, PlayerStats.StatType.GlobalPriceMultiplier, .20f, StatModifier.ModifyMethod.ADDITIVE);
            //Set the rarity of the item
            item.quality = PickupObject.ItemQuality.A;
            
        }
        float coolpoints = 0;
        protected override void DoEffect(PlayerController user)
        {
            coolpoints = 0;
            float dura = 20f;
            user.OnDodgedProjectile += this.OnDodgedProjectile;
            StartCoroutine(ItemBuilder.HandleDuration(this, dura, user, EndEffect));
        }
        private void OnDodgedProjectile(Projectile projectile)
        {
            AkSoundEngine.PostEvent("Play_WPN_radgun_noice_01", base.gameObject);
            coolpoints = coolpoints + 1;
        }
        
        protected void EndEffect(PlayerController user)
        {
            
            coolpoints =  coolpoints / 6;

            ItemBuilder.AddPassiveStatModifier(this, PlayerStats.StatType.Coolness, coolpoints, StatModifier.ModifyMethod.ADDITIVE);
            this.LastOwner.stats.RecalculateStats(LastOwner, true);
            user.OnDodgedProjectile -= this.OnDodgedProjectile;
        }
    }
   
}

  