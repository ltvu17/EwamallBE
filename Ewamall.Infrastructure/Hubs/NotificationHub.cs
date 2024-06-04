using Microsoft.AspNetCore.SignalR;
using Ewamall.Infrastructure.Dbcontext;
using Ewamall.Business.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace Ewamall.DataAccess.Hubs
{
    public class NotificationHub : Hub
    {
        private readonly IServiceProvider _serviceProvider;
        private EwamallDBContext _dbContext;

        public NotificationHub(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task SendNotificationToAll(string title, string message)
        {
            await Clients.All.SendAsync("ReceivedNotification",title, message);
        }

        public async Task SendNotificationToClient(string title, string message, string username)
        {
            using var scope = _serviceProvider.CreateScope();
            _dbContext = scope.ServiceProvider.GetRequiredService<EwamallDBContext>();
            var hubConnections = _dbContext.HubConnections.Where(con => con.Username == username).ToList();
            foreach (var hubConnection in hubConnections)
            {
                await Clients.Client(hubConnection.ConnectionId).SendAsync("ReceivedPersonalNotification", title, message, username);
            }
        }
        public async Task SendNotificationToGroup(string title, string message, int group)
        {
            using var scope = _serviceProvider.CreateScope();
            _dbContext = scope.ServiceProvider.GetRequiredService<EwamallDBContext>();
            var hubConnections = _dbContext.HubConnections.Join(_dbContext.Accounts, c => c.RoleId, o => o.RoleId, (c, o) => new { c.Username, c.ConnectionId, o.RoleId }).Where(o => o.RoleId == group).ToList();
            foreach (var hubConnection in hubConnections)
            {
                string username = hubConnection.Username;
                await Clients.Client(hubConnection.ConnectionId).SendAsync("ReceivedPersonalNotification", title, message, username);
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
            using var scope = _serviceProvider.CreateScope();
            _dbContext = scope.ServiceProvider.GetRequiredService<EwamallDBContext>();
            var connectionId = Context.ConnectionId;
            HubConnection hubConnection = new HubConnection
            {
                ConnectionId = connectionId,
                Username = username,
                RoleId = roleId
            };

            _dbContext.HubConnections.Add(hubConnection);
            await _dbContext.SaveChangesAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            using var scope = _serviceProvider.CreateScope();
            _dbContext = scope.ServiceProvider.GetRequiredService<EwamallDBContext>();
            var hubConnection = _dbContext.HubConnections.FirstOrDefault(con => con.ConnectionId == Context.ConnectionId);
            if(!hubConnection.Equals(null))
            {
                _dbContext.HubConnections.Remove(hubConnection);
                _dbContext.SaveChangesAsync();
            }
            return base.OnDisconnectedAsync(exception);
        }
    }
}
