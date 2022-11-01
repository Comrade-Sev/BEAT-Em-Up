using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public enum states
    {
        IdleState,
        BlockState
    }

    public class Statez : MonoBehaviour
    {


        states states = states.IdleState;
        public HealthScript health;
        private Animator anim;
        public BeatManager bm;
        public int StateChangeAmount = 5;
        public int currentAmount = 0;
        // Start is called before the first frame update
        void Start()
        {
            FindObjectOfType<BeatManager>().BeatHappened.AddListener(OnBeat);
        }

        // Update is called once per frame
        void Update()
        {
            switch (states)
            {
                case states.IdleState:
                    //anim = gameObject.GetComponent<Animator>();
                    //anim.Play("idle");
                    health.Damage = 10f;
                    if (bm.Hit == true)
                    {
                       // anim = gameObject.GetComponent<Animator>();
                        //anim.Play("hit");
                        bm.Hit = false;
                    }

                    //This is supposed to play the idle animation
                    //all of your code here

                    break;
                case states.BlockState:
                    {
                        //anim = gameObject.GetComponent<Animator>();
                        //anim.Play("block");
                        //if damage stays 0 revert damage back to original damage.
                        health.Damage = 0f;
                        //plays the block animation
                        if (bm.Hit == true)
                        {
                            //anim = gameObject.GetComponent<Animator>();
                            //anim.Play("blockhit");
                            bm.Hit = false;

                        }
                    }
                    break;
            }
            if (bm._beatCount % 5 == 0)
            {
                
                //Debug.Log("beatcount: " + bm._beatCount + "Currentstate: " + states);            
            }
            //else if ((bm._beatCount % 5 == 0) && (states == states.BlockState))
            //{
            //    states = states.IdleState;
            //}
            //Debug.Log(states);
        }

        void OnBeat()
        {
            currentAmount++;
            if(currentAmount > StateChangeAmount)
            {
                if (states == states.IdleState)
                {
                    states = states.BlockState;
                }
                else
                {
                    states = states.IdleState;
                }
                currentAmount = 0;
            }
            Debug.Log("Beat happened" + health.Damage);
        }


    }
}
