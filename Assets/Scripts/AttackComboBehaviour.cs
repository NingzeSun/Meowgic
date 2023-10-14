// Reference: https://www.cnblogs.com/sanyejun/p/12685963.html

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackComboBehaviour : StateMachineBehaviour {
    private static readonly int Attack = Animator.StringToHash("attackCombo");

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        FindObjectOfType<Weapon>().Attack();
        GameObject.Find("Melee Attack Sound").GetComponent<AudioSource>().Play();
        animator.SetBool(Attack, false);
        animator.SetBool("attackCombo2", false);
    }
}