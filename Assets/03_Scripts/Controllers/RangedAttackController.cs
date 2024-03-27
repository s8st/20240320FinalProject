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
        // GetComponentInChildren : ���� �ڽı��� �����ؼ� ������Ʈ �˻�
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _trailRenderer = GetComponent<TrailRenderer>();
    }

    // �̵�ó��
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

    // Ʈ���� �浹
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ���̾� �˻��� ��Ʈ���� ---> ������
        // collision.gameObject.layer : �浹�� ��ü�� ���̾�
        // | ��Ʈ���� : �ϳ��� 1�̶�� 1 
        // << ��Ʈ����
        // 1 << 3(3�� ���̾�) --> 1000
        // �¿����� ���Ƹ� ���� ã�� ��ü�� �浹�ߴ�

        // ���̶� �浹
        if (levelCollisionLayer.value == (levelCollisionLayer.value | (1 << collision.gameObject.layer)))
        {
            // ClosestPoint : ���� ����� ��ġ
            // - _direction * .2f : �ε�ģ ������ ���� ��������
            DestroyProjectile(collision.ClosestPoint(transform.position) - _direction * .2f, fxOnDestory);
        }
        else if (/*Ÿ������ ���� ��ü*/_attackData.target.value/*���̾� ����ũ*/ == (_attackData.target.value | (1 << collision.gameObject.layer)))
        {
            HealthSystem healthSystem = collision.GetComponent<HealthSystem>(); //�浹ü�� �ｺ�ý���
            if (healthSystem != null) // �浹ü�� �Ȱ����� ������
            {
                healthSystem.ChangeHealth(-_attackData.power); // ���õ������� �Ŀ�??
                if (_attackData.isOnKnockback)
                {
                    TopDownMovement movement = collision.GetComponent<TopDownMovement>();
                    if (movement != null)
                    {
                        // ���� �Ÿ� ����??
                        movement.ApplyKnockback(transform, _attackData.knockbackPower, _attackData.knockbackTime);
                    }
                }
            }
            DestroyProjectile(collision.ClosestPoint(transform.position), fxOnDestory);
        }

    }

    //////////////////// �ʱ�ȭ ����  ////////////////////
    public void InitializeAttack(Vector2 direction, RangedAttackData attackData, ProjectileManager projectileManager)
    {
        _projectileManager = projectileManager;
        _attackData = attackData;
        _direction = direction;

        UpdateProjectilSprite();
        _trailRenderer.Clear(); // ���� �ڵ带 ����
        _currentDuration = 0;
        _spriteRenderer.color = attackData.projectileColor; // ����ü ��

        // x�� left  <----> right  �������� �𷺼ǹ�������
        transform.right = _direction;

        _isReady = true;
    }

    private void UpdateProjectilSprite()
    {
        // one?  _attackData.size�� ũ�� ����
        transform.localScale = Vector3.one * _attackData.size;
    }
    //////////////////// ������� �ʱ�ȭ  ////////////////////


    // ������ ��
    private void DestroyProjectile(Vector3 position, bool createFx)
    {
        if (createFx)
        {
            _projectileManager.CreateImpactParticlesAtPostion(position, _attackData);
        }
        gameObject.SetActive(false); // ������ ���� �������� �ʰ� ���߱�
    }
}