using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;
using ItemAPI;

namespace Knives
{
    class dog :PlayerItem
    {
        public static void Register()
        {
            string itemName = "Dog Problem";

            string resourceName = "Knives/Resources/dog";

            GameObject obj = new GameObject(itemName);

            var item = obj.AddComponent<dog>();

            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);

            //Ammonomicon entry variables
            string shortDesc = "You are carrying too many dogs";
            string longDesc = "yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip" +
                " yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip" +
                " yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip" +
                " yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip" +
                " yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip" +
                " yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip" +
                " yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip yip" +
                "\n\n\n - Knife_to_a_Gunfight";

            //Adds the item to the gungeon item list, the ammonomicon, the loot table, etc.
            //Do this after ItemBuilder.AddSpriteToObject!
            ItemBuilder.SetupItem(item, shortDesc, longDesc, "ski");

            ItemBuilder.SetCooldownType(item, ItemBuilder.CooldownType.PerRoom, 2f);



            item.consumable = false;
            item.quality = PickupObject.ItemQuality.S;
        }
        public int DogNumber;

       
       
        protected override void DoEffect(PlayerController owner)
        {

            string dogGuid = "c07ef60ae32b404f99e294a6f9acba75";

            AIActor orLoadByGuid = EnemyDatabase.GetOrLoadByGuid(dogGuid);
            Vector3 vector = owner.transform.position;
            if (GameManager.Instance.CurrentLevelOverrideState == GameManager.LevelOverrideState.FOYER)
            {
                vector += new Vector3(1.125f, -0.3125f, 0f);
            }
            GameObject extantCompanion = UnityEngine.Object.Instantiate<GameObject>(orLoadByGuid.gameObject, vector, Quaternion.identity);
            this.m_extantCompanion = extantCompanion;
            CompanionController orAddComponent = this.m_extantCompanion.GetOrAddComponent<CompanionController>();
            orAddComponent.Initialize(owner);
           
            if (orAddComponent.specRigidbody)
            {
                PhysicsEngine.Instance.RegisterOverlappingGhostCollisionExceptions(orAddComponent.specRigidbody, null, false);
                DogNumber++;
            }

            AIActor orLoadByGuid2 = EnemyDatabase.GetOrLoadByGuid(dogGuid);
            Vector3 vector2 = owner.transform.position;
            if (GameManager.Instance.CurrentLevelOverrideState == GameManager.LevelOverrideState.FOYER)
            {
                vector2 += new Vector3(1.125f, -0.3125f, 0f);
            }
            GameObject extantCompanion2 = UnityEngine.Object.Instantiate<GameObject>(orLoadByGuid.gameObject, vector, Quaternion.identity);
            this.m_extantCompanion = extantCompanion2;
            CompanionController orAddComponent2 = this.m_extantCompanion.GetOrAddComponent<CompanionController>();
            orAddComponent2.Initialize(owner);

            if (orAddComponent2.specRigidbody)
            {
                PhysicsEngine.Instance.RegisterOverlappingGhostCollisionExceptions(orAddComponent2.specRigidbody, null, false);
                DogNumber++;
            }

            if (hasSynergy)
            {
                string dogGuid2 = "c07ef60ae32b404f99e294a6f9acba75";

                AIActor orLoadByGuid3 = EnemyDatabase.GetOrLoadByGuid(dogGuid2);
                Vector3 vector3 = owner.transform.position;
                if (GameManager.Instance.CurrentLevelOverrideState == GameManager.LevelOverrideState.FOYER)
                {
                    vector3 += new Vector3(1.125f, -0.3125f, 0f);
                }
                GameObject extantCompanion3 = UnityEngine.Object.Instantiate<GameObject>(orLoadByGuid3.gameObject, vector3, Quaternion.identity);
                this.m_extantCompanion = extantCompanion3;
                CompanionController orAddComponent3 = this.m_extantCompanion.GetOrAddComponent<CompanionController>();
                orAddComponent3.Initialize(owner);
                if (orAddComponent3.specRigidbody)
                {
                    PhysicsEngine.Instance.RegisterOverlappingGhostCollisionExceptions(orAddComponent3.specRigidbody, null, false);
                    DogNumber++;
                }
               
            }

            
        }
        

        protected void EndEffect(PlayerController user)
        {
           


        }
        
        private bool hasSynergy = false;
        public override void Update()
        {
            bool flag = this.LastOwner;
            if (flag)
            {
                bool flag2 = this.LastOwner.HasPickupID(PickupObjectDatabase.GetByEncounterName("Turtle Problem").PickupObjectId);
                if (flag2)
                {
                    this.hasSynergy = true;
                }
                else
                {
                    this.hasSynergy = false;
                }
                

                base.Update();
            }
        }
        private GameObject m_extantCompanion;
    }
}
