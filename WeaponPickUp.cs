using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LOD 
{
    public class WeaponPickUp : Interactable
    {
        public WeaponItem weaponItem;

        public override void Interact(PlayerManager pm)
        {
            base.Interact(pm);
            PickUpItem(pm);
        }

        private void PickUpItem(PlayerManager pm)
        {
            PlayerInventory playerInventory;
            PlayerLocomotion playerLocomotion;
            AnimatorHandler animatorHandler;

            playerInventory = pm.GetComponent<PlayerInventory>();
            playerLocomotion = pm.GetComponent<PlayerLocomotion>();
            animatorHandler = pm.GetComponentInChildren<AnimatorHandler>();

            playerLocomotion.rigidBody.velocity = Vector3.zero; //stops the player from moving
            animatorHandler.PlayTargetAnimation("Pick Up Item", true); //plays animation to pick up item
            playerInventory.weaponsInventory.Add(weaponItem);

            Destroy(gameObject);

        }
    }

}
