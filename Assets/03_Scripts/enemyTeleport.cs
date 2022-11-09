using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class enemyTeleport : MonoBehaviour
    {
        public GameObject position1;
        public GameObject position2;
        public GameObject position3;
        public GameObject position4;
        public BeatManager bm;
        void Start()
        {
            FindObjectOfType<BeatManager>().beatHappened.AddListener(OnBeat);
        }
        void OnBeat()
        {
            var beat = bm.beatCount;
            Debug.Log(beat);
            
            if (gameObject.transform.position == position4.transform.position && beat == 3)
            {
                gameObject.transform.position = position1.transform.position;
                gameObject.transform.rotation = position1.transform.rotation;
            }
            else if (gameObject.transform.position == position1.transform.position && beat == 6)
            {
                gameObject.transform.position = position2.transform.position;
                gameObject.transform.rotation = position2.transform.rotation;
            }
            else if (gameObject.transform.position == position2.transform.position && beat == 9)
            {
                gameObject.transform.position = position3.transform.position;
                gameObject.transform.rotation = position3.transform.rotation;
            }
            else if (gameObject.transform.position == position3.transform.position && beat == 12)
            {
                gameObject.transform.position = position4.transform.position;
                gameObject.transform.rotation = position4.transform.rotation;
            }
        }
    }
}
