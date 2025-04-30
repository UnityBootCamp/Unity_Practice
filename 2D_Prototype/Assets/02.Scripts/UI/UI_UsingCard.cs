using UnityEngine;
using UnityEngine.EventSystems;

public class UI_UsingCard : MonoBehaviour, IPointerClickHandler

{
    [SerializeField] SkillCardList _skillCardList;

    public void OnPointerClick(PointerEventData eventData)
    {
        _skillCardList.UnChooseCardInList();
    }
}
