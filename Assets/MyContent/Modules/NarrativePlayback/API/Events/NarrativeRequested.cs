using Modules.EventHandling.API;

namespace Modules.NarrativePlayback.API
{
    public class NarrativeRequested : DomainEventBase
    {
        public NarrativeSequence Sequence { get; }

        public NarrativeRequested(NarrativeSequence sequence)
        {
            Sequence = sequence;
        }
    }
}