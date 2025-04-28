using UnityEngine;

public class PlayableCharacter : MonoBehaviour
{
    [field: SerializeField] public SkillCardData Skill1 { get; private set;}
    [field: SerializeField] public SkillCardData Skill2 { get; private set; }

}
