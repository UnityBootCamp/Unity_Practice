using UnityEngine;

public class PlayerUnitSpawner : UnitSpawner<PlayerUnitData>
{
    // ������ �÷��̾� ���� ����Ʈ
    public SpawnedPlayerUnitList UnitList = new SpawnedPlayerUnitList();


    // ���� ������ �������� �ƴ��� �Ǻ��� ���� �ʵ�
    public int MaxFarmingUnitCount;         // �ϲ� ����



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
        // ��ΰ� �ƴ� ������ �����Ŀ� �ڽ��� �տ� �ִ� ������ ������ ����
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
        // ���
        else
        {
            PlayerFarmingUnit spawnedUnit = PoolManager.Instance.Get(unitType.ToString()).transform.GetChild(0).GetComponent<PlayerFarmingUnit>();

        }

        
    }

    



}
