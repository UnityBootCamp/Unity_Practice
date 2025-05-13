using UnityEngine;



public class Unit : MonoBehaviour
{
    [SerializeField] UnitData _unitData;

    PlayerUnitType _unitType;
    float _hp;
    float _attackForce;
    float _moveSpeed;
    float _attackDelay;
    Animator _unitAnim;

    private void Awake()
    {
        SetData();
        _unitAnim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (true)
        {
            transform.parent.Translate(Vector3.right * _moveSpeed * Time.deltaTime);
            _unitAnim.SetBool("1_Move", true);
        }
        
    }



    void SetData()
    {
        _unitType = _unitData.UnitType;
        _hp = _unitData.Hp;
        _attackForce = _unitData.AttackForce;
        _moveSpeed = _unitData.MoveSpeed;
        _attackDelay = _unitData.AttackDelay;
    }

}