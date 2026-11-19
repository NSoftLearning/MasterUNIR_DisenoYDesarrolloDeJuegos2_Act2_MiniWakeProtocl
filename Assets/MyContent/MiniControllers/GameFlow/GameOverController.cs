using Modules.Core.API.Events;
using Modules.EventHandling.API;
using Modules.PlayerControl.API;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace MiniControllers.GameFlow
{
    public class GameOverController : MonoBehaviour,
        IDomainEventHandler<GameLost>,
        IDomainEventHandler<GameWon>
    {
        [SerializeField] private GameObject gameOverPanel;
        [SerializeField] private GameObject victoryPanel;
        
        private DomainEventBus _eventBus;
        private IPlayerControlBlocker _controlBlocker;

        [Inject]
        private void Construct(
            DomainEventBus eventBus,
            IPlayerControlBlocker controlBlocker)
        {
            _eventBus = eventBus;
            _controlBlocker = controlBlocker;
        }

        private void Awake()
        {
            gameOverPanel.SetActive(false);
            victoryPanel.SetActive(false);
        }

        private void OnEnable()
        {
            _eventBus.RegisterHandler<GameLost>(this);
            _eventBus.RegisterHandler<GameWon>(this);
        }

        public void Handle(GameLost domainEvent)
        {
            ShowPanel(gameOverPanel);
        }

        public void Handle(GameWon domainEvent)
        {
            ShowPanel(victoryPanel);
        }

        private void ShowPanel(GameObject panel)
        {
            _controlBlocker.BlockControl(this);

            gameOverPanel.SetActive(false);
            victoryPanel.SetActive(false);
            panel.SetActive(true);

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        public void RestartGame()
        {
            SceneManager.LoadScene(
                SceneManager.GetActiveScene().name);
        }

        private void OnDisable()
        {
            _eventBus.UnregisterHandler<GameLost>(this);
            _eventBus.UnregisterHandler<GameWon>(this);
        }

 
    }
}