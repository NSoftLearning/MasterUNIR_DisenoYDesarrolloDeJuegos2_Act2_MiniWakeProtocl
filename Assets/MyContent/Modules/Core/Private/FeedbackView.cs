using DG.Tweening;
using Modules.Core.API;
using Modules.EventHandling.API;
using TMPro;
using UnityEngine;
using Zenject;

namespace Modules.Core.Private
{
    public class FeedbackView : MonoBehaviour,
        IDomainEventHandler<FeedbackRequested>
    {
        [SerializeField] private TMP_Text feedbackText;
        [SerializeField] private CanvasGroup feedbackGroup;

        [SerializeField, Min(0f)] private float fadeInDuration = 0.15f;
        [SerializeField, Min(0f)] private float fadeOutDuration = 0.25f;

        private DomainEventBus _eventBus;
        private Sequence _animation;

        [Inject]
        private void Construct(DomainEventBus eventBus)
        {
            _eventBus = eventBus;
        }


        public void Handle(FeedbackRequested domainEvent)
        {
            _animation?.Kill();

            feedbackText.text = domainEvent.Message;
            feedbackGroup.alpha = 0f;

            _animation = DOTween.Sequence()
                .SetUpdate(true)
                .Append(feedbackGroup.DOFade(1f, fadeInDuration))
                .AppendInterval(domainEvent.Duration)
                .Append(feedbackGroup.DOFade(0f, fadeOutDuration))
                .OnComplete(ClearFeedback);
        }

        private void ClearFeedback()
        {
            feedbackGroup.alpha = 0f;
            feedbackText.text = string.Empty;
            _animation = null;
        }

        private void OnEnable()
        {
            ClearFeedback();
            _eventBus.RegisterHandler<FeedbackRequested>(this);
        }
        private void OnDisable()
        {
            _eventBus.UnregisterHandler<FeedbackRequested>(this);

            _animation?.Kill();
            ClearFeedback();
        }
    }
}