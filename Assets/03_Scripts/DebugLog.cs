using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class DebugLog : MonoBehaviour
    {
        public GameObject prefab;
        public float BeatInterval = 2;
        public float Beat = 0;

        void Start()
        {
            Instantiate(prefab, new Vector3(1 * 2.0f, 0, 0), Quaternion.identity);
            Debug.Log(Beat);
        }

        // Update is called once per frame
        void Update()
        {
            Beat += Time.deltaTime;
            if (Beat >= BeatInterval)
            {
                Beat = Beat - BeatInterval;
                Debug.Log(Beat);
                Instantiate(prefab, new Vector3(1 * 2.0f, 0, 0), Quaternion.identity);
            }
        }
    }
}
