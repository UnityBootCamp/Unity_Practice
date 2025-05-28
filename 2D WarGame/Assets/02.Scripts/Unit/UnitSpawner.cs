using System.Collections.Generic;
using UnityEngine;

public class UnitSpawner : MonoBehaviour
{
    //������ �����͸� ��� Scriptable Object
    [SerializeField] List<UnitData> _units;
    public List<UnitData> Units => _units;
    public SpawnedPlayerUnitList UnitList = new SpawnedPlayerUnitList();


    // ���� ������ �������� �ƴ��� �Ǻ��� ���� �ʵ�
    public int MaxUnitCount;
    public int MaxFarmingUnitCount;

    // ������ ������ ���ֿ� ���� ����
    PlayerUnit _prevUnit;



}
