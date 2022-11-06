using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class punchDestroy : MonoBehaviour
    {
        public PlayerHealth playerHealth;
        public Statez sz;
        public bool DamageBlocked = false;

        void OnTriggerEnter(Collider collision)
        {
            //the box collider of the instantiated punch will check the tag each time before it decides to do anything
            if (collision.CompareTag("Player"))
            {
                // if the punch hits anything with the tag "Player" it will take away health from the PlayerHealth.cs script
                //given that the script is embedded in correctly ofcourse
                
                playerHealth.GetHit();
                Destroy(gameObject, 1.0f);
                
                //this for loop clearly wasn't needed
                /*for (int i = 0; i < 2; i++) 
                {
                    Destroy(gameObject);
                }*/
            }

            if (collision.CompareTag("HostileObject"))
            {
                Destroy(gameObject);
            }

            if (collision.CompareTag("DamageBlock"))
            {
                sz.DamageBlocked = true;
                Destroy(gameObject);
            }
                
            
        }
    }
}    
