using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_behaviour : MonoBehaviour
{
    #region Public Variables
    public Transform rayCast;
    public LayerMask raycastMask;
    public float rayCastLength;
    public float attackDistance; //Minimum distance for attack
    public float moveSpeed;
    public float timer; //Timer for cooldown between attacks
    public Transform leftLimit;
    public Transform rightLimit;
    public int health = 2;
    #endregion

    #region Private Variables
    private RaycastHit2D hit;
    private Transform target;
    private Animator anim;
    private float distance; //Store the distance b/w enemy and player
    private bool attackMode;
    private bool inRange; //Check if Player is in range
    private bool cooling; //Check if Enemy is cooling after attack
    private float intTimer;
    private Color OriginalColor;
    private SpriteRenderer sr;
    public float flashtime;
    public AudioSource onHitAudio, DeathAudio;

    #endregion

    Player player;
    //Weapon weapon;

    void Awake()
    {   
        sr = GetComponent<SpriteRenderer>();
        OriginalColor = sr.color;
        SelectTarget();
        intTimer = timer; //Store the inital value of timer
        anim = GetComponent<Animator>();
        player = FindObjectOfType<Player>();
    }

    void Update()
    {
        if (health <= 0)
        {   
            DeathAudio.Play();
            Destroy(gameObject);
        }

        if (!attackMode)
        {
            Move();
        }

        if (!InsideOfLimits() && !inRange && !anim.GetCurrentAnimatorStateInfo(0).IsName("Enemy_attack"))
        {
            SelectTarget();
        }

        if (inRange)
        {
            hit = Physics2D.Raycast(rayCast.position, transform.right, rayCastLength, raycastMask);
            RaycastDebugger();
        }

        //When Player is detected
        if (hit.collider != null)
        {
            EnemyLogic();
        }
        else if (hit.collider == null)
        {
            inRange = false;
        }

        if (inRange == false)
        {
            StopAttack();
        }
    }

    void OnTriggerEnter2D(Collider2D trig)
    {

        if (trig.gameObject.tag == "Player")
        {
            target = trig.transform;
            inRange = true;
            Flip();
        }

    }

    void EnemyLogic()
    {
        distance = Vector2.Distance(transform.position, target.position);

        if (distance > attackDistance)
        {
            StopAttack();
        }
        //else if (attackDistance >= distance && cooling == false && attackMode == true)
        else if (attackDistance >= distance && cooling == false)
        {
            Attack();
            cooling = true;
        }

        if (cooling)
        {
            //anim.SetBool("Attack", false);
            Cooldown();
            //anim.SetBool("Attack", false);
        }
    }

    void Move()
    {
        anim.SetBool("canWalk", true);

        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Enemy_attack"))
        {
            Vector2 targetPosition = new Vector2(target.position.x, transform.position.y);

            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
    }

    void Attack()
    {
        timer = intTimer; //Reset Timer when Player enter Attack Range
        attackMode = true; //To check if Enemy can still attack or not

        anim.SetBool("canWalk", false);
        anim.SetBool("Attack", true);

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Enemy_attack") == true)
        {
            Debug.Log("Enemy attacked");
            player.OnHit();
        }

        //player.OnHit();
    }

    void Cooldown()
    {
        timer -= Time.deltaTime;

        if (timer <= 0 && cooling && attackMode)
        //if (timer <= 0 && cooling)
        {
            cooling = false;
            timer = intTimer;

            //attackMode = true;
            //anim.SetBool("Attack", true);
        }
    }

    void StopAttack()
    {
        //cooling = true;
        cooling = false;
        attackMode = false;
        anim.SetBool("Attack", false);
    }

    void RaycastDebugger()
    {
        if (distance > attackDistance)
        {
            Debug.DrawRay(rayCast.position, transform.right * rayCastLength, Color.red);
        }
        else if (attackDistance > distance)
        {
            Debug.DrawRay(rayCast.position, transform.right * rayCastLength, Color.green);
        }
    }

    public void TriggerCooling()
    {
        cooling = true;
    }

    private bool InsideOfLimits()
    {
        return transform.position.x > leftLimit.position.x && transform.position.x < rightLimit.position.x;
    }

    private void SelectTarget()
    {
        float distanceToLeft = Vector3.Distance(transform.position, leftLimit.position);
        float distanceToRight = Vector3.Distance(transform.position, rightLimit.position);

        if (distanceToLeft > distanceToRight)
        {
            target = leftLimit;
        }
        else
        {
            target = rightLimit;
        }

        //Ternary Operator
        //target = distanceToLeft > distanceToRight ? leftLimit : rightLimit;

        Flip();
    }

    void Flip()
    {
        Vector3 rotation = transform.eulerAngles;

        if (transform.position.x > target.position.x)
        {
            rotation.y = 180;
            Debug.Log("Twist");
        }
        else
        {
            rotation.y = 0;
        }

        //Ternary Operator
        //rotation.y = (currentTarget.position.x < transform.position.x) ? rotation.y = 180f : rotation.y = 0f;

        transform.eulerAngles = rotation;
    }

    public void OnHit()
    {
        health--;
        flashColor(flashtime);
        if (health >=1){onHitAudio.Play();}
        if (health < 1){DeathAudio.Play();}
        
    }


    public void flashColor(float time){
        sr.color = Color.red;
        Invoke("ResetColor",time);
    }

    public void ResetColor(){
        sr.color = OriginalColor;
    }
}