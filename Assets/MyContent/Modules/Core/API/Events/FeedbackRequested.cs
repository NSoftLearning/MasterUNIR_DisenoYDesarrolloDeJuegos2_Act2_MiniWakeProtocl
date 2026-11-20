using Modules.EventHandling.API;

namespace Modules.Core.API.Events
{
    public class FeedbackRequested : DomainEventBase
    {
        public string Message { get; }
        public float Duration { get; }

        public FeedbackRequested(string message, float duration = 3f)
        {
            Message = message;
            Duration = duration;
        }
    }
}