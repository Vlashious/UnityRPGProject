using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField]
    private float _movementSpeed = 3.0f;
    private enum CharStates
    {
        walkEast = 1,
        walkSouth = 2,
        walkWest = 3,
        walkNorth = 4,
        idle = 5
    }
    private Vector2 _movement = new Vector2();
    private Rigidbody2D _rBody;
    private Animator _animator;
    private string animState = "AnimationState";
    void Start()
    {
        _rBody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }
    void Update()
    {
        UpdateState();
    }

    private void UpdateState()
    {
        if (_movement.x > 0) _animator.SetInteger(animState, (int)CharStates.walkEast);
        else if (_movement.x < 0) _animator.SetInteger(animState, (int)CharStates.walkWest);
        else if (_movement.y > 0) _animator.SetInteger(animState, (int)CharStates.walkNorth);
        else if (_movement.y < 0) _animator.SetInteger(animState, (int)CharStates.walkSouth);
        else _animator.SetInteger(animState, (int)CharStates.idle);
    }

    void FixedUpdate()
    {
        MoveCharacter();
    }

    private void MoveCharacter()
    {
        _movement.x = Input.GetAxisRaw("Horizontal");
        _movement.y = Input.GetAxisRaw("Vertical");

        _movement.Normalize();

        _rBody.velocity = _movement * _movementSpeed;
    }
}
