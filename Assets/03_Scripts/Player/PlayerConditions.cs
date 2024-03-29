using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

//�������̽� ����
//�������̽��� ���� Ŭ�������� �������� ������ �����ϰ�, �̷��� ���۵��� �����ϴ� Ŭ�������� �ش� �������̽��� ����(implement)�����ν� ���� �Ծ��� �ؼ��� �� �ֽ��ϴ�.
//�������̽��� �����ϴ� �ֿ� Ư¡�� ������ �����ϴ�.
//�߻�ȭ: �������̽��� �߻����� ��������, ������ ������ �޼��尡 ����, �޼����� �ñ״�ó���� �����ϴ�. ���� �������̽��� �ν��Ͻ�ȭ�� �� ������, ����ü�� �ʿ��մϴ�.
//�޼��� �ñ״�ó: �������̽��� ���� Ŭ������ �ݵ�� �����ؾ� �ϴ� �޼������ �ñ״�ó�� �����մϴ�. �޼����� �̸�, �Ű�����, ��ȯ Ÿ���� ���Ե˴ϴ�.
//���� ��� ����: Ŭ������ �ϳ��� Ŭ������ ��ӹ��� �� ������, ���� �������̽��� ���ÿ� ������ �� �ֽ��ϴ�. �̸� ���� ���� ����� �䳻���� ���� �����մϴ�.
//������ ����: Ŭ������ �������̽��� �����ϸ�, �������̽����� ������ ��� �޼��带 �ݵ�� �����ؾ� �մϴ�. �̷� ���� Ŭ������ �������̽��� ���ǵ� ������ ������ �����ϰ� �˴ϴ�.
//�������̽� �� Ȯ��: �������̽��� �ٸ� �������̽��� Ȯ��(extends)�� �� �ֽ��ϴ�. �̸� ���� �� ū ������ ���� ������ ������ �� �ֽ��ϴ�.



public interface IDamagable //������ ���缭 Ŭ������ ��ӹް� ���� �������̽��� ������� ����
{
    void TakePhysicalDamage(int damageAmount);
}

[System.Serializable] //Condition�� PlayerConditions���� �������� ���ؼ� �߰�
public class Condition
{
    [HideInInspector] //����, �ϳ��� ���� ---> �Ʒ��� curValue
    public float curValue; // ������

    public float maxValue;

    public float startValue;
    public float regenRate;
    public float decayRate;
    public Image uiBar;

    public void Add(float amount)
    {
        // �ִ밪�� �ȳѱ�� ���� maxValue�� ���ؼ� ���� ��
        curValue = Mathf.Min(curValue + amount, maxValue);
    }

    public void Subtract(float amount)
    {
        curValue = Mathf.Max(curValue - amount, 0.0f);
    }

    public float GetPercentage()
    {
        // �ۼ�Ʈ�� 0�� 1�� ǥ��
        return curValue / maxValue;
    }

}

// Ŭ������ ���� ����� �ȵ����� �������̽��� ���� ��� ����
public class PlayerConditions : MonoBehaviour, IDamagable
{
    // [System.Serializable] ---> ��ܿ��� �߰��� ������ Condition�� �������� ���ϱ� ������
    public Condition health;

    // �ν����Ϳ��� health�Ʒ��� �Ʒ��� conditionŬ������ �ʵ尡 ��Ÿ��
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

    //�������� �޾����� ó���� ����Ƽ �̺�Ʈ
    public UnityEvent onTakeDamage;

    void Start()
    {
        // ������ ���� ���۰����� 
        health.curValue = health.startValue;
        hunger.curValue = hunger.startValue;
        stamina.curValue = stamina.startValue;
    }

    // Update is called once per frame
    void Update()
    {
        hunger.Subtract(hunger.decayRate * Time.deltaTime);
        stamina.Add(stamina.regenRate * Time.deltaTime); //ȸ��

        if (hunger.curValue == 0.0f)
            // 0�̸�health ���
            health.Subtract(noHungerHealthDecay * Time.deltaTime);

        // health�� 0�� �Ǹ� Die
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
            return false; // amount�� �� ������ false��ȯ

        stamina.Subtract(amount); // �ƴ϶�� ���׹̳� ����ϰ� true��ȯ
        return true;
    }

    public void Die()
    {
        Debug.Log("�÷��̾ �׾���.");
    }

    // �������̽��� ���߻��, ���⼭ �����ϰ� ����
    public void TakePhysicalDamage(int damageAmount)
    {
        health.Subtract(damageAmount);
        onTakeDamage?.Invoke(); // nullüũ �� ����
    }
}