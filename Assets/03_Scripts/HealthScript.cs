using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class HealthScript : MonoBehaviour
    {

        public float startingHealth = 100f;

        public float currentHealth = 100f;

        public float playerDamage = 5f;
        public float CurrentHealth
        {
            get { return currentHealth; }
            set
            {
                currentHealth = Mathf.Clamp(value,0f,100f);

                if(currentHealth <= 0f)
                {
                    Destroy(gameObject);
                }
            }
        }

        public void GetHit()
        {
            CurrentHealth = CurrentHealth - playerDamage;
        }

        void Start()
        {
            CurrentHealth = startingHealth;
        }
        
    }
}