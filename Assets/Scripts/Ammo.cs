using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    public int damageDone;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider is BoxCollider2D)
        {
            var enemy = collider.gameObject.GetComponent<Enemy>();

            StartCoroutine(enemy.DamageCharacter(damageDone, 0));
            gameObject.SetActive(false);
        }
    }
}
