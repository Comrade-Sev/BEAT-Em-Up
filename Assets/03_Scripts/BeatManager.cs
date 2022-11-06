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
        public ParticleSystem hitEffect;
        private float _timer;
        public float beat = (60 / 90);
        public float beatCount;
        public bool hit;
        public UnityEvent beatHappened;
        public HealthBar healthBar;
        public AudioSource source;
        public AudioClip hitSound;
        public bool InputVR = false;
        
        //Renderer rend;
        //Color currentColor;
        //Color _originalColor;
        //public Material colour;
        
        
        //An Event is just something that happened. You can link an action to an event which is what
        //I did for when a beat happened

        //Opponent Object begins looking for and only respond to the glove prefabs which are tagged
        //"HostileObject"
        void Awake()
        {
            toBeAttacked = GameObject.FindGameObjectWithTag("HostileObject");
            beatHappened = new UnityEvent();
            
            //_originalColor = rend.material.color;
            //rend = gameObject.GetComponent<Renderer>();
        }

        void OnTriggerEnter(Collider collision)
        {
            if (_timer > 0.8 * beat || _timer < 0.2f)
            {
                hit = true;
                source.PlayOneShot(hitSound);
                Debug.Log(OppHealth.currentHealth);
                OppHealth.GetHit();
                healthBar.SetHealth(OppHealth.currentHealth);

                if (collision.gameObject.CompareTag("DamageBlock"))
                {
                    var hit = collision.gameObject.GetComponent<Collider>(
                        ).ClosestPointOnBounds(transform.position);
                    var hitTrue = Instantiate (hitEffect, hit, transform.rotation);
                    Destroy(hitTrue, 0.5f);
                }

                if (Input.GetKeyDown(KeyCode.Q))
                {
                    InputVR = true;
                }
            }
            //rend.material.color = Color.green;
            //gameObject.GetComponent<MeshRenderer>().material = colour;
        }
        
        private void FixedUpdate()
        {

            {
                
                healthBar.SetHealth(OppHealth.currentHealth);
                
                if (_timer > beat)
                {
                    
                    //If a beat happened then BeatHappened logs it and sends that into the Statez
                    //script so we know when a beat happened
                    
                    _timer -= beat;
                    beatHappened.Invoke();

                    /*if (rend.material.color == _originalColor)
                        {
                            //rend.material.color = Color.red;
                            //currentColor = rend.material.color;
                        }
                        else
                        {
                            //rend.material.color = Color.blue;
                            //_originalColor = rend.material.color;
                        }*/
                    //_beatCount = _beatCount + 1;
                    
                }

                _timer += Time.deltaTime;
            }
        }
    }
}
