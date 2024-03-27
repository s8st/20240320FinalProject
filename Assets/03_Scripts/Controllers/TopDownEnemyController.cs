using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownEnemyController : TopDownCharacterController
{
    GameManager gameManager;
    protected Transform ClosestTarget { get; private set; }

    protected override void Awake()
    {
        base.Awake();
    }

    protected virtual void Start()
    {
        gameManager = GameManager.instance;
        ClosestTarget = gameManager.Player;
    }

    protected virtual void FixedUpdate()
    {

    }

    protected float DistanceToTarget()
    {
        // Vector3.Distance(Vector3 a, Vector3 b) : a와 b의 거리

        return Vector3.Distance(transform.position, ClosestTarget.position);
    }


    protected Vector2 DirectionToTarget()
    {
        // 방향 현재 위치에서 목표의 바라보는 방향이 나온다 (벡터 차 A-B ---> B에서 A로 가는 방향)
        return (ClosestTarget.position - transform.position).normalized;
    }
}