using System.Text;
using Modules.EventHandling;
using Modules.EventHandling.API;

namespace PrototypesAndTests.Infra.EventBusTest
{
    public class EventBusGeneralTestEvent : DomainEventBase
    {
        public readonly string SamplePayload;

        public EventBusGeneralTestEvent (string samplePayload)
        {
            SamplePayload = samplePayload;
        }

        public override string ToString()
        {

            var stringBuilder = new StringBuilder("nameof");

            stringBuilder.Append("[");
            stringBuilder.Append("DOMAIN EVENT");
            stringBuilder.Append("]");
            stringBuilder.Append(nameof(EventBusGeneralTestEvent));
            stringBuilder.Append($"SamplePayload: {SamplePayload}");

            return stringBuilder.ToString();

        }
    }
}
