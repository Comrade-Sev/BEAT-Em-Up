using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

namespace Game
{
    public class CollisionDespawn : MonoBehaviour
    {
        public GameObject ToBeDestroyed;
        public Material Colour;

        void awake()
        {
            ToBeDestroyed = GameObject.FindGameObjectWithTag("ToBeDestoryed");
        }

        void OnCollisionEnter(Collision collision)
        {
            Destroy(ToBeDestroyed);
        }
        
        
    }
}
