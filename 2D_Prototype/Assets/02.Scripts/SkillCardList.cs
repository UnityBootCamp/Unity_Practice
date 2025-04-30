using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;
using PT.Models;
using UnityEngine.UI;
using Unity.VisualScripting;


public class SkillCardList : MonoBehaviour
{

    [Header("For Generate Skill List")]
    Queue<SkillCardData> _skillQueue;                    // 생성 대기중인 스킬카드 큐
    List<SkillCardData> _skillSequence;                  // 랜덤한 순서로 저장된 스킬들의 생성 순서
    List<SkillCard> _skillList;                   // 현재 플레어가 가지고 있는 스킬카드데이터 목록
    [SerializeField] Image _defaultSkillCard;     // sprite가 비어있는 기본 스킬 카드


    [Header("About PC")]
    [SerializeField] PlayableCharacter[] _playableCharacters;    // 현재 편성한 플레이어블 캐릭터 목록
    List<SkillCardData> _playerSkills;                                 // 플레이어가 가질 수 있는 스킬 종류


    [Header("For Casting Skill")]
    [SerializeField] GameObject _usingCardList;          // 사용할 카드목록 리스트
    Image[] _usingCardSlots;                            // 현재턴에 사용하려고 고른 카드 배열


    [SerializeField] SkillCardData _moveCard;
    int[] _deltaOfCardLeftWay;                     // 현재턴, 카드선택 단계에 각 인덱스별로 선택된 횟수
    int _choosenCardsCount;                             // 선택한 카드 수
    bool _isUsingPhase;

    const int MAX_SKILL_CARD_COUNT = 10;               // 플레이어가 한 턴에 소지가능한 카드 수
                          


