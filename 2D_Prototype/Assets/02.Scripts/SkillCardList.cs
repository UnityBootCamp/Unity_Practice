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
    Queue<SkillCardData> _skillQueue;                    // ���� ������� ��ųī�� ť
    List<SkillCardData> _skillSequence;                  // ������ ������ ����� ��ų���� ���� ����
    List<SkillCard> _skillList;                   // ���� �÷�� ������ �ִ� ��ųī�嵥���� ���
    [SerializeField] Image _defaultSkillCard;     // sprite�� ����ִ� �⺻ ��ų ī��


    [Header("About PC")]
    [SerializeField] PlayableCharacter[] _playableCharacters;    // ���� ���� �÷��̾�� ĳ���� ���
    List<SkillCardData> _playerSkills;                                 // �÷��̾ ���� �� �ִ� ��ų ����


    [Header("For Casting Skill")]
    [SerializeField] GameObject _usingCardList;          // ����� ī���� ����Ʈ
    Image[] _usingCardSlots;                            // �����Ͽ� ����Ϸ��� �� ī�� �迭


    [SerializeField] SkillCardData _moveCard;
    int[] _deltaOfCardLeftWay;                     // ������, ī�弱�� �ܰ迡 �� �ε������� ���õ� Ƚ��
    int _choosenCardsCount;                             // ������ ī�� ��
    bool _isUsingPhase;

    const int MAX_SKILL_CARD_COUNT = 10;               // �÷��̾ �� �Ͽ� ���������� ī�� ��
                          


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
            //�ڷ�ƾ ����
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
            //�ڷ�ƾ ����
            StartCoroutine(UseCardDelay());
        }

    }

    public void ChooseCardInList(int index)
    {
        // 4�� �̻� ������� ���� x
        if (_choosenCardsCount >= 4)
            return;

        _isUsingPhase = true;
        // ���ī�彽���� ��������Ʈ�� ����Ʈ���� ������ ī���� ��������Ʈ�� ����
        _usingCardSlots[_choosenCardsCount].sprite = _skillList[index+_deltaOfCardLeftWay[index]].SkillCardImage.sprite;

        // ������ ī�� UI �ν��Ͻ� ��Ȱ��ȭ
        _skillList[index+_deltaOfCardLeftWay[index]].SkillCardImage.gameObject.SetActive(false);

        
        // delta ����
        for(int i = index; i<_deltaOfCardLeftWay.Length; i++)
        {
            _deltaOfCardLeftWay[i]++;
        }

     
        // ���� �� ī�� �� 1 ����
        _choosenCardsCount++;

        // ���� �� ī�尡 4�����
        if (_choosenCardsCount == 4)
        {
            //�ڷ�ƾ ����
            StartCoroutine(UseCardDelay());
        }
    }

    IEnumerator UseCardDelay()
    {
        // 3�� ������
        yield return new WaitForSeconds(3);
        UseCard();
    }

    public void UseCard()
    {
        // ���Կ� �ִ� ��� ��������Ʈ null
        while (_choosenCardsCount > 0)
        {
            _usingCardSlots[_choosenCardsCount - 1].sprite = null;
            _choosenCardsCount--;
        }

        
        // ���� �÷��̾ ������ �ִ� ��ų ī�� ����Ʈ�� �������� ���鼭
        for(int i =_skillList.Count-1; i >=0; i--)
        {
            // ����, ��Ȱ��ȭ ������ ������Ʈ�� �����Ѵٸ�
            if (_skillList[i].SkillCardImage.gameObject.activeSelf == false)
            {
                // UI �ν��Ͻ��� �ı��ϰ�
                Destroy(_skillList[i].SkillCardImage.gameObject);

                // �ش� �ε����� ī�带 ����Ʈ���� Remove�Ѵ�
                _skillList.RemoveAt(i);
            }
        }

        _isUsingPhase = false ;
        _deltaOfCardLeftWay = new int[MAX_SKILL_CARD_COUNT];
    }

    /// <summary>
    /// ī�� ���� ���
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
        // SkillList �� ī�� ���� 10�� ���Ϸ� �ٸ�
        while (_skillList.Count < 10)
        {
            // Skill ī�� ��� ť�� ����ִٸ� �ٽ� ä���
            if(_skillQueue.Count == 0)
            {
                RefillQueue();
            }

            SkillCard newSkillCard = new SkillCard(); // ����ִ� �̹���  �� �ν��Ͻ��� �ڽ����� �����ؼ� 
            newSkillCard.SkillCardData = _skillQueue.Dequeue(); // ��ų ��� ť���� dequeue �� ��������Ʈ�� �̹��� ��������Ʈ �����ϰ�
            newSkillCard.SkillCardImage = Instantiate(_defaultSkillCard, transform); // �̹��� �����Ͽ� SkillCardList �ν��Ͻ��� �ڽ����� �����ϰ�, ������ SkillCard �ν��Ͻ��� �ʵ忡 ���� �����Ѵ�.
            newSkillCard.Index = _skillList.Count;
            newSkillCard.Init();
            
            _skillList.Add(newSkillCard);       // _skillCardList�� �߰�
        }
    }
    
    /// <summary>
    /// Queue �ٽ� ä���
    /// </summary>
    public void RefillQueue()
    {
        for (int i = 0; i < _skillSequence.Count; i++)
        {
            _skillQueue.Enqueue(_skillSequence[i]);
        }
    }

    /// <summary>
    /// Queue�� ä��� ��ų ī�� ���� ����
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
    /// ���� 1ȸ ����Ʈ ����
    /// </summary>
    /// <param name="list"> ����Ʈ �Է� </param>
    /// <returns> ������ ����Ʈ ��ȯ </returns>
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
