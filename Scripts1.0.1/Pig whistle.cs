using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using ItemAPI;


namespace Knives
{
    class Pig_Whistle :PlayerItem

    {
        public static void Register()
        {
            //The name of the item
            string itemName = "Pig whistle";

            //Refers to an embedded png in the project. Make sure to embed your resources! Google it
            string resourceName = "Knives/Resources/pig_whistle";

            //Create new GameObject
            GameObject obj = new GameObject(itemName);

            //Add a PassiveItem component to the object
            var item = obj.AddComponent<Pig_Whistle>();

            //Adds a sprite component to the object and adds your texture to the item sprite collection
            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);

            //Ammonomicon entry variables
            string shortDesc = "SEEWWW WEEE!";
            string longDesc ="Here! pig pig pig pig pig pig pig!  \n\n" +
                "A cursed relic created by the piglord to summon forth legions of pig soldier who would die for their leader.\n\n" +
                "The relic requires a heavy sacrifice to be payed for its services";

            //Adds the item to the gungeon item list, the ammonomicon, the loot table, etc.
            //Do this after ItemBuilder.AddSpriteToObject!
            ItemBuilder.SetupItem(item, shortDesc, longDesc, "ski");

            //Adds the actual passive effect to the item
            //PlayerController owner = item.LastOwner as PlayerController;

            ItemBuilder.SetCooldownType(item, ItemBuilder.CooldownType.Damage, 1200f);
            ItemBuilder.AddPassiveStatModifier(item, PlayerStats.StatType.Curse, 10f, StatModifier.ModifyMethod.ADDITIVE);
            ItemBuilder.AddPassiveStatModifier(item, PlayerStats.StatType.Health, -2f, StatModifier.ModifyMethod.ADDITIVE);
            //Set the rarity of the item
            item.AddToSubShop(ItemBuilder.ShopType.Cursula, .01f);
            item.consumable = true;
            
            item.quality = PickupObject.ItemQuality.S;
            item.numberOfUses = 2;
        }

       //applies damage on last use
        protected override void DoEffect(PlayerController user)
        {
            AkSoundEngine.PostEvent("Play_ENM_smiley_whistle_02", base.gameObject);

            user.GiveItem("pig");
            user.GiveItem("pig");
            if(numberOfUses == 1)
            { user.GiveItem("pig"); }
            user.healthHaver.ApplyDamage(200f, Vector2.zero, "Cursed relic" ,CoreDamageTypes.Void,DamageCategory.Unstoppable,true,null,true);

        }


        
    }
}

