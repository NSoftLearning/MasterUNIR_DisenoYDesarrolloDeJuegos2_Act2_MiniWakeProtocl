using Modules.PlayerControl.API;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;
using Zenject;

namespace MiniControllers
{
    public class MenuSequenceController : MonoBehaviour
    {
        [SerializeField] private PlayableDirector menuDirector;
        [SerializeField] private SequenceController introSequence;
        [SerializeField] private InputActionReference startGameAction;

        private IPlayerControlBlocker _controlBlocker;
        private bool _waitingForInput;

        [Inject]
        private void Construct(IPlayerControlBlocker controlBlocker)
        {
            _controlBlocker = controlBlocker;
        }

        private void OnEnable()
        {
            startGameAction.action.performed += StartGame;
        }

        private void Start()
        {
            _controlBlocker.BlockControl(this);

            menuDirector.Play();

            _waitingForInput = true;
            startGameAction.action.Enable();
        }

        private void StartGame(InputAction.CallbackContext context)
        {
            if (!_waitingForInput)
                return;

            _waitingForInput = false;
            startGameAction.action.Disable();

            introSequence.PlaySequence();
            _controlBlocker.ReleaseControl(this);
        }

        private void OnDisable()
        {
            startGameAction.action.performed -= StartGame;
            startGameAction.action.Disable();
        }
    }
}