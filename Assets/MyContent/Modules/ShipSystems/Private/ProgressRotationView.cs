using Modules.Interaction.API;
using UnityEngine;

namespace Modules.ShipSystems.Private
{
    public class ProgressRotationView : MonoBehaviour
    {
        [SerializeField] private MonoBehaviour progressProvider;
        [SerializeField] private Transform rotationTarget;
        [SerializeField] private Vector3 localAxis = Vector3.forward;
        [SerializeField] private float totalAngle = 360f;

        private IHoldToCompleteInteraction _progressProvider;
        private Quaternion _initialRotation;

        private void Awake()
        {
            _progressProvider =
                (IHoldToCompleteInteraction)progressProvider;

            _initialRotation = rotationTarget.localRotation;
        }

        private void OnEnable()
        {
            _progressProvider.ProgressChanged += Rotate;
        }

        private void OnDisable()
        {
            _progressProvider.ProgressChanged -= Rotate;
        }

        private void Rotate(float progress)
        {
            rotationTarget.localRotation =
                _initialRotation *
                Quaternion.AngleAxis(
                    totalAngle * progress,
                    localAxis.normalized);
        }
    }
}