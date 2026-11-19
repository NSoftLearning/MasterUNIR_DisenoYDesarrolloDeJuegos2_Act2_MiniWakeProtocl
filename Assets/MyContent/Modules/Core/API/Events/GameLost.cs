using Modules.EventHandling.API;

namespace Modules.Core.API.Events
{
    public class GameLost : DomainEventBase
    {

        public GameLost( object source = null)
            : base(source)
        {

        }
    }
}