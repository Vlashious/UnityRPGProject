using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Animator))]
public class Wander : MonoBehaviour
{
    [SerializeField]
    private float _pursuitSpeed;
    [SerializeField]
    private float _wanderSpeed;
    private float _currentSpeed;

    [SerializeField]
    float _directionChangeInterval;

    [SerializeField]
    bool _followPlayer;

    Coroutine _moveCoroutine;
    Rigidbody2D _rb2d;
    Animator _animator;

    Transform _targetTransform = null;
    Vector3 _endPos;
    float _currAngle = 0;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _currentSpeed = _wanderSpeed;
        _rb2d = GetComponent<Rigidbody2D>();

        StartCoroutine(WanderRoutine());
    }

    private IEnumerator WanderRoutine()
    {
        while (true)
        {
            ChooseNewEndPoint();
            if (_moveCoroutine != null)
            {
                StopCoroutine(_moveCoroutine);
            }
            _moveCoroutine = StartCoroutine(Move(_rb2d, _currentSpeed));

            yield return new WaitForSeconds(_directionChangeInterval);
        }
    }

    private IEnumerator Move(Rigidbody2D rb2d, float currentSpeed)
    {
        var remainingDistance = (transform.position - _endPos).sqrMagnitude;
        while(remainingDistance > float.Epsilon)
        {
            if(_targetTransform)
            {
                _endPos = _targetTransform.position;
            }

            if(rb2d)
            {
                _animator.SetBool("isWalking", true);
                var newPos = Vector3.MoveTowards(rb2d.position, _endPos, currentSpeed * Time.deltaTime);

                rb2d.MovePosition(newPos);
                remainingDistance = (transform.position - _endPos).sqrMagnitude;
            }

            yield return new WaitForFixedUpdate();
        }
        _animator.SetBool("isWalking", false);
    }

    private void ChooseNewEndPoint()
    {
        _currAngle += UnityEngine.Random.Range(0, 360);
        _currAngle = Mathf.Repeat(_currAngle, 360);

        _endPos += Vector3FromAngle(_currAngle);
    }

    private Vector3 Vector3FromAngle(float currAngle)
    {
        currAngle *= Mathf.Deg2Rad;
        return new Vector3(Mathf.Cos(currAngle), Mathf.Sin(currAngle), 0);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player") && _followPlayer)
        {
            _currentSpeed = _pursuitSpeed;
            _targetTransform = collider.gameObject.transform;
            if(_moveCoroutine != null)
            {
                StopCoroutine(_moveCoroutine);
            }

            _moveCoroutine = StartCoroutine(Move(_rb2d, _currentSpeed));
        }
    }
}
