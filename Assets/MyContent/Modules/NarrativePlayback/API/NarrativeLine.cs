using System;
using UnityEngine;

namespace Modules.NarrativePlayback.API
{
    [Serializable]
    public class NarrativeLine
    {
        [SerializeField] private string speaker;
        [SerializeField, TextArea(2, 6)] private string text;
        [SerializeField, Min(0.1f)] private float duration = 4f;
        [SerializeField] private Color nameColor;
        
        public string Speaker => speaker;
        public string Text => text;
        public float Duration => duration;
        public Color NameColor => nameColor;
    }
}