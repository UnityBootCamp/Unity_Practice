using UnityEngine;



[CreateAssetMenu(fileName = "PlayerUnit", menuName = "Scriptable Objects/Unit/PlayerUnit")]
public class PlayerUnitData : UnitData
{
    [field: SerializeField] public Sprite UnitPortrait { get; set; }        // ���� �ʻ�ȭ

    [field: SerializeField] public PlayerUnitType UnitType { get; set; }
}
