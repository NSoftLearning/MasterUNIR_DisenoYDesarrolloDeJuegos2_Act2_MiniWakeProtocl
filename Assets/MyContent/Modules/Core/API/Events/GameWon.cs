using Modules.EventHandling.API;

namespace Modules.Core.API.Events
{
    public class GameWon : DomainEventBase
    {
        public GameWon(object source = null) : base(source)
        {
        }
    }
}