using UnityEngine;

[CreateAssetMenu(fileName = "SkillCard", menuName = "Scriptable Objects/SkillCard")]
public class SkillCardData : ScriptableObject
{
    [field : SerializeField] public int SkillCardId { get; private set; }
    [field: SerializeField] public Sprite SkillCardSprite { get; private set; }
}
