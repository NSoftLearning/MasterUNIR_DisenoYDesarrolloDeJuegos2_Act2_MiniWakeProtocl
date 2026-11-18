using System.Collections.Generic;
using DG.Tweening;
using Modules.Interaction.API;
using UnityEngine;
using UnityEngine.Events;

namespace Modules.ShipSystems.Private
{
    public partial class TranslateAndRotateInteraction :
        MonoBehaviour,
        IInteractableReadData
    {
        [SerializeField] private string activatePrompt = "Abrir";
        [SerializeField] private string deactivatePrompt = "Cerrar";

        [SerializeField] private List<Translation> translations = new();
        [SerializeField] private List<Rotation> rotations = new();

        [SerializeField] private UnityEvent activated;
        [SerializeField] private UnityEvent deactivated;
        private readonly List<Tween> _tweens = new();

        private IInteractable _interactable;
        private bool _activated;

        public string InteractionPrompt => CalculatePrompt();
            

        private void Awake()
        {
            _interactable = GetComponent<IInteractable>();

            foreach (Translation translation in translations)
            {
                Vector3 destination =
                    translation.target.localPosition +
                    translation.direction.normalized * translation.distance;

                Tween tween = translation.target
                    .DOLocalMove(destination, translation.duration)
                    .SetEase(Ease.InOutSine)
                    .SetAutoKill(false)
                    .Pause();

                _tweens.Add(tween);
            }

            foreach (Rotation rotation in rotations)
            {
                Tween tween = rotation.target
                    .DOLocalRotate(
                        rotation.localEulerOffset,
                        rotation.duration,
                        RotateMode.LocalAxisAdd)
                    .SetEase(Ease.InOutSine)
                    .SetAutoKill(false)
                    .Pause();

                _tweens.Add(tween);
            }
        }

        private void OnEnable()
        {
            _interactable.InteractionStarted += Toggle;
        }

        private void Toggle()
        {
            if (_activated)
                Deactivate();
            else
                Activate();
        }

        public void Activate()
        {
            _activated = true;

            foreach (Tween tween in _tweens)
                tween.PlayForward();
            
            activated.Invoke();
        }

        public void Deactivate()
        {
            _activated = false;

            foreach (Tween tween in _tweens)
                tween.PlayBackwards();
            deactivated.Invoke();
        }


        private string CalculatePrompt()
        {
            return _activated ? deactivatePrompt : activatePrompt;
        }
        private void OnDisable()
        {
            _interactable.InteractionStarted -= Toggle;

            foreach (Tween tween in _tweens)
                tween.Pause();
        }

        private void OnDestroy()
        {
            foreach (Tween tween in _tweens)
                tween.Kill();
        }
    }
}