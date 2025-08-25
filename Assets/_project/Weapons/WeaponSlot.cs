using System;
using UnityEngine;

namespace ObbyDefender.Weapons
{
    [Serializable]
    public class WeaponSlot
    {
        public WeaponType Type;
        public GameObject Weapon;
        public GameObject FirePoint;
    }
}
