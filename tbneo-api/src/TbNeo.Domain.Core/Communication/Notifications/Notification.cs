using MediatR;
using System;

namespace TbNeo.Domain.Core.Communication.Notifications
{
    public class Notification : INotification
    {
        public DateTime Timestamp { get; private set; }

        public Guid NotificationId { get; private set; }

        public string Mensagem { get; private set; }

        public Notification(string mensagem)
        {
            Mensagem = mensagem;
            Timestamp = DateTime.Now;
            NotificationId = Guid.NewGuid();
        }
    }
}
