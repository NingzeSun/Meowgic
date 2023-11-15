using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class GroundCheck : MonoBehaviour
{
    public Collider2D tileColider;
    Collider2D selfColider2D;
    Player player;
    void Start()
    {
        player = GetComponentInParent<Player>();
        selfColider2D=GetComponent<Collider2D>();
    }
    void Update()
    {
        player.isOnGround = selfColider2D.IsTouching(tileColider);
    }
}
