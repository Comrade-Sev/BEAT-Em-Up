using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class CubeSpawner : MonoBehaviour
    {
        public GameObject prefab;
        public float BeatInterval = 2;
        public float Beat = 0;

        void start()
        {
            Instantiate(prefab, new Vector3(1 * 2.0f, 0, 0), Quaternion.identity);
            print("Hello!");
            Debug.Log(Beat);
        }
        void update()
        {
            Beat += Time.deltaTime;
            Debug.Log(Beat);
            print("Hello!");
            if (Beat >= BeatInterval)
            {
                Beat = 0;
                Instantiate(prefab, new Vector3(1 * 2.0f, 0, 0), Quaternion.identity);
                Debug.Log("WhileCheck");
            }
        }
    }
}
