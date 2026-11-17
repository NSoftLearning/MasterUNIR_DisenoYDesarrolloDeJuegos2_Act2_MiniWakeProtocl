namespace Modules.NarrativePlayback.API
{
    public interface INarrativePlaybackControl
    {
        void SuspendPlayback();
        void ResumePlayback();
    }
}