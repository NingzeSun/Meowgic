// Reference: https://www.cnblogs.com/sanyejun/p/12685963.html

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackComboBehaviour : StateMachineBehaviour {
    private static readonly int Attack = Animator.StringToHash("attackCombo");
    Weapon weapon;
    BoxCollider2D boxCollider2D;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        weapon = FindObjectOfType<Weapon>();
        boxCollider2D = weapon.GetComponent<BoxCollider2D>();
        GameObject.Find("Melee Attack Sound").GetComponent<AudioSource>().Play();
        if (animator.GetBool("attackCombo2") == true)  // modify the weapon size aka hitbox area
        {
            SetBoxColliderSizeX(boxCollider2D, 5.0f);
            SetBoxColliderSizeY(boxCollider2D, 1.5f);
        }
        weapon.Attack();
        
        animator.SetBool(Attack, false);
        animator.SetBool("attackCombo2", false);
    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        SetBoxColliderSizeX(boxCollider2D, 1.9f);  // return to the original weapon size
        SetBoxColliderSizeY(boxCollider2D, 0.85f);
    }

    void SetBoxColliderSizeX(BoxCollider2D boxCollider2D, float value)
    {
        Vector2 size = boxCollider2D.size;
        size.x = value;
        boxCollider2D.size = size;
    }

    void SetBoxColliderSizeY(BoxCollider2D boxCollider2D, float value)
    {
        Vector2 size = boxCollider2D.size;
        size.y = value;
        boxCollider2D.size = size;
    }

}