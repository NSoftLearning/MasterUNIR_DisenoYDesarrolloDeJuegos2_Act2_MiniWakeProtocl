using System;
using Modules.Interaction.API;
using Unity.VisualScripting;
using UnityEngine;

namespace Modules.Interaction.Examples
{
    public class LightSwitchInteraction : MonoBehaviour, IInteractableReadData
    {
        [SerializeField] private Light targetLight;

        private IInteractable _interactable;

        public string InteractionPrompt => CalculatePrompt();

        private void Awake()
        {
            _interactable = GetComponent<IInteractable>();
        }

        private void OnEnable()
        {
            _interactable.InteractionStarted += ToggleLight;
        }

        private void OnDisable()
        {
            _interactable.InteractionStarted -= ToggleLight;
        }
        private void ToggleLight()
        {
            targetLight.enabled = !targetLight.enabled;
        }
        private string CalculatePrompt()
        {
            if (targetLight.enabled)
                return "Apagar luz";
            return "Encender luz";
        }
        
    }
}