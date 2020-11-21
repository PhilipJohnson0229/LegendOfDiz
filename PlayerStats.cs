using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LOD 
{
    public class PlayerStats : MonoBehaviour
    {
        public int healthLevel = 10;
        public int maxHealth;
        public int currentHealth;

        public int staminaLevel = 10;
        public int maxStamina;
        public int currentStamina;

        public HealthBar healthBar;
        public StaminaBar staminaBar;

        AnimatorHandler animatorHandler;

        private void Awake()
        {
            animatorHandler = GetComponentInChildren<AnimatorHandler>();
        }

        void Start()
        {
            maxHealth = SetMaxHealthFromHealthLevel();
            maxStamina = SetMaxStaminaFromStaminaLevel();
            currentHealth = maxHealth;
            currentStamina = maxStamina;
            healthBar.SetMaxHealth(maxHealth);
            staminaBar.SetMaxHealth(maxStamina);
        }

        private int SetMaxHealthFromHealthLevel() 
        {
            maxHealth = healthLevel * 10;
            return maxHealth;
        }

        private int SetMaxStaminaFromStaminaLevel()
        {
            maxStamina = staminaLevel * 10;
            return maxStamina;
        }

        public void TakeDamage(int damage) 
        {
            currentHealth = currentHealth - damage;
            healthBar.SetCurrentHealth(currentHealth);
            animatorHandler.PlayTargetAnimation("Damage01", true);

            if (currentHealth <= 0) 
            {
                currentHealth = 0;
                animatorHandler.PlayTargetAnimation("Death01", true);
            }
        }

        public void TakeStaminaDamage(int damage) 
        {
            currentStamina = currentStamina - damage;
            staminaBar.SetCurrentStamina(currentStamina);
        }
    }

}
