using System;
using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class TopDownCharacterController : MonoBehaviour
{
    // event�� �ܺο��� ȣ������ ���ϰ� ���� ����
    public event Action<Vector2> OnMoveEvent;
    public event Action<Vector2> OnLookEvent;
    public event Action<AttackSO> OnAttackEvent;

    public event Action<Vector2> OnInteractEvent;

    private float _timeSinceLastAttack = float.MaxValue;
    protected bool IsAttacking { get; set; }

    protected CharacterStatsHandler Stats { get; private set; }

    protected virtual void Awake()
    {
        Stats = GetComponent<CharacterStatsHandler>();
    }


    protected /*private*/ virtual void Update()
    {
        HandleAttackDelay();
    }

    private void HandleAttackDelay()
    {
        // ������ ������
        if (Stats.CurrentStats.attackSO == null)
        {
            return;
        }


        // ������?
        // if (_timeSinceLastAttack <= .2f)
        if (_timeSinceLastAttack <= Stats.CurrentStats.attackSO.delay)
        {
            _timeSinceLastAttack += Time.deltaTime;
        }

        // else if(IsAttacking && _timeSinceLastAttack > .2f )
        else if (IsAttacking && _timeSinceLastAttack > Stats.CurrentStats.attackSO.delay)
        {
            _timeSinceLastAttack = 0;
            CallAttackEvent(Stats.CurrentStats.attackSO); // ���� �߻�� TopDownShooting����
        }


    }


    // Input.GetAxis( )��ü ---> Input system
    // ����Ƽ-������- ��Ű�� �Ŵ���- �б�����:����Ƽ ������Ʈ�� - input sytem 1.7.0

    public void CallMoveEvent(Vector2 direction)
    {
        // A ?. B  ---> A�� null�� �ƴϸ� B����, ���� Action�� �� �ɷȴٸ� ����
        OnMoveEvent?.Invoke(direction);
    }

    public void CallLookEvent(Vector2 direction)
    {
        OnLookEvent?.Invoke(direction);
    }


    public void CallAttackEvent(AttackSO attackSO)
    {
        OnAttackEvent?.Invoke(attackSO);
    }

    public void CallInteractEvent(Vector2 direction)
    {
        OnInteractEvent?.Invoke(direction);
    }




    //[SerializeField] ����Ƽ�� ����ȭ??
    //    [SerializeField] public float speed = 5;

    // Start is called before the first frame update
    //void Start()
    //{

    //}

    // Update is called once per frame
    //void Update()
    //{
    //    //float x= Input.GetAxis("Horizontal");
    //    //float y = Input.GetAxis("Vertical");

    //    // GetAxisRaw : -1,0,1
    //    //float x = Input.GetAxisRaw("Horizontal");
    //    //float y = Input.GetAxisRaw("Vertical");

    //    //// transform.position = new Vector3(x, y); 
    //    //transform.position  += new Vector3(x,y)*Time.deltaTime*speed;

    //}
}
