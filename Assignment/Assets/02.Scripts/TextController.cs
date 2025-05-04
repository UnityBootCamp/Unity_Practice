using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _nameOfSongText;
    [SerializeField] TextMeshProUGUI[] _choicesText;
    [SerializeField] TextMeshProUGUI _lylicsOfSongText;

    [SerializeField] Button _button;
    [SerializeField]GameObject _finishUI;

    public GameObject correctUI;
    public GameObject wrongUI;

    public Dialog[] dialogs;
    public int dialogIndex;
    const int AMOUNT_OF_CHOICE = 3;
    public bool isUiVisable;
    public bool isPrintingLylics;

    private void Awake()
    {
        dialogIndex = 0;
        _lylicsOfSongText.text = "";
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetSong();
        _button.onClick.AddListener(SetNextLylics);
    }


    IEnumerator C_TextPrinting(string lylics)
    {
        isPrintingLylics = true;
        for (int i = 0; i < lylics.Length; i++)
        {
            _lylicsOfSongText.text += lylics[i];
            yield return new WaitForSeconds(0.05f);
        }
        isPrintingLylics = false;
    }



    void SetNextLylics()
    {
        if (_finishUI.activeSelf || isPrintingLylics)
            return;

        _lylicsOfSongText.text = "";
        if (isUiVisable == false)
        {
            //_lylicsOfSongText.text = dialogs[dialogIndex].lylicsOfSong2;
            StartCoroutine(C_TextPrinting(dialogs[dialogIndex].lylicsOfSong2));
        }

        if (isUiVisable)
        {
            dialogIndex++;

            if(dialogIndex >= dialogs.Length)
            {
                correctUI.gameObject.SetActive(false);
                wrongUI.gameObject.SetActive(false);
                _finishUI.SetActive(true);
                return;
            }


            correctUI.gameObject.SetActive(false);
            wrongUI.gameObject.SetActive(false);
            isUiVisable = false;
            SetSong();

        }
    }

    void SetSong()
    {
        _nameOfSongText.text = dialogs[dialogIndex].nameOfSong;
        StartCoroutine(C_TextPrinting(dialogs[dialogIndex].lylicsOfSong1));
        //_lylicsOfSongText.text = dialogs[dialogIndex].lylicsOfSong1;

        Queue<string> dialogQueue = new Queue<string>(AMOUNT_OF_CHOICE);


        // 옳은 답과 틀린 답 삽입
        dialogQueue.Enqueue(dialogs[dialogIndex].correctChoice);

        for (int i = 0; i < AMOUNT_OF_CHOICE-1; i++)
        {
            dialogQueue.Enqueue(dialogs[dialogIndex].wrongChoices[i]);
        }

        // 큐 섞기
        shuffleQueue(dialogQueue, Random.Range(0, 5));
        
        // 선택지 텍스트 설정
        for(int i = 0; i<AMOUNT_OF_CHOICE; i++)
        {
            _choicesText[i].text = dialogQueue.Dequeue();
        }

    }

    void shuffleQueue(Queue<string> queue, int shuffleCount)
    {
        for(int i=0; i<shuffleCount; i++)
        {
            string temp = queue.Dequeue();
            queue.Enqueue(temp);
        }
    }

}
