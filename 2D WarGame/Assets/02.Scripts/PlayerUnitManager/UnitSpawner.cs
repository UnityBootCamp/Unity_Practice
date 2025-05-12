using UnityEngine;

public class UnitSpawner : MonoBehaviour
{
    [SerializeField] Unit[] _units;

    void Start()
    {
        PoolManager.Instance.CreatePool("PlayerUnit", () => Instantiate(_units[0].UnitPrefab, transform.position, Quaternion.identity, transform));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
