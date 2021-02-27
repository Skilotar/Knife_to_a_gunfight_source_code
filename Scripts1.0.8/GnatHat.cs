using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gungeon;
using UnityEngine;
using ItemAPI;
using System.Runtime.CompilerServices;
using Dungeonator;
using HutongGames.PlayMaker.Actions;

namespace Knives
{
    class GnatHat :PlayerItem
    {
        public static void Register()
        {
            //The name of the item
            string itemName = "Diminished Cap";

            //Refers to an embedded png in the project. Make sure to embed your resources! Google it
            string resourceName = "Knives/Resources/deminish_cap";

            //Create new GameObject
            GameObject obj = new GameObject(itemName);

            //Add a PassiveItem component to the object
            var item = obj.AddComponent<GnatHat>();

            //Adds a sprite component to the object and adds your texture to the item sprite collection
            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);

            //Ammonomicon entry variables
            string shortDesc = "This hat is way too small";
            string longDesc = "Wait, no its not....  Wait! Yes It IS!! \nWait, its not.\n" + 
            "_________________________\n" +
            "This hat will shrink the user and cause them to take double damage. Due to the caps strange size morphing abilities many many items can be fit inside.\n\n" +
            "Dev notes: DONT PICKUP OR DROP GUNS WHILE ACTIVE. I tried to fix this so much and every fix adds more bugs. \n\n" +
            "You can not toggle next to the wall also don't try to... it can lead to horrible horrible misfortunes. Just trust me. Thanks.";

            //Adds the item to the gungeon item list, the ammonomicon, the loot table, etc.
            //Do this after ItemBuilder.AddSpriteToObject!
            ItemBuilder.SetupItem(item, shortDesc, longDesc, "ski");

            //Adds the actual passive effect to the item
            //PlayerController owner = item.LastOwner as PlayerController;
            ItemBuilder.SetCooldownType(item, ItemBuilder.CooldownType.Timed, .25f);


            //Set the rarity of the item

            item.CanBeDropped = false;
            item.quality = PickupObject.ItemQuality.A;
        }
        
        bool toggle = true;
        public float OrigX;
        public float OrigY = 0;

        


        protected override void DoEffect(PlayerController user)
        {
            user.secondaryHand.sprite.renderer.enabled = false;
            var X = user.transform.localScale.x;
            var Y = user.transform.localScale.y;

            if (OrigY == 0)
            {
                OrigX = X;
                OrigY = Y;
            }
            var deminishX = (user.transform.localScale.x * .45f);
            var deminishY = (user.transform.localScale.y * .45f);
            
            //l
            RoomHandler room;
            room = GameManager.Instance.Dungeon.data.GetAbsoluteRoomFromPosition(Vector2Extensions.ToIntVector2(user.CenterPosition,VectorConversions.Round));
            CellData cellaim = room.GetNearestCellToPosition(user.CenterPosition);

            // Oh hey there,
            // I see you looking through my code...
            // its okay you can stay
            // we're all friends here.
            // take whatever you need
            // alright have a good day,
            // see you later   
            //- Skilotar_

            if (toggle)
            {
                if (cellaim.isNextToWall == false)
                {
                    user.transform.localScale = new Vector3(deminishX, deminishY, user.transform.localScale.z);


                   
                    user.specRigidbody.UpdateCollidersOnScale = true;
                    user.specRigidbody.UpdateColliderPositions();
                    toggle = false;
                    

                }

            }
           else
            {
                if (cellaim.isNextToWall == false)
                {
                    user.transform.localScale = new Vector3(OrigX, OrigY, user.transform.localScale.z);

                   
                    user.specRigidbody.UpdateColliderPositions();
                    
                    toggle = true;
                    
                }
            }

        }

        
        public override void Update()
        {
           
            if (toggle == false)
            {
                foreach (var projectile in GetBullets())
                {
                    projectile.BecomeBlackBullet();
                }
            }

            RemoveStat(PlayerStats.StatType.AdditionalItemCapacity);
            AddStat(PlayerStats.StatType.AdditionalItemCapacity, this.LastOwner.activeItems.Count + 1); 
            this.LastOwner.stats.RecalculateStats(LastOwner, true);

            
            //item scale corrector script
                
            base.Update();

        }
        private List<Projectile> GetBullets()
        {
           
            List<Projectile> list = new List<Projectile>();
            var allProjectiles = StaticReferenceManager.AllProjectiles;
            for (int i = 0; i < allProjectiles.Count; i++)
            {
                Projectile projectile = allProjectiles[i];
                if (projectile && projectile.sprite && !projectile.ImmuneToBlanks && !projectile.ImmuneToSustainedBlanks)
                {
                    if (projectile.Owner != null)
                    {
                        if (projectile.isFakeBullet || projectile.Owner is AIActor || (projectile.Shooter != null && projectile.Shooter.aiActor != null) || projectile.ForcePlayerBlankable)
                        {
                           
                            list.Add(projectile);
                           
                        }
                    }
                }
            }
            return list;

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
