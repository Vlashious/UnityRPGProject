using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField]
    private HealthBar healthBarPrefab;
    HealthBar healthBar;
    private void Start()
    {
        hitPoints.value = startHitPoints;
        healthBar = Instantiate(healthBarPrefab);
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
                        shouldDisappear = true;
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
