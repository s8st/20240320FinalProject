using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private float healthChangeDelay = .5f;

    private CharacterStatsHandler _statsHandler;
    private float _timeSinceLastChange = float.MaxValue; // 시간 저장

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
        // 처음에 최대헬스값
        CurrentHealth = _statsHandler.CurrentStats.maxHealth;
    }

    private void Update()
    {
        // 마지막에 바뀐 값 < .5f   ---> .5초보다 작으면
        if (_timeSinceLastChange < healthChangeDelay)
        {
            // 시간을 누적
            _timeSinceLastChange += Time.deltaTime;

            // .5초보다 커지면
            if (_timeSinceLastChange >= healthChangeDelay)
            {
                // 무적을 푼다??
                OnInvincibilityEnd?.Invoke();
            }
        }
    }

    public bool ChangeHealth(float change)
    {
        if (change == 0 || _timeSinceLastChange < healthChangeDelay/*데미지를 받고있는 중이라면*/)
        {
            return false; // 데미지를 받고 있는 중이라면 넘어간다
        }

        _timeSinceLastChange = 0f; // 데미지를 다 받았다면 0초, 다시 위에서 검사할 수 있게
        CurrentHealth += change;

        // 현재 헬스가 최대값보다 작으면 치료하고 아니면 아니면 최대값으로
        CurrentHealth = CurrentHealth > MaxHealth ? MaxHealth : CurrentHealth;
        CurrentHealth = CurrentHealth < 0 ? 0 : CurrentHealth;

        if (change > 0)
        {
            // 피가 차면 힐
            OnHeal?.Invoke();
        }
        else
        {
            //피가 빠지면 데미지
            OnDamage?.Invoke();

            // 오디오클립이 있을때만 플레이
            if (damageClip)
                SoundManager.PlayClip(damageClip);

        }

        if (CurrentHealth <= 0f)
        {
            // 0보다 작으면 데스로
            CallDeath();
        }

        return true;
    }

    private void CallDeath()
    {
        OnDeath?.Invoke();
    }
}