using System;
using UnityEngine;

namespace Modules.ShipSystems.Private
{
        [Serializable]
        public class Translation
        {
            public Transform target;
            public Vector3 direction = Vector3.forward;

            [Min(0f)] public float distance = 0.4f;
            [Min(0.01f)] public float duration = 0.6f;
        }
}