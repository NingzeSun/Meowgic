using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class BossHealth : MonoBehaviour
{

	public int health = 50;
	
	public GameObject deathEffect;
	public AudioSource bossHitSound;

	public bool isInvulnerable = false;

	

	public void TakeDamage(int damage)
	{	
		if (isInvulnerable)
			return;
		print("a");
		health -= damage;
		bossHitSound.Play();
		if (health <= 25)
		{
			
			GetComponent<Animator>().SetBool("IsEnraged", true);
		}

		if (health <= 0)
		{
			Die();
			Invoke("LoadScene", 2.0f);
		}
	}


	void LoadScene()
    {
		SceneManager.LoadScene("CutScene6");
    }

	void Die()
	{
		Instantiate(deathEffect, transform.position, Quaternion.identity);
		gameObject.SetActive(false);
		//Destroy(gameObject);
	}

}
