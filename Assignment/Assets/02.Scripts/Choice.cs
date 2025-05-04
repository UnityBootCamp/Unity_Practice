using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Choice: MonoBehaviour
{
    
    TextController _textController;
    public Button _button;
    public TextMeshProUGUI _buttonText;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _button = GetComponent<Button>();
        _buttonText = GetComponentInChildren<TextMeshProUGUI>();
        _textController = FindFirstObjectByType<TextController>();


        _button.onClick.AddListener(ChooseAnswer);
    }

    void ChooseAnswer()
    {
        if (_textController.isUiVisable == true || _textController.isPrintingLylics)
            return;

        if(_buttonText.text == _textController.dialogs[_textController.dialogIndex].correctChoice)
        {
            _textController.correctUI.gameObject.SetActive(true);
        }
        else
        {
            _textController.wrongUI.gameObject.SetActive(true);
        }

        _textController.isUiVisable = true;
    }

    
}
