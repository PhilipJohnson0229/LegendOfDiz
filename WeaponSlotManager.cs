using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LOD 
{
    public class WeaponSlotManager : MonoBehaviour
    {
        WeaponHolderSlot leftHandSlot;
        WeaponHolderSlot rightHandSlot;

        DamageCollider leftDamageCollider;
        DamageCollider rightDamageCollider;

        private void Awake()
        {
            WeaponHolderSlot[] weaponHolderSlots = GetComponentsInChildren<WeaponHolderSlot>();
            foreach (WeaponHolderSlot weaponSlot in weaponHolderSlots)
            {
                if (weaponSlot.isLeftHandSlot)
                {
                    leftHandSlot = weaponSlot;  
                }
                else if (weaponSlot.isRightHandSlot) 
                {
                    rightHandSlot = weaponSlot;
                }
            }
        }

        #region Handle Weapon Damage Collider
        public void LoadWeaponOnSlot(WeaponItem weaponItem, bool isLeft) 
        {
            if (isLeft)
            {
                leftHandSlot.LoadWeaponModel(weaponItem);
                LoadLeftDamageCollider();
            }
            else 
            {
                rightHandSlot.LoadWeaponModel(weaponItem);
                LoadRightDamageCollider();
            }
        }

        public void LoadLeftDamageCollider() 
        {
            leftDamageCollider = leftHandSlot.currentWeaponModel.GetComponentInChildren<DamageCollider>();
        }

        public void LoadRightDamageCollider()
        {
            rightDamageCollider = rightHandSlot.currentWeaponModel.GetComponentInChildren<DamageCollider>();
        }

        public void OpenLeftDamageCollider() 
        {
            leftDamageCollider.EnableDamageCollider();
        }

        public void OpenRightDamageCollider()
        {
            rightDamageCollider.EnableDamageCollider();
        }

        public void CloseLeftDamageCollider()
        {
            leftDamageCollider.DisableDamageCollider();
        }

        public void CloseRightDamageCollider()
        {
            rightDamageCollider.DisableDamageCollider();
        }

        #endregion
    }

}