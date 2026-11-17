using System;
using System.Collections.Generic;
using Modules.EventHandling.API;
using Modules.PlayerControl.API;

namespace Modules.PlayerControl.Private
{
    public class PlayerControlBlocker : IPlayerControlBlocker
    {
        private readonly HashSet<object> _requesters = new();
        private readonly IPlayerControl _playerControl;
        private readonly DomainEventBus _eventBus;

        public PlayerControlBlocker(
            IPlayerControl playerControl,
            DomainEventBus eventBus)
        {
            _playerControl = playerControl;
            _eventBus = eventBus;
        }

        public void BlockControl(object requester)
        {
            if (requester == null)
                throw new ArgumentNullException(nameof(requester));

            bool added = _requesters.Add(requester);

            if (added && _requesters.Count == 1)
            {
                _playerControl.DisableControl();
                _eventBus.Publish(new PlayerControlBlocked(this));
            }
        }

        public void ReleaseControl(object requester)
        {
            if (requester == null)
                throw new ArgumentNullException(nameof(requester));

            bool removed = _requesters.Remove(requester);

            if (removed && _requesters.Count == 0)
            {
                _playerControl.EnableControl();
                _eventBus.Publish(new PlayerControlRestored(this));
            }
        }
    }
}