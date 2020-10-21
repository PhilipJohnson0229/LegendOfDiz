using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LOD 
{
    [CreateAssetMenu(menuName = "Item/ Weapon Item")]
    public class WeaponItem : Item
    {
        public GameObject modelPrefab;
        public bool isUnarmed;

        [Header("One Handed Attacks")]
        public string oneHandedLightAttack1;
        public string oneHandedLightAttack2;
        public string oneHandedHeavyAttack1;
    }

}
