using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class CubeSpawner : MonoBehaviour
    {
        public GameObject prefab;
        public float beatInterval = 2;
        public float beat = 0;
        
        //NOTE: This was for earlier when I thought we wanted to make something similar to beat saber so blocks needed
        //to be spawned in spontaneously. 

        void Start()
        {
            Instantiate(prefab, new Vector3(1 * 2.0f, 0, 0), Quaternion.identity);
            print("Hello!");
            Debug.Log(beat);
        }
        void Update()
        {
            beat += Time.deltaTime;
            Debug.Log(beat);
            print("Hello!");
            if (beat >= beatInterval)
            {
                beat = 0;
                Instantiate(prefab, new Vector3(1 * 2.0f, 0, 0), Quaternion.identity);
                Debug.Log("WhileCheck");
            }
        }
    }
}
