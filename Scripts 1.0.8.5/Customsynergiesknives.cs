using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using ItemAPI;
using MultiplayerBasicExample;
using JetBrains.Annotations;

namespace Knives
{
    class Customsynergiesknives
    {
        public class Daft_Punk : AdvancedSynergyEntry
        {

            public Daft_Punk()
            {
                this.NameKey = "Harder! Better! Faster! Stronger!";
                this.MandatoryItemIDs = new List<int> //Look in the items ID map in the gungeon code for the ids.
                    {
                    ETGMod.Databases.Items["Daft"].PickupObjectId,
                    ETGMod.Databases.Items["Punk"].PickupObjectId
                    };
                this.IgnoreLichEyeBullets = true;
                this.statModifiers = new List<StatModifier>(0)
                {
                StatModifier.Create(PlayerStats.StatType.MovementSpeed,StatModifier.ModifyMethod.ADDITIVE, 2f),
                StatModifier.Create(PlayerStats.StatType.KnockbackMultiplier,StatModifier.ModifyMethod.ADDITIVE, .3f),
                StatModifier.Create(PlayerStats.StatType.Damage,StatModifier.ModifyMethod.ADDITIVE, .4f),
                StatModifier.Create(PlayerStats.StatType.Accuracy,StatModifier.ModifyMethod.ADDITIVE, -.2f),
                StatModifier.Create(PlayerStats.StatType.RateOfFire,StatModifier.ModifyMethod.ADDITIVE, .2f)
                };

                this.bonusSynergies = new List<CustomSynergyType>();
                
            }

        }



        public class Super_Duper_Fly : AdvancedSynergyEntry
        {

            public Super_Duper_Fly()
            {
                this.NameKey = "Super Duper Fly";
                this.MandatoryItemIDs = new List<int> //Look in the items ID map in the gungeon code for the ids.
                {
                    ETGMod.Databases.Items["Fly Friend"].PickupObjectId,ETGMod.Databases.Items["Super Fly"].PickupObjectId,

                };
                this.IgnoreLichEyeBullets = true;
                this.statModifiers = new List<StatModifier>(0)
                {
                StatModifier.Create(PlayerStats.StatType.Coolness,StatModifier.ModifyMethod.ADDITIVE, 4f),
                 StatModifier.Create(PlayerStats.StatType.Accuracy,StatModifier.ModifyMethod.ADDITIVE, .2f),

                };

                this.bonusSynergies = new List<CustomSynergyType>();
            }

        }
        public class tomislav : AdvancedSynergyEntry
        {

            public tomislav()
            {
                this.NameKey = "Tomislav";
                this.MandatoryGunIDs = new List<int>
                {
                    84
                };
                this.MandatoryItemIDs = new List<int> //Look in the items ID map in the gungeon code for the ids.
                {
                    ETGMod.Databases.Items["Sandvich"].PickupObjectId,

                };
                this.IgnoreLichEyeBullets = true;
                this.statModifiers = new List<StatModifier>(0)
                {
                StatModifier.Create(PlayerStats.StatType.KnockbackMultiplier,StatModifier.ModifyMethod.ADDITIVE, -.80f),
                StatModifier.Create(PlayerStats.StatType.Accuracy,StatModifier.ModifyMethod.ADDITIVE, -.80f),
                 StatModifier.Create(PlayerStats.StatType.RateOfFire,StatModifier.ModifyMethod.ADDITIVE, -.1f),
                };

                this.bonusSynergies = new List<CustomSynergyType>();
            }

        }
        public class split : AdvancedSynergyEntry
        {

