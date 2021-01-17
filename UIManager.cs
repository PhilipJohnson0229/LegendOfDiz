using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LOD 
{
    public class UIManager : MonoBehaviour
    {
        public PlayerInventory playerInventory;
        EquipmentWindowUI equipmentWindowUI;

        [Header("UI Windows")]
        public GameObject hudWindow;
        public GameObject selectWindow;
        public GameObject weaponInventoryWindow;

        [Header("Weapons Inventory")]
        public GameObject weaponInventorySlotPrefab;
        public Transform weaponInventorySlotParent;
        WeaponInventorySlot[] weaponinventorySlots;

        private void Awake()
        {
            equipmentWindowUI = FindObjectOfType<EquipmentWindowUI>();
        }

        private void Start()
        {
            weaponinventorySlots = weaponInventorySlotParent.GetComponentsInChildren<WeaponInventorySlot>();
            equipmentWindowUI.LoadWeaponOnEquipmentScreen(playerInventory);
        }
        public void UpdateUI() 
        {
            
            #region Weapon Inventory Slots
            for (int i = 0; i < weaponinventorySlots.Length; i++) 
            {
                Debug.Log("trying to display some" + weaponinventorySlots[i]);
                if (i < playerInventory.weaponsInventory.Count)
                {
                    if (weaponinventorySlots.Length < playerInventory.weaponsInventory.Count)
                    {
                       
                        Instantiate(weaponInventorySlotPrefab, weaponInventorySlotParent);
                        weaponinventorySlots = weaponInventorySlotParent.GetComponentsInChildren<WeaponInventorySlot>();
                    }

                    weaponinventorySlots[i].AddItem(playerInventory.weaponsInventory[i]);
                }
                else 
                {
                    weaponinventorySlots[i].ClearInventorySlot();
                }
            }
{}

            #endregion
        }
        public void OpenSelectWindow() 
        {
            selectWindow.SetActive(true);
        }

        public void CloseSelectWindow() 
        {
            selectWindow.SetActive(false);
        }

        public void CloseAllInventoryWindows() 
        {
            weaponInventoryWindow.SetActive(false);
        }
    }

}
