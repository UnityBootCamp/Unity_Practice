using System.Collections;
using Unity.VisualScripting;
using UnityEngine;



public class EnemyUnit : Unit<EnemyUnitData>
{

    // ������ �⺻������
    public EnemyUnitType UnitType;
    public Vector3 ThisUnitSize => _unitSize;

    GameObject _oppositeUnit;


    // �ٷ� �� ���� ����
    public EnemyUnit _prevUnit;

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
        _unitSize = EnemyUnitSize.EnemyUnitSizes[(int)UnitType];
        

    }

    private void Update()
    {
        // �̵�
        if (IsCanMove && isAttacking == false)
        {
            if (_prevUnit == this ||
                (
                (_prevUnit.transform.position.x + _prevUnitSize.x / 2)
                - (transform.position.x - _unitSize.x / 2) <= 0)
                )
            {
                transform.parent.Translate(Vector3.left * _moveSpeed * Time.deltaTime);
                _unitAnim.SetBool("1_Move", true);
            }

            else
            {
                _unitAnim.SetBool("1_Move", false);
                StartCoroutine(C_MoveCool());
            }
        }


        // �÷��̾� ���� ������ �����Ѵٸ�
        if (UnitAttackManager.Instance.PlayerFirstUnit != null)
        {

            // ����
            if (
                (UnitAttackManager.Instance.PlayerFirstUnit.gameObject.transform.parent.transform.position.x + UnitAttackManager.Instance.PlayerFirstUnit.ThisUnitSize.x / 2)
                - (transform.parent.transform.position.x - _unitSize.x / 2) >= 0)
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

                isAttacking = false;
                _unitAnim.SetBool("1_Move", true);
            }

            if (_hp <= 0)
            {
                OnDeath();
            }
        }
        


    }

    public void GetDamage(float value)
    {
        _hp -= value;
    }

    IEnumerator C_AttackRoutine()
    {
        isCanAttack = false;
        _unitAnim.SetTrigger("2_Attack");
        UnitAttackManager.Instance.PlayerFirstUnit.GetDamage(_attackForce);
        yield return new WaitForSeconds(_attackDelay);
        isCanAttack = true;
    }


    public void SetPrevUnit(EnemyUnit prevUnit)
    {
        _prevUnit = prevUnit;
        _prevUnitSize = EnemyUnitSize.EnemyUnitSizes[(int)_prevUnit.UnitType];
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
        EnemySpawnManager.Instance.UnitList.DequeueUnitList(); 
        
        if (EnemySpawnManager.Instance.UnitList.SpawnedBattleUnit.Count == 0)
        {

            UnitAttackManager.Instance.EnemyFirstUnit = null;
        }
        else
        {

            UnitAttackManager.Instance.EnemyFirstUnit = EnemySpawnManager.Instance.UnitList.SpawnedBattleUnit.Peek();
        }
    }


}