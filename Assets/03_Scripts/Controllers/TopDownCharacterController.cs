using System;
using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class TopDownCharacterController : MonoBehaviour
{
    // event는 외부에서 호출하지 못하게 막는 역할
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
        // 공격이 없으면
        if (Stats.CurrentStats.attackSO == null)
        {
            return;
        }


        // 딜레이?
        // if (_timeSinceLastAttack <= .2f)
        if (_timeSinceLastAttack <= Stats.CurrentStats.attackSO.delay)
        {
            _timeSinceLastAttack += Time.deltaTime;
        }

        // else if(IsAttacking && _timeSinceLastAttack > .2f )
        else if (IsAttacking && _timeSinceLastAttack > Stats.CurrentStats.attackSO.delay)
        {
            _timeSinceLastAttack = 0;
            CallAttackEvent(Stats.CurrentStats.attackSO); // 실제 발사는 TopDownShooting에서
        }


    }


    // Input.GetAxis( )대체 ---> Input system
    // 유니티-윈도우- 패키지 매니저- 패기지스:유니티 레지스트리 - input sytem 1.7.0

    public void CallMoveEvent(Vector2 direction)
    {
        // A ?. B  ---> A가 null이 아니면 B실행, 위에 Action이 잘 걸렸다면 실행
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




    //[SerializeField] 유니티와 동기화??
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
