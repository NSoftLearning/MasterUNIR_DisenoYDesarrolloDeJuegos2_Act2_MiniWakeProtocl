using Modules.EventHandling.API;

namespace Modules.Interaction.API
{
    public class InteractableSelected : DomainEventBase
    {
        public IInteractableReadData interactable { get; }

        public InteractableSelected (IInteractableReadData interactable)
        {
            this.interactable = interactable;
        }
    }
}