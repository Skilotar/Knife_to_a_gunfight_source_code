using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using ItemAPI;
using MonoMod.RuntimeDetour;


namespace Knives
{ 
    class koolbucks :PlayerItem
    { 
        public static void Register()
        {
            string itemName = "Kool Kat Bucks";

            string resourceName = "Knives/Resources/koolkatbucks";

            GameObject obj = new GameObject(itemName);

            var item = obj.AddComponent<koolbucks>();

            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);

            //Ammonomicon entry variables
            string shortDesc = "Way Past Koool!!";
            string longDesc = "Despite their name these 'dollars' are the least cool form of currency in the known universe. They were originally tokens for a pizza arcade. Just having to touch them makes you feel less cool.\n\n" +
                "The tokens still hold some of their orignal power your coolness and pride can be traded for neat prizes." +
                "\n\n\n - Knife_to_a_Gunfight";

            //Adds the item to the gungeon item list, the ammonomicon, the loot table, etc.
            //Do this after ItemBuilder.AddSpriteToObject!
            ItemBuilder.SetupItem(item, shortDesc, longDesc, "ski");

            //Adds the actual passive effect to the item


            ItemBuilder.SetCooldownType(item, ItemBuilder.CooldownType.Timed, 1f);
            ItemBuilder.AddPassiveStatModifier(item, PlayerStats.StatType.Curse, 2f, StatModifier.ModifyMethod.ADDITIVE);
            item.quality = PickupObject.ItemQuality.B;
            item.CanBeDropped = false;
        }
       
        private System.Random rng = new System.Random();
        protected override void DoEffect(PlayerController user)
        {
           
            float dura = 1f;
            StartCoroutine(ItemBuilder.HandleDuration(this, dura, user, EndEffect));
            float coolness = PlayerStats.GetTotalCoolness();
            if (coolness >= 3)
            {
                AkSoundEngine.PostEvent("Play_WPN_radgun_wack_01", base.gameObject);
                ItemBuilder.AddPassiveStatModifier(this, PlayerStats.StatType.Coolness, -3f, StatModifier.ModifyMethod.ADDITIVE);
                int jim = rng.Next(1, 15);
                var health = user.healthHaver;
                if (jim == 1)
                {
                    int Jackpot = rng.Next(1, 10);
                    if (Jackpot == 1)
                    {
                        user.GiveItem("briefcase_of_cash");
                    }
                    if (Jackpot == 2)
                    {
                        user.GiveItem("stuffed_star");
                    }
                    if (Jackpot == 3)
                    {
                        user.GiveItem("turtle_problem");
                    }
                    if (Jackpot == 4)
                    {
                        user.GiveItem("turtle_problem");
                    }
                    if (Jackpot == 5)
                    {
                        user.GiveItem("turtle_problem");
                    }
                    if (Jackpot == 6)
                    {
                        user.GiveItem("sunglasses");
                    }
                    if (Jackpot == 7)
                    {
                        user.GiveItem("ring_of_the_resourceful_rat");
                    }
                    if (Jackpot == 8)
                    {
                        user.GiveItem("ring_of_chest_friendship");
                    }
                    if (Jackpot == 9)
                    {
                        user.GiveItem("ring_of_fire_resistance");
                    }
                    if (Jackpot == 10)
                    {
                        user.GiveItem("armor");
                        user.GiveItem("armor");
                        user.GiveItem("armor");
                       
                    }
                }
                if (jim == 2)
                {
                    user.GiveItem("armor");
                    user.GiveItem("armor");
                }
                if (jim == 3)
                {
                    user.GiveItem("armor");
                }
                if (jim == 4)
                {
                    user.GiveItem("armor");
                }
                if (jim == 5)
                {
                    user.GiveItem("glass_guon_stone");
                   
                }
                if (jim == 6)
                {
                    user.GiveItem("glass_guon_stone");
                   
                }
                if (jim == 7)
                {
                    user.GiveItem("glass_guon_stone");
                   
                }
                if (jim == 8)
                {
                    user.GiveItem("glass_guon_stone");
                    
                }
                if (jim == 9)
                {
                    health.ApplyHealing(1f);
                }
                if (jim == 10)
                {
                    health.ApplyHealing(1f);
                }
                if (jim == 11)
                {
                    health.ApplyHealing(1f);
                }
                if (jim == 12)
                {
                    health.ApplyHealing(1f);
                }
                if (jim == 13)
                {
                    health.ApplyHealing(1f);
                }
                if (jim == 14)
                {
                    health.ApplyHealing(1f);
                }
              
                this.LastOwner.stats.RecalculateStats(LastOwner, true);
            }

            else
            {
                AkSoundEngine.PostEvent("Play_WPN_devolver_morph_01", base.gameObject);

            }
        }

        protected void EndEffect(PlayerController user)
        {
            
        }





    }
}
