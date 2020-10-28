using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Pickable"))
        {
            Item hitItem = collider.gameObject.GetComponent<Consumable>().item;
            if (hitItem != null)
            {
                print($"hit {hitItem.name}");

                switch (hitItem.type)
                {
                    case Item.ItemType.COIN:
                        break;
                    case Item.ItemType.HEALTH:
                        AdjustHitPoints(hitItem.quantity);
                        break;
                    default:
                        break;
                }
                collider.gameObject.SetActive(false);
            }
        }
    }

    private void AdjustHitPoints(int quantity)
    {
        hitPoints += quantity;
        print($"Adjusted by {quantity}. New value is {hitPoints}.");
    }
}
