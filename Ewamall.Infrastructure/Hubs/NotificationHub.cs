using Microsoft.AspNetCore.SignalR;
using Ewamall.Infrastructure.Dbcontext;
using Ewamall.Business.Entities;
using Ewamall.Domain.Entities;

namespace Ewamall.DataAccess.Hubs
{
    public class NotificationHub : Hub
    {
        private readonly EwamallDBContext dbContext;

        public NotificationHub(EwamallDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task SendNotificationToAll(string message)
        {
            await Clients.All.SendAsync("ReceivedNotification", message);
        }

        public async Task SendNotificationToClient(string message, string username)
        {
            var hubConnections = dbContext.HubConnections.Where(con => con.Username == username).ToList();
            foreach (var hubConnection in hubConnections)
            {
                await Clients.Client(hubConnection.ConnectionId).SendAsync("ReceivedPersonalNotification", message, username);
            }
        }
        public async Task SendNotificationToGroup(string message, int group)
        {
            var hubConnections = dbContext.HubConnections.Join(dbContext.Accounts, c => c.RoleId, o => o.RoleId, (c, o) => new { c.Username, c.ConnectionId, o.RoleId }).Where(o => o.RoleId == group).ToList();
            foreach (var hubConnection in hubConnections)
            {
                string username = hubConnection.Username;
                await Clients.Client(hubConnection.ConnectionId).SendAsync("ReceivedPersonalNotification", message, username);
                //Call Send Email function here
            }
        }

        public override Task OnConnectedAsync()
        {
            Clients.Caller.SendAsync("OnConnected");
            return base.OnConnectedAsync();
        }

        public async Task SaveUserConnection(string username, int roleId)
        {
            var connectionId = Context.ConnectionId;
            HubConnection hubConnection = new HubConnection
            {
                ConnectionId = connectionId,
                Username = username,
                RoleId = roleId
            };

            dbContext.HubConnections.Add(hubConnection);
            await dbContext.SaveChangesAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            var hubConnection = dbContext.HubConnections.FirstOrDefault(con => con.ConnectionId == Context.ConnectionId);
            if(hubConnection != null)
            {
                dbContext.HubConnections.Remove(hubConnection);
                dbContext.SaveChangesAsync();
            }

            return base.OnDisconnectedAsync(exception);
        }
    }
}
