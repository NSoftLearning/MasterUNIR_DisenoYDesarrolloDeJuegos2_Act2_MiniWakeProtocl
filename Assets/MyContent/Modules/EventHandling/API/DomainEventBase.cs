using System;
using UnityEngine;

namespace Modules.EventHandling.API
{
    public class DomainEventBase : IDomainEvent
    {

        private readonly string _identifier;
        private readonly float _timestamp;
        public object Source { get; }
        public string Identifier { get { return _identifier; } }
        public float Timestamp { get { return _timestamp; } }
    

        protected DomainEventBase(object source = null)
        {
            _identifier = Guid.NewGuid().ToString();
            _timestamp = Time.time;
            Source = source;
        }
    }
}
