﻿using Microsoft.AspNetCore.SignalR;
using Ewamall.Infrastructure.Dbcontext;
using Ewamall.Business.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace Ewamall.DataAccess.Hubs
{
    public class NotificationHub : Hub
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly EwamallDBContext _dbContext;

        public NotificationHub(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            using var scope = _serviceProvider.CreateScope();
            _dbContext = scope.ServiceProvider.GetRequiredService<EwamallDBContext>();
        }

        public async Task SendNotificationToAll(string message)
        {
            await Clients.All.SendAsync("ReceivedNotification", message);
        }

        public async Task SendNotificationToClient(string message, string username)
        {
            var hubConnections = _dbContext.HubConnections.Where(con => con.Username == username).ToList();
            foreach (var hubConnection in hubConnections)
            {
                await Clients.Client(hubConnection.ConnectionId).SendAsync("ReceivedPersonalNotification", message, username);
            }
        }
        public async Task SendNotificationToGroup(string message, int group)
        {
            var hubConnections = _dbContext.HubConnections.Join(_dbContext.Accounts, c => c.RoleId, o => o.RoleId, (c, o) => new { c.Username, c.ConnectionId, o.RoleId }).Where(o => o.RoleId == group).ToList();
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

        public async Task SaveUserConnection(string username)
        {
            var connectionId = Context.ConnectionId;
            HubConnection hubConnection = new HubConnection
            {
                ConnectionId = connectionId,
                Username = username
            };

            _dbContext.HubConnections.Add(hubConnection);
            await _dbContext.SaveChangesAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            var hubConnection = _dbContext.HubConnections.FirstOrDefault(con => con.ConnectionId == Context.ConnectionId);
            if(hubConnection != null)
            {
                _dbContext.HubConnections.Remove(hubConnection);
                _dbContext.SaveChangesAsync();
            }

            return base.OnDisconnectedAsync(exception);
        }
    }
}
