using System.Collections.Generic;
using UnityEngine;

namespace Modules.NarrativePlayback.API
{
    [CreateAssetMenu(
        fileName = "NarrativeSequence",
        menuName = "Tools/Config/Modules/NarrativePlayback/Sequence")]
    public class NarrativeSequence : ScriptableObject
    {
        [SerializeField] private List<NarrativeLine> lines = new();
        [SerializeField] private bool blockPlayerControl;

        public bool BlockPlayerControl => blockPlayerControl;
        public IReadOnlyList<NarrativeLine> Lines => lines;
    }
}