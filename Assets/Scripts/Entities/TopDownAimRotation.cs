using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class TopDownAimRotation : MonoBehaviour
{
    [SerializeField] private SpriteRenderer armRenderer;
    [SerializeField] private Transform armPivot;
    [SerializeField] private SpriteRenderer characterRenderer;

    private TopDownCharacterController _controller;


    private void Awake()
    {
        _controller = GetComponent<TopDownCharacterController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        // 마우스 움직임에 구독
        _controller.OnLookEvent += OnAim;
    }

    private void OnAim(Vector2 newAimDirection)
    {
        // 마우스 움직일때마다 몸과 활의 방향을 꺾는다
        RotateArm(newAimDirection);
    }

    private void RotateArm(Vector2 direction)
    {
        // Atan2: 아크탄젠트, 벡터의 각도 구하기, (=세타값 θ)
        //라디언값을 디그리값으로 : Rad2Deg , (0 ~ 3.14) -> (0˚~ 180˚)
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // 캐릭터의 방향이 90를 넘어서면(=케릭터가 방향을 90도 이상 바꾸면)
        armRenderer.flipY = Mathf.Abs(rotZ) > 90f;
        characterRenderer.flipX = armRenderer.flipY;
        //회전은 Quaternion( 4원소)사용, 라디안 --> degree ,rotZ -> 오일러각도
        armPivot.rotation = Quaternion.Euler(0, 0, rotZ);


        //- 아크탄젠트 함수는 삼각함수의 역함수입니다.특정 비율에 대한 각도를 계산하는 데 사용됩니다.
        //- 2차원 공간에서 점 또는 벡터에 대한 각도를 찾는 데 아크탄젠트 함수가 종종 사용됩니다.
        //- 예를 들어, 벡터(x, y)의 경우, Math.Atan2(y, x) 함수를 사용하여 벡터가 x축과 이루는 각도를 라디안으로 얻을 수 있습니다.
        //- 아크탄젠트 함수는 사인(sine)과 코사인(cosine) 함수를 이용하여 직각 삼각형에서 빗변의 각도를 계산하는 데 사용될 수 있습니다.
        //- 이 기능은 게임 개발에서 많이 사용되며, 특히 객체가 특정 방향을 향하도록 만들거나 두 점 사이의 각도를 계산할 때 유용합니다.

    }

    // Update is called once per frame
    void Update()
    {

    }
}

