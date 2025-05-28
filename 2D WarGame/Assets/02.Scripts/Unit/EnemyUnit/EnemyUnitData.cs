using UnityEngine;


[CreateAssetMenu(fileName = "EnemyUnit", menuName = "Scriptable Objects/Unit/EnemyUnit")]
public class EnemyUnitData : UnitData
{
    [field: SerializeField] public EnemyUnitType UnitType { get; set; }
}
