using UnityEngine;

[CreateAssetMenu(fileName = "Unit", menuName = "Scriptable Objects/Unit")]
public class Unit : ScriptableObject
{
    [field: SerializeField] public GameObject UnitPrefab { get; set; }
    [field: SerializeField] public float Hp { get; set; }
    [field: SerializeField] public float AttackForce { get; set; }
    [field: SerializeField] public float MoveSpeed { get; set; }
    [field: SerializeField] public float AttackDelay { get; set; }

}
