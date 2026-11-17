using Modules.Core.Exceptions;
using Modules.EventHandling.API;
using Modules.Interaction.API;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Modules.Interaction.Private
{
    public class PlayerInteractor : MonoBehaviour, IInteractionControl
    {
        [SerializeField] private Camera interactionCamera;
        [SerializeField, Min(0f)] private float maxDistance = 2.5f;
        [SerializeField] private LayerMask interactionLayermask =
            Physics.DefaultRaycastLayers;
        [SerializeField] private InputActionReference interactAction;

        private IInteractable _selectedInteractable;
        private bool _isInteracting;
        private DomainEventBus _eventBus;
        private readonly Vector3 _originInViewport =
            new (0.5f, 0.5f, 0f);


        [Inject]
        void Construct(DomainEventBus eventBus)
        {
            _eventBus = eventBus;
        }
        private void Awake()
        {
            if (interactionCamera == null)
                throw new WrongPrefabConfigurationException(
                    "Player interactor without camera");
        }
        
        private void Update()
        {
            ChangeSelection(FindTarget());

            if (interactAction.action.WasPressedThisFrame())
                StartInteraction();

            if (interactAction.action.WasReleasedThisFrame())
                StopInteraction();
        }

        private IInteractable FindTarget()
        {
            Ray ray = interactionCamera.ViewportPointToRay(_originInViewport);

            if (!Physics.Raycast(
                    ray,
                    out RaycastHit hit,
                    maxDistance,
                    interactionLayermask,
                    QueryTriggerInteraction.Ignore))
            {
                return null;
            }

            return hit.collider.GetComponentInParent<IInteractable>();
        }

        private void ChangeSelection(IInteractable next)
        {
            if (ReferenceEquals(next, _selectedInteractable))
                return;

            ClearSelection();

            _selectedInteractable = next;

            if (_selectedInteractable == null)
                return;

            _selectedInteractable.Select();
            _eventBus.Publish(new InteractableSelected(_selectedInteractable));
        }

        private void ClearSelection()
        {
            StopInteraction();

            IInteractable previous = _selectedInteractable;
            _selectedInteractable = null;

            if (previous == null)
                return;

            previous.Unselect();
            _eventBus.Publish(new InteractableUnselected(previous));
        }

        private void StartInteraction()
        {
            if (_selectedInteractable == null || _isInteracting)
                return;

            _isInteracting = true;
            _selectedInteractable.StartInteraction();
            
            _eventBus.Publish(
                new InteractableSelected(_selectedInteractable));
        }

        private void StopInteraction()
        {
            if (!_isInteracting)
                return;

            _isInteracting = false;
            _selectedInteractable.StopInteration();
        }

        private void OnEnable()
        {
            interactAction.action.Enable();
        }
        private void OnDisable()
        {
            ClearSelection();
            interactAction.action.Disable();
        }

        public void EnableInteraction()
        {
            enabled = true;
        }

        public void DisableInteraction()
        {
            enabled = false;
        }
    }
}