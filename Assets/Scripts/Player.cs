using UnityEngine;

public class Player : MonoBehaviour {
    private Rigidbody2D rb;
    private Collider2D coll;

    [Header("Moving Reference")]
    public float speed = 8f;

    float xVelocity;

    [Header("Jumping Reference")]
    public float jumpForce = 8f;

    int jumpCount; // the maximum times a player can jump from the ground is one, if less than one(initial value), the player cannot jump

    [Header("Status")]
    public bool isOnGround;

    [Header("Environmental Detection")]
    public LayerMask groundLayer;  // setting the groundLayer to layer 'Ground' 

    bool jumpPress;  // if the player has pressed the jump button
    public bool closeWithLeftWall, closeWithRightWall;
    Animator animator;

    Vector3 lastFramePos;
    

    public int health = 10;
    public void OnHit() {
        health--;
        if (health <= 0)
            Destroy(gameObject);
    }
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
    }


    void Update() {
        Debug.DrawRay(transform.position, transform.right);
        if (rb.velocity.y > 0.1)
            animator.Play("Jump");
        if (rb.velocity.y < -0.1)
            animator.Play("Fall");
        //if(rb.velocity.x !=0)
        //    animator.Play("Run");
        if (Input.GetButtonDown("Jump") && jumpCount > 0) {
            jumpPress = true;
        }
        lastFramePos = transform.position;
    }

    void FixedUpdate() {
        isOnGroundCheck();
        Move();
        Jump();
    }

    void isOnGroundCheck() {
        ////determine if the player is colliding with the groundLayer which is just any 'Ground' layer object
        if (coll.IsTouchingLayers(groundLayer)) {
            isOnGround = true;
        } else {
            isOnGround = false;
        }
    }

    void Move() {
        xVelocity = Input.GetAxisRaw("Horizontal");
        animator.SetBool("IsMove", xVelocity != 0);
        transform.Translate(xVelocity * speed, 0, 0);
        //flipping
        if (xVelocity != 0) {
            transform.localScale = new Vector3(-xVelocity, 1, 1);
        }
        
    }

    void Jump() {

        //The player is on the ground
        if (isOnGround) {
            jumpCount = 1;
        }
        //Jumpng from the ground
        if (jumpPress && isOnGround) {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpCount--;
            jumpPress = false;

        }

        if (jumpPress && !isOnGround) {
            float wallJumpForce = 5;
            if (closeWithLeftWall) {
                rb.velocity = new Vector2(wallJumpForce, jumpForce);
                jumpPress = false;

            }
            if (closeWithRightWall) {
                rb.velocity = new Vector2(-wallJumpForce, jumpForce);
                jumpPress = false;

            }
        }


    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.name.Contains("Wall")) {
            if (collision.contacts[0].point.x > transform.position.x)
                closeWithRightWall = true;
            if (collision.contacts[0].point.x < transform.position.x)
                closeWithLeftWall = true;
        }
    }
    void OnCollisionExit2D(Collision2D collision) {
        if (collision.gameObject.name.Contains("Wall")) {
            if (collision.transform.position.x > transform.position.x)
                closeWithRightWall = false;
            if (collision.transform.position.x < transform.position.x)
                closeWithLeftWall = false;
        }
    }
}