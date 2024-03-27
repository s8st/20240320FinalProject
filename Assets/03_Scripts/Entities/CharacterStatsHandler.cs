using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class CharacterStatsHandler : MonoBehaviour
{
    [SerializeField] private CharacterStats baseStats;
    public CharacterStats CurrentStats { get; private set; }
    public List<CharacterStats> statsModifiers = new List<CharacterStats>();

    private const float MinAttackDelay = 0.03f;
    private const float MinAttackPower = 0.5f;
    private const float MinAttackSize = 0.4f;
    private const float MinAttackSpeed = .1f;

    private const float MinSpeed = 0.8f;

    private const int MinMaxHealth = 5;


    private void Awake()
    {
        UpdateCharacterStats();
    }

    public void AddStatModifier(CharacterStats statModifier)
    {
        statsModifiers.Add(statModifier);
        UpdateCharacterStats();
    }

    public void RemoveStatModifier(CharacterStats statModifier)
    {
        statsModifiers.Remove(statModifier);
        UpdateCharacterStats();
    }


    // 뭔가가 들어오면 최신화
    private void UpdateCharacterStats()
    {
        AttackSO attackSO = null;
        if (baseStats.attackSO != null)
        {
            attackSO = Instantiate(baseStats.attackSO); // 먼저 내 베이스에 있는 것을 넣기
        }

        CurrentStats = new CharacterStats { attackSO = attackSO };
        // TODO
        CurrentStats.statsChangeType = baseStats.statsChangeType;
        CurrentStats.maxHealth = baseStats.maxHealth;
        CurrentStats.speed = baseStats.speed;
        //baseStats을 커렌트에 
        UpdateStats((a, b) => b, baseStats); //a,b를 받아서 b만 사용
        if (CurrentStats.attackSO != null) //attackSO 있는지 확인
        {
            CurrentStats.attackSO.target = baseStats.attackSO.target; //타겟 설정
        }

        // OrderBy :LINQ 리스트
        // statsChangeType에 맞춰 정렬
        // 정렬해서 오버라이드나 추가나 곱하기
        // 차곡차곡 스탯을 더 쌓아 올리기
        foreach (CharacterStats modifier in statsModifiers.OrderBy(o => o.statsChangeType))
        {
            if (modifier.statsChangeType == StatsChangeType.Override)
            {
                UpdateStats((o, o1) => o1, modifier);
            }
            else if (modifier.statsChangeType == StatsChangeType.Add)
            {
                UpdateStats((o, o1) => o + o1, modifier);
            }
            else if (modifier.statsChangeType == StatsChangeType.Multiple)
            {
                UpdateStats((o, o1) => o * o1, modifier);
            }
        }

        LimitAllStats();
    }

    //Func<매개변수,매개변수,반환값>
    private void UpdateStats(Func<float, float, float> operation, CharacterStats newModifier)
    {
        CurrentStats.maxHealth = (int)operation(CurrentStats.maxHealth, newModifier.maxHealth);
        CurrentStats.speed = operation(CurrentStats.speed, newModifier.speed);

        if (CurrentStats.attackSO == null || newModifier.attackSO == null)
            return;

        UpdateAttackStats(operation, CurrentStats.attackSO, newModifier.attackSO);

        if (CurrentStats.attackSO.GetType() != newModifier.attackSO.GetType())
        {
            return;
        }

        //패턴매칭 : 패턴을 제공하면 case에서 패턴을 사용 가능함
        switch (CurrentStats.attackSO)
        {
            // case AnotherAttackData : 다른 공격을 case로 추가 가능
            case RangedAttackData _:
                ApplyRangedStats(operation, newModifier);
                break;
        }
    }

    private void UpdateAttackStats(Func<float, float, float> operation, AttackSO currentAttack, AttackSO newAttack)
    {
        if (currentAttack == null || newAttack == null)
        {
            return;
        }

        currentAttack.delay = operation(currentAttack.delay, newAttack.delay);
        currentAttack.power = operation(currentAttack.power, newAttack.power);
        currentAttack.size = operation(currentAttack.size, newAttack.size);
        currentAttack.speed = operation(currentAttack.speed, newAttack.speed);
    }

    private void ApplyRangedStats(Func<float, float, float> operation, CharacterStats newModifier)
    {
        RangedAttackData currentRangedAttacks = (RangedAttackData)CurrentStats.attackSO;

        if (!(newModifier.attackSO is RangedAttackData))
        {
            return;
        }

        RangedAttackData rangedAttacksModifier = (RangedAttackData)newModifier.attackSO;
        currentRangedAttacks.multipleProjectilesAngel =
            operation(currentRangedAttacks.multipleProjectilesAngel, rangedAttacksModifier.multipleProjectilesAngel);
        currentRangedAttacks.spread = operation(currentRangedAttacks.spread, rangedAttacksModifier.spread);
        currentRangedAttacks.duration = operation(currentRangedAttacks.duration, rangedAttacksModifier.duration);
        currentRangedAttacks.numberofProjectilesPerShot = Mathf.CeilToInt(operation(currentRangedAttacks.numberofProjectilesPerShot,
            rangedAttacksModifier.numberofProjectilesPerShot));
        currentRangedAttacks.projectileColor = UpdateColor(operation, currentRangedAttacks.projectileColor, rangedAttacksModifier.projectileColor);
    }

    private Color UpdateColor(Func<float, float, float> operation, Color currentColor, Color newColor)
    {
        return new Color(
            operation(currentColor.r, newColor.r),
            operation(currentColor.g, newColor.g),
            operation(currentColor.b, newColor.b),
            operation(currentColor.a, newColor.a));
    }

    private void LimitStats(ref float stat, float minVal)
    {
        stat = Mathf.Max(stat, minVal);
    }

    private void LimitAllStats()
    {
        if (CurrentStats == null || CurrentStats.attackSO == null)
        {
            return;
        }

        LimitStats(ref CurrentStats.attackSO.delay, MinAttackDelay);
        LimitStats(ref CurrentStats.attackSO.power, MinAttackPower);
        LimitStats(ref CurrentStats.attackSO.size, MinAttackSize);
        LimitStats(ref CurrentStats.attackSO.speed, MinAttackSpeed);
        LimitStats(ref CurrentStats.speed, MinSpeed);
        CurrentStats.maxHealth = Mathf.Max(CurrentStats.maxHealth, MinMaxHealth);
    }

}



