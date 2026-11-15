using Modules.EventHandling.API;

namespace Modules.Interaction.API
{
    public class InteractableUnselected : DomainEventBase
    {
        public IInteractableReadData interactable { get; }

        public InteractableUnselected (IInteractableReadData interactable)
        {
            this.interactable = interactable;
        }
    }
}