using Modules.NarrativePlayback.API;
using UnityEngine;
using Zenject;

namespace MiniControllers
{
    public class TimelineNarrativeController : MonoBehaviour
    {
        [SerializeField] private NarrativeSequence sequence;

        private IManualNarrativePlaybackControl _playback;

        [Inject]
        private void Construct(IManualNarrativePlaybackControl playback)
        {
            _playback = playback;
        }

        public void Begin()
        {
            _playback.BeginManualPlayback();
        }

        public void ShowLine(int index)
        {
            _playback.ShowManualLine(sequence.Lines[index]);
        }

        public void HideLine()
        {
            _playback.HideManualLine();
        }

        public void End()
        {
            _playback.EndManualPlayback();
        }
    }
}