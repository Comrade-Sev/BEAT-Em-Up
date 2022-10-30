using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class HealthScript : MonoBehaviour
    {

        public float StartingHealth = 100f;

        public float _CurrentHealth = 100f;

        public float CurrentHealth
        {
            get { return _CurrentHealth; }
            set
            {
                _CurrentHealth = Mathf.Clamp(value,0f,100f);

                if(_CurrentHealth <= 0f)
                {
                    Destroy(gameObject);
                }
            }
        }

        void Start()
        {
            CurrentHealth = StartingHealth;
        }
    }
}