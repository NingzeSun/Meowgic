// Reference: https://www.cnblogs.com/sanyejun/p/12685963.html

using UnityEngine;

public class ComboAttack : MonoBehaviour {
    public Animator anim;
    public int clickNum = 0;
    public int clickNum2 = 0;
    private float lastClickedTime = 0;
    //The maximum delay between each sequence of a combo attack
    public float maxComboDelay = 0.9f;
    
    private static readonly int AttackCombo = Animator.StringToHash("attackCombo");
    void Start() {
        anim = GetComponent<Animator>();
    }
    void Update() {
        if (Time.time - lastClickedTime > maxComboDelay) {
            clickNum = 0;
            clickNum2 = 0;
        }
        if (Input.GetMouseButtonDown(0) && !anim.GetBool("IsMove")) { //Left mouse click
            lastClickedTime = Time.time;
            clickNum++;
            if (clickNum == 1) {
                anim.SetBool(AttackCombo, true);
            }
            clickNum = Mathf.Clamp(clickNum, 0, 3);
        }
        if (Input.GetMouseButtonDown(1) && !anim.GetBool("IsMove")) { //Right mouse click
            lastClickedTime = Time.time;
            clickNum2++;
            if (clickNum2 == 1) {
                anim.SetBool("attackCombo2", true);
            }
            clickNum2 = Mathf.Clamp(clickNum2, 0, 3);
        }
    }
    public void ComboCheck(int num) {  // Animation event
        if (clickNum >= num) {  //Animation event at the end of animation, so 1>0, 2>1, 3>2
            anim.SetBool("attackCombo", true); //transtion trigger condition
        }
    }
    public void ComboCheck2(int num) {
        if (clickNum2 >= num) {
            anim.SetBool("attackCombo2", true);
        }
    }
    public void ClearComboClickNum() {
        clickNum = 0;
    }
}