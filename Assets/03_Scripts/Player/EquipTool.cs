using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipTool : Equip // virtual ���
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

    //�������̵� 
    //public override void OnAttackInput(PlayerConditions conditions)
    //{
    //    if (!attacking)
    //    {
    //        // attacking = true;
    //        //  animator.SetTrigger("Attack");
    //        //  Invoke("OnCanAttack", attackRate);

    //        if (conditions.UseStamina(useStamina)) // useStamina�� ���� ���� 
    //        {
    //            attacking = true;
    //            animator.SetTrigger("Attack");
    //            Invoke("OnCanAttack", attackRate); // OnCanAttack�� attackRate��ŭ ��������
    //        }
    //    }
    //}




    void OnCanAttack()
    {
        attacking = false;
    }

    public void OnHit() //����Ƽ ���ϸ��̼�(ctrl+6)���� �̺�Ʈ�� �߰��ϰ� inspector�� function���� �߰� �����ϱ�
    {
        //// ȭ�� ����� ���
        //Ray ray = camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        //RaycastHit hit;

        //if (Physics.Raycast(ray, out hit, attackDistance))
        //{
        //    // �����ҷ��� Ÿ�ֿ̹� ����� ������ ��� �ε����� ��ü�� ���� ó��
        //    if (doesGatherResources && hit.collider.TryGetComponent(out Resource resouce)) //�ڿ������� �������� �׸��� �浹�� ��ü�� ���ҽ� ��������
        //    {
        //        //TryGetComponent : ������ �����Ͷ�

        //        resouce.Gather(hit.point, hit.normal);
        //    }

        //    if (doesDealDamage && hit.collider.TryGetComponent(out IDamagable damageable)) // �������� ���� �� �ְ� �浹�� ��ü�� ������ ��������
        //    {
        //        damageable.TakePhysicalDamage(damage);
        //    }
        //}

    }

}



