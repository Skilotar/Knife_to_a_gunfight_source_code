using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using ItemAPI;

namespace Knives
{
    class Eye_of_the_tiger : PlayerItem
    {
        public static void Register()
        {
            //The name of the item
            string itemName = "Tiger Eye";

            //Refers to an embedded png in the project. Make sure to embed your resources! Google it
            string resourceName = "Knives/Resources/Eye_of_the_tiger";

            //Create new GameObject
            GameObject obj = new GameObject(itemName);

            //Add a PassiveItem component to the object
            var item = obj.AddComponent<Eye_of_the_tiger>();

            //Adds a sprite component to the object and adds your texture to the item sprite collection
            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);

            //Ammonomicon entry variables
            string shortDesc = "Blind Rage";
            string longDesc = "It's the eye of that tiger\n" +
                "It's the thrill of the fright\n" +
                "Running away from the rage of that tiger\n" +
                "And the last known survivor is fleeing the fight\n" +
                "It's all from stealing the eyeeeeeeeee of that tiger!!!!\n\n______________________________\n"+
                "Damages player to use and Summons tigers. These particular " +
                "tigers are pissed with you for stealing their eyes. However they can't exactally " +
                "tell when they've got you or an enemy." +
                "\n\n\n - Knife_to_a_Gunfight";

            //Adds the item to the gungeon item list, the ammonomicon, the loot table, etc.
            //Do this after ItemBuilder.AddSpriteToObject!
            ItemBuilder.SetupItem(item, shortDesc, longDesc, "ski");
            ItemBuilder.SetCooldownType(item, ItemBuilder.CooldownType.Damage, 100f);
            //Adds the actual passive effect to the item

            //Set the rarity of the item
            item.quality = PickupObject.ItemQuality.D;


        }

        protected override void DoEffect(PlayerController user)
        {
            Vector2 targetPosition = Vector2.zero;
            ItemBuilder.AddPassiveStatModifier(this, PlayerStats.StatType.MovementSpeed, 6f, StatModifier.ModifyMethod.ADDITIVE);
            Vector2 vector = targetPosition - base.transform.position.XY();
            float z = Mathf.Atan2(vector.y, vector.x) * 57.29578f;
            
            Projectile projectile = ((Gun)ETGMod.Databases.Items[369]).DefaultModule.projectiles[0];
            Projectile projectile2 = ((Gun)ETGMod.Databases.Items[369]).DefaultModule.projectiles[0];
            Projectile projectile3 = ((Gun)ETGMod.Databases.Items[369]).DefaultModule.projectiles[0];
            Projectile projectile4 = ((Gun)ETGMod.Databases.Items[369]).DefaultModule.projectiles[0];
            Projectile projectile5 = ((Gun)ETGMod.Databases.Items[369]).DefaultModule.projectiles[0];



            GameObject gameObject1 = SpawnManager.SpawnProjectile(projectile.gameObject, base.transform.position.XY(), Quaternion.Euler(0f, 0f, z), true);
            GameObject gameObject2 = SpawnManager.SpawnProjectile(projectile2.gameObject, base.transform.position.XY(), Quaternion.Euler(0f, 0f, z), true);
            GameObject gameObject3 = SpawnManager.SpawnProjectile(projectile3.gameObject, base.transform.position.XY(), Quaternion.Euler(0f, 0f, z), true);
            GameObject gameObject4 = SpawnManager.SpawnProjectile(projectile4.gameObject, base.transform.position.XY(), Quaternion.Euler(0f, 0f, z), true);
            GameObject gameObject5 = SpawnManager.SpawnProjectile(projectile5.gameObject, base.transform.position.XY(), Quaternion.Euler(0f, 0f, z), true);
            
            
            
            float dura = 10f;
            this.LastOwner.stats.RecalculateStats(LastOwner, true);
            StartCoroutine(ItemBuilder.HandleDuration(this, dura, user, EndEffect));
            
        }
     
        protected void EndEffect(PlayerController user)
        {
           
            ItemBuilder.AddPassiveStatModifier(this, PlayerStats.StatType.MovementSpeed, -6f, StatModifier.ModifyMethod.ADDITIVE);
            this.LastOwner.stats.RecalculateStats(LastOwner, true);
        }
    }
}

