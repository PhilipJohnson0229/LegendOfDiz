using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//adding this comment to see a change happen in git
namespace LOD 
{
    public class PlayerAttacker : MonoBehaviour
    {
        AnimatorHandler animatorHandler;
        InputHandler inputHandler;
        WeaponSlotManager weaponSlotManager;
        public string lastAttack;

        private void Awake()
        {
            animatorHandler = GetComponentInChildren<AnimatorHandler>();
            weaponSlotManager = GetComponentInChildren<WeaponSlotManager>();
            inputHandler = GetComponent<InputHandler>();
        }
        public void HandleLightAttack(WeaponItem weapon) 
        {
            weaponSlotManager.attackingWeapon = weapon;
            animatorHandler.PlayTargetAnimation(weapon.oneHandedLightAttack1, true);
            lastAttack = weapon.oneHandedLightAttack1;
        }
        public void HandleHeavyAttack(WeaponItem weapon) 
        {
            weaponSlotManager.attackingWeapon = weapon;
            animatorHandler.PlayTargetAnimation(weapon.oneHandedHeavyAttack1, true);
            lastAttack = weapon.oneHandedHeavyAttack1;
        }

        public void HandleWeaponCombo(WeaponItem weapon) 
        {
            if (inputHandler.comboFlag) 
            {
                animatorHandler.anim.SetBool("canDoCombo", false);
                if (lastAttack == weapon.oneHandedLightAttack1)
                {
                    animatorHandler.PlayTargetAnimation(weapon.oneHandedLightAttack2, true);
                }
            }
        }
    }

}
