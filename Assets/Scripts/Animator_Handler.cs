using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DD
{
    public class Animator_Handler : MonoBehaviour
    {
        public Animator anim;
        int actionType;
        public int currentAttack;
        public bool isActive;
        int vertical;
        int horizontal;
        public bool canRotate;
        public string trigger = "idle";

        public void Initialize()
        {
            anim = GetComponent<Animator>();
            actionType = Animator.StringToHash("ActionType");
            currentAttack = Animator.StringToHash("CurrentAttack");
            vertical = Animator.StringToHash("Vertical");
            horizontal = Animator.StringToHash("Horizontal");

        }

        public void UpdateAnimatorValues(float verticalMovement, float horizontalMovement, string action)
        {
            if(action == "move")
            {
                anim.SetTrigger("Move");
                updateMoveAnimations(verticalMovement, horizontalMovement);
            }
        }

        public void updateMoveAnimations(float verticalMovement, float horizontalMovement)
        {
            //Debug.Log(horizontalMovement);

            #region Vertical
            float v = verticalMovement;

            #endregion

            #region Horizontal
            float h = horizontalMovement;

            float movementAnim = 0;

            if(v != 0 && h == 0)
            {
                movementAnim = 1;
            } else if (v == 0 && h != 0)
            {
                movementAnim = 1;
            } else if(v == 1 && h == 1)
            {
                movementAnim = 1.25f;
            } else if(v == 1 && h == -1)
            {
                movementAnim = 1.5f;
            } else if(v == -1 && h == 1)
            {
                movementAnim = 1.25f;
            } else if(v == -1 && h == -1)
            {
                movementAnim = 1.5f;
            }

            #endregion

            if (trigger != "move" && movementAnim != 0 && isActive == false)
            {
                anim.SetTrigger("Move");

                trigger = "move";
            }

            if (movementAnim != 0 && isActive == false)
            {
                anim.SetFloat(vertical, movementAnim, 0.1f, Time.deltaTime);
                //anim.SetFloat(horizontal, h, 0.1f, Time.deltaTime);
            } else if(trigger != "idle" && isActive == false)
            {
                anim.SetTrigger("Idle");
                trigger = "idle";
            }
        }

        public void updateActionAnimation(float action)
        {


            anim.SetTrigger("Action");
            anim.SetFloat(actionType, action, 0.1f, Time.deltaTime);

            if (action == 0.5f)
            {
                trigger = "2H1";
            } else if(action == 1)
            {
                trigger = "2H2";
            }

            //Invoke("triggerIdleAnimation", 1);
        }

        public void triggerIdle()
        {
            anim.SetTrigger("Idle");
        }

        public void CanRotate()
        {
            canRotate = true;

        }

        public void stopAttacks()
        {
            if(anim.GetInteger("CurrentAttack") == currentAttack)
            {
                currentAttack = 0;
                anim.SetInteger(currentAttack, currentAttack);
            } else
            {
                currentAttack += 1;
                anim.SetInteger(currentAttack, currentAttack);
            }
        }


        public void startAttack()
        {
            currentAttack = 1;
            anim.SetTrigger("Action");
            trigger = "action";
            isActive = true;
            anim.SetInteger("CurrentAttack", 1);
        }


        public void StopRotate()
        {
            Debug.Log("THE ROTATE IS FALSE HEHE");
            canRotate = false;

        }


        public void errorNoMore()
        {
            //Debug.Log("");
        }

        public void attackToIdle(AnimationEvent e)
        {
            
            int attackAnim = anim.GetInteger("CurrentAttack");

            if (e.intParameter < currentAttack && currentAttack < 5 && anim.GetInteger("CurrentAttack") != currentAttack)
            {
                anim.SetInteger("CurrentAttack", currentAttack);
                anim.SetBool("NextAnim", true);

            }
            else if(e.intParameter >= currentAttack || currentAttack >= 5)
            {
                anim.SetInteger("CurrentAttack", 0);
                trigger = "idle";
                //isActive = false;
            }

            StopRotate();
        }

        public void disableNextAnimation()
        {
            anim.SetBool("NextAnim", false);
        }

        public void disableIsActive(AnimationEvent e)
        {
            if (e.intParameter == currentAttack)
            {
                currentAttack = 0;
                isActive = false;
            }

            CanRotate();
        }

        public void triggerJumpAnim()
        {
            Debug.Log("HELLO");
            anim.SetTrigger("Jump");
        }

        public void disableJumpAnim()
        {
            anim.SetTrigger("Land");
        }
    }

}