    private void Start()
    {

        _skillList = new List<SkillCard>(MAX_SKILL_CARD_COUNT);
        _playerSkills = new List<SkillCardData>(_playableCharacters.Length * 2);
        _skillQueue = new Queue<SkillCardData>(_playableCharacters.Length * 2);
        _skillSequence = new List<SkillCardData>(_playableCharacters.Length * 2);
        _deltaOfCardLeftWay = new int[MAX_SKILL_CARD_COUNT];

        _isUsingPhase = false;
        _usingCardSlots = _usingCardList.GetComponentsInChildren<Image>();
        _choosenCardsCount = 0;


        CreateSkillSequence();

        FillSkillList();

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            FillSkillList();
        }
        if (Input.GetMouseButtonDown(1))
        {
            UnChooseCardInList();
        }
    }

    public void CardPositionSwap(int Card1, int Card2)
    {
        SkillCard tmp = _skillList[Card1];
        _skillList[Card1] = _skillList[Card2];
        _skillList[Card2] = tmp;

        Image tmpImage = _skillList[Card1].SkillCardImage;
        _skillList[Card1].SkillCardImage = _skillList[Card2].SkillCardImage;
        _skillList[Card2].SkillCardImage = tmpImage;

        _skillList[Card1].UpdateCard();
        _skillList[Card2].UpdateCard();
    }

    public void CardListReplaceToLeftWay(int startIndex, int targetIndex)
    {
        if (_isUsingPhase)
            return;

        SkillCard tmpSkillCard = _skillList[startIndex];
        targetIndex = targetIndex + _deltaOfCardLeftWay[targetIndex];

        for(int i = startIndex; i<targetIndex; i++)
        {
        
            CardPositionSwap(i, i + 1);
        }

        _skillList[targetIndex] = tmpSkillCard;
        _skillList[targetIndex].SkillCardImage = tmpSkillCard.SkillCardImage;
        _skillList[targetIndex].UpdateCard();

        _usingCardSlots[_choosenCardsCount].sprite = _moveCard.SkillCardSprite;
        _choosenCardsCount++;

        if (_choosenCardsCount == 4)
        {
            //코루틴 실행
            StartCoroutine(UseCardDelay());
        }

    }

    public void CardListReplaceToRightWay(int startIndex, int targetIndex)
    {
        if (_isUsingPhase)
            return;

        SkillCard tmpSkillCard = _skillList[startIndex];
        targetIndex = targetIndex - _deltaOfCardLeftWay[targetIndex];

        for (int i = startIndex; i > targetIndex; i--)
        {
            CardPositionSwap(i, i - 1);
        }

        _skillList[targetIndex] = tmpSkillCard;
        _skillList[targetIndex].SkillCardImage = tmpSkillCard.SkillCardImage;
        _skillList[targetIndex].UpdateCard();

        _usingCardSlots[_choosenCardsCount].sprite = _moveCard.SkillCardSprite;
        _choosenCardsCount++;

        if (_choosenCardsCount == 4)
        {
            //코루틴 실행
            StartCoroutine(UseCardDelay());
        }

    }

    public void ChooseCardInList(int index)
    {
        // 4개 이상 골랐으면 실행 x
        if (_choosenCardsCount >= 4)
            return;

        _isUsingPhase = true;
        // 사용카드슬롯의 스프라이트를 리스트에서 선택한 카드의 스프라이트로 변경
        _usingCardSlots[_choosenCardsCount].sprite = _skillList[index+_deltaOfCardLeftWay[index]].SkillCardImage.sprite;

        // 선택한 카드 UI 인스턴스 비활성화
        _skillList[index+_deltaOfCardLeftWay[index]].SkillCardImage.gameObject.SetActive(false);

        
        // delta 조정
        for(int i = index; i<_deltaOfCardLeftWay.Length; i++)
        {
            _deltaOfCardLeftWay[i]++;
        }

     
        // 현재 고른 카드 수 1 증가
        _choosenCardsCount++;

        // 현재 고른 카드가 4개라면
        if (_choosenCardsCount == 4)
        {
            //코루틴 실행
            StartCoroutine(UseCardDelay());
        }
    }

    IEnumerator UseCardDelay()
    {
        // 3초 딜레이
        yield return new WaitForSeconds(3);
        UseCard();
    }

    public void UseCard()
    {
        // 슬롯에 있는 모든 스프라이트 null
        while (_choosenCardsCount > 0)
        {
            _usingCardSlots[_choosenCardsCount - 1].sprite = null;
            _choosenCardsCount--;
        }

        
        // 현재 플레이어가 가지고 있는 스킬 카드 리스트를 역순으로 돌면서
        for(int i =_skillList.Count-1; i >=0; i--)
        {
            // 만약, 비활성화 상태인 오브젝트가 존재한다면
            if (_skillList[i].SkillCardImage.gameObject.activeSelf == false)
            {
                // UI 인스턴스를 파괴하고
                Destroy(_skillList[i].SkillCardImage.gameObject);

                // 해당 인덱스의 카드를 리스트에서 Remove한다
                _skillList.RemoveAt(i);
            }
        }

        _isUsingPhase = false ;
        _deltaOfCardLeftWay = new int[MAX_SKILL_CARD_COUNT];
    }

    /// <summary>
    /// 카드 선택 취소
    /// </summary>
    public void UnChooseCardInList()
    {
        if (_choosenCardsCount <= 0)
            return;
        
        while (_choosenCardsCount > 0)
        {
            _usingCardSlots[_choosenCardsCount-1].sprite = null;
            _choosenCardsCount--;
        }

        for(int i=0; i<MAX_SKILL_CARD_COUNT; i++)
        {
            _skillList[i].SkillCardImage.gameObject.SetActive(true);
        }

        _isUsingPhase = false;
        _deltaOfCardLeftWay = new int[MAX_SKILL_CARD_COUNT];
    }

    public void FillSkillList()
    {
        // SkillList 의 카드 수가 10개 이하로 줄면
        while (_skillList.Count < 10)
        {
            // Skill 카드 대기 큐가 비어있다면 다시 채우고
            if(_skillQueue.Count == 0)
            {
                RefillQueue();
            }

            SkillCard newSkillCard = new SkillCard(); // 비어있는 이미지  이 인스턴스의 자식으로 생성해서 
            newSkillCard.SkillCardData = _skillQueue.Dequeue(); // 스킬 대기 큐에서 dequeue 한 스프라이트로 이미지 스프라이트 변경하고
            newSkillCard.SkillCardImage = Instantiate(_defaultSkillCard, transform); // 이미지 생성하여 SkillCardList 인스턴스의 자식으로 변경하고, 생성된 SkillCard 인스턴스의 필드에 참조 전달한다.
            newSkillCard.Index = _skillList.Count;
            newSkillCard.Init();
            
            _skillList.Add(newSkillCard);       // _skillCardList에 추가
        }
    }
    
    /// <summary>
    /// Queue 다시 채우기
    /// </summary>
    public void RefillQueue()
    {
        for (int i = 0; i < _skillSequence.Count; i++)
        {
            _skillQueue.Enqueue(_skillSequence[i]);
        }
    }

    /// <summary>
    /// Queue에 채우는 스킬 카드 순서 구성
    /// </summary>
    public void CreateSkillSequence()
    {
        foreach (PlayableCharacter pc in _playableCharacters)
        {
            _playerSkills.Add(pc.Skill1);
            _playerSkills.Add(pc.Skill2);
        }

        _skillSequence = new List<SkillCardData>(_playerSkills.Count);

        List<SkillCardData> tempList = Shuffle(_playerSkills);

        for (int i = 0; i < tempList.Count; i++)
        {
            _skillSequence.Add(tempList[i]);
        }
    }

    /// <summary>
    /// 최초 1회 리스트 셔플
    /// </summary>
    /// <param name="list"> 리스트 입력 </param>
    /// <returns> 셔플한 리스트 반환 </returns>
    public List<SkillCardData> Shuffle(List<SkillCardData> list)
    {
        Random random = new Random();
        List<SkillCardData> randomSkillList= new List<SkillCardData>(list.Count);

        int n = list.Count;

        for (int i = 0; i<n; i++)
        {
            randomSkillList.Add(list[i]);
        }

        for (int i = 0; i < n; i++)
        {
            int j = random.Next(i, n);

            if (j != i)
            {
                SkillCardData temp = randomSkillList[i];
                randomSkillList[i] = randomSkillList[j];
                randomSkillList[j] = temp;
            }
        }

        return randomSkillList;
    }
}
