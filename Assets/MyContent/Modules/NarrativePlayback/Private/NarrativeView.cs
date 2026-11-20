using System;
using System.Collections.Generic;
using DG.Tweening;
using Modules.EventHandling.API;
using Modules.NarrativePlayback.API;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Modules.NarrativePlayback.Private
{
    public class NarrativeView : MonoBehaviour,
        INarrativePlaybackControl,
        IManualNarrativePlaybackControl,
        IDomainEventHandler<NarrativeRequested>
    {
        [SerializeField] private GameObject narrativePanel;
        [SerializeField] private TMP_Text speakerText;
        [SerializeField] private TMP_Text lineText;
        [SerializeField] private InputActionReference advanceNarrative;

        private readonly Queue<NarrativeSequence> _pending = new();

        private DomainEventBus _eventBus;
        private NarrativeSequence _currentSequence;
        private int _lineIndex;
        private Tween _lineTimer;
        private bool _suspended;
        private bool _manualPlayback;
        
        [Inject]
        private void Construct(DomainEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        private void OnEnable()
        {
            ClearView();

            _eventBus.RegisterHandler<NarrativeRequested>(this);
            advanceNarrative.action.performed += Advance;
        }

        public void Handle(NarrativeRequested domainEvent)
        {
            NarrativeSequence sequence = domainEvent.Sequence;

            if (sequence == null || sequence.Lines.Count == 0)
            {
                throw new ArgumentException(
                    "Narrative requires a sequence with at least one line");
            }

            _pending.Enqueue(sequence);

            if (_currentSequence == null)
                PlayNextSequence();
        }

        private void PlayNextSequence()
        {
            if (_suspended || _manualPlayback)
                return;

            if (_pending.Count == 0)
            {
                advanceNarrative.action.Disable();
                ClearView();
                return;
            }

            _currentSequence = _pending.Dequeue();
            _lineIndex = 0;

            narrativePanel.SetActive(true);
            advanceNarrative.action.Enable();

            _eventBus.Publish(
                new NarrativeStarted(_currentSequence, this));

            ShowLine();
        }

        private void ShowLine()
        {
            NarrativeLine line = _currentSequence.Lines[_lineIndex];

            speakerText.text = line.Speaker;
            speakerText.color = line.NameColor;
            lineText.text = line.Text;

            _lineTimer = DOVirtual.DelayedCall(
                line.Duration,
                AdvanceLine,
                true);
        }

        private void Advance(InputAction.CallbackContext context)
        {
            AdvanceLine();
        }

        private void AdvanceLine()
        {
            if (_manualPlayback || _suspended || _currentSequence == null)
                return;
            
            _lineTimer?.Kill();
            _lineTimer = null;

            _lineIndex++;

            if (_lineIndex < _currentSequence.Lines.Count)
            {
                ShowLine();
                return;
            }

            _eventBus.Publish(
                new NarrativeFinished(_currentSequence, this));

            _currentSequence = null;
            PlayNextSequence();
        }

        public void SuspendPlayback()
        {
            _suspended = true;

            _lineTimer?.Pause();
            advanceNarrative.action.Disable();
            narrativePanel.SetActive(false);
        }

        public void ResumePlayback()
        {
            _suspended = false;

            if (_manualPlayback)
                return;
            if (_currentSequence == null)
            {
                PlayNextSequence();
                return;
            }
            
            NarrativeLine line = _currentSequence.Lines[_lineIndex];
            speakerText.text = line.Speaker;
            speakerText.color = line.NameColor;
            lineText.text = line.Text;

            narrativePanel.SetActive(true);
            advanceNarrative.action.Enable();
            _lineTimer?.Play();
        }

        private void ClearView()
        {
            narrativePanel.SetActive(false);
            speakerText.text = string.Empty;
            lineText.text = string.Empty;
        }

        private void OnDisable()
        {
            _eventBus.UnregisterHandler<NarrativeRequested>(this);
            advanceNarrative.action.performed -= Advance;
            advanceNarrative.action.Disable();

            _lineTimer?.Kill();
            _lineTimer = null;

            _pending.Clear();
            _currentSequence = null;
            _suspended = false;
            _manualPlayback = false;

            ClearView();
        }

        public void BeginManualPlayback()
        {
            _manualPlayback = true;

            _lineTimer?.Pause();
            advanceNarrative.action.Disable();

            ClearView();
        }

        public void ShowManualLine(NarrativeLine line)
        {
            speakerText.text = line.Speaker;
            speakerText.color = line.NameColor;
            lineText.text = line.Text;

            narrativePanel.SetActive(true);
        }

        public void HideManualLine()
        {
            ClearView();
        }

        public void EndManualPlayback()
        {
            _manualPlayback = false;

            ClearView();

            if (!_suspended)
                ResumePlayback();
        }
    }
}