            public split()
            {
                this.NameKey = "Split personality";
                this.MandatoryItemIDs = new List<int> //Look in the items ID map in the gungeon code for the ids.
                {
                    ETGMod.Databases.Items["Bad company"].PickupObjectId, 187

                };
                this.IgnoreLichEyeBullets = true;
                this.statModifiers = new List<StatModifier>(0)
                {

                    StatModifier.Create(PlayerStats.StatType.GlobalPriceMultiplier,StatModifier.ModifyMethod.ADDITIVE, -.1f),
                    StatModifier.Create(PlayerStats.StatType.MoneyMultiplierFromEnemies,StatModifier.ModifyMethod.ADDITIVE, 2f)
                };

                this.bonusSynergies = new List<CustomSynergyType>();
            }


        }
        public class flurry_of_blows : AdvancedSynergyEntry
        {

            public flurry_of_blows()
            {
                this.NameKey = "Flurry rush";
                this.MandatoryGunIDs = new List<int>
                {
                    PickupObjectDatabase.GetByEncounterName("Hail 2 U").PickupObjectId
                };
                this.MandatoryItemIDs = new List<int> //Look in the items ID map in the gungeon code for the ids.
                {
                     

                };
                this.OptionalItemIDs = new List<int>
                {
                    ETGMod.Databases.Items["Jar of Stardust"].PickupObjectId,
                    ETGMod.Databases.Items["Fate's Blessing"].PickupObjectId
                };
                this.IgnoreLichEyeBullets = false;
                this.statModifiers = new List<StatModifier>(0)
                {

                    StatModifier.Create(PlayerStats.StatType.RateOfFire,StatModifier.ModifyMethod.ADDITIVE, 8f),
                    StatModifier.Create(PlayerStats.StatType.Damage,StatModifier.ModifyMethod.ADDITIVE, -.7f),
                    StatModifier.Create(PlayerStats.StatType.DamageToBosses,StatModifier.ModifyMethod.ADDITIVE, -.7f),
                    StatModifier.Create(PlayerStats.StatType.AdditionalClipCapacityMultiplier,StatModifier.ModifyMethod.ADDITIVE, 8f),
                    StatModifier.Create(PlayerStats.StatType.AdditionalGunCapacity,StatModifier.ModifyMethod.ADDITIVE, 1f),

                };

                this.bonusSynergies = new List<CustomSynergyType>();
            }
        }

        public class BEEES : AdvancedSynergyEntry
        {

            public BEEES()
            {
                this.NameKey = "Biolgical Warfare";
                this.MandatoryGunIDs = new List<int>
                {

                };
                this.MandatoryItemIDs = new List<int> //Look in the items ID map in the gungeon code for the ids.
                {
                    ETGMod.Databases.Items["rocket boots"].PickupObjectId

                };
                this.OptionalItemIDs = new List<int>
                {
                    92,14,630,138,
                };
                this.IgnoreLichEyeBullets = true;
                this.statModifiers = new List<StatModifier>(0)
                {


                };

                this.bonusSynergies = new List<CustomSynergyType>();
            }
        }

        public class nano : AdvancedSynergyEntry
        {

            public nano()
            {
                this.NameKey = "You're powered up!";
                this.MandatoryItemIDs = new List<int> //Look in the items ID map in the gungeon code for the ids.
                {
                    ETGMod.Databases.Items["nano boost"].PickupObjectId,  565

                };
                this.IgnoreLichEyeBullets = true;
                this.statModifiers = new List<StatModifier>(0)
                {


                };

                this.bonusSynergies = new List<CustomSynergyType>();
            }
        }
        public class Big_problem : AdvancedSynergyEntry
        {

            public Big_problem()
            {
                this.NameKey = "A really big problem";
                this.MandatoryItemIDs = new List<int> //Look in the items ID map in the gungeon code for the ids.
                {
                    ETGMod.Databases.Items["dog problem"].PickupObjectId, PickupObjectDatabase.GetByEncounterName("Turtle problem").PickupObjectId

                };
                this.IgnoreLichEyeBullets = true;
                this.statModifiers = new List<StatModifier>(0)
                {

                    
                };

                this.bonusSynergies = new List<CustomSynergyType>();
            }


        }
        public class lich : AdvancedSynergyEntry
        {

