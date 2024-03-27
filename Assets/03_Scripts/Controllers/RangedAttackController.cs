using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttackController : MonoBehaviour
{
    [SerializeField] private LayerMask levelCollisionLayer;

    private RangedAttackData _attackData;
    private float _currentDuration;
    private Vector2 _direction;
    private bool _isReady;

    private Rigidbody2D _rigidbody;
    private SpriteRenderer _spriteRenderer;
    private TrailRenderer _trailRenderer;
    private ProjectileManager _projectileManager;

    public bool fxOnDestory = true;

    private void Awake()
    {
        // GetComponentInChildren : 나와 자식까지 포함해서 컴포넌트 검사
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _trailRenderer = GetComponent<TrailRenderer>();
    }

    // 이동처리
    private void Update()
    {
        if (!_isReady)
        {
            return;
        }

        _currentDuration += Time.deltaTime;

        if (_currentDuration > _attackData.duration)
        {
            DestroyProjectile(transform.position, false);
        }

        _rigidbody.velocity = _direction * _attackData.speed;
    }

    // 트리거 충돌
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 레이어 검사의 비트연산 ---> 빠르다
        // collision.gameObject.layer : 충돌한 객체의 레이어
        // | 비트연산 : 하나라도 1이라면 1 
        // << 비트연산
        // 1 << 3(3번 레이어) --> 1000
        // 좌우항이 같아면 내가 찾는 객체와 충돌했다

        // 벽이랑 충돌
        if (levelCollisionLayer.value == (levelCollisionLayer.value | (1 << collision.gameObject.layer)))
        {
            // ClosestPoint : 가장 가까운 위치
            // - _direction * .2f : 부딪친 곳에서 조금 안쪽으로
            DestroyProjectile(collision.ClosestPoint(transform.position) - _direction * .2f, fxOnDestory);
        }
        else if (/*타겟으로 잡은 객체*/_attackData.target.value/*레이어 마스크*/ == (_attackData.target.value | (1 << collision.gameObject.layer)))
        {
            HealthSystem healthSystem = collision.GetComponent<HealthSystem>(); //충돌체의 헬스시스템
            if (healthSystem != null) // 충돌체가 안가지고 있으면
            {
                healthSystem.ChangeHealth(-_attackData.power); // 어택데미지에 파워??
                if (_attackData.isOnKnockback)
                {
                    TopDownMovement movement = collision.GetComponent<TopDownMovement>();
                    if (movement != null)
                    {
                        // 힘과 거리 제공??
                        movement.ApplyKnockback(transform, _attackData.knockbackPower, _attackData.knockbackTime);
                    }
                }
            }
            DestroyProjectile(collision.ClosestPoint(transform.position), fxOnDestory);
        }

    }

    //////////////////// 초기화 시작  ////////////////////
    public void InitializeAttack(Vector2 direction, RangedAttackData attackData, ProjectileManager projectileManager)
    {
        _projectileManager = projectileManager;
        _attackData = attackData;
        _direction = direction;

        UpdateProjectilSprite();
        _trailRenderer.Clear(); // 재사용 코드를 위해
        _currentDuration = 0;
        _spriteRenderer.color = attackData.projectileColor; // 투사체 색

        // x축 left  <----> right  오른쪽을 디렉션방향으로
        transform.right = _direction;

        _isReady = true;
    }

    private void UpdateProjectilSprite()
    {
        // one?  _attackData.size로 크기 결정
        transform.localScale = Vector3.one * _attackData.size;
    }
    //////////////////// 여기까지 초기화  ////////////////////


    // 삭제될 때
    private void DestroyProjectile(Vector3 position, bool createFx)
    {
        if (createFx)
        {
            _projectileManager.CreateImpactParticlesAtPostion(position, _attackData);
        }
        gameObject.SetActive(false); // 재사용을 위해 삭제하지 않고 감추기
    }
}