using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using UnityEngine;
using ItemAPI;

namespace Knives
{
    class Dio :PassiveItem
    {
        public static System.Random rng = new System.Random();
        public static void Register()
        {
            //The name of the item
            string itemName = "Dio Mimic";
            
            int random = rng.Next(1,9);
            string resourcePath = Dio.spritePaths[0];
            Dio.spriteIDs = new int[Dio.spritePaths.Length];
            GameObject obj = new GameObject(itemName);
            Dio dio = obj.AddComponent<Dio>();
            ItemBuilder.AddSpriteToObject(itemName, resourcePath, obj);

            //real sprite
            Dio.spriteIDs[0] = dio.sprite.spriteId;

            //bait sprites
            Dio.spriteIDs[1] = SpriteBuilder.AddSpriteToCollection(Dio.spritePaths[1], dio.sprite.Collection);
            Dio.spriteIDs[2] = SpriteBuilder.AddSpriteToCollection(Dio.spritePaths[2], dio.sprite.Collection);
            Dio.spriteIDs[3] = SpriteBuilder.AddSpriteToCollection(Dio.spritePaths[3], dio.sprite.Collection);
            Dio.spriteIDs[4] = SpriteBuilder.AddSpriteToCollection(Dio.spritePaths[4], dio.sprite.Collection);
            Dio.spriteIDs[5] = SpriteBuilder.AddSpriteToCollection(Dio.spritePaths[5], dio.sprite.Collection);
            Dio.spriteIDs[6] = SpriteBuilder.AddSpriteToCollection(Dio.spritePaths[6], dio.sprite.Collection);
            Dio.spriteIDs[7] = SpriteBuilder.AddSpriteToCollection(Dio.spritePaths[7], dio.sprite.Collection);
            Dio.spriteIDs[8] = SpriteBuilder.AddSpriteToCollection(Dio.spritePaths[8], dio.sprite.Collection);
            Dio.spriteIDs[9] = SpriteBuilder.AddSpriteToCollection(Dio.spritePaths[9], dio.sprite.Collection);
            

            string shortDesc = "Trickery!";
            string longDesc = "You were expecting a good item but it was I! DIO!";

            //Adds the item to the gungeon item list, the ammonomicon, the loot table, etc.
            //Do this after ItemBuilder.AddSpriteToObject!
            ItemBuilder.SetupItem(dio, shortDesc, longDesc, "ski");

            //Adds the actual passive effect to the item

            ItemBuilder.AddPassiveStatModifier(dio, PlayerStats.StatType.Damage, .3f, StatModifier.ModifyMethod.ADDITIVE);


            //Set the rarity of the item

            dio.quality = PickupObject.ItemQuality.A;

            dio.id = Dio.spriteIDs[random];
            dio.sprite.SetSprite(dio.id);
            
        }

        public override void Pickup(PlayerController player)
        {
            this.id = Dio.spriteIDs[0];
            base.sprite.SetSprite(this.id);
        }
        public override DebrisObject Drop(PlayerController player)
        {
            int random = rng.Next(1, 9);
            this.id = Dio.spriteIDs[random];
            base.sprite.SetSprite(this.id);
            return base.Drop(player);
        }

        private static readonly string[] spritePaths = new string[]
        {
            "Knives/Resources/dio/dio_mimic_001",
            "Knives/Resources/dio/dio_mimic_002",
            "Knives/Resources/dio/dio_mimic_003",
            "Knives/Resources/dio/dio_mimic_004",
            "Knives/Resources/dio/dio_mimic_005",
            "Knives/Resources/dio/dio_mimic_006",
            "Knives/Resources/dio/dio_mimic_007",
            "Knives/Resources/dio/dio_mimic_008",
            "Knives/Resources/dio/dio_mimic_009",
            "Knives/Resources/dio/dio_mimic_010",
        };
        private static int[] spriteIDs;
        private int id;
    }
}
