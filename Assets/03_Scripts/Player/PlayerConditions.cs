using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

//인터페이스 복습
//인터페이스를 통해 클래스들은 공통적인 동작을 정의하고, 이러한 동작들을 구현하는 클래스들은 해당 인터페이스를 구현(implement)함으로써 공통 규약을 준수할 수 있습니다.
//인터페이스를 설명하는 주요 특징은 다음과 같습니다.
//추상화: 인터페이스는 추상적인 개념으로, 실제로 구현된 메서드가 없고, 메서드의 시그니처만을 가집니다. 따라서 인터페이스는 인스턴스화될 수 없으며, 구현체가 필요합니다.
//메서드 시그니처: 인터페이스는 구현 클래스가 반드시 구현해야 하는 메서드들의 시그니처를 정의합니다. 메서드의 이름, 매개변수, 반환 타입이 포함됩니다.
//다중 상속 가능: 클래스는 하나의 클래스만 상속받을 수 있지만, 여러 인터페이스를 동시에 구현할 수 있습니다. 이를 통해 다중 상속을 흉내내는 것이 가능합니다.
//강제적 구현: 클래스가 인터페이스를 구현하면, 인터페이스에서 정의한 모든 메서드를 반드시 구현해야 합니다. 이로 인해 클래스는 인터페이스에 정의된 동작을 강제로 구현하게 됩니다.
//인터페이스 간 확장: 인터페이스는 다른 인터페이스를 확장(extends)할 수 있습니다. 이를 통해 더 큰 범위의 공통 동작을 정의할 수 있습니다.



public interface IDamagable //구조에 맞춰서 클래스를 상속받게 할지 인터페이스를 사용할지 결정
{
    void TakePhysicalDamage(int damageAmount);
}

[System.Serializable] //Condition을 PlayerConditions에서 가져오지 못해서 추가
public class Condition
{
    [HideInInspector] //숨김, 하나만 숨김 ---> 아래의 curValue
    public float curValue; // 숨겨짐

    public float maxValue;

    public float startValue;
    public float regenRate;
    public float decayRate;
    public Image uiBar;

    public void Add(float amount)
    {
        // 최대값을 안넘기기 위해 maxValue와 비교해서 작은 값
        curValue = Mathf.Min(curValue + amount, maxValue);
    }

    public void Subtract(float amount)
    {
        curValue = Mathf.Max(curValue - amount, 0.0f);
    }

    public float GetPercentage()
    {
        // 퍼센트는 0과 1로 표현
        return curValue / maxValue;
    }

}

// 클래스는 다중 상속이 안되지만 인터페이스는 다중 상속 가능
public class PlayerConditions : MonoBehaviour, IDamagable
{
    // [System.Serializable] ---> 상단에서 추가한 이유는 Condition을 가져오지 못하기 때문에
    public Condition health;

    // 인스펙터에서 health아래로 아래의 condition클래스의 필드가 나타남
    //public float maxValue;
    //public float startValue;
    //public float regenRate;
    //public float decayRate;
    //public Image uiBar;

    public Condition hunger;
    public Condition stamina;
    //public Condition test4;
    //public Condition test5;


    public float noHungerHealthDecay;

    //데미지를 받았을때 처리할 유니티 이벤트
    public UnityEvent onTakeDamage;

    void Start()
    {
        // 각각의 값을 시작값으로 
        health.curValue = health.startValue;
        hunger.curValue = hunger.startValue;
        stamina.curValue = stamina.startValue;
    }

    // Update is called once per frame
    void Update()
    {
        hunger.Subtract(hunger.decayRate * Time.deltaTime);
        stamina.Add(stamina.regenRate * Time.deltaTime); //회복

        if (hunger.curValue == 0.0f)
            // 0이면health 깍기
            health.Subtract(noHungerHealthDecay * Time.deltaTime);

        // health가 0이 되면 Die
        if (health.curValue == 0.0f)
            Die();

        health.uiBar.fillAmount = health.GetPercentage();
        hunger.uiBar.fillAmount = hunger.GetPercentage();
        stamina.uiBar.fillAmount = stamina.GetPercentage();
    }

    public void Heal(float amount)
    {
        health.Add(amount);
    }

    public void Eat(float amount)
    {
        hunger.Add(amount);
    }

    public bool UseStamina(float amount)
    {
        if (stamina.curValue - amount < 0)
            return false; // amount가 더 많을때 false반환

        stamina.Subtract(amount); // 아니라면 스테미나 사용하고 true반환
        return true;
    }

    public void Die()
    {
        Debug.Log("플레이어가 죽었다.");
    }

    // 인터페이스로 다중상속, 여기서 구현하고 있음
    public void TakePhysicalDamage(int damageAmount)
    {
        health.Subtract(damageAmount);
        onTakeDamage?.Invoke(); // null체크 후 실행
    }
}