using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    [SerializeField]
    private float _hitPoints;
    [SerializeField]
    private int _damageStrength;
    private Coroutine _damageCoroutine;

    void OnEnabled()
    {
        ResetCharacter();
    }

    public override IEnumerator DamageCharacter(int damage, float interval)
    {
        while (true)
        {
            _hitPoints -= damage;
            if (_hitPoints <= float.Epsilon)
            {
                KillCharacter();
                break;
            }
            if (interval > float.Epsilon)
            {
                yield return new WaitForSeconds(interval);
            }
            else break;
        }
    }

    public override void ResetCharacter()
    {
        _hitPoints = startHitPoints;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            var player = collision.gameObject.GetComponent<Player>();
            print("heh");
            if (_damageCoroutine == null)
            {
                _damageCoroutine = StartCoroutine(player.DamageCharacter(_damageStrength, 1f));
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (_damageCoroutine != null)
        {
            StopCoroutine(_damageCoroutine);
            _damageCoroutine = null;
        }
    }
}
