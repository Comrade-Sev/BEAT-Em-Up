using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class PlayerHealth : MonoBehaviour
    {

        public BeatManager damageSource;
        
        public float StartingHealth = 100f;

        public float _CurrentHealth = 100f;

        public float playerDamage = 10f;
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

        public void GetHit()
        {
            CurrentHealth = CurrentHealth - playerDamage;
        }

        void Start()
        {
            CurrentHealth = StartingHealth;
        }
        
    }
}
