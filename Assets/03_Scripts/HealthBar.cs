using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class HealthBar : MonoBehaviour
    {
            public Slider slider;



        public void SetHealth(float health)
        {
            slider.value = health;
        }

        

        public void SetMaxHealth(float health)
        {
            slider.maxValue = health;
            slider.value = health;
        }


    }
}
