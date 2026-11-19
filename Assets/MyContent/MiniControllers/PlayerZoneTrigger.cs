using Modules.PlayerControl.API;
using UnityEngine;
using UnityEngine.Events;

namespace MiniControllers
{
    public class PlayerZoneTrigger : MonoBehaviour
    {
        [SerializeField] private bool triggerOnce = true;
        [SerializeField] private UnityEvent playerEntered;

        private bool _triggered;

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponentInParent<PlayerEntity>() == null)
                return;

            if (triggerOnce && _triggered)
                return;

            _triggered = true;
            playerEntered.Invoke();
        }
    }
}