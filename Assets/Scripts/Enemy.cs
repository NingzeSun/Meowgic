using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 2;
    public float Speed = 10f;
    private float directionTimer;//current number of seconds since last animation frame update
    private float directionTimerMax = 1.0f;//max number of seconds for each frame, defined by Framerate
    private Vector3 direction;

    private bool idel = true;
    public GameObject player;
    float lastAttackTime = -3;
    //public Transform player;

    // Update is called once per frame
    void Update()
    {
        DirectionUpdate();
        transform.position += direction * (Time.deltaTime * Speed);

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    //Default animation update
    protected void DirectionUpdate()
    {
        //directionTimer += Time.deltaTime;

        //if(directionTimer > directionTimerMax)
        if (idel)
        {
            //directionTimer = 0;
            direction = new Vector3(Random.Range(-0.5f, 0.5f), 0);
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

        /*if ((transform.position - player.transform.position).magnitude < 1.8f)
        {
            transform.position += (player.transform.position - transform.position).normalized * Time.deltaTime * Speed;
        }*/
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (Time.time-lastAttackTime>=3&&collision.gameObject.TryGetComponent<Player>(out Player player))  // if the contact is more than 3 seconds, player gets hit again
        {
            //print("Attack");
            lastAttackTime=Time.time;
            player.OnHit();
        }
    }

    public void OnHit()
    {
        health--;

        /*if (health <= 0)
        {
            Destroy(gameObject);
        }*/
    }
}
