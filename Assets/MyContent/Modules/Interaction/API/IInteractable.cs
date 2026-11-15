namespace Modules.Interaction.API
{
    public interface IInteractable : IInteractableReadData
    {
        void Select();
        void Unselect();
        void StartInteraction();
        void StopInteration();
    }
}
