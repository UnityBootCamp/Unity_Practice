using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerUnitSpawner : MonoBehaviour
{


    //������ �����͸� ��� Scriptable Object
    [SerializeField] List<PlayerUnitData> _units;
    public List<PlayerUnitData> Units => _units;
    public SpawnedPlayerUnitList UnitList = new SpawnedPlayerUnitList();


    // ���� ������ �������� �ƴ��� �Ǻ��� ���� �ʵ�
    public int MaxUnitCount;
    public int MaxFarmingUnitCount;

    // ������ ������ ���ֿ� ���� ����
    PlayerUnit _prevUnit;

   

    void Start()
    {
        MaxUnitCount = 10;
        MaxFarmingUnitCount = 5;


        for (int i=0; i<_units.Count; i++)
        {
            PoolManager.Instance.CreatePool(_units[i].UnitType.ToString(), () => Instantiate(_units[i].UnitPrefab, transform.position, Quaternion.identity, transform));
        }
    }

    public void Spawn(PlayerUnitType unitType) 
    {
        // ��ΰ� �ƴ� ������ �����Ŀ� �ڽ��� �տ� �ִ� ������ ������ ����
        if (unitType != PlayerUnitType.farmer)
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
