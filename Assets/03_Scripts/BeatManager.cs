using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class BeatManager : MonoBehaviour
    {
        public HealthScript OppHealth;

        public float Damage = 10f;
        public GameObject toBeAttacked;
        public GameObject hitEffect;
        public ParticleSystem hitEffect2;
        public Material colour;
        private float _timer;
        public float beat = (60 / 90);
        
        Renderer rend;
        Color currentColor;
        Color _originalColor;
        
        public float _beatCount;
        public bool Hit;
        public UnityEvent BeatHappened;
        public HealthBar healthBar;
        public AudioSource source;
        public AudioClip hitSound;
        public bool InputVR = false;
        
        
        //An Event is just something that happened. You can link an action to an event which is what I did for when a beat happened

        //Opponent Object begins looking for and only respond to the glove prefabs which are tagged "HostileObject"
        void Awake()
        {
            toBeAttacked = GameObject.FindGameObjectWithTag("HostileObject");
            rend = gameObject.GetComponent<Renderer>();
            _originalColor = rend.material.color;
            BeatHappened = new UnityEvent();
        }

        void OnTriggerEnter(Collider collision)
        {
            if (_timer > 0.8 * beat || _timer < 0.2f)
            {
                Hit = true;
                source.PlayOneShot(hitSound);
                Debug.Log(OppHealth._CurrentHealth);
                rend.material.color = Color.green;
                OppHealth.GetHit();
                healthBar.SetHealth(OppHealth._CurrentHealth);

                if (collision.gameObject.CompareTag("DamageBlock"))
                {
                    Debug.Log("YO MAMAMMAMAMMAMAMMAMAMMAM");
                    var hit = collision.gameObject.GetComponent<Collider>(
                        ).ClosestPointOnBounds(transform.position);
                    var hitTrue = Instantiate (hitEffect2, hit, transform.rotation);
                    Destroy(hitTrue, 1f);
                }

                if (Input.GetKeyDown(KeyCode.Q))
                {
                    InputVR = true;
                }
            }
            //gameObject.GetComponent<MeshRenderer>().material = colour;
            //Debug.Log(OppHealth._CurrentHealth);
        }
        
        private void FixedUpdate()
        {

            {
                healthBar.SetHealth(OppHealth._CurrentHealth);
                //Debug.Log(OppHealth.playerDamage);
                if (_timer > beat)
                    {
                        //if (rend.material.color == _originalColor)
                        {
                            //rend.material.color = Color.red;
                            //currentColor = rend.material.color;
                        }
                        //else
                        {
                            //rend.material.color = Color.blue;
                            //_originalColor = rend.material.color;
                        }

                        _timer -= beat;
                        BeatHappened.Invoke();
                        //If a beat happened then BeatHappened logs it and sends that into the Statez script so we know when a beat happened
                        //_beatCount = _beatCount + 1;
                    }

                    _timer += Time.deltaTime;
            }
        }
    }
}
