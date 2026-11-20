using UnityEngine;

namespace MiniControllers
{
    public class SequenceOnStart : MonoBehaviour
    {
        [SerializeField] private SequenceController sequenceController;

        private void Start()
        {
            sequenceController.PlaySequence();
        }
    }
}