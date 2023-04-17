using UnityEngine;
using GameStateMachine;
using Zenject;
using System;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D), typeof(Rigidbody2D), typeof(Animator))]
public class Plane : MonoBehaviour
{
    public event Action OnDead;

    [SerializeField] private int _jumpForce;
    [SerializeField] private InputZone _inputZone;

    private Rigidbody2D _rigidbody;
    private Animator _animator;

    private Vector2 _spawnPosition;

    private Vector2 _positionWhenJump;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _rigidbody.simulated = false;
        gameObject.SetActive(false);

        _spawnPosition = transform.position;
    }

    private void OnEnable()
    {
        _rigidbody.simulated = true;
        transform.position = _spawnPosition;

        _inputZone.OnClicked += Jump;

        _positionWhenJump = transform.position;
    }

    private void OnDisable()
    {
        _inputZone.OnClicked -= Jump;
    }

    private void Update()
    {
        _animator.SetFloat("Plane delta position", transform.position.y - _positionWhenJump.y);
    }

    private void Jump()
    {
        _rigidbody.velocity = new Vector2(0f, 1.5f);
        _rigidbody.AddForce(Vector3.up * _jumpForce);

        _positionWhenJump = transform.position;
        _animator.SetFloat("Plane delta position", 1);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PointsCounterTrigger>())
            return;

        _animator.SetTrigger("Dead");
        _rigidbody.simulated = false;
        OnDead?.Invoke();        
    }   
}
