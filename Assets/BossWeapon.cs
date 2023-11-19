using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class BossWeapon : MonoBehaviour
{
	public int attackDamage = 20;
	public int enragedAttackDamage = 40;

	public Vector3 attackOffset;
	public float attackRange = 1f;
	public LayerMask attackMask;
    float lastAttackTime = -3;
    public int attackCountRemain = 5;

    public void Attack()
	{
		Vector3 pos = transform.position;
		pos += transform.right * attackOffset.x;
		pos += transform.up * attackOffset.y;

		Collider2D colInfo = Physics2D.OverlapCircle(pos, attackRange, attackMask);
		//print(colInfo);
		if (colInfo != null)
		{
			
            colInfo.GetComponent<Player>()?.OnHit();
        }
        attackCountRemain--;
        lastAttackTime = Time.time;

    }

    public void EnragedAttack()
	{
		Vector3 pos = transform.position;
		pos += transform.right * attackOffset.x;
		pos += transform.up * attackOffset.y;

		Collider2D colInfo = Physics2D.OverlapCircle(pos, attackRange, attackMask);
		if (colInfo != null)
		{
			colInfo.GetComponent<Player>()?.OnHit();
		}
		attackCountRemain--;
		lastAttackTime=Time.time;
	}
	void Update()
	{
        if (Time.time - lastAttackTime >= 3)
        {
            if (GetComponent<Animator>().GetBool("IsEnraged") == true)
                attackCountRemain = 10;
            else
                attackCountRemain = 5;
        }
    }
	void OnDrawGizmosSelected()
	{
		Vector3 pos = transform.position;
		pos += transform.right * attackOffset.x;
		pos += transform.up * attackOffset.y;

		Gizmos.DrawWireSphere(pos, attackRange);
	}
}
