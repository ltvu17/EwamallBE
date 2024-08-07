﻿using Ewamall.DataAccess.SubscribeTableDependencies;
using Ewamall.DataAccess.Hubs;
using Ewamall.Business.Entities;
using TableDependency.SqlClient;

namespace Ewamall.DataAccess.SubscribeTableDependencies
{
    public class SubscribeNotificationTableDependency : ISubscribeTableDependency
    {
        SqlTableDependency<Notification> tableDependency;
        NotificationHub notificationHub;

        public SubscribeNotificationTableDependency(NotificationHub notificationHub)
        {
            this.notificationHub = notificationHub;
        }

        public void SubscribeTableDependency(string connectionString)
        {
            tableDependency = new SqlTableDependency<Notification>(connectionString);
            tableDependency.OnChanged += TableDependency_OnChanged;
            tableDependency.OnError += TableDependency_OnError;
            tableDependency.Start();
        }

        private void TableDependency_OnError(object sender, TableDependency.SqlClient.Base.EventArgs.ErrorEventArgs e)
        {
            Console.WriteLine($"{nameof(Notification)} SqlTableDependency error: {e.Error.Message}");
        }

        private async void TableDependency_OnChanged(object sender, TableDependency.SqlClient.Base.EventArgs.RecordChangedEventArgs<Notification> e)
        {
            if(e.ChangeType != TableDependency.SqlClient.Base.Enums.ChangeType.None)
            {
                var notification = e.Entity;
                if(notification.NotificationType == "All")
                {
                    await notificationHub.SendNotificationToAll(notification.Title,notification.Message);
                }
                else if(notification.NotificationType == "Personal")
                {
                    await notificationHub.SendNotificationToClient(notification.Title, notification.Message, notification.Username.ToString());
                }
                else if (notification.NotificationType == "Group")
                {
                    await notificationHub.SendNotificationToGroup(notification.Title, notification.Message, notification.RoleId);
                }
            }
        }
    }
}
