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
            if (collision.tag == "Player")
            {
                Destroy(gameObject);
                playerHealth.GetHit();
                Debug.Log(playerHealth._CurrentHealth);
            }
        }
    }
}    
