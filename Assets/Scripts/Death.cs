using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Death : StateMachineBehaviour
{

    //public AudioSource deathAudio;
    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.Play("Death");
        GameObject.Find("DeathAudio").GetComponent<AudioSource>().Play();
    }
    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {   
        Destroy(GameObject.FindGameObjectWithTag("Player"));
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


}

