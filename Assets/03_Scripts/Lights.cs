using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class Lights : MonoBehaviour
    {
        public Light light1;
        public Light light2;
        public Light light3;
        public BeatManager bm;

        // Start is called before the first frame update
        public void Start()
        {
            FindObjectOfType<BeatManager>().BeatHappened.AddListener(OnBeat);
            //light1 = GetComponent<Light>();
           // light2 = GetComponent<Light>();
           // light3 = GetComponent<Light>();
        }

        // Update is called once per frame
        void OnBeat()
        {
            //Debug.Log("blyat");
            if (light1.enabled)
            {
                light1.enabled = false;
                light2.enabled = true;
                //return;
                Debug.Log(light1.enabled = false);
            }
            else if (light2.enabled)
            {
                light2.enabled = false;
                light3.enabled = true;
                //return;
            }
            else if (light3.enabled)
            {
                light3.enabled = false;
                light1.enabled = true;
                //return;
            }
        }
    }
}
