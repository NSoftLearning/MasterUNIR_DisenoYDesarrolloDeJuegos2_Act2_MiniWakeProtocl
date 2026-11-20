using Modules.Core.API.Events;
using Modules.EventHandling.API;
using TMPro;
using UnityEngine;
using Zenject;

namespace MiniControllers.GameFlow
{
    public class LevelCountdown : MonoBehaviour,
        IDomainEventHandler<GameCompleted>
    {
        [SerializeField, Min(0.1f)] private float duration = 600f;
        [SerializeField] private bool startAutomatically = true;
        [SerializeField] private TMP_Text countdownText;

        private DomainEventBus _eventBus;
        private float _endTime;
        private bool _started;
        private bool _running;
        private bool _finished;
        private int _displayedSeconds = -1;
        private float _nextTextRefresh;
        [Inject]
        private void Construct(DomainEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        private void OnEnable()
        {
            _eventBus.RegisterHandler<GameCompleted>(this);
        }

        private void Start()
        {
            ShowRemainingTime(duration);

            if (startAutomatically)
                StartCountdown();
        }

        public void StartCountdown()
        {
            if (_started || _finished)
                return;

            _started = true;
            _endTime = Time.time + duration;
            _running = true;
            _nextTextRefresh = Time.time;
        }

        private void Update()
        {
            if (!_running)
                return;

            float now = Time.time;

            if (now >= _endTime)
            {
                StopCountdown();
                ShowRemainingTime(0f);

                _eventBus.Publish(
                    new GameLost(this));

                return;
            }

            if (now < _nextTextRefresh)
                return;

            _nextTextRefresh = now + 1f;
            ShowRemainingTime(_endTime - now);
        }

        private void ShowRemainingTime(float seconds)
        {
            int totalSeconds = Mathf.CeilToInt(seconds);

            if (totalSeconds == _displayedSeconds)
                return;

            _displayedSeconds = totalSeconds;

            int minutes = totalSeconds / 60;
            int remainingSeconds = totalSeconds % 60;

            countdownText.SetText(
                "{0:00}:{1:00}",
                minutes,
                remainingSeconds);
        }

        public void StopCountdown()
        {
            _running = false;
            _finished = true;
        }

        public void Handle(GameCompleted domainEvent)
        {
            StopCountdown();
        }

        private void OnDisable()
        {
            _eventBus.UnregisterHandler<GameCompleted>(this);
        }
    }
}