using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MagicBoss : MonoBehaviour
{
    public int health = 40;
    public AudioSource bossHitSound;
    Animator animator;
    public float leftX,rightX;
    public List<Trap> traps = new List<Trap>();
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        bossHitSound.Play();
        if (health <= 0)
        {
            animator.Play("magicbossdeath");   
            Invoke("LoadScene", 2.0f);
        }
    }
    public void Transport()
    {
        transform.position = new Vector3(Random.Range(leftX,rightX),transform.position.y,transform.position.z);
    }
    public void Attack()
    {
        int index= Random.Range(1, 4);
        while(index<traps.Count-1)
        {
            traps[index].Atack();
            index += Random.Range(1, 4);
        }
    }

    void LoadScene()
    {
        SceneManager.LoadScene("Level2 Scene");
    }
}
