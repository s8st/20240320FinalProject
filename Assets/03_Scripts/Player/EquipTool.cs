using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipTool : Equip // virtual 상속
{
    public float attackRate;
    private bool attacking;
    public float attackDistance;

    public float useStamina;

    [Header("Resource Gathering")]
    public bool doesGatherResources;

    [Header("Combat")]
    public bool doesDealDamage;
    public int damage;

    private Animator animator;
    private Camera camera;



    private void Awake()
    {
        camera = Camera.main;
        animator = GetComponent<Animator>();
    }

    //오버라이드 
    //public override void OnAttackInput(PlayerConditions conditions)
    //{
    //    if (!attacking)
    //    {
    //        // attacking = true;
    //        //  animator.SetTrigger("Attack");
    //        //  Invoke("OnCanAttack", attackRate);

    //        if (conditions.UseStamina(useStamina)) // useStamina가 있을 때만 
    //        {
    //            attacking = true;
    //            animator.SetTrigger("Attack");
    //            Invoke("OnCanAttack", attackRate); // OnCanAttack를 attackRate만큼 지연실행
    //        }
    //    }
    //}




    void OnCanAttack()
    {
        attacking = false;
    }

    public void OnHit() //유니티 에니메이션(ctrl+6)에서 이벤트를 추가하고 inspector의 function에서 추가 연결하기
    {
        //// 화면 가운데서 쏜다
        //Ray ray = camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        //RaycastHit hit;

        //if (Physics.Raycast(ray, out hit, attackDistance))
        //{
        //    // 공격할려는 타이밍에 가운데에 광선을 쏘고 부딪히는 객체에 따라서 처리
        //    if (doesGatherResources && hit.collider.TryGetComponent(out Resource resouce)) //자원수집이 가능한지 그리고 충돌한 물체의 리소스 가져오기
        //    {
        //        //TryGetComponent : 있으면 가져와라

        //        resouce.Gather(hit.point, hit.normal);
        //    }

        //    if (doesDealDamage && hit.collider.TryGetComponent(out IDamagable damageable)) // 데미지를 받을 수 있고 충돌한 객체의 데미지 가져오기
        //    {
        //        damageable.TakePhysicalDamage(damage);
        //    }
        //}

    }

}



