using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LOD 
{
    public class PlayerManager : MonoBehaviour
    {
        InputHandler inputHandler;
        Animator anim;
        CameraHandler cameraHandler;
        PlayerLocomotion playerLocomotion;

        InteractUI interactableUI;
        public GameObject interactableUIGameobject;
        public GameObject itemInteractableGameobject;

        public bool isInteracting;

        [Header("Player Flags")]
        public bool isSprinting;
        public bool isInAir;
        public bool isGrounded;
        public bool canDoCombo;
        public bool isTryingToPickUp;

        private void Awake()
        {
            cameraHandler = FindObjectOfType<CameraHandler>();
        }

        void Start()
        {
            inputHandler = GetComponent<InputHandler>();
            anim = GetComponentInChildren<Animator>();
            playerLocomotion = GetComponent<PlayerLocomotion>();
            interactableUI = FindObjectOfType<InteractUI>();
        }

        void Update()
        {
            float delta = Time.deltaTime;
            canDoCombo = anim.GetBool("canDoCombo");
            isInteracting = anim.GetBool("IsInteracting");
            anim.SetBool("IsInAir", isInAir);

            inputHandler.TickInput(delta);
            inputHandler.rollFlag = false;
            inputHandler.sprintFlag = false;
            isTryingToPickUp = inputHandler.a_input;
        
            CheckForInteractableObject();
            isSprinting = inputHandler.b_input;
            //this may have to go into fixed update once i add flight mechanics for pan
            //right now this button simply plays an animation
            playerLocomotion.HandleJumping();
            playerLocomotion.HandleRollingAndSprinting(delta);
        }

        private void FixedUpdate()
        {
            float delta = Time.fixedDeltaTime;

            playerLocomotion.HandleMovement(delta);
            playerLocomotion.HandleFalling(delta, playerLocomotion.moveDirection);
        }

        //mainly used to reset flags
        private void LateUpdate()
        {
            inputHandler.rollFlag = false;
            inputHandler.rb_input = false;
            inputHandler.rt_input = false;
            inputHandler.d_Pad_Down = false;
            inputHandler.d_Pad_Up = false;
            inputHandler.d_Pad_Left = false;
            inputHandler.d_Pad_Right = false;
            inputHandler.a_input = false;
            inputHandler.jump_input = false;
            inputHandler.inventory_input = false;

            float delta = Time.deltaTime;

            if (cameraHandler != null)
            {
                cameraHandler.FollowTarget(delta);
                cameraHandler.HandleCameraRotation(delta, inputHandler.mouseX, inputHandler.mouseY);
            }

            if (isInAir)
            {
                playerLocomotion.inAirTimer = playerLocomotion.inAirTimer + Time.deltaTime;
            }
        }

        public void CheckForInteractableObject() 
        {
            RaycastHit hit;

            if (Physics.SphereCast(transform.position, 0.3f, transform.forward, out hit, 1f, cameraHandler.ignoreLayers))
            {
                if (hit.collider.tag == "Interactable")
                {

                    Interactable interactableObject = hit.collider.GetComponent<Interactable>();

                    if (interactableObject != null)
                    {
                        //we sill set the UI to match the interacable objects text
                        string interactableText = interactableObject.interactableText;
                        interactableUI.interactableText.text = interactableText;
                        interactableUIGameobject.SetActive(true);

                        if (inputHandler.a_input)
                        {
                            Debug.Log("hitting pick up button");
                            interactableObject.Interact(this);
                        }
                    }

                }
            }
            else 
            {
                if (interactableUI != false)
                {
                    interactableUIGameobject.SetActive(false);
                }
                if (itemInteractableGameobject != null && inputHandler.a_input)
                {
                    itemInteractableGameobject.SetActive(false);
                }
            }



           
        }


    }

}
 