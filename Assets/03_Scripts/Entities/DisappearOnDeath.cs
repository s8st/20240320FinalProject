using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearOnDeath : MonoBehaviour
{
    private HealthSystem _healthSystem;
    private Rigidbody2D _rigidbody;

    private void Start()
    {
        _healthSystem = GetComponent<HealthSystem>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _healthSystem.OnDeath += OnDeath;
    }

    void OnDeath()
    {
        //������ �������� ���ϰ�
        _rigidbody.velocity = Vector3.zero;

        foreach (SpriteRenderer renderer
            in transform.GetComponentsInChildren/*���������ؼ� ��� ���� ������Ʈ���� ã��*/<SpriteRenderer>())
        {
            // �� ������
            // SpriteRenderer�� ��� ���İ� ����
            //�׾����� ������ ����
            Color color = renderer.color;
            color.a = 0.3f; // ���İ�
            renderer.color = color;
        }

        foreach (Behaviour component in transform.GetComponentsInChildren<Behaviour>())
        {

            //MonoBehaviour�� �θ�Ŭ������ Behaviour
            // Behaviour�� ���� 
            component.enabled = false;
        }
        // ���ӿ�����Ʈ ����
        Destroy(gameObject, 2f);
    }
}