using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField]
    private HealthBar _healthBarPrefab;
    HealthBar healthBar;
    [SerializeField]
    private Inventory _invPrefab;
    Inventory _inv;
    private void Start()
    {
        _inv = Instantiate(_invPrefab);
        hitPoints.value = startHitPoints;
        healthBar = Instantiate(_healthBarPrefab);
        healthBar.character = this;
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
        if (hitPoints.value < maxHitPoints)
        {
            hitPoints.value += quantity;
            return true;
        }
        return false;
    }
}
