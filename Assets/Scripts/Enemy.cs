using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 2;
    public float Speed = 5f;
    private float directionTimer;//current number of seconds since last animation frame update
    private float directionTimerMax = 1.0f;//max number of seconds for each frame, defined by Framerate
    private Vector3 direction;

    //private bool idel = true;
    //public GameObject player;
    float lastAttackTime = -3;
    public Transform player;

    Rigidbody2D rb;
    Transform target;
    Vector2 moveDirection;


    /*public LayerMask enemyMask;
    public float speed = 10;
    Rigidbody2D myBody;
    Transform myTrans;
    float myWidth, myHeight;*/


    void Start()
    {
        /*myTrans = this.transform;
        myBody = this.GetComponent<Rigidbody2D>();

        SpriteRenderer mySprite = this.GetComponent<SpriteRenderer>();

        myWidth = mySprite.bounds.extents.x;
        myHeight = mySprite.bounds.extents.y;*/

        rb = GetComponent<Rigidbody2D>();
        target = GameObject.Find("Character").transform;
        direction.x = Random.Range(-0.5f, 0.5f);
    }


    // Update is called once per frame
    void Update()
    {
        DirectionUpdate();
        transform.position += direction * (Time.deltaTime * Speed);

        if ( (transform.position - target.transform.position).magnitude < 3f )
        {
            Vector3 direction = (target.position - transform.position).normalized;

            //Vector3 direction = (target.position - transform.position).normalized * Time.deltaTime * Speed;

            //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            //rb.rotation = angle;
            moveDirection = direction;

            //transform.position += (player.transform.position - transform.position).normalized * Time.deltaTime * Speed;
        }

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        if ( (transform.position - target.transform.position).magnitude < 3f )
        {
            rb.velocity = new Vector2(moveDirection.x, moveDirection.y) * Speed * 0.5f;

            //rb.velocity = new Vector2(moveDirection.x, moveDirection.y).normalized * Time.deltaTime * Speed;
        }
    }

    //Default animation update
    protected void DirectionUpdate()
    {
        /*directionTimer += Time.deltaTime;

        if (directionTimer > directionTimerMax)
        {
            directionTimer = 0;
            direction = new Vector3(Random.Range(-1f, 1f), 0);
        }*/

        if (direction.x >= 1f)
        {
            directionTimer = 0;
            direction = new Vector3(-1f, 0);
        }

        if (direction.x <= -1f)
        {
            directionTimer = 0;
            direction = new Vector3(1f, 0);
        }

        /*//if(directionTimer > directionTimerMax)
        if (idel == true)
        {
            if (directionTimer > directionTimerMax)
            {
                directionTimer = 0;
                direction = new Vector3(Random.Range(-0.5f, 0.5f), 0);
            }
                
        }
        else
        {
            transform.position += (player.transform.position - transform.position).normalized * Time.deltaTime * Speed;
        }

        if ((transform.position - player.transform.position).magnitude < 1.8f)
        {
            idel = false;
        }
        else
        {
            idel = true;
        }

        if ( (transform.position - player.transform.position).magnitude < 1.8f )
        {
            transform.position += (player.transform.position - transform.position).normalized * Time.deltaTime * Speed;
        }*/
    }

    /*void FixedUpdate()
    {
        //Use this position to cast the isGrounded/isBlocked lines from
        Vector2 lineCastPos = myTrans.position.toVector2() - myTrans.right.toVector2() * myWidth + Vector2.up * myHeight;
        //Check to see if there's ground in front of us before moving forward
        //NOTE: Unity 4.6 and below use "- Vector2.up" instead of "+ Vector2.down"
        Debug.DrawLine(lineCastPos, lineCastPos + Vector2.down);

        bool isGrounded = Physics2D.Linecast(lineCastPos, lineCastPos + Vector2.down, enemyMask);

        //Check to see if there's a wall in front of us before moving forward
        Debug.DrawLine(lineCastPos, lineCastPos - myTrans.right.toVector2() * 0.05f);

        bool isBlocked = Physics2D.Linecast(lineCastPos, lineCastPos - myTrans.right.toVector2() * 0.05f, enemyMask);

        //If theres no ground, turn around. Or if I hit a wall, turn around
        if (!isGrounded || isBlocked)
        {
            Vector3 currRot = myTrans.eulerAngles;
            currRot.y += 180;
            myTrans.eulerAngles = currRot;
        }

        //Always move forward
        Vector2 myVel = myBody.velocity;
        myVel.x = -myTrans.right.x * speed;
        myBody.velocity = myVel;
    }*/

    void OnCollisionEnter2D(Collision2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();

        if (Time.time - lastAttackTime >= 3 && player != null)  // if the contact is more than 3 seconds, player gets hit again
        {
            //print("Attack");
            lastAttackTime = Time.time;
            player.OnHit();
        }

        if (player == null)
        {
            direction = -direction;
        }
    }

    public void OnHit()
    {
        health--;
    }
}
