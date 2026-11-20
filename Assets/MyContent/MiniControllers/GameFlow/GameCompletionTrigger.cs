using Modules.Core.API.Events;
using Modules.EventHandling.API;
using UnityEngine;
using Zenject;

namespace MiniControllers.GameFlow
{
    public class GameCompletionTrigger : MonoBehaviour
    {
        private DomainEventBus _eventBus;
        private bool _completionAnnounced;
        private bool _victoryAnnounced;
        
        [Inject]
        private void Construct(DomainEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public void CompleteGame()
        {
            if (_completionAnnounced)
                return;

            _completionAnnounced = true;
            _eventBus.Publish(new GameCompleted(this));
        }
        
        

        public void WinGame()
        {
            if (_victoryAnnounced)
                return;

            _victoryAnnounced = true;
            _eventBus.Publish(new GameWon(this));
        }
    }
}