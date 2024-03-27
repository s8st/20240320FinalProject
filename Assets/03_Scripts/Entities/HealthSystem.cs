using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private float healthChangeDelay = .5f;

    private CharacterStatsHandler _statsHandler;
    private float _timeSinceLastChange = float.MaxValue; // �ð� ����

    public event Action OnDamage;
    public event Action OnHeal;
    public event Action OnDeath;
    public event Action OnInvincibilityEnd;

    public AudioClip damageClip;

    public float CurrentHealth { get; private set; }

    public float MaxHealth => _statsHandler.CurrentStats.maxHealth;

    private void Awake()
    {
        _statsHandler = GetComponent<CharacterStatsHandler>();
    }

    private void Start()
    {
        // ó���� �ִ��ｺ��
        CurrentHealth = _statsHandler.CurrentStats.maxHealth;
    }

    private void Update()
    {
        // �������� �ٲ� �� < .5f   ---> .5�ʺ��� ������
        if (_timeSinceLastChange < healthChangeDelay)
        {
            // �ð��� ����
            _timeSinceLastChange += Time.deltaTime;

            // .5�ʺ��� Ŀ����
            if (_timeSinceLastChange >= healthChangeDelay)
            {
                // ������ Ǭ��??
                OnInvincibilityEnd?.Invoke();
            }
        }
    }

    public bool ChangeHealth(float change)
    {
        if (change == 0 || _timeSinceLastChange < healthChangeDelay/*�������� �ް��ִ� ���̶��*/)
        {
            return false; // �������� �ް� �ִ� ���̶�� �Ѿ��
        }

        _timeSinceLastChange = 0f; // �������� �� �޾Ҵٸ� 0��, �ٽ� ������ �˻��� �� �ְ�
        CurrentHealth += change;

        // ���� �ｺ�� �ִ밪���� ������ ġ���ϰ� �ƴϸ� �ƴϸ� �ִ밪����
        CurrentHealth = CurrentHealth > MaxHealth ? MaxHealth : CurrentHealth;
        CurrentHealth = CurrentHealth < 0 ? 0 : CurrentHealth;

        if (change > 0)
        {
            // �ǰ� ���� ��
            OnHeal?.Invoke();
        }
        else
        {
            //�ǰ� ������ ������
            OnDamage?.Invoke();

            // �����Ŭ���� �������� �÷���
            if (damageClip)
                SoundManager.PlayClip(damageClip);

        }

        if (CurrentHealth <= 0f)
        {
            // 0���� ������ ������
            CallDeath();
        }

        return true;
    }

    private void CallDeath()
    {
        OnDeath?.Invoke();
    }
}