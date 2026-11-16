using Modules.Interaction.API;
using Unity.VisualScripting;
using UnityEngine;

namespace Modules.Interaction.Examples
{
    public class LightSwitchInteractable : MonoBehaviour, IInteractable
    {
        [SerializeField] private Light targetLight;

        public string InteractionPrompt => CalculatePrompt(); 
        
        public void Select()
        {
            Debug.Log("Button selected", this);
        }

        public void Unselect()
        {
            Debug.Log("Button DE-selected", this);
        }

        public void StartInteraction()
        {
            Debug.Log("interaction started", this);
            targetLight.enabled = !targetLight.enabled;
        }

        public void StopInteration()
        {
            Debug.Log("interaction finished", this);
        }

        private string CalculatePrompt()
        {
            if (targetLight.enabled)
                return "Apagar luz";
            return "Encender luz";



        }
        
    }
}