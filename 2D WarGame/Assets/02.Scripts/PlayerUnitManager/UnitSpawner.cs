using Unity.VisualScripting;
using UnityEngine;

public class UnitSpawner : MonoBehaviour
{
    [SerializeField] UnitData[] _units;

    void Start()
    {
        for(int i=0; i<_units.Length; i++)
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

    public void Spawn(int index) 
    { 
        PoolManager.Instance.Get(_units[index].UnitType.ToString()); 
    }


}
