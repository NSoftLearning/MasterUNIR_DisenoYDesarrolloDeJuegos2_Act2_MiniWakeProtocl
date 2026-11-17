using System;
using Modules.Core.Exceptions;
using Modules.Interaction.API;
using UnityEngine;

namespace Modules.Interaction.Private
{
    public class SimpleInteractable : MonoBehaviour, IInteractable
    {
        [SerializeField] private MonoBehaviour dataProvider;

        private IInteractableReadData _readData;

        public string InteractionPrompt => _readData.InteractionPrompt;

        public event Action InteractionStarted;
        public event Action InteractionStopped;

        private void Awake()
        {
            if (dataProvider == this ||
                dataProvider is not IInteractableReadData)
            {
                throw new WrongPrefabConfigurationException(
                    "SimpleInteractable requires an external data provider");
            }

            _readData = (IInteractableReadData)dataProvider;
        }

        public void Select()
        {
        }

        public void Unselect()
        {
        }

        public void StartInteraction()
        {
            InteractionStarted?.Invoke();
        }

        public void StopInteration()
        {
            InteractionStopped?.Invoke();
        }
    }
}