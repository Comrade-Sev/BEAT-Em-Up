using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public enum states
    {
        IdleState,
        BlockState,
        AttackState,
        ParryState
    }

    public class Statez : MonoBehaviour
    {
        public states oppStates = states.IdleState;
        public HealthScript oppHealth;
        public PlayerHealth playerHealth;
        public oppPunch attackPlayer;
        public punchDestroy pd;
        private Animator anim;
        public BeatManager bm;
        public bool Ready = false;
        public bool DamageBlocked = false;

        //currentAmount keeps track of the amount of beats that have happened and StateChangeAmount is a set value that gets used later to indicate when the state should change
        // Start is called before the first frame update    
        public int StateChangeAmount = 5;
        public int currentAmount = 0;

        void Start()
        {
            FindObjectOfType<BeatManager>().beatHappened.AddListener(OnBeat);
            anim = gameObject.GetComponent<Animator>();
            //This makes it so that the void OnBeat can notice when it needs to switch oppStates
        }

        // Update is called once per frame
        void Update()
        {
            switch (oppStates)
            {
                case states.IdleState:
                    Ready = true;
                    //anim.SetBool("isStun", false);
                    anim = gameObject.GetComponent<Animator>();
                    anim.Play("enemy_done|Idle");
                    oppHealth.playerDamage = 5f;
                    if (bm.hit == true)
                    {
                        //anim.SetBool("isIdle", false);
                        //anim.SetBool("isHit", true);
                        anim = gameObject.GetComponent<Animator>();
                        anim.Play("enemy_done|hit");
                        bm.hit = false;
                    }

                    //This is supposed to play the idle animation
                    //all of your code here

                    break;
                case states.BlockState:
                    {
                        //anim.SetBool("isHit", false);
                        //Debug.Log("blocked");
                        anim.SetBool("isBlock", true);
                        anim = gameObject.GetComponent<Animator>();
                        anim.Play("enemy_done|block");
                        //if damage stays 0 revert damage back to original damage.
                        oppHealth.playerDamage = 0f;
                        //plays the block animation
                        if (bm.hit == true)
                        {
                            //anim.SetBool("isBlock", false);
                            //anim.SetBool("isBlockHit", true);
                            anim = gameObject.GetComponent<Animator>();
                            anim.Play("enemy_done|block_hit");
                            bm.hit = false;

                        }
                    }
                    break;

                case states.AttackState:
                {
                        anim.SetBool("isBlock", false);
                        //anim.SetBool("isBlockHit", false);
                        oppHealth.playerDamage = 5f;
                    if (Ready == true)
                    {
                        Ready = false;
                        attackPlayer.triggered = true;
                        anim = gameObject.GetComponent<Animator>();
                        anim.Play("enemy_done|punch_right");
                        }
                    else
                    {
                        attackPlayer.triggered = false;
                    }

                    if ((DamageBlocked == true) && (bm.InputVR == true))
                    {
                        oppStates = states.ParryState;
                        DamageBlocked = false;
                    }
                    /*for (oppStates = oppStates.AttackState)
                    {
                        oppPunch.triggered = true;
                        
                    }*/

                }
                    break;

                case states.ParryState:
                    {
                        anim.SetBool("isStun", true);
                        anim = gameObject.GetComponent<Animator>();
                        anim.Play("enemy_done|stunned");
                        oppHealth.playerDamage = 10f;
                        if (bm.hit == true)
                        {
                            oppStates = states.IdleState;
                            bm.hit = false;
                        }

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
                switch (oppStates)
                {
                    case states.IdleState:
                        oppStates = states.BlockState;
                        break;
                    case states.BlockState:
                        oppStates = states.AttackState;
                        break;
                    case states.AttackState:
                        oppStates = states.IdleState;
                        break;
                }

                currentAmount = 0;
            }
            Debug.Log("Beat happened" + oppHealth.playerDamage);
        }


    }
}
