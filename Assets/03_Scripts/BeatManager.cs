using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class BeatManager : MonoBehaviour
    {
        public HealthScript health;
        public GameObject toBeAttacked;
        public Material colour;
        private float _timer;
        public float beat = (60 / 90);
        Renderer rend;
        Color currentColor;
        Color _originalColor;
        private float _beatCount;

        //Opponent Object begins looking for and only respond to the glove prefabs which are tagged "HostileObject"
        void Awake()
        {
            toBeAttacked = GameObject.FindGameObjectWithTag("HostileObject");
            rend = gameObject.GetComponent<Renderer>();
            _originalColor = rend.material.color;
        }

        void OnTriggerEnter(Collider collision)
        {
            Debug.Log(rend.material.color);
            Debug.Log(health._CurrentHealth);
            if (_timer > 0.8 * beat || _timer < 0.1f)
            {
                rend.material.color = Color.green;
                health.GetHit();
            }
            //gameObject.GetComponent<MeshRenderer>().material = colour;
        }
        
        void Update()
        {
            if(_timer>beat)
            {
                if(rend.material.color == _originalColor)
                {
                    rend.material.color = Color.red;
                    currentColor = rend.material.color;
                }
                else
                {
                    rend.material.color = Color.blue;
                    _originalColor = rend.material.color;
                }
                _timer -= beat;
                _beatCount = _beatCount +1;
            }
            _timer += Time.deltaTime;
        }
    }
}
