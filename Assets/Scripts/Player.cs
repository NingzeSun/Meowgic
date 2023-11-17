using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private Collider2D coll;
    public int damage;

    [Header("Moving Reference")]
    public float speed = 20f;

    float xVelocity;

    [Header("Jumping Reference")]
    public float jumpForce = 8;

    int wallJumpCount = 5;

    [Header("Status")]
    public bool isOnGround;
    bool jumpPress;  // if the player has pressed the jump button
    public bool closeWithLeftWall, closeWithRightWall;
    Animator animator;
    public float goundCheckLength;
    public int health = 10;
    public AudioSource runAudio, onHitAudio, jumpAudio, healAudio;
    public bool canBeHit=true;


    public void OnHit()
    {
        if (canBeHit == true)
        {
            health--;
            if (health >= 1)
            {
                onHitAudio.Play();
                StartCoroutine(Flash());
            }
        }
        

    }

    IEnumerator Flash()
    {
        canBeHit = false;
        Material material = GetComponent<SpriteRenderer>().material;
        for (int i = 0; i < 6; i++)
        {
            if (material.GetInt("_BeAttack") == 1)
                material.SetInt("_BeAttack", 0);
            else
                material.SetInt("_BeAttack", 1);
            yield return new WaitForSeconds(.05f);  // When a yield statement is used, the coroutine pauses execution and automatically resumes at the next time.
        }
        material.SetInt("_BeAttack", 0);
        canBeHit = true;

    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Jump();

        closeWithRightWall = (Physics2D.Raycast(transform.position, transform.right, 1, LayerMask.GetMask("Ground")).collider != null);
        closeWithLeftWall = (Physics2D.Raycast(transform.position, -transform.right, 1, LayerMask.GetMask("Ground")).collider != null);
        if (!animator.GetBool("attackCombo") && !animator.GetBool("attackCombo2") && AttackComboBehaviour.attacking == false)
        {
            if (rb.velocity.y > 0.1)
                animator.Play("Jump");
            if (rb.velocity.y < -0.1)
                animator.Play("Fall");
        }
        if (Input.GetButtonDown("Jump") && wallJumpCount > 0)
        {
            jumpPress = true;
        }

        if (health <= 0)
        {
            animator.Play("Death");
            //Destroy(gameObject);
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    void FixedUpdate()
    {
        Move();

        isOnGroundCheck();
    }

    void isOnGroundCheck()
    {
        ////determine if the player is colliding with the groundLayer which is just any 'Ground' layer object
        
    }

    void Move()
    {
        xVelocity = Input.GetAxisRaw("Horizontal");
        animator.SetBool("IsMove", xVelocity != 0);

        //transform.Translate(xVelocity * speed, 0, 0);
        transform.Translate(xVelocity * speed, 0, 0);

        //flipping
        if (xVelocity != 0)
        {
            transform.localScale = new Vector3(-xVelocity, 1, 1);
            if (!runAudio.isPlaying && isOnGround)
                runAudio.Play();
        }
        else
        {
            runAudio.Stop();
        }
    }

    void Jump()
    {
        //The player is on the ground
        if (isOnGround)
        {
            wallJumpCount = 5;
        }
        //Jumpng from the ground
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpAudio.Play();

        }
        if (Input.GetKeyDown(KeyCode.Space) && !isOnGround&&wallJumpCount>0)
        {
            if (closeWithLeftWall)
            {
                wallJumpCount--;
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                jumpAudio.Play();
            }
            if (closeWithRightWall)
            {
                wallJumpCount--;
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                jumpAudio.Play();
            }
        }
    }

    public void Heal()
    {
        health += 2;
        healAudio.Play();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        /*if (collision.gameObject.name.Contains("Wall"))
        {
            if (collision.contacts[0].point.x > transform.position.x)
                closeWithRightWall = true;
            if (collision.contacts[0].point.x < transform.position.x)
                closeWithLeftWall = true;
        }*/
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        /*if (collision.gameObject.name.Contains("Wall"))
        {
            if (collision.transform.position.x > transform.position.x)
                closeWithRightWall = false;
            if (collision.transform.position.x < transform.position.x)
                closeWithLeftWall = false;
        }*/
    }
}