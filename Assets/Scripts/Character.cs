using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    public float maxHitPoints;
    [SerializeField]
    protected float startHitPoints;
    public abstract void ResetCharacter();
    public abstract IEnumerator DamageCharacter(int damage, float interval);
    public virtual void KillCharacter()
    {
        Destroy(gameObject);
    }
    public virtual IEnumerator Flicker()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().color = Color.white;
    }
}
