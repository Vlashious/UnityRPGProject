using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [SerializeField]
    protected HitPoint hitPoints;

    public float maxHitPoints;
    [SerializeField]
    protected float startHitPoints;
}
