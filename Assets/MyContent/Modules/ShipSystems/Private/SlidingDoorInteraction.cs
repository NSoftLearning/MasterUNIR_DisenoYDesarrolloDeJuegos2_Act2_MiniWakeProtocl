using System;
using DG.Tweening;
using Modules.Interaction.API;
using UnityEngine;

namespace Modules.ShipSystems.Private
{
    public class SlidingDoorInteraction : MonoBehaviour, IInteractableReadData
    {
        [SerializeField] private Transform leftPanel;
        [SerializeField] private Transform rightPanel;

        [SerializeField, Min(0f)] private float openingDistance = 1.2f;
        [SerializeField, Min(0.01f)] private float openingDuration = 1f;

        private Sequence _movement;
        private IInteractable _interactable;
        public string InteractionPrompt => "Abrir puerta";
        private void Awake()
        {
            _interactable = GetComponent<IInteractable>();
            
            Vector3 leftTarget =
                leftPanel.localPosition + Vector3.left * openingDistance;

            Vector3 rightTarget =
                rightPanel.localPosition + Vector3.right * openingDistance;

            _movement = DOTween.Sequence()
                .Join(leftPanel
                    .DOLocalMove(leftTarget, openingDuration)
                    .SetEase(Ease.InOutSine))
                .Join(rightPanel
                    .DOLocalMove(rightTarget, openingDuration)
                    .SetEase(Ease.InOutSine))
                .SetAutoKill(false)
                .Pause();
        }

        public void Open()
        {
            _movement.PlayForward();
        }

        public void Close()
        {
            _movement.PlayBackwards();
        }

        private void OnEnable()
        { 
            _interactable.InteractionStarted += Open;
        }

        private void OnDisable()
        {
            _interactable.InteractionStarted -= Open;
            _movement?.Pause();
        }

        private void OnDestroy()
        {
            _movement?.Kill();
        }

        
    }
}