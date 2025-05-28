using Unity.VisualScripting;
using UnityEngine;

public class EnemyUnitSpawner : UnitSpawner<EnemyUnitData>
{

    EnemyUnit _prevEnemyUnit;

    protected override void Start()
    {
        base.Start();

        for (int i = 0; i < _units.Count; i++)
        {
            PoolManager.Instance.CreatePool(_units[i].UnitType.ToString(), () => Instantiate(_units[i].UnitPrefab, transform.position, Quaternion.identity, transform));
        }
    }

    private void Update()
    {
        
    }

    public void Spawn(EnemyUnitType unitType)
    {
        EnemyUnit spawnedUnit = PoolManager.Instance.Get(unitType.ToString()).transform.GetChild(0).GetComponent<EnemyUnit>();

        if (_prevEnemyUnit == null)
        {
            spawnedUnit.SetPrevUnit(spawnedUnit);
        }
        else
        {
            spawnedUnit.SetPrevUnit(_prevEnemyUnit);
        }

        _prevEnemyUnit = spawnedUnit;

    }





}
