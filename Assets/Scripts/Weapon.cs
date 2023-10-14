using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    List<Enemy> enemysOnRange = new List<Enemy>();
    public void Attack() {

        for (int i = 0; i < enemysOnRange.Count; i++) {
            enemysOnRange[i].OnHit();
        }
        //foreach (Enemy e in enemysOnRange) {
        //    print(e);
        //    e.OnHit();
        //}
    }

    void OnTriggerEnter2D(Collider2D col) {
        col.gameObject.TryGetComponent<Enemy>(out Enemy enemy);
        enemysOnRange.Add(enemy);
    }
    void OnTriggerExit2D(Collider2D col) {
        col.gameObject.TryGetComponent<Enemy>(out Enemy enemy);
        enemysOnRange.Remove(enemy);
    }

}
