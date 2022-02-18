using Microsoft.AspNetCore.SignalR;
using SignalRGuyTutorialServer.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRGuyTutorialServer.Hubs
{
    public class TutorialHub : Hub
    {
        public Task Ping()
        {
            return Clients.Caller.SendAsync("Pong");
        }

        public Task SendMessage(string message)
        {
            return Clients.Caller.SendAsync("ReceiveMessage", message);
        }

        public async Task JoinToGroup(string group)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, group);
        }

        public async Task SendMessageToGroup(string group, string user, string message)
        {
            await Clients.Group(group).SendAsync("ReceiveMessageFromGroup", group, user, message);
        }

        public async Task LeaveFromGroup(string group)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, group);
        }

        public Task GetWeek(Date date)
        {
            var datetime = new DateTime(date.Year, date.Month, date.Day);
            var dayOfWeek = datetime.ToString("dddd", new CultureInfo(date.Locale));
            return Clients.Caller.SendAsync("GetWeek", dayOfWeek);
        }

        public Task CalcTotal(int[] numbers)
        {
            var total = numbers.Sum();
            return Clients.Caller.SendAsync("CalcTotal", total);
        }
    }
}
