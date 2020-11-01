using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField]
    private HitPoint _hitPoints;
    [SerializeField]
    private HealthBar _healthBarPrefab;
    HealthBar _healthBar;
    [SerializeField]
    private Inventory _invPrefab;
    Inventory _inv;
    private void Start()
    {
        ResetCharacter();
    }

    private void OnEnabled()
    {
        ResetCharacter();
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Pickable"))
        {
            Item hitItem = collider.gameObject.GetComponent<Consumable>().item;
            if (hitItem != null)
            {
                bool shouldDisappear = false;

                switch (hitItem.type)
                {
                    case Item.ItemType.COIN:
                        shouldDisappear = _inv.AddItem(hitItem);
                        break;
                    case Item.ItemType.HEALTH:
                        shouldDisappear = AdjustHitPoints(hitItem.quantity);
                        break;
                    default:
                        break;
                }
                if (shouldDisappear) collider.gameObject.SetActive(false);
            }
        }
    }

    private bool AdjustHitPoints(int quantity)
    {
        if (_hitPoints.value < maxHitPoints)
        {
            _hitPoints.value += quantity;
            return true;
        }
        return false;
    }

    public override void ResetCharacter()
    {
        _inv = Instantiate(_invPrefab);
        _healthBar = Instantiate(_healthBarPrefab);
        _healthBar.character = this;

        _hitPoints.value = startHitPoints;
    }

    public override void KillCharacter()
    {
        base.KillCharacter();
        Destroy(_healthBar);
        Destroy(_inv);
    }

    public override IEnumerator DamageCharacter(int damage, float interval)
    {
        while (true)
        {
            StartCoroutine(Flicker());
            _hitPoints.value -= damage;
            if (_hitPoints.value <= float.Epsilon)
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
}
