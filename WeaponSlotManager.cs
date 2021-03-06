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

        public WeaponItem attackingWeapon;

        Animator animator;

        QuickSlotsUI quickSlotUI;

        PlayerStats playerStats;
        private void Awake()
        {
            animator = GetComponent<Animator>();
            quickSlotUI = FindObjectOfType<QuickSlotsUI>();
            playerStats = GetComponentInParent<PlayerStats>();

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

        public void LoadWeaponOnSlot(WeaponItem weaponItem, bool isLeft)
        {
            if (isLeft)
            {
                leftHandSlot.LoadWeaponModel(weaponItem);
                LoadLeftDamageCollider();
                quickSlotUI.UpdateWeaponQuicklostsUI(true, weaponItem);

                #region Handle left hand idle animations
                if (weaponItem != null)
                {
                    animator.CrossFade(weaponItem.left_hand_idle, 0.2f);
                }
                else
                {
                    animator.CrossFade("Left Arm Empty", 0.2f);
                }
                #endregion
            }
            else
            {
                rightHandSlot.LoadWeaponModel(weaponItem);
                LoadRightDamageCollider();
                quickSlotUI.UpdateWeaponQuicklostsUI(false, weaponItem);

                #region Handle right hand idle animations
                if (weaponItem != null)
                {
                    animator.CrossFade(weaponItem.right_hand_idle, 0.2f);
                }
                else
                {
                    animator.CrossFade("Right Arm Empty", 0.2f);
                }
                #endregion
            }
        }

        #region Handle Weapon Damage Collider

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

        #region Handle Weapon Stamina Drainage
        public void DrainStaminaLightAttack() 
        {
            playerStats.TakeStaminaDamage(Mathf.RoundToInt(attackingWeapon.baseStamina * attackingWeapon.lightAttackMultiplier));
        }

        public void DrainStaminaHeavyAttack()
        {
            playerStats.TakeStaminaDamage(Mathf.RoundToInt(attackingWeapon.baseStamina * attackingWeapon.heavyAttackMultiplier));
        }
        #endregion
    }

}
