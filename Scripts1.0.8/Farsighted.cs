using System;
using System.Collections;
using System.Linq;
using System.Text;
using Gungeon;
using UnityEngine;
using ItemAPI;
using HutongGames.PlayMaker.Actions;
using MultiplayerBasicExample;
using MonoMod.Utils;
using MonoMod;
using System.Runtime.CompilerServices;

namespace Knives
{
    public class Farsighted : PassiveItem
    {
        //Call this method from the Start() method of your ETGModule extension
        public static void Register()
        {
            //The name of the item
            string itemName = "Farsighted bullets";

            //Refers to an embedded png in the project. Make sure to embed your resources! Google it
            string resourceName = "Knives/Resources/farsighted_bullets";

            //Create new GameObject
            GameObject obj = new GameObject(itemName);

            //Add a PassiveItem component to the object
            var item = obj.AddComponent<Farsighted>();

            //Adds a sprite component to the object and adds your texture to the item sprite collection
            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);

            //Ammonomicon entry variables
            string shortDesc = "Vision test";
            string longDesc =

                "This New bullet type spontaniously generated one day when Homing bullets lost its glasses.";

            //Adds the item to the gungeon item list, the ammonomicon, the loot table, etc.
            //Do this after ItemBuilder.AddSpriteToObject!
            ItemBuilder.SetupItem(item, shortDesc, longDesc, "ski");

            //Adds the actual passive effect to the item
            //PlayerController owner = item.LastOwner as PlayerController;
            ItemBuilder.AddPassiveStatModifier(item, PlayerStats.StatType.Accuracy, -4, StatModifier.ModifyMethod.ADDITIVE);
            ItemBuilder.AddPassiveStatModifier(item, PlayerStats.StatType.RangeMultiplier, 3, StatModifier.ModifyMethod.ADDITIVE);

            //Set the rarity of the item


            item.quality = PickupObject.ItemQuality.B;
        }

        public override void Pickup(PlayerController player)
        {
            player.PostProcessProjectile += this.PostProcessProjectile;
           
            base.Pickup(player);
        }
        
        //this attempts to fire a rocket projectile currently broken
       
        private void PostProcessProjectile(Projectile SourceProjectile, float chance)
        {
            StartCoroutine(direction(SourceProjectile));
        }

        public IEnumerator direction(Projectile bullet)
        {
            
            yield return new WaitForSecondsRealtime(.25f);
            
            Vector2 vector = bullet.sprite.WorldCenter;
             Vector2 Aim = this.Owner.unadjustedAimPoint;
             bullet.SendInDirection(Aim - vector, false, true);
           
        }

        //unsubs from projectiles
        public override DebrisObject Drop(PlayerController player)
        {
            player.PostProcessProjectile -= this.PostProcessProjectile;
            return base.Drop(player);
        }

    }
}
