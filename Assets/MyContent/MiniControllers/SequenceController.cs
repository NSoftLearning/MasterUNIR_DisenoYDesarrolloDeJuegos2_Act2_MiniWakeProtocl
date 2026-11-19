using Modules.PlayerControl.API;
using UnityEngine;
using UnityEngine.Playables;
using Zenject;

namespace Modules.ShipSystems.Private
{
    public class SequenceController : MonoBehaviour
    {
        [SerializeField] private PlayableDirector director;
        [SerializeField] private bool restoreControlOnFinish = true;

        private IPlayerControlBlocker _controlBlocker;
        private bool _started;

        [Inject]
        private void Construct(IPlayerControlBlocker controlBlocker)
        {
            _controlBlocker = controlBlocker;
        }

        private void OnEnable()
        {
            director.stopped += FinishSequence;
        }

        private void OnDisable()
        {
            director.stopped -= FinishSequence;
        }

        [ContextMenu("Play Sequence")]
        public void PlaySequence()
        {
            if (_started)
                return;

            _started = true;

            _controlBlocker.BlockControl(this);
            director.Play();
        }

        private void FinishSequence(PlayableDirector finishedDirector)
        {
            if (restoreControlOnFinish)
                _controlBlocker.ReleaseControl(this);
        }
    }
}