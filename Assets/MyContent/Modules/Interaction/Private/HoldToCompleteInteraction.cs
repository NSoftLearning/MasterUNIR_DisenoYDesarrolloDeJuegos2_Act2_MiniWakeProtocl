using System;
using DG.Tweening;
using Modules.Interaction.API;
using UnityEngine;

namespace Modules.Interaction.Private
{
    public class HoldToCompleteInteraction :
        MonoBehaviour,
        IInteractableReadData,
        IHoldToCompleteInteraction
    {
        [SerializeField] private string prompt = "Mantener pulsado para activar";
        [SerializeField, Min(0.1f)] private float completionDuration = 5f;
        [SerializeField] private bool progressEnabled = true;

        private IInteractable _interactable;
        private Tween _progressTween;
        private float _progress;
        private bool _isHeld;

        public string InteractionPrompt => prompt;
        public bool IsCompleted { get; private set; }

        public event Action Completed;
        public event Action<float> ProgressChanged;

        private void Awake()
        {
            _interactable = GetComponent<IInteractable>();

            _progressTween = DOTween.To(
                    () => _progress,
                    SetProgress,
                    1f,
                    completionDuration)
                .SetEase(Ease.Linear)
                .SetAutoKill(false)
                .OnComplete(Complete)
                .Pause();
        }
        
        
        private void SetProgress(float value)
        {
            _progress = value;
            ProgressChanged?.Invoke(value);
        }
        private void OnEnable()
        {
            _interactable.InteractionStarted += StartProgress;
            _interactable.InteractionStopped += StopProgress;
        }

        private void StartProgress()
        {
            _isHeld = true;

            if (!progressEnabled || IsCompleted)
                return;

            _progressTween.Play();
        }

        private void StopProgress()
        {
            _isHeld = false;
            _progressTween.Pause();
        }

        public void EnableProgress()
        {
            progressEnabled = true;

            if (_isHeld && !IsCompleted)
                _progressTween.Play();
        }

        public void DisableProgress()
        {
            progressEnabled = false;
            _progressTween.Pause();
        }

        private void Complete()
        {
            IsCompleted = true;
            Completed?.Invoke();
        }

        private void OnDisable()
        {
            _interactable.InteractionStarted -= StartProgress;
            _interactable.InteractionStopped -= StopProgress;

            StopProgress();
        }

        private void OnDestroy()
        {
            _progressTween.Kill();
        }
    }
}