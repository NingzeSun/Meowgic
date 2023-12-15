using UnityEngine;

public class AttackComboBehaviour : StateMachineBehaviour
{
    Weapon weapon;
    BoxCollider2D boxCollider2D;
    public static bool attacking=false;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        attacking= true;
        weapon = FindObjectOfType<Weapon>();
        boxCollider2D = weapon.GetComponent<BoxCollider2D>();
        GameObject.Find("Melee Attack Sound").GetComponent<AudioSource>().Play();
        if (animator.GetBool("attackCombo2") == true)  // modify the weapon size aka hitbox area
        {
            SetBoxColliderSizeX(boxCollider2D, 5.0f);
            SetBoxColliderSizeY(boxCollider2D, 1.5f);
        }
        weapon.Attack();
        //Debug.Log("a");
        animator.SetBool("attackCombo", false);
        animator.SetBool("attackCombo2", false);
    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        attacking = false;
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