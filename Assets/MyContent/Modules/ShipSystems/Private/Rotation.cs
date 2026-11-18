using System;
using UnityEngine;

namespace Modules.ShipSystems.Private
{
    public partial class TranslateAndRotateInteraction
    {
        [Serializable]
        private class Rotation
        {
            public Transform target;
            public Vector3 localEulerOffset = new(0f, 90f, 0f);

            [Min(0.01f)] public float duration = 0.6f;
        }
    }
}