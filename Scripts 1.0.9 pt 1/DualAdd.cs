
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Knives
{
    class DualGunsManager
    {
        public static void AddDual()
        {
            DualWieldController doubleup = PickupObjectDatabase.GetByEncounterName("Regular Shotgun").gameObject.AddComponent<DualWieldController>();
            doubleup.PartnerGunID = PickupObjectDatabase.GetByEncounterName("Mozambique").PickupObjectId;

            DualWieldController doubleup2 = PickupObjectDatabase.GetByEncounterName("Old Goldie").gameObject.AddComponent<DualWieldController>();
            doubleup2.PartnerGunID = PickupObjectDatabase.GetByEncounterName("Mozambique").PickupObjectId;

            DualWieldController doubleup3 = PickupObjectDatabase.GetByEncounterName("Blunderbuss").gameObject.AddComponent<DualWieldController>();
            doubleup3.PartnerGunID = PickupObjectDatabase.GetByEncounterName("Mozambique").PickupObjectId;

            DualWieldController doubleup4 = PickupObjectDatabase.GetByEncounterName("Zilla Shotgun").gameObject.AddComponent<DualWieldController>();
            doubleup4.PartnerGunID = PickupObjectDatabase.GetByEncounterName("Mozambique").PickupObjectId;

        }
    }
}