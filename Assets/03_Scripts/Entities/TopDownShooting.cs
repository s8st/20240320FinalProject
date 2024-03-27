//using System; Random �ߺ� ����Ƽ���� �����޼��� ��� 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownShooting : MonoBehaviour
{
    private ProjectileManager _projectileManager;
    private TopDownCharacterController _controller;

    [SerializeField] private Transform projectileSpawnPosition;
    private Vector2 _aimDirection = Vector2.right; //(1,0)

    // public GameObject testPrefab;
    public AudioClip shootingClip;

    private void Awake()
    {
        _controller = GetComponent<TopDownCharacterController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _projectileManager = ProjectileManager.instance;
        _controller.OnAttackEvent += OnShoot;
        _controller.OnLookEvent += OnAim;
    }

    private void OnAim(Vector2 newAimDirection)
    {
        _aimDirection = newAimDirection;
    }

    private void OnShoot(AttackSO attackSO)
    {
        // as ����ȯ??
        RangedAttackData rangedAttackData = attackSO as RangedAttackData;

        // ??
        float projectilesAngleSpace = rangedAttackData.multipleProjectilesAngel;
        int numberOfProjectilesPerShot = rangedAttackData.numberofProjectilesPerShot;
        float minAngle = -(numberOfProjectilesPerShot / 2f)/*�����ϴ°��� 2���*/
            * projectilesAngleSpace/*�̸� ������ ���±��*/ +
            .5f * rangedAttackData.multipleProjectilesAngel;

        for (int i = 0; i < numberOfProjectilesPerShot; i++)
        {
            float angle = minAngle/*�ּҾޱ�*/ + projectilesAngleSpace * i/*�� ���̻��� ���� �����ش�*/;
            float randomSpread = Random.Range(-rangedAttackData.spread, rangedAttackData.spread);
            angle += randomSpread; // ���� �߻� �������� ���� ���� �߰�

            CreateProjectile(rangedAttackData, angle);


        }
    }

    private void CreateProjectile(RangedAttackData rangedAttackData, float angle)
    {
        // Debug.Log("Fire");
        //  Instantiate(testPrefab, projectileSpwanPosition.position, Quaternion.identity /*������ (0.0.0)*/);
        // Instantiate�� �����ε� : �Ű������� ���� ȭ���� ��������� ��ġ�� �޶���
        // �Ű����� ù��° - ����, �ι�° - ���� ��ġ , 3��° ȸ����

        _projectileManager.ShootBullet(projectileSpawnPosition.position,
          RotateVector2(_aimDirection, angle),
          rangedAttackData);

        // shootingClip������ �����Ŭ�� ������Ѷ�
        if (shootingClip)
            SoundManager.PlayClip(shootingClip);
    }


    private static Vector2 RotateVector2(Vector2 v, float degree)
    {
        return Quaternion.Euler(0, 0, degree) * v;
        // �ڿ��� ���� ���͸� �տ��� ���ʹϾ��� ȸ����Ų ���� ����
        // ���ʹϾ� * ���� : �� ���͸� �� ������ ȸ�����Ѷ�??
    }
}





