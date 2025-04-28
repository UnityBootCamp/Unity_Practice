using UnityEngine;
using UnityEngine.UI;

namespace PT.Models {
    public class SkillCard
    {


        public Image SkillCardImage
        {
            get
            {
                return _skillCardImage;
            }

            set
            {
                _skillCardImage = value;
            }
        }

        public SkillCardData SkillCardData
        {
            get
            {
                return _skillCardData;
            }

            set
            {
                _skillCardData = value;
            }
        }

        public void Init()
        {
            _skillCardImage.sprite = _skillCardData.SkillCardSprite;
            _skillCardId = _skillCardData.SkillCardId;

        }

        public void UpdateCard()
        {
            _skillCardImage.sprite = _skillCardData.SkillCardSprite;
        }

        public int Index;
        public bool IsUsed;
        int _skillCardId;
        int _skillCardGrade;

        SkillCardData _skillCardData;   // UI에 작성 할 데이터셋
        Image _skillCardImage;          // 참조하는 Image UI

    }

}