using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    BoxCollider2D boxCollider;
    AudioSource audioSource;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
            col.GetComponent<Player>().OnHitbyMage();
    }
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
        audioSource = GetComponent<AudioSource>();
    }
    public void Atack()
    {
        StartCoroutine(IE_Attack());

    }
    public IEnumerator IE_Attack()
    {
        spriteRenderer.color = new Color(1,1,1,.1f);
        yield return new WaitForSeconds(2);
        Vector3 startPos= transform.position;
        transform.localPosition = new Vector3(transform.localPosition.x,-13.75f,30);
        boxCollider.enabled = true;
        spriteRenderer.color = new Color(1, 1, 1, 1f);
        audioSource.Play();
        //start to rise
        yield return transform.DOLocalMoveY(1.394f,2).WaitForCompletion();//rising
        yield return new WaitForSeconds(1);
        spriteRenderer.color = new Color(1, 1, 1, 0f);
        boxCollider.enabled = false;


    }
}
