using System;
using DG.Tweening;
using Modules.Interaction.API;
using UnityEngine;

namespace Modules.ShipSystems.Private
{
    public class ButtonControlledSlidingDoorInteraction : MonoBehaviour
    {
        [SerializeField] private Transform leftPanel;
        [SerializeField] private Transform rightPanel;
        [SerializeField] private MonoBehaviour openInteractable;
        [SerializeField] private MonoBehaviour closeInteractable;
        
        [SerializeField, Min(0f)] private float openingDistance = 1.2f;
        [SerializeField, Min(0.01f)] private float openingDuration = 1f;

        
        private IInteractable _openInteractable;
        private IInteractable _closeInteractable;
        private Sequence _movement;

        private void Awake()
        {
            _openInteractable = (IInteractable)openInteractable;
            _closeInteractable = (IInteractable)closeInteractable;
            
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
            _openInteractable.InteractionStarted += Open;
            _closeInteractable.InteractionStarted += Close;
        }

        private void OnDisable()
        {
            _openInteractable.InteractionStarted -= Open;
            _closeInteractable.InteractionStarted -= Close;
            
            _movement?.Pause();
        }

        private void OnDestroy()
        {
            _movement?.Kill();
        }
    }
}