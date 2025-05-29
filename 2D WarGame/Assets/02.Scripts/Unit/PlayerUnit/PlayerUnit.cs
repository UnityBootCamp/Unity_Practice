using System.Collections;
using Unity.VisualScripting;
using UnityEngine;



public class PlayerUnit : Unit<PlayerUnitData>
{

    // 유닛의 기본데이터
    public PlayerUnitType UnitType;
    public Vector3 ThisUnitSize => _unitSize;


    // 바로 앞 유닛 관련
    public PlayerUnit _prevUnit;

    GameObject _oppositeUnit;


    bool isCanAttack;
    bool isAttacking;

    private void OnEnable()
    {
        IsCanMove = true;
        SetData();
        isCanAttack = true;
    }

    private void Start()
    {
        _unitAnim = GetComponent<Animator>();
        _unitSize = PlayerUnitSize.PlayerUnitSizes[(int)UnitType];
        

    }

    private void Update()
    {
        if (IsCanMove && isAttacking==false)
        {
            if (_prevUnit == this ||
                (
                (_prevUnit.transform.position.x - _prevUnitSize.x / 2)
                - (transform.position.x + _unitSize.x / 2) >= 0)
                )
            {
                transform.parent.Translate(Vector3.right * _moveSpeed * Time.deltaTime);
                _unitAnim.SetBool("1_Move", true);
            }

            else
            {
                _unitAnim.SetBool("1_Move", false);
                StartCoroutine(C_MoveCool());
            }
        }


        // 적 선봉 유닛이 존재한다면
        if (UnitAttackManager.Instance.EnemyFirstUnit != null)
        { // 공격
            if (
            (UnitAttackManager.Instance.EnemyFirstUnit.gameObject.transform.parent.transform.position.x - UnitAttackManager.Instance.EnemyFirstUnit.ThisUnitSize.x / 2)
            - (transform.parent.transform.position.x + _unitSize.x / 2) <= 0)
            {
                _unitAnim.SetBool("1_Move", false);
                
                if (isCanAttack)
                {
                    isAttacking = true;
                    StartCoroutine(C_AttackRoutine());
                }
            }
            else
            {
                _unitAnim.SetBool("1_Move", true);
                isAttacking = false;
            }

            if (_hp <= 0)
            {
                OnDeath();
            }
        }
           
    }

    IEnumerator C_AttackRoutine()
    {
        isCanAttack = false;
        
        _unitAnim.SetTrigger("2_Attack");
        UnitAttackManager.Instance.EnemyFirstUnit.GetDamage(_attackForce);
        yield return new WaitForSeconds(_attackDelay);
        isCanAttack = true;
    }

    public void GetDamage(float value)
    {
        _hp -= value;
    }



    public void SetPrevUnit(PlayerUnit prevUnit)
    {
        _prevUnit = prevUnit;
        _prevUnitSize = PlayerUnitSize.PlayerUnitSizes[(int)_prevUnit.UnitType];
    }

    void SetData()
    {
        UnitType = _unitData.UnitType;
        _hp = _unitData.Hp;
        _attackForce = _unitData.AttackForce;
        _moveSpeed = _unitData.MoveSpeed;
        _attackDelay = _unitData.AttackDelay;
    }


    public void OnDeath()
    {
        PoolManager.Instance.Release(UnitType.ToString(), gameObject.transform.parent.gameObject);
        PlayerSpawnManager.Instance.UnitList.DequeueUnitList();
        if(PlayerSpawnManager.Instance.UnitList.SpawnedBattleUnit.Count == 0)
        {

            UnitAttackManager.Instance.PlayerFirstUnit = null;
        }
        else
        {

            UnitAttackManager.Instance.PlayerFirstUnit = PlayerSpawnManager.Instance.UnitList.SpawnedBattleUnit.Peek();
        }
    }


}