using System.Collections;
using UnityEngine;



public class EnemyUnit : MonoBehaviour
{
    [SerializeField] EnemyUnitData _unitData;

    // 유닛의 기본데이터
    public EnemyUnitType UnitType;
    float _hp;
    float _attackForce;
    float _moveSpeed;
    float _attackDelay;
    Animator _unitAnim;
    Vector3 _unitSize;
    public Vector3 ThisUnitSize => _unitSize;


    // 바로 앞 유닛 관련
    public EnemyUnit _prevUnit;
    Vector3 _prevUnitSize;


    bool IsCanMove;

    private void OnEnable()
    {
        IsCanMove = true;
        SetData();
    }

    private void Start()
    {
        _unitAnim = GetComponent<Animator>();
        _unitSize = UnitSize.UnitSizes[(int)UnitType];


    }

    private void Update()
    {
        if (IsCanMove)
        {
            if (_prevUnit == this ||
                (
                (_prevUnit.transform.position.x - _prevUnitSize.x / 2)
                - (transform.position.x + _unitSize.x / 2) >= 0)
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
    }

    IEnumerator C_MoveCool()
    {
        IsCanMove = false;
        yield return new WaitForSeconds(0.5f);
        IsCanMove = true;
    }

    public void SetPrevUnit(EnemyUnit prevUnit)
    {
        _prevUnit = prevUnit;
        _prevUnitSize = UnitSize.UnitSizes[(int)_prevUnit.UnitType];
    }

    void SetData()
    {
        UnitType = _unitData.UnitType;
        _hp = _unitData.Hp;
        _attackForce = _unitData.AttackForce;
        _moveSpeed = _unitData.MoveSpeed;
        _attackDelay = _unitData.AttackDelay;
    }

}