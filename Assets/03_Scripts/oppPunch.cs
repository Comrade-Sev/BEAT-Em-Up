using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class oppPunch : MonoBehaviour
    {
        public Transform punchSpawnPoint;
        public GameObject punchPrefab;
        public float punchSpeed = 7;
        public bool triggered = false;
 
        void Update()
        {
            if(triggered == true)
            {
                var punch = Instantiate(punchPrefab, punchSpawnPoint.position, punchSpawnPoint.rotation);
                punch.GetComponent<Rigidbody>().velocity = punchSpawnPoint.forward * punchSpeed;
            }
        }
    }
}
