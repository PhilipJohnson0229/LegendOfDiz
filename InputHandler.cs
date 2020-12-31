using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LOD 
{
    public class InputHandler : MonoBehaviour
    {
        public float horizontal;
        public float vertical;
        public float moveAmount;
        public float mouseX;
        public float mouseY;

        public bool a_input;
        public bool b_input;
        public bool rb_input;
        public bool rt_input;
        public bool d_Pad_Up;
        public bool d_Pad_Down;
        public bool d_Pad_Left;
        public bool d_Pad_Right;

        public bool rollFlag;
        public bool sprintFlag;
        public bool comboFlag;
        public float rollInputTimer;

        PlayerControls inputActions;
        PlayerAttacker playerAttacker;
        PlayerInventory playerInventory;
        PlayerManager playerManager;

        Vector2 movementInput;
        Vector2 cameraInput;

        public void Awake()
        {
            playerAttacker = GetComponent<PlayerAttacker>();
            playerInventory = GetComponent<PlayerInventory>();
            playerManager = GetComponent<PlayerManager>();
        }

        public void OnEnable()
        {
            if (inputActions == null) 
            {
                inputActions = new PlayerControls();
                inputActions.PlayerMovement.Movement.performed += inputActions => movementInput = inputActions.ReadValue<Vector2>();
                inputActions.PlayerMovement.Camera.performed += i => cameraInput = i.ReadValue<Vector2>();
            }

            inputActions.Enable();
        }

        private void OnDisable()
        {
            inputActions.Disable();
        }

        public void TickInput(float delta) 
        {
            MoveInput(delta);
            HandleRollInput(delta);
            HandleAttackInput(delta);
            HandleQuickSlotInput();
            HandleInteractableInput();
           
        }

        public void MoveInput(float delta) 
        {
            horizontal = movementInput.x;
            vertical = movementInput.y;
            moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));
            mouseX = cameraInput.x;
            mouseY = cameraInput.y;
        }

        private void HandleRollInput(float delta)
        {

            b_input = inputActions.PlayerActions.Roll.phase == UnityEngine.InputSystem.InputActionPhase.Started;
            if (b_input)
            {
                rollInputTimer += delta;
                sprintFlag = true;
            }
            else 
            {
                if (rollInputTimer > 0 && rollInputTimer < 0.5f)
                {
                    sprintFlag = false;
                    rollFlag = true;
                }

                rollInputTimer = 0;
            }
        }

        private void HandleAttackInput(float delta) 
        {
            inputActions.PlayerActions.RB.performed += i => rb_input = true;
            inputActions.PlayerActions.RT.performed += i => rt_input = true;

            if (rb_input)
            {
                if (playerManager.canDoCombo)
                {
                    comboFlag = true;
                    playerAttacker.HandleWeaponCombo(playerInventory.rightWeapon);
                    comboFlag = false;
                }
                else 
                {
                    if (playerManager.isInteracting) 
                    {
                        return;
                    }

                    if (playerManager.canDoCombo)
                    {
                        return;
                    }
                    playerAttacker.HandleLightAttack(playerInventory.rightWeapon);
                }
            }

            if (rt_input)
            {
                playerAttacker.HandleHeavyAttack(playerInventory.rightWeapon);
            }
        }

        private void HandleQuickSlotInput() 
        {
            inputActions.PlayerActions.DPadRight.performed += i => d_Pad_Right = true;
            inputActions.PlayerActions.DPadLeft.performed += i => d_Pad_Left = true;
            inputActions.PlayerActions.DPadUp.performed += i => d_Pad_Up = true;
            inputActions.PlayerActions.DPadDown.performed += i => d_Pad_Down = true;
            if (d_Pad_Right)
            {
                playerInventory.ChangeRightWeapon();
            }
            else if (d_Pad_Left) 
            {
                playerInventory.ChangeLeftWeapon();
            }
        }

        private void HandleInteractableInput() 
        {
            inputActions.PlayerActions.Interact.performed += i => a_input = true;
            
        }

    }

}
