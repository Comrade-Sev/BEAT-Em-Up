using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using CommonUsages = UnityEngine.XR.CommonUsages;

namespace Game
{
    public class BeatManager : MonoBehaviour
    {
        public HealthScript OppHealth;
        public Statez sz;
        
        //public ActionBasedController actionBasedController;
        //public GameObject toBeAttacked;

        public float Damage = 10f;
        public ParticleSystem hitEffect;
        public ParticleSystem blockEffect;
        private float _timer;
        public float beat = (60 / 90);
        public float beatCount;
        public bool hit;
        public UnityEvent beatHappened;
        public HealthBar healthBar;
        public AudioSource source;
        public AudioClip hitSound;
        public AudioClip blockSound;
        public bool InputVR = false;
        public bool stillHolding;
        private UnityEngine.XR.InputDevice Device;
        
        //Renderer rend;
        //Color currentColor;
        //Color _originalColor;
        //public Material colour;
        
        
        //An Event is just something that happened. You can link an action to an event which is what
        //I did for when a beat happened

        //Opponent Object begins looking for and only respond to the glove prefabs which are tagged
        //"HostileObject"
        
                
        private void GetInputs()
        {
            bool triggerValue;
            if (Device.TryGetFeatureValue(CommonUsages.triggerButton, out triggerValue) && triggerValue)
            {
                Debug.Log("Trigger button is pressed.");
            }
            
            /*var thisController = this.gameObject.GetComponentInParent<ActionBasedController>(
            ).selectAction.action.ReadValue<float>();
            
            if (thisController > 0.5f && !stillHolding)
            {
                stillHolding = true;

                Debug.Log("grab trigger pressed");
            }
            else if(thisController < 0.5f && stillHolding)
            {
                stillHolding = false;
                Debug.Log("grab trigger let go");
            }*/
        }
        void Awake()
        {
            //toBeAttacked = GameObject.FindGameObjectWithTag("HostileObject");
            beatHappened = new UnityEvent();
            
            //_originalColor = rend.material.color;
            //rend = gameObject.GetComponent<Renderer>();
        }

        void OnTriggerEnter(Collider collision)
        {
            if (_timer > 0.8 * beat || _timer < 0.15f)
            {
                hit = true;
                Debug.Log(OppHealth.currentHealth);
                OppHealth.GetHit();
                healthBar.SetHealth(OppHealth.currentHealth);

                if (collision.gameObject.CompareTag("DamageBlock"))
                {
                    var hit = collision.gameObject.GetComponent<Collider>(
                        ).ClosestPointOnBounds(transform.position);
                    var rot = transform.rotation;
                    var currentState = sz.oppStates;
                    
                    /*if (currentState == states.AttackState)
                    {
                        source.PlayOneShot(hitSound);
                        var hitTrue = Instantiate (hitEffect, hit, rot);
                        Destroy(hitTrue, 0.5f);
                    }*/
                    
                    if (currentState == states.BlockState)
                    {
                        source.PlayOneShot(blockSound);
                        var blockTrue = (Instantiate(blockEffect, hit, rot));
                        Destroy(blockTrue, 0.5f);
                    }
                    else
                    {
                        source.PlayOneShot(hitSound);
                        var hitTrue = Instantiate (hitEffect, hit, rot);
                        Destroy(hitTrue, 0.5f);
                    }
                }

                /*if (Input.GetKeyDown(KeyCode.Q))
                {
                    InputVR = true;
                }*/
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
                    
                    beatCount++;
                    if (beatCount == 13)
                    {
                        beatCount = 1;
                    }
                }

                if (Device.TryGetFeatureValue(CommonUsages.triggerButton, out var triggerValue) && triggerValue)
                {
                    InputVR = true;
                }

                if (Input.GetKeyDown(KeyCode.Q))
                {
                    InputVR = true;
                }
                _timer += Time.deltaTime;
                //Debug.Log(InputVR);
            }
        }
        void Update()
        {
            GetInputs();
        }
    }
}
