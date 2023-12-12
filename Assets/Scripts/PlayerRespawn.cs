using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    public Vector2 respawnPoint;

    public void RespawnNow()
    {
        transform.position = respawnPoint;
        GameObject playerObject = GameObject.Find("Player");
        Player playerScript = playerObject.GetComponent<Player>();
        playerScript.health = 10;
    }
}
