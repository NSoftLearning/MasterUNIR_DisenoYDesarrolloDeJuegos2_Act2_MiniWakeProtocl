using Modules.EventHandling.API;

namespace Modules.PlayerControl.API
{
    public class PlayerControlRestored : DomainEventBase
    {
        public PlayerControlRestored(object source) : base(source)
        {
        }
    }
}