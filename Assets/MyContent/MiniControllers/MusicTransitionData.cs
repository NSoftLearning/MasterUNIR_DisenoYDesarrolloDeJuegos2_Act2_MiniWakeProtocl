using UnityEngine;

namespace MiniControllers
{
    [CreateAssetMenu(
        fileName = "MusicCue",
        menuName = "Tools/Config/Music Transition")]
    public class MusicTransitionData : ScriptableObject
    {
        [SerializeField] private AudioClip clip;
        [SerializeField] private MusicTransitionMode transitionMode;
        [SerializeField, Min(0f)] private float startTime;
        [SerializeField, Min(0.1f)] private float blendDuration = 3f;

    
        public AudioClip Clip => clip;
        public MusicTransitionMode TransitionMode => transitionMode;
        public float StartTime => startTime;
        public float BlendDuration => blendDuration;
    }
}