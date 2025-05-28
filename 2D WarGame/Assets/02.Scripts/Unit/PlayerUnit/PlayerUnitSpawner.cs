using UnityEngine;

public class PlayerUnitSpawner : UnitSpawner<PlayerUnitData>
{
    // 생성된 플레이어 유닛 리스트
    public SpawnedPlayerUnitList UnitList = new SpawnedPlayerUnitList();


    // 유닛 스폰이 가능한지 아닌지 판별을 위한 필드
    public int MaxFarmingUnitCount;         // 일꾼 유닛



    protected override void Start()
    {
        base.Start();

        MaxFarmingUnitCount = 5;

        for (int i=0; i<_units.Count; i++)
        {
            PoolManager.Instance.CreatePool(_units[i].UnitType.ToString(), () => Instantiate(_units[i].UnitPrefab, transform.position, Quaternion.identity, transform));
        }
    }

    public void Spawn(PlayerUnitType unitType) 
    {
        // 농부가 아닌 유닛은 생성후에 자신의 앞에 있는 유닛의 참조를 저장
        if (unitType != PlayerUnitType.Farmer)
        {
            PlayerUnit spawnedUnit = PoolManager.Instance.Get(unitType.ToString()).transform.GetChild(0).GetComponent<PlayerUnit>();

            if (_prevUnit == null)
            {
                spawnedUnit.SetPrevUnit(spawnedUnit);
            }
            else
            {
                spawnedUnit.SetPrevUnit(_prevUnit);
            }

            _prevUnit = spawnedUnit;
        }
        // 농부
        else
        {
            PlayerFarmingUnit spawnedUnit = PoolManager.Instance.Get(unitType.ToString()).transform.GetChild(0).GetComponent<PlayerFarmingUnit>();

        }

        
    }

    



}
