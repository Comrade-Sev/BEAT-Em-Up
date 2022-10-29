using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class BeatManager : MonoBehaviour
    {
        public GameObject toBeAttacked;
        public Material colour;
        private float _timer;
        public float beat = (60 / 90);
        Renderer _rend;
        Color _currentColor;
        Color _originalColor;
        private float _beatCount;

        //Opponent Object begins looking for and only respond to the glove prefabs which are tagged "HostileObject"
        void Awake()
        {
            toBeAttacked = GameObject.FindGameObjectWithTag("HostileObject");
            _rend = gameObject.GetComponent<Renderer>();
            _originalColor = _rend.material.color;
        }

        void OnTriggerEnter(Collider collision)
        {
            Debug.Log(_rend.material.color);
            if (_timer > 0.7 * beat || _timer < 0.1f)
            {
                _rend.material.color = Color.green;
                //Debug.Log();
            }
            //gameObject.GetComponent<MeshRenderer>().material = colour;
        }
        
        void Update()
        {
            Debug.Log(beat);
            if(_timer>beat)
            {
                if(_rend.material.color == _originalColor)
                {
                    _rend.material.color = Color.red;
                    _originalColor = _rend.material.color;
                }
                else
                {
                    _rend.material.color = Color.blue;
                    _originalColor = _rend.material.color;
                }
                _timer -= beat;
                //_beatCount = _beatCount +1;
                
            }
            _timer += Time.deltaTime;
        }
    }
}
