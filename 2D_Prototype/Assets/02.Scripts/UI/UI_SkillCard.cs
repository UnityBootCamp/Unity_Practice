using UnityEngine;
using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Unity.VisualScripting;

public class UI_SkillCard : MonoBehaviour, IPointerUpHandler, IPointerClickHandler, IPointerDownHandler
{
    [SerializeField] SkillCardList _skillCardList;
    float cellSizeX;
    float cellSpacing;
    float screenRatio;

    private void Awake()
    {
        _skillCardList = FindFirstObjectByType<SkillCardList>();
        screenRatio = Screen.width / 1920f;
        cellSizeX = _skillCardList.GetComponent<GridLayoutGroup>().cellSize.x * screenRatio;
        cellSpacing = _skillCardList.GetComponent<GridLayoutGroup>().spacing.x * screenRatio;
        
    }

    public Action OnClicked;
    public Action OnPressed;
    int _index;
    int _preIndex;
    int _nextIndex;
    const int MAX_SKILL_CARD_COUNT = 10;

    void Update()
    {

    }

    // Å¬¸¯
    public void OnPointerClick(PointerEventData eventData)
    {
        
        _index = Mathf.FloorToInt((Screen.width - eventData.position.x) / (cellSizeX + cellSpacing));
        _skillCardList.ChooseCardInList(_index);

        Debug.Log($"click Index : {_index}");
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        _preIndex = Mathf.FloorToInt((Screen.width - eventData.position.x) / (cellSizeX + cellSpacing));
        
        Debug.Log($"pre Index : {_preIndex}");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _nextIndex = Mathf.FloorToInt((Screen.width - eventData.position.x) / (cellSizeX + cellSpacing));
        
        Debug.Log($"next Index : {_nextIndex}");


        if (_nextIndex > MAX_SKILL_CARD_COUNT - 1)
        {
            _nextIndex = MAX_SKILL_CARD_COUNT - 1;
        }

        if (_preIndex < 0)
        {
            _preIndex = 0;
        }

        if (_preIndex == _nextIndex)
            return;

        if (_preIndex < _nextIndex)
        {
            _skillCardList.CardListReplaceToLeftWay(_preIndex, _nextIndex);
        }

        if (_preIndex > _nextIndex)
        {
            _skillCardList.CardListReplaceToRightWay(_preIndex, _nextIndex);
        }

    }

    
    
}
