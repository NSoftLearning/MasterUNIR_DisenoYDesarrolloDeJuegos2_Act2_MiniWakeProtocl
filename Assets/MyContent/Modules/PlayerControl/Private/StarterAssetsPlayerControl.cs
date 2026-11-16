using Modules.PlayerControl.API;
using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Modules.PlayerControl.Private
{
    public class StarterAssetsPlayerControl : MonoBehaviour, IPlayerControl
    {

        [SerializeField] private FirstPersonController controller;
        [SerializeField] private StarterAssetsInputs inputs;
        [SerializeField] private PlayerInput playerInput;
        
        
        
        public void EnableControl()
        {
            ClearInput();
            playerInput.ActivateInput();
            controller.enabled = true;
        }

        public void DisableControl()
        {
            playerInput.DeactivateInput();
            ClearInput();
            controller.enabled = false;
        }

        private void ClearInput()
        {
            inputs.MoveInput(Vector2.zero);
            inputs.LookInput(Vector2.zero);
            inputs.JumpInput(false);
            inputs.SprintInput(false);
        }
    }
}