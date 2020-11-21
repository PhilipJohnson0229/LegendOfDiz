using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

namespace LOD 
{
    public class StaminaBar : MonoBehaviour
    {
        public Slider slider;

        public void SetMaxHealth(int maxStamina)
        {
            slider.maxValue = maxStamina;
            slider.value = maxStamina;
        }

        public void SetCurrentStamina(int currentStamina)
        {
            slider.value = currentStamina;
        }

    }

}
