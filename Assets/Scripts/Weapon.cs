using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    List<Enemy> enemysOnRange = new List<Enemy>();
    List<Enemy_behaviour> enemy_Behaviours = new List<Enemy_behaviour>();
    BossHealth bossHealth;
    public AudioSource weaponAudioSource;
    public AudioClip normalHit, hitWall;
    public void Attack()
    {
        for (int i = 0; i < enemysOnRange.Count; i++)
        {
            enemysOnRange[i].OnHit();
        }
        for (int i = 0; i < enemy_Behaviours.Count; i++)
        {
            enemy_Behaviours[i].OnHit();
        }
        bossHealth?.TakeDamage(1);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.TryGetComponent<Enemy>(out Enemy enemy))
            enemysOnRange.Add(enemy);
        if (col.gameObject.TryGetComponent<Enemy_behaviour>(out Enemy_behaviour enemy_behaviour))
            enemy_Behaviours.Add(enemy_behaviour);
        if (col.gameObject.TryGetComponent<BossHealth>(out BossHealth boss_Health))
            bossHealth = boss_Health;
        if (col.name.Contains("Wall"))
            weaponAudioSource.clip = hitWall;
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.TryGetComponent<Enemy>(out Enemy enemy))
            enemysOnRange.Remove(enemy);
        if (col.gameObject.TryGetComponent<Enemy_behaviour>(out Enemy_behaviour enemy_behaviour))
            enemy_Behaviours.Remove(enemy_behaviour);
        if (col.gameObject.TryGetComponent<BossHealth>(out BossHealth boss_Health))
            bossHealth = null;
        if (col.name.Contains("Wall"))
            weaponAudioSource.clip = normalHit;

    }
}

