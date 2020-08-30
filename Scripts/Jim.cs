using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using ItemAPI;
using MultiplayerBasicExample;

namespace Knives
{
    class Jim :PlayerItem
    {
        public static void Register()
        {
            //The name of the item
            string itemName = "Jim in the box";

            //Refers to an embedded png in the project. Make sure to embed your resources! Google it
            string resourceName = "Knives/Resources/jim_in_the_box";

            //Create new GameObject
            GameObject obj = new GameObject(itemName);

            //Add a PassiveItem component to the object
            var item = obj.AddComponent<Jim>();

            //Adds a sprite component to the object and adds your texture to the item sprite collection
            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);

            //Ammonomicon entry variables
            string shortDesc = "Jingle Jingle";
            string longDesc = "The soul of an gambling man who after death and failing to get into heaven, fell to an even worse fate. He gambled his soul with satan not to be let into hell and for once he was lucky\n\n" +
                "He won the bet, but with no eternal resting place his soul wandered the afterlife and made its way to the gungeon looking to fix is past. By some series of unfortunate evens he is now trapped in the box looking to make a wager...";

            //Adds the item to the gungeon item list, the ammonomicon, the loot table, etc.
            //Do this after ItemBuilder.AddSpriteToObject!
            ItemBuilder.SetupItem(item, shortDesc, longDesc, "ski");

            //Adds the actual passive effect to the item
            //PlayerController owner = item.LastOwner as PlayerController;
            ItemBuilder.SetCooldownType(item, ItemBuilder.CooldownType.Timed, 2f);

           
            item.quality = PickupObject.ItemQuality.B;
            
            
            item.numberOfUses = 4;
            item.UsesNumberOfUsesBeforeCooldown = true;
            
        }

        //applies damage on last use
        protected override void DoEffect(PlayerController user)
        {
            System.Random rng = new System.Random();

            if (numberOfUses == 4)
            {
                AkSoundEngine.PostEvent("m_WPN_windupgun_reload_01", base.gameObject);
                AkSoundEngine.PostEvent("m_WPN_windupgun_reload_01", base.gameObject);
            }

            if (numberOfUses == 3)
            {
                AkSoundEngine.PostEvent("m_WPN_windupgun_reload_02", base.gameObject);
                AkSoundEngine.PostEvent("m_WPN_windupgun_reload_02", base.gameObject);
            }
            if (numberOfUses == 2)
            {
                AkSoundEngine.PostEvent("m_WPN_windupgun_reload_03", base.gameObject);
                AkSoundEngine.PostEvent("m_WPN_windupgun_reload_03", base.gameObject);
            }
            if (numberOfUses == 1)
            {
                AkSoundEngine.PostEvent("m_WPN_windupgun_reload_04", base.gameObject);
                AkSoundEngine.PostEvent("m_WPN_windupgun_reload_04", base.gameObject);
                int jim = rng.Next(1, 20);
                var health = user.healthHaver;
                if (jim == 1)
                {
                    int Jackpot = rng.Next(1,6);
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
                        user.GiveItem("gun_soul");
                    }
                    if (Jackpot == 4)
                    {
                        user.GiveItem("turtle_problem");
                    }
                    if (Jackpot == 5)
                    {
                        user.GiveItem("sunglasses");
                    }
                    if (Jackpot == 6)
                    {
                        user.GiveItem("daruma");
                    }
                }
                if (jim == 2)
                {
                    user.GiveItem("armor");
                    user.GiveItem("armor");
                }
                if (jim == 3)
                {
                    user.GiveItem("casing 20");
                }
                if (jim == 4)
                {
                    user.GiveItem("casing 15");
                }
                if (jim == 5)
                {
                    user.GiveItem("glass_guon_stone 2");
                }
                if (jim == 6)
                {
                    user.GiveItem("casing 5");
                }
                if (jim == 7)
                {
                    user.GiveItem("casing 5");
                }
                if (jim == 8)
                {
                    user.GiveItem("casing 5");
                }
                if (jim == 9)
                {
                    user.GiveItem("casing 5");
                }
                if (jim == 10)
                {
                    user.GiveItem("casing 5");
                }
                if (jim == 11)
                {
                    
                }
                if (jim == 12)
                {
                    
                }
                if (jim == 13)
                {
                    
                }
                if (jim == 14)
                {
                   
                }
                if (jim == 15)
                {
                   
                }
                if (jim == 16)
                {
                    health.ApplyDamage(.5f, Vector2.zero, "Lost a bet", CoreDamageTypes.Magic, DamageCategory.Unstoppable, true);
                }
                if (jim == 17)
                {
                    health.ApplyDamage(.5f, Vector2.zero, "Lost a bet", CoreDamageTypes.Magic, DamageCategory.Unstoppable, true);
                }
                if (jim == 18)
                {
                    health.ApplyDamage(.5f, Vector2.zero, "Lost a bet", CoreDamageTypes.Magic, DamageCategory.Unstoppable, true);
                }
                if (jim == 19)
                {
                    health.ApplyDamage(1f, Vector2.zero, "Lost a bet", CoreDamageTypes.Magic, DamageCategory.Unstoppable, true);
                }
                if (jim == 20)
                {
                    health.ApplyDamage(1.5f, Vector2.zero, "Lost a bet", CoreDamageTypes.Magic, DamageCategory.Unstoppable, true);
                }
            }
        }







    }
}
