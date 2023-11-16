using Microsoft.AspNetCore.SignalR;
using System.Text.RegularExpressions;

namespace ChatApplication4.Models
{
    public class VideoChatHub :Hub
    {
        public async Task JoinRoom(string roomName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, roomName);
        }

        public async Task LeaveRoom(string roomName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomName);
        }

        public async Task SendSignal(string user, string signal)
        {
            await Clients.User(user).SendAsync("ReceiveSignal", Context.ConnectionId, signal);
        }
    }
}
