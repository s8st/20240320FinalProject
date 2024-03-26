using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
  //  [SerializeField] private ParticleSystem _impactParticleSystem;

    public static ProjectileManager instance; //싱글턴

    // [SerializeField] private GameObject testObj;
    private ObjectPool objectPool;



    // 싱글턴
    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        objectPool = GetComponent<ObjectPool>();
    }

    public void ShootBullet(Vector2 startPostiion, Vector2 direction, RangedAttackData attackData)
    {
        // GameObject obj = Instantiate(testObj);
        GameObject obj = objectPool.SpawnFromPool(attackData.bulletNameTag);

        obj.transform.position = startPostiion;
        RangedAttackController attackController = obj.GetComponent<RangedAttackController>();
        attackController.InitializeAttack(direction, attackData, this); // this == ProjectileManager

        obj.SetActive(true); // 재사용을 위해
    }

    // 이 위치에 파티클
    //public void CreateImpactParticlesAtPostion(Vector3 position, RangedAttackData attackData)
    //{
    //    _impactParticleSystem.transform.position = position; // 파티클 시스템 위치 변경
    //    ParticleSystem.EmissionModule em = _impactParticleSystem.emission;
    //    em.SetBurst(0, new ParticleSystem.Burst(0, Mathf.Ceil(attackData.size * 5)));
    //    ParticleSystem.MainModule mainModule = _impactParticleSystem.main;
    //    mainModule.startSpeedMultiplier = attackData.size * 10f;
    //    _impactParticleSystem.Play();
    //}


}