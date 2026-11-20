using DG.Tweening;
using UnityEngine;

namespace MiniControllers
{
    public class AmbientMusicController : MonoBehaviour
    {
        [SerializeField] private AudioSource sourceA;
        [SerializeField] private AudioSource sourceB;
        [SerializeField, Min(0.1f)] private float blendDuration = 3f;
        [SerializeField, Range(0f, 1f)] private float musicVolume = 0.6f;
        [SerializeField] private MusicTransitionData initialMusic;
        
        private AudioSource _activeSource;
        private AudioSource _availableSource;
        private Sequence _transition;

        private void Awake()
        {
            _activeSource = sourceA;
            _availableSource = sourceB;

            sourceA.volume = 0f;
            sourceB.volume = 0f;
        }
        private void Start()
        {
            PlayTransition(initialMusic);
        }
        public void PlayTransition(MusicTransitionData transitionData)
        {
            PlayTrack(
                transitionData.Clip,
                transitionData.TransitionMode,
                transitionData.StartTime,
                transitionData.BlendDuration);
        }
        
        public void PlayTrack(
            AudioClip clip,
            MusicTransitionMode transitionMode,
            float startTime,
            float blendDuration)
        {
            _transition?.Kill();

            
            if (_availableSource.volume > _activeSource.volume)
                SwapSources();

            AudioSource outgoing = _activeSource;
            AudioSource incoming = _availableSource;

            incoming.Stop();
            incoming.clip = clip;
            incoming.volume = 0f;
            incoming.time = startTime;
            incoming.Play();

            SwapSources();

            if (transitionMode == MusicTransitionMode.Cut)
            {
                outgoing.Stop();
                outgoing.volume = 0f;
                incoming.volume = musicVolume;
                return;
            }

            _transition = DOTween.Sequence()
                .Join(outgoing.DOFade(0f, blendDuration))
                .Join(incoming.DOFade(musicVolume, blendDuration))
                .OnComplete(outgoing.Stop);
        }

        private void SwapSources()
        {
            AudioSource previous = _activeSource;
            _activeSource = _availableSource;
            _availableSource = previous;
        }

        private void OnDestroy()
        {
            _transition?.Kill();
        }
    }
    
}