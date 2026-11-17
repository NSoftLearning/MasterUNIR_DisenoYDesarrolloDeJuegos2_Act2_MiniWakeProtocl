using Modules.EventHandling.API;

namespace Modules.NarrativePlayback.API
{
    public class NarrativeFinished : DomainEventBase
    {
        public NarrativeSequence Sequence { get; }

        public NarrativeFinished(
            NarrativeSequence sequence,
            object source) : base(source)
        {
            Sequence = sequence;
        }
    }
}