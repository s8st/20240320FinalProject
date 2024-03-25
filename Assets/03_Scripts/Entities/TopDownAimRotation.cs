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
        // ���콺 �����ӿ� ����
        _controller.OnLookEvent += OnAim;
    }

    private void OnAim(Vector2 newAimDirection)
    {
        // ���콺 �����϶����� ���� Ȱ�� ������ ���´�
        RotateArm(newAimDirection);
    }

    private void RotateArm(Vector2 direction)
    {
        // Atan2: ��ũź��Ʈ, ������ ���� ���ϱ�, (=��Ÿ�� ��)
        //������ ��׸������� : Rad2Deg , (0 ~ 3.14) -> (0��~ 180��)
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // ĳ������ ������ 90�� �Ѿ��(=�ɸ��Ͱ� ������ 90�� �̻� �ٲٸ�)
        armRenderer.flipY = Mathf.Abs(rotZ) > 90f;
        characterRenderer.flipX = armRenderer.flipY;
        //ȸ���� Quaternion( 4����)���, ���� --> degree ,rotZ -> ���Ϸ�����
        armPivot.rotation = Quaternion.Euler(0, 0, rotZ);


        //- ��ũź��Ʈ �Լ��� �ﰢ�Լ��� ���Լ��Դϴ�.Ư�� ������ ���� ������ ����ϴ� �� ���˴ϴ�.
        //- 2���� �������� �� �Ǵ� ���Ϳ� ���� ������ ã�� �� ��ũź��Ʈ �Լ��� ���� ���˴ϴ�.
        //- ���� ���, ����(x, y)�� ���, Math.Atan2(y, x) �Լ��� ����Ͽ� ���Ͱ� x��� �̷�� ������ �������� ���� �� �ֽ��ϴ�.
        //- ��ũź��Ʈ �Լ��� ����(sine)�� �ڻ���(cosine) �Լ��� �̿��Ͽ� ���� �ﰢ������ ������ ������ ����ϴ� �� ���� �� �ֽ��ϴ�.
        //- �� ����� ���� ���߿��� ���� ���Ǹ�, Ư�� ��ü�� Ư�� ������ ���ϵ��� ����ų� �� �� ������ ������ ����� �� �����մϴ�.

    }

    // Update is called once per frame
    void Update()
    {

    }
}

