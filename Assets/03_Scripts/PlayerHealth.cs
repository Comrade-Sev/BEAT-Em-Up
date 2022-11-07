using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public class PlayerHealth : MonoBehaviour
    {

        public BeatManager damageSource;
        
        public float StartingHealth = 30f;

        public float _CurrentHealth = 30f;

        public float playerDamage = 10f;
        public float CurrentHealth
        {
            get { return _CurrentHealth; }
            set
            {
                _CurrentHealth = Mathf.Clamp(value,0f,30f);

                if(_CurrentHealth <= 0f)
                {
                    //Destroy(gameObject);
                    //Application.Quit();
                    SceneManager.LoadScene("scene_demo");
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
