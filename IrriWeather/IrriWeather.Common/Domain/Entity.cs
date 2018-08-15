using System;
using System.Collections.Generic;
using System.Text;

namespace IrriWeather.Common.Domain
{
    public abstract class Entity
    {
        protected Entity()
        {
            Id = Guid.NewGuid();
        }

        private List<IDomainEvent> _domainEvents;
        public Guid Id { get; private set; }

        public IEnumerable<IDomainEvent> DomainEvents { get => _domainEvents; }



        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }

        public void AddDomainEvent(IDomainEvent eventItem)
        {
            _domainEvents = _domainEvents ?? new List<IDomainEvent>();
            _domainEvents.Add(eventItem);
        }

        public void RemoveDomainEvent(IDomainEvent eventItem)
        {
            if (_domainEvents is null)
                return;
            _domainEvents.Remove(eventItem);
        }
    }
}
