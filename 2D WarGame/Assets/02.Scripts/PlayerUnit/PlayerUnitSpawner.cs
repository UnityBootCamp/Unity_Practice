using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerUnitSpawner : MonoBehaviour
{
    [SerializeField] List<PlayerUnitData> _units;
    public List<PlayerUnitData> Units => _units;

    public SpawnedPlayerUnitList unitList = new SpawnedPlayerUnitList();

    PlayerUnit _prevUnit;

    void Start()
    {
        for(int i=0; i<_units.Count; i++)
        {
            PoolManager.Instance.CreatePool(_units[i].UnitType.ToString(), () => Instantiate(_units[i].UnitPrefab, transform.position, Quaternion.identity, transform));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
        }
    }

    public void Spawn(PlayerUnitType unitType) 
    {
        PlayerUnit spawnedUnit = PoolManager.Instance.Get(unitType.ToString()).transform.GetChild(0).GetComponent<PlayerUnit>();

        if(_prevUnit == null)
        {
            spawnedUnit.SetPrevUnit(spawnedUnit);
        }
        else
        {
            spawnedUnit.SetPrevUnit(_prevUnit);
        }

        _prevUnit = spawnedUnit;

        
    }

    

}
