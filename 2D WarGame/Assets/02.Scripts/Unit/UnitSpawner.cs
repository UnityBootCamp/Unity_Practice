using System.Collections.Generic;
using UnityEngine;

public class UnitSpawner : MonoBehaviour
{
    //유닛의 데이터를 담는 Scriptable Object
    [SerializeField] List<UnitData> _units;
    public List<UnitData> Units => _units;
    public SpawnedPlayerUnitList UnitList = new SpawnedPlayerUnitList();


    // 유닛 스폰이 가능한지 아닌지 판별을 위한 필드
    public int MaxUnitCount;
    public int MaxFarmingUnitCount;

    // 직전에 생성된 유닛에 대한 참조
    PlayerUnit _prevUnit;



}
