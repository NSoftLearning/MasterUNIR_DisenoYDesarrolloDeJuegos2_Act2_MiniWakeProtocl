namespace Modules.NarrativePlayback.API
{
    public interface IManualNarrativePlaybackControl
    {
        void BeginManualPlayback();
        void ShowManualLine(NarrativeLine line);
        void HideManualLine();
        void EndManualPlayback();
    }
}