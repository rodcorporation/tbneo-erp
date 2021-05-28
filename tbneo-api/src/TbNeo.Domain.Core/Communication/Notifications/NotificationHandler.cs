using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace TbNeo.Domain.Core.Communication.Notifications
{
    public class NotificationHandler : INotificationHandler<Notification>, IDisposable
    {
        private IList<Notification> _notifications;

        public NotificationHandler()
        {
            _notifications = new List<Notification>();
        }

        public Task Handle(Notification notification, CancellationToken cancellationToken)
        {
            _notifications.Add(notification);
            return Task.CompletedTask;
        }

        public IReadOnlyList<Notification> ObterNotificacoes() => _notifications.ToList();

        public virtual bool TemNotificacao() => ObterNotificacoes().Any();

        public void Dispose()
        {
            _notifications = new List<Notification>();
        }
    }
}
