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

        }

        void Update()
        {
            float delta = Time.deltaTime;
            canDoCombo = anim.GetBool("canDoCombo");
            isInteracting = anim.GetBool("IsInteracting");
            inputHandler.rollFlag = false;
            inputHandler.sprintFlag = false;
            isTryingToPickUp = inputHandler.a_input;

            isSprinting = inputHandler.b_input;
            inputHandler.TickInput(delta);
            playerLocomotion.HandleMovement(delta);
            playerLocomotion.HandleRollingAndSprinting(delta);
            CheckForInteractableObject();

            playerLocomotion.HandleFalling(delta, playerLocomotion.moveDirection);
        }


        private void FixedUpdate()
        {
            float delta = Time.deltaTime;
            if (cameraHandler != null)
            {
                cameraHandler.FollowTarget(delta);
                cameraHandler.HandleCameraRotation(delta, inputHandler.mouseX, inputHandler.mouseY);
            }
        }

        //mainly used to reset flags
        private void LateUpdate()
        {
            inputHandler.rollFlag = false;
            inputHandler.sprintFlag = false;
            inputHandler.rb_input = false;
            inputHandler.rt_input = false;
            inputHandler.d_Pad_Down = false;
            inputHandler.d_Pad_Up = false;
            inputHandler.d_Pad_Left = false;
            inputHandler.d_Pad_Right = false;
            inputHandler.a_input = false;

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
                        isTryingToPickUp = true;
                        Debug.Log("An interactable has been found");
                        //we sill set the UI to match the interacable objects text
                        string interactableText = interactableObject.interactableText;

                        if (inputHandler.a_input)
                        {
                            Debug.Log("hitting pick up button");
                            interactableObject.Interact(this);
                        }
                    }
                    else 
                    {
                        isTryingToPickUp = false;
                    }
                }
            }

           
        }

        public void InteractWithPickUp(Interactable interactableObject) 
        {
            if (inputHandler.a_input)
            {
                Debug.Log("hitting pick up button");
                interactableObject.Interact(this);
            }
        }
    }

}
 