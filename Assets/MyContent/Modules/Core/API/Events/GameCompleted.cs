using Modules.EventHandling.API;

namespace Modules.Core.API.Events
{
    public class GameCompleted : DomainEventBase
    {
        public GameCompleted(object source = null) : base(source)
        {
        }
    }
}