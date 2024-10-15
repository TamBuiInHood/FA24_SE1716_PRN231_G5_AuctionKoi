using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace KoiAuction.Service.ISerivice
{
    public interface IWebSocketService
    {
        void AddClient(string auctionId, string fishId, WebSocket webSocket);
        void RemoveClient(string auctionId, string fishId, WebSocket webSocket);
        Task BroadcastToClients(string auctionId, string fishId, object data);
    }
}
