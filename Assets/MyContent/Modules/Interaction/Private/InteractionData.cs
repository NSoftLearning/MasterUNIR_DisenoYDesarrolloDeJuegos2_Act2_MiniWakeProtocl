using Modules.Interaction.API;
using UnityEngine;

namespace Modules.Interaction.Private
{
    public class InteractionData :
        MonoBehaviour,
        IInteractableReadData
    {
        [SerializeField] private string prompt;

        public string InteractionPrompt => prompt;
    }
}