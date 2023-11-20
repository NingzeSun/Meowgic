using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading;

public class BossHealth : MonoBehaviour
{

	public int health = 50;

	public GameObject deathEffect;

	public bool isInvulnerable = false;

	public void TakeDamage(int damage)
	{
		if (isInvulnerable)
			return;
		print("a");
		health -= damage;

		if (health <= 25)
		{
			GetComponent<Animator>().SetBool("IsEnraged", true);
		}

		if (health <= 0)
		{
			Die();
			Thread.Sleep(3000);
			SceneManager.LoadScene("CutScene6");
		}
	}

	void Die()
	{
		Instantiate(deathEffect, transform.position, Quaternion.identity);
		Destroy(gameObject);
	}

}
