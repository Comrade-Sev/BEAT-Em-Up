using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class punchDestroy : MonoBehaviour
    {
        public PlayerHealth playerHealth;

        void OnTriggerEnter(Collider collision)
        {
            //the box collider of the instantiated punch will check the tag each time before it decides to do anything
            if (collision.CompareTag("Player"))
            {
                playerHealth.GetHit();
                for (int i = 0; i < 2; i++) 
                {
                    Destroy(gameObject);
                }
            }

            if (collision.CompareTag("HostileObject"))
            {
                Destroy(gameObject);
            }

            if (collision.CompareTag("DamageBlock"))
            {
                
            }
                
            
        }
    }
}    
