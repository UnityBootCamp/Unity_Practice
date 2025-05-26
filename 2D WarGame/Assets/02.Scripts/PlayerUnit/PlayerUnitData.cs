using System;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public enum PlayerUnitType
{
    farmer,
    swordMan,
    archer,
    paladin,
    knight,
}

[CreateAssetMenu(fileName = "Unit", menuName = "Scriptable Objects/Unit")]
public class PlayerUnitData : ScriptableObject
{
    [field: SerializeField] public GameObject UnitPrefab { get; set; }
    [field: SerializeField] public Sprite UnitPortrait { get; set; }
    [field: SerializeField] public PlayerUnitType UnitType { get; set; }
    [field: SerializeField] public float SpawnCoolDown { get; set; }
    [field: SerializeField] public float Hp { get; set; }
    [field: SerializeField] public float AttackForce { get; set; }
    [field: SerializeField] public float MoveSpeed { get; set; }
    [field: SerializeField] public float AttackDelay { get; set; }

    
}
