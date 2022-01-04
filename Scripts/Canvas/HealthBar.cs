using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace BlackPearl
{
    public class HealthBar : MonoBehaviour
    {
        public Slider slider;

    public void SetMaxHealth(int maxhealth)
    {
        slider.maxValue = maxhealth;
        slider.value = maxhealth;
    }
    public void SetCurrentHealth(int currenthealth)
        {
            slider.value = currenthealth;
        }
    }
}