            public lich()
            {
                this.NameKey = "Whole again";
                this.MandatoryGunIDs = new List<int>
                {
                    PickupObjectDatabase.GetByEncounterName("lich's trigger hand").PickupObjectId
                };
                this.MandatoryItemIDs = new List<int> //Look in the items ID map in the gungeon code for the ids.
                {
                     213,

                };
                this.OptionalItemIDs = new List<int>
                {

                };
                this.IgnoreLichEyeBullets = false;
                this.statModifiers = new List<StatModifier>(0)
                {

                    StatModifier.Create(PlayerStats.StatType.ReloadSpeed,StatModifier.ModifyMethod.ADDITIVE, -.3f),

                    StatModifier.Create(PlayerStats.StatType.RateOfFire,StatModifier.ModifyMethod.ADDITIVE, .2f),


                };

                this.bonusSynergies = new List<CustomSynergyType>();
            }
        }
        public class Chariot : AdvancedSynergyEntry
        {

            public Chariot()
            {
                this.NameKey = "Droppable Armor";
                this.MandatoryGunIDs = new List<int>
                {
                    PickupObjectDatabase.GetByEncounterName("Copper Chariot").PickupObjectId
                };
                this.MandatoryItemIDs = new List<int> //Look in the items ID map in the gungeon code for the ids.
                {
                     545,

                };
                this.OptionalItemIDs = new List<int>
                {

                };
                this.IgnoreLichEyeBullets = false;
                this.statModifiers = new List<StatModifier>(0)
                {

                  


                };

                this.bonusSynergies = new List<CustomSynergyType>();
            }
        }
        public class the_World_revolving : AdvancedSynergyEntry
        {

            public the_World_revolving()
            {
                this.NameKey = "Chaos Chaos CHAOS!";
                this.MandatoryItemIDs = new List<int> //Look in the items ID map in the gungeon code for the ids.
                    {
                    ETGMod.Databases.Items["Chamber of Chambers"].PickupObjectId,
                    ETGMod.Databases.Items["Speedy Chamber"].PickupObjectId
                    };
                this.IgnoreLichEyeBullets = true;
                this.statModifiers = new List<StatModifier>(0)
                {
               
                StatModifier.Create(PlayerStats.StatType.ReloadSpeed,StatModifier.ModifyMethod.ADDITIVE, -3f),
                
                StatModifier.Create(PlayerStats.StatType.RateOfFire,StatModifier.ModifyMethod.ADDITIVE, .5f)
                };

                this.bonusSynergies = new List<CustomSynergyType>();
            }

        }

        public class doubleStandard : AdvancedSynergyEntry
        {

            public doubleStandard()
            {
                this.NameKey = "Double Standards";
                this.MandatoryItemIDs = new List<int> //Look in the items ID map in the gungeon code for the ids.
                {
                    ETGMod.Databases.Items["Peace Standard"].PickupObjectId,  529

                };
                this.IgnoreLichEyeBullets = true;
                this.statModifiers = new List<StatModifier>(0)
                {


                };

                this.bonusSynergies = new List<CustomSynergyType>();
            }
        }

        public class Mozam_hammer : AdvancedSynergyEntry
        {

