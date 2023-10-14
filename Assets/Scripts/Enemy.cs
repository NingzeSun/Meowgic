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

    void OnCollisionEnter2D(Collision2D collision) {
            if(collision.gameObject.TryGetComponent<Player>(out Player player)) {
                player.OnHit();
            }
    }
    public void OnHit() {
        health--;
        if (health <= 0)
            Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        DirectionUpdate();
        transform.position += direction * (Time.deltaTime * Speed);
    }

    //Default animation update
    protected void DirectionUpdate()
    {
        directionTimer += Time.deltaTime;

        if (directionTimer > directionTimerMax)
        {
            directionTimer = 0;
            direction = new Vector3(Random.Range(-0.1f, 0.1f), 0);
        }
    }
}
