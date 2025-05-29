using System.Collections.Generic;
using UnityEngine;

public class UnitSpawner<T> : MonoBehaviour
{
    [SerializeField] protected List<T> _units;  //������ �����͸� ��� Scriptable Object

    public List<T> Units => _units;
    public int MaxUnitCount;                    // ���� ������ �������� �ƴ��� �Ǻ��� ���� �ʵ�




    protected virtual void Start()
    {
        MaxUnitCount = 10;

    }


}
