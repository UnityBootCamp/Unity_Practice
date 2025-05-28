using System.Collections.Generic;
using UnityEngine;

public class UnitSpawner<T> : MonoBehaviour
{
    [SerializeField] protected List<T> _units;  //유닛의 데이터를 담는 Scriptable Object

    public List<T> Units => _units;
    public int MaxUnitCount;                    // 유닛 스폰이 가능한지 아닌지 판별을 위한 필드

    protected PlayerUnit _prevUnit;             // 직전에 생성된 유닛에 대한 참조



    protected virtual void Start()
    {
        MaxUnitCount = 10;

    }


}
