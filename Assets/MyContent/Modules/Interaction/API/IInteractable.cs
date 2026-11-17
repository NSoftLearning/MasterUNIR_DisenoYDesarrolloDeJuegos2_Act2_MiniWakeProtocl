using System;

namespace Modules.Interaction.API
{
    public interface IInteractable : IInteractableReadData
    {
        event Action InteractionStarted;
        event Action InteractionStopped;
        
        void Select();
        void Unselect();
        void StartInteraction();
        void StopInteration();
    }
}
