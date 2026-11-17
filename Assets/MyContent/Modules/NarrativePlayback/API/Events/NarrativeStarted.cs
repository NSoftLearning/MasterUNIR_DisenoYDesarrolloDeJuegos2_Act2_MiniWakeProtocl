using Modules.EventHandling.API;

namespace Modules.NarrativePlayback.API
{
    public class NarrativeStarted : DomainEventBase
    {
        public NarrativeSequence Sequence { get; }

        public NarrativeStarted(
            NarrativeSequence sequence,
            object source) : base(source)
        {
            Sequence = sequence;
        }
    }
}