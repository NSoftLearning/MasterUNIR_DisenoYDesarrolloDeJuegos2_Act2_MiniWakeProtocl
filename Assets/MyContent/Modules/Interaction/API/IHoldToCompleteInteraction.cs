using System;

namespace Modules.Interaction.API
{
    public interface IHoldToCompleteInteraction
    {
        bool IsCompleted { get; }

        event Action Completed;
        event Action <float> ProgressChanged;
        
        void EnableProgress();
        void DisableProgress();
    }
}