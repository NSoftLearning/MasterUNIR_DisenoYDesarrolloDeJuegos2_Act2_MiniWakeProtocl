using System;
using UnityEngine;

namespace Modules.EventHandling.API
{
    public class DomainEventBase : IDomainEvent
    {

        private readonly string _identifier;
        private readonly float _timestamp;

        public string Identifier { get { return _identifier; } }
        public float Timestamp { get { return _timestamp; } }
    

        protected DomainEventBase ()
        {
            _identifier = Guid.NewGuid().ToString();
            _timestamp = Time.time;
        }
    }
}
