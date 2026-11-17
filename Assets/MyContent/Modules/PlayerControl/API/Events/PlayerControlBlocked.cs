using Modules.EventHandling.API;

namespace Modules.PlayerControl.API
{
    public class PlayerControlBlocked : DomainEventBase
    {
        public PlayerControlBlocked(object source) : base(source)
        {
        }
    }
}