using MediatR;
using System;

namespace TbNeo.Domain.Core
{
    public abstract class Event : INotification
    {
        public DateTime Timestamp { get; protected set; }

        public Event()
        {
            Timestamp = DateTime.Now;
        }
    }
}
