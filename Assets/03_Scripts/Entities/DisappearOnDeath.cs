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
        //죽으면 움직이지 못하게
        _rigidbody.velocity = Vector3.zero;

        foreach (SpriteRenderer renderer
            in transform.GetComponentsInChildren/*나를포함해서 모든 하위 컴포넌트에서 찾기*/<SpriteRenderer>())
        {
            // 색 변경방법
            // SpriteRenderer를 모두 알파값 변경
            //죽었을때 연출을 위해
            Color color = renderer.color;
            color.a = 0.3f; // 알파값
            renderer.color = color;
        }

        foreach (Behaviour component in transform.GetComponentsInChildren<Behaviour>())
        {

            //MonoBehaviour의 부모클래스가 Behaviour
            // Behaviour를 꺼라 
            component.enabled = false;
        }
        // 게임오브젝트 삭제
        Destroy(gameObject, 2f);
    }
}