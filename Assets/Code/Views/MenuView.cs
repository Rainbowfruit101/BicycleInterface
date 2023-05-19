using BicycleInterface.Code.Views.Core;
using DG.Tweening;
using UnityEngine;

namespace BicycleInterface.Code.Views
{
    public class MenuView: View
    {
        [SerializeField] private float animationDuration = 2f;
        [SerializeField] private RectTransform hiddenRectTransform;
        
        private Vector2 _shownPosition;

        public override bool IsShown { get; protected set; }
        public override bool IsInteractable { get; protected set; }
        public override RectTransform SelfTransform => transform as RectTransform;

        private void Awake()
        {
            _shownPosition = SelfTransform.position;
            SelfTransform.position = hiddenRectTransform.position;
            SelfTransform.localScale = hiddenRectTransform.localScale;
            gameObject.SetActive(false);
            IsInteractable = true;
        }

        [ContextMenu("Show")]
        public override void Show()
        {
            IsInteractable = false;
            gameObject.SetActive(true);
            
            var sequence = DOTween.Sequence()
                .Join(SelfTransform
                    .DOMove(_shownPosition, animationDuration)
                    .SetEase(Ease.OutBack)
                )
                .Join(SelfTransform
                    .DOScale(1, animationDuration)
                    .SetEase(Ease.OutBack)
                );

            sequence.OnComplete(() =>
            {
                IsShown = true;
                IsInteractable = true;
            });
        }

        [ContextMenu("Hide")]
        public override void Hide()
        {
            IsInteractable = false;
            
            var sequence = DOTween.Sequence()
                .Join(SelfTransform
                    .DOMove(hiddenRectTransform.position, animationDuration)
                    .SetEase(Ease.InBack)
                )
                .Join(SelfTransform
                    .DOScale(hiddenRectTransform.localScale, animationDuration)
                    .SetEase(Ease.InBack)
                );

            sequence.OnComplete(() =>
            {
                gameObject.SetActive(false);
                IsShown = false;
                IsInteractable = true;
            });
        }
    }
}