//using System; Random 중복 유니티엔진 랜덤메서드 사용 
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
        // as 형변환??
        RangedAttackData rangedAttackData = attackSO as RangedAttackData;

        // ??
        float projectilesAngleSpace = rangedAttackData.multipleProjectilesAngel;
        int numberOfProjectilesPerShot = rangedAttackData.numberofProjectilesPerShot;
        float minAngle = -(numberOfProjectilesPerShot / 2f)/*쏴야하는갯수 2등분*/
            * projectilesAngleSpace/*미리 각도를 꺾는기능*/ +
            .5f * rangedAttackData.multipleProjectilesAngel;

        for (int i = 0; i < numberOfProjectilesPerShot; i++)
        {
            float angle = minAngle/*최소앵글*/ + projectilesAngleSpace * i/*각 사이사이 값을 곱해준다*/;
            float randomSpread = Random.Range(-rangedAttackData.spread, rangedAttackData.spread);
            angle += randomSpread; // 실제 발사 각도에서 랜덤 각도 추가

            CreateProjectile(rangedAttackData, angle);


        }
    }

    private void CreateProjectile(RangedAttackData rangedAttackData, float angle)
    {
        // Debug.Log("Fire");
        //  Instantiate(testPrefab, projectileSpwanPosition.position, Quaternion.identity /*벡터의 (0.0.0)*/);
        // Instantiate의 오버로딩 : 매개변수에 따라 화살이 만들어지는 위치가 달라짐
        // 매개변수 첫번째 - 원본, 두번째 - 생성 위치 , 3번째 회전값

        _projectileManager.ShootBullet(projectileSpawnPosition.position,
          RotateVector2(_aimDirection, angle),
          rangedAttackData);

        // shootingClip있으면 오디오클립 실행시켜라
        if (shootingClip)
            SoundManager.PlayClip(shootingClip);
    }


    private static Vector2 RotateVector2(Vector2 v, float degree)
    {
        return Quaternion.Euler(0, 0, degree) * v;
        // 뒤에서 곱한 벡터를 앞에서 쿼터니언을 회전시킨 값의 벡터
        // 쿼터니언 * 벡터 : 이 벡터를 이 각도로 회전시켜라??
    }
}





