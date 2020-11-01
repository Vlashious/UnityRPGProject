using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField]
    private float _movementSpeed = 3.0f;
    private Vector2 _movement = new Vector2();
    private Rigidbody2D _rBody;
    private Animator _animator;
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
        if (Mathf.Approximately(_movement.x, 0) && Mathf.Approximately(_movement.y, 0))
        {
            _animator.SetBool("isWalking", false);
        }
        else
        {
            _animator.SetBool("isWalking", true);
        }

        _animator.SetFloat("xDir", _movement.x);
        _animator.SetFloat("yDir", _movement.y);
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
