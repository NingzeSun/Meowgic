using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 2;

    private Vector3 direction;

    //private bool idel = true;
    //public GameObject player;
    float lastAttackTime = -3;
    public Transform player;

    public Rigidbody2D rb;
    public Transform leftpoint, rightpoint;
    private bool Faceleft = false;
    public float speed;
    public float leftx ,rightx;
    private Color OriginalColor;
    private SpriteRenderer sr;
    public float flashtime;


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
        sr = GetComponent<SpriteRenderer>();
        OriginalColor = sr.color;
        rb = GetComponent<Rigidbody2D>();
        transform.DetachChildren();
        leftx = leftpoint.position.x;
        rightx = rightpoint.position.x;
        Destroy(leftpoint.gameObject);
        Destroy(rightpoint.gameObject);        
    }


    // Update is called once per frame
    void Update()
    {
        movement();

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }


    //Default animation update
    void movement(){
        if(Faceleft){
            rb.velocity = new Vector2(-speed,rb.velocity.y);
            if (transform.position.x < leftx){
                transform.localScale = new Vector3(-transform.localScale.x,transform.localScale.y,1);
                Faceleft = false;
            }
        }
        else{
            rb.velocity = new Vector2(speed,rb.velocity.y);
            if (transform.position.x > rightx){
                transform.localScale = new Vector3(-transform.localScale.x,transform.localScale.y,1);
                Faceleft = true;
            }
        }
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

        if (Time.time - lastAttackTime >= 0 && player != null)  // if the contact is more than 3 seconds, player gets hit again
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
        flashColor(flashtime);
    }

    public void flashColor(float time){
        sr.color = Color.red;
        Invoke("ResetColor",time);
    }

    public void ResetColor(){
        sr.color = OriginalColor;
    }
}
