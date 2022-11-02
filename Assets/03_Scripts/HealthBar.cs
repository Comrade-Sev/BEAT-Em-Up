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
    }
}