            public Mozam_hammer()
            {
                this.NameKey = "Hop-up: Hammer Point Rounds";
                this.MandatoryItemIDs = new List<int> //Look in the items ID map in the gungeon code for the ids.
                    {
                     PickupObjectDatabase.GetByEncounterName("Mozambique").PickupObjectId

                    };
                this.OptionalItemIDs = new List<int>
                    {
                     PickupObjectDatabase.GetByEncounterName("Can't Touch This").PickupObjectId,


                     111,//heavy bullets
                     
                     256,// heavy boots
                     457,//spiked armor
                     271//riddle of lead

                    };
                this.OptionalGunIDs = new List<int>
                {
                     390,//cobalt hammer
                     91,//the hammer
                     610,//woodbeam
                     393,//anvilain,
                     157,// big iron
                     545,//ac15
                     601,//big shotgun
                };
                this.IgnoreLichEyeBullets = false;
                this.statModifiers = new List<StatModifier>(0)
                {

                };

                this.bonusSynergies = new List<CustomSynergyType>();

            }
        }
        public class Mozam_fools : AdvancedSynergyEntry
        {
            public Mozam_fools()
            {
                this.NameKey = "Hop-up: April Fools";
                this.MandatoryItemIDs = new List<int> //Look in the items ID map in the gungeon code for the ids.
                {
                    PickupObjectDatabase.GetByEncounterName("Mozambique").PickupObjectId

                };
                this.OptionalItemIDs = new List<int>
                {
                    241,// skatter
                    //PickupObjectDatabase.GetByEncounterName("Table Tech Dizzy").PickupObjectId,
                    //PickupObjectDatabase.GetByEncounterName("Slide Tech Slide").PickupObjectId,
                    PickupObjectDatabase.GetByEncounterName("Tiger Eye").PickupObjectId,
                    //409,//tv
                    216 // box

                };
                this.OptionalGunIDs = new List<int>
                {
                    539, // boxing glove
                    340, //lower r
                    10,  //watergun
                    503, //bullet
                    512 // shell
                      
                };
                this.IgnoreLichEyeBullets = false;
                this.statModifiers = new List<StatModifier>(0)
                {

                };

                this.bonusSynergies = new List<CustomSynergyType>();

            }

        }

        public class Mozam_Throw : AdvancedSynergyEntry
        {
            public Mozam_Throw()
            {
                this.NameKey = "Hop-up: Throw Away Joke";
                this.MandatoryItemIDs = new List<int> //Look in the items ID map in the gungeon code for the ids.
                {
                    PickupObjectDatabase.GetByEncounterName("Mozambique").PickupObjectId

                };
                this.OptionalItemIDs = new List<int>
                {
                   500, // hip holdster
                  


                };
                this.OptionalGunIDs = new List<int>
                {
                  PickupObjectDatabase.GetByEncounterName("Jk-47").PickupObjectId,
                   126, // shotbow
                   8,   // bow
                   31,  // klobbe

                };
                this.IgnoreLichEyeBullets = false;
                this.statModifiers = new List<StatModifier>(0)
                {
                    StatModifier.Create(PlayerStats.StatType.ThrownGunDamage,StatModifier.ModifyMethod.ADDITIVE, 20f),
                };

                this.bonusSynergies = new List<CustomSynergyType>();

            }

        }

        public class Mozam_Shatter : AdvancedSynergyEntry
        {
            public Mozam_Shatter()
            {
                this.NameKey = "Hop-up: Shattering Tier Lists";
                this.MandatoryItemIDs = new List<int> //Look in the items ID map in the gungeon code for the ids.
                {
                    PickupObjectDatabase.GetByEncounterName("Mozambique").PickupObjectId,
                    PickupObjectDatabase.GetByEncounterName("World Shatter").PickupObjectId

                };
                this.OptionalItemIDs = new List<int>
                {
                  
                  


                };
                this.OptionalGunIDs = new List<int>
                {
                
                };
                this.IgnoreLichEyeBullets = false;
                this.statModifiers = new List<StatModifier>(0)
                {
                   
                };

                this.bonusSynergies = new List<CustomSynergyType>();

            }

        }

        public class Mozam_mazoM : AdvancedSynergyEntry
        {
            public Mozam_mazoM()
            {
                this.NameKey = "Hop-up: Double Up";
                this.MandatoryItemIDs = new List<int> //Look in the items ID map in the gungeon code for the ids.
                {
                    PickupObjectDatabase.GetByEncounterName("Mozambique").PickupObjectId,
                    

                };
                this.OptionalItemIDs = new List<int>
                {

                };
                this.OptionalGunIDs = new List<int>
                {
                    93,
                    329,
                    122,
                    51
                };
                this.IgnoreLichEyeBullets = false;
                this.statModifiers = new List<StatModifier>(0)
                {

                };

                this.bonusSynergies = new List<CustomSynergyType>();

            }

        }
        //
    }
}
       





