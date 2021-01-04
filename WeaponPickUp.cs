using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LOD 
{
    public class WeaponPickUp : Interactable
    {
        public WeaponItem weaponItem;

        public override void Interact(PlayerManager pm)
        {
            Debug.Log("I found an item i can pick up");
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
            pm.itemInteractableGameobject.GetComponentInChildren<Text>().text = weaponItem.itemName;
            pm.itemInteractableGameobject.GetComponentInChildren<RawImage>().texture = weaponItem.itemIcon.texture;
            pm.itemInteractableGameobject.SetActive(true);
            Destroy(gameObject);

        }
    }

}
