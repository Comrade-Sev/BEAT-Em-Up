using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public enum states
    {
        IdleState,
        BlockState,
        AttackState
    }

    public class Statez : MonoBehaviour
    {


        states states = states.IdleState;
        public HealthScript oppHealth;
        public PlayerHealth playerHealth;
        public oppPunch attackPlayer;
        private Animator anim;
        public BeatManager bm;
        public bool Ready = false;

        //currentAmount keeps track of the amount of beats that have happened and StateChangeAmount is a set value that gets used later to indicate when the state should change
        // Start is called before the first frame update    
        public int StateChangeAmount = 5;
        public int currentAmount = 0;

        void Start()
        {
            FindObjectOfType<BeatManager>().beatHappened.AddListener(OnBeat);
            //This makes it so that the void OnBeat can notice when it needs to switch states
        }

        // Update is called once per frame
        void Update()
        {
            switch (states)
            {
                case states.IdleState:
                    Ready = true;
                    //anim = gameObject.GetComponent<Animator>();
                    //anim.Play("idle");
                    oppHealth.playerDamage = 5f;
                    if (bm.hit == true)
                    {
                       // anim = gameObject.GetComponent<Animator>();
                        //anim.Play("hit");
                        bm.hit = false;
                    }

                    //This is supposed to play the idle animation
                    //all of your code here

                    break;
                case states.BlockState:
                    {
                        //Debug.Log("blocked");
                        //anim = gameObject.GetComponent<Animator>();
                        //anim.Play("block");
                        //if damage stays 0 revert damage back to original damage.
                        oppHealth.playerDamage = 0f;
                        //plays the block animation
                        if (bm.hit == true)
                        {
                            //anim = gameObject.GetComponent<Animator>();
                            //anim.Play("blockhit");
                            bm.hit = false;

                        }
                    }
                    break;

                case states.AttackState:
                {
                    oppHealth.playerDamage = 5f;
                    if (Ready == true)
                    {
                        Ready = false;
                        attackPlayer.triggered = true;
                    }
                    else
                    {
                        attackPlayer.triggered = false;
                    }
                    /*for (states = states.AttackState)
                    {
                        oppPunch.triggered = true;
                        
                    }*/

                }
                    break;
            }
        }

        void OnBeat()
        {
            currentAmount++;
            if(currentAmount > StateChangeAmount)
            //If current amount reaches the beat amount of StateChangeAmount it tells it to change state and reset the value of currentAmount
            {
                if (states == states.IdleState)
                {
                    states = states.BlockState;
                }
                else if (states == states.BlockState)
                {
                    states = states.AttackState;
                }
                else if (states == states.AttackState)
                {
                    states = states.IdleState;
                }
                currentAmount = 0;
            }
            //Debug.Log("Beat happened" + oppHealth.playerDamage);
        }


    }
}
