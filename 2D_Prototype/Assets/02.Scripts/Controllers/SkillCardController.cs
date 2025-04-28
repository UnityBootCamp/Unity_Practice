using System.Collections.Generic;
using PT.Models;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;


namespace PT.Controllers
{
    public class SkillCardController : MonoBehaviour
    {
        [SerializeField] InputActionReference _leftClick;           // ������ Input Action
        [SerializeField] GraphicRaycaster _graphicRaycaster;        // ī�޶󿡼� UI ����. graphicRaycaster ����
        [SerializeField] SkillCardList _skillCardList;
        PointerEventData _pointerEventData;                         // '���콺 �̺�Ʈ�� �߻��Ҷ�'���� ���� ����
        List<RaycastResult> _results = new List<RaycastResult>();   // Raycast �ؼ� ���� ��� �����ϴ� ����Ʈ
       

        private void Awake()
        {
            _pointerEventData = new PointerEventData(EventSystem.current);

        }


        private void OnEnable()
        {
            _leftClick.action.performed += OnLeftClick;
            _leftClick.action.Enable();
        }

        private void OnDisable()
        {
            _leftClick.action.performed -= OnLeftClick;
            _leftClick.action.Disable();
        }

        void OnLeftClick(InputAction.CallbackContext context)
        {
            // ���� ���¿��� ���� x ���콺�� ���� ����
            if (context.ReadValueAsButton())
                return;

            // ������ Ŭ���ߴ���
            if (TryCastSkillCard(out SkillCard skillCard) == false)
                return;

            _skillCardList.ChooseCardInList(skillCard.Index);



        }

        bool TryCastSkillCard(out SkillCard skillCard)
        {
            _results.Clear(); // ĳ���� ��� ĳ�� ����
            _pointerEventData.position = Mouse.current.position.value; // ���� ���콺 ��ġ�� �߻��� �̺�Ʈ ������ �ʱ�ȭ
            _graphicRaycaster.Raycast(_pointerEventData, _results); // ���콺 �̺�Ʈ�����ͷ� ĳ����

            // ĳ���õ� ��Ұ� �ִ�
            if (_results.Count > 0)
            {
                // slot UI �� ĳ���� �Ǿ���.
                if (_results[0].gameObject.TryGetComponent(out skillCard))
                {
                    return true;
                }
            }

            skillCard = null;
            return false;

        }
    }
}

