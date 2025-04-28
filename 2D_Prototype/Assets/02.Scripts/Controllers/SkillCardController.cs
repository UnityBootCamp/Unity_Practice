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
        [SerializeField] InputActionReference _leftClick;           // 참조할 Input Action
        [SerializeField] GraphicRaycaster _graphicRaycaster;        // 카메라에서 UI 까지. graphicRaycaster 참조
        [SerializeField] SkillCardList _skillCardList;
        PointerEventData _pointerEventData;                         // '마우스 이벤트가 발생할때'마다 정보 갱신
        List<RaycastResult> _results = new List<RaycastResult>();   // Raycast 해서 얻은 결과 저장하는 리스트
       

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
            // 눌린 상태에선 동작 x 마우스를 떼면 동작
            if (context.ReadValueAsButton())
                return;

            // 슬롯을 클릭했는지
            if (TryCastSkillCard(out SkillCard skillCard) == false)
                return;

            _skillCardList.ChooseCardInList(skillCard.Index);



        }

        bool TryCastSkillCard(out SkillCard skillCard)
        {
            _results.Clear(); // 캐스팅 결과 캐시 지움
            _pointerEventData.position = Mouse.current.position.value; // 현재 마우스 위치에 발생한 이벤트 데이터 초기화
            _graphicRaycaster.Raycast(_pointerEventData, _results); // 마우스 이벤트데이터로 캐스팅

            // 캐스팅된 요소가 있다
            if (_results.Count > 0)
            {
                // slot UI 가 캐스팅 되었다.
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

