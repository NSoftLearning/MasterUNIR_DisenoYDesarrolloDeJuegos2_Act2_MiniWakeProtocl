using DG.Tweening;
using UnityEngine;

public class SimpleTweenRotator : MonoBehaviour
{
 [SerializeField, Min(0.1f)]
        private float secondsPerRotation = 12f;

        private Tween _rotation;

        private void OnEnable()
        {
            _rotation = transform
                .DOLocalRotate(
                    new Vector3(0f, 360f, 0f),
                    secondsPerRotation,
                    RotateMode.LocalAxisAdd)
                .SetEase(Ease.Linear)
                .SetLoops(-1, LoopType.Restart)
                .SetUpdate(true);
        }

        private void OnDisable()
        {
            _rotation.Kill();
        }
}
