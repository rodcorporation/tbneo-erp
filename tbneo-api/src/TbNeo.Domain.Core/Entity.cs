using System;
using System.Collections.Generic;

namespace TbNeo.Domain.Core
{
    public abstract class Entity
    {
        public int Id { get; protected set; }

        public Guid IdLogReference { get; protected set; }

        protected List<Event> _events;
        public IReadOnlyCollection<Event> Events => _events?.AsReadOnly();

        protected Entity()
        {
            _events = new List<Event>();
            IdLogReference = Guid.NewGuid();
        }

        public Entity(int id)
        {
            Id = id;
            IdLogReference = Guid.NewGuid();
            _events = new List<Event>();
        }

        public void LimparEventos()
        {
            _events.Clear();
        }
    }
}
