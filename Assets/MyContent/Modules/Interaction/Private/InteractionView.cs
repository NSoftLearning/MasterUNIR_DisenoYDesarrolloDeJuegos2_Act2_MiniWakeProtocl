using System;
using Modules.EventHandling.API;
using Modules.Interaction.API;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Zenject;

public class InteractionView : MonoBehaviour,
    IDomainEventHandler<InteractableSelected>,
    IDomainEventHandler<InteractableUnselected>
{

    [SerializeField] private Image _crosshair;
    [SerializeField] private TMP_Text _promptTMP; 
    [SerializeField] private Sprite _normalCrosshair;
    [SerializeField] private Sprite _selectedCrosshair;

    private DomainEventBus _eventBus;
    //private IInteractable _currentTarget;

    [Inject]
    private void Construct(DomainEventBus eventBus)
    {
        _eventBus = eventBus;
    }

    public void Handle(InteractableSelected domainEvent)
    {
        _crosshair.sprite = _selectedCrosshair;
        _promptTMP.text = domainEvent.interactable.InteractionPrompt;
    }

    public void Handle(InteractableUnselected domainEvent)
    {
        _crosshair.sprite = _normalCrosshair;
        _promptTMP.text = string.Empty;
    }
    
    private void OnEnable()
    {
        _eventBus.RegisterHandler<InteractableSelected>(this);
        _eventBus.RegisterHandler<InteractableUnselected>(this);

        _crosshair.sprite = _normalCrosshair;
        _promptTMP.text = string.Empty;

        //    ClearView();
    }
    
    private void OnDisable()
    {
        _eventBus.UnregisterHandler<InteractableSelected>(this);
        _eventBus.UnregisterHandler<InteractableUnselected>(this);
    }
}
