using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GirdHP : MonoBehaviour
{
    Player player;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();

    }

    // Update is called once per frame
    void Update()
    {
        foreach (Transform t in transform)
            t.gameObject.SetActive(false);
        for (int i = 0; i < player.health; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }

    }
}
