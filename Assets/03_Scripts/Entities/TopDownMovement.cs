using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownMovement : MonoBehaviour
{
    private TopDownCharacterController _controller;

    private CharacterStatsHandler _stats;

    private Vector2 _movementDirection = Vector2.zero;
    private Rigidbody2D _rigidbody;

    private Vector2 _knockback = Vector2.zero;
    private float knockbackDuration = 0.0f;

    private void Awake()
    {
        // GetComponent: 자기의 컴포넌트 중에서 가져오기
        _controller = GetComponent<TopDownCharacterController>();
        _rigidbody = GetComponent<Rigidbody2D>();

        _stats = GetComponent<CharacterStatsHandler>();
    }

    private void Start()
    {
        // Move()를 구독??
        _controller.OnMoveEvent += Move;
        // TopDownCharacterController -> public event Action<Vector2> OnMoveEvent;

    }

    // 물리처리가 끝나고 실행되고 update()보다 느리다
    private void FixedUpdate()
    {
        ApplyMovement(_movementDirection);
        if (knockbackDuration > 0.0f)
        {
            // deltaTime: Update에서 사용
            // FixedUpdate에서는 deltaTime ---> fixedDeltaTime
            knockbackDuration -= Time.fixedDeltaTime;
        }
    }



    private void Move(Vector2 direction)
    {
        _movementDirection = direction;
    }

    public void ApplyKnockback(Transform other, float power, float duration)
    {
        knockbackDuration = duration;
        _knockback = -(other.position - transform.position).normalized * power;
    }



    private void ApplyMovement(Vector2 direction)
    {
        // direction = direction * 5;
        direction = direction * _stats.CurrentStats.speed;

        if (knockbackDuration > 0.0f)
        {
            direction += _knockback;
        }
        _rigidbody.velocity = direction;
    }



}
