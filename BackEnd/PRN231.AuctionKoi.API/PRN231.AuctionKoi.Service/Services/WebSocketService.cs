using KoiAuction.Service.ISerivice;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace KoiAuction.Service.Services
{
    public class WebSocketService : IWebSocketService
    {
        private static readonly object _lock = new object(); // Đảm bảo xử lý đồng bộ
        private static readonly Dictionary<string, Dictionary<string, List<WebSocket>>> clients = new();
        public void AddClient(string auctionId, string fishId, WebSocket webSocket)
        {
            lock (_lock)
            {
                if (!clients.ContainsKey(auctionId))
                {
                    clients[auctionId] = new Dictionary<string, List<WebSocket>>();
                }

                if (!clients[auctionId].ContainsKey(fishId))
                {
                    clients[auctionId][fishId] = new List<WebSocket>();
                }

                clients[auctionId][fishId].Add(webSocket);
                Console.WriteLine($"Client added to auction {auctionId}, fish {fishId}");
            }
        }

        public void RemoveClient(string auctionId, string fishId, WebSocket webSocket)
        {
            lock (_lock)
            {
                if (clients.ContainsKey(auctionId) && clients[auctionId].ContainsKey(fishId))
                {
                    clients[auctionId][fishId].Remove(webSocket);
                    Console.WriteLine($"Client removed from auction {auctionId}, fish {fishId}");

                    if (clients[auctionId][fishId].Count == 0)
                    {
                        clients[auctionId].Remove(fishId);
                    }

                    if (clients[auctionId].Count == 0)
                    {
                        clients.Remove(auctionId);
                    }
                }
            }
        }

        public async Task BroadcastToClients(string auctionId, string fishId, object data)
        {
            lock (_lock)
            {
                if (clients.ContainsKey(auctionId) && clients[auctionId].ContainsKey(fishId))
                {
                    var settings = new JsonSerializerSettings
                    {
                        ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver()
                    };
                    var message = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(data, settings));

                    foreach (var client in clients[auctionId][fishId])
                    {
                        if (client.State == WebSocketState.Open)
                        {
                            _ = client.SendAsync(new ArraySegment<byte>(message), WebSocketMessageType.Text, true, CancellationToken.None);
                        }
                    }
                }
            }
        }
    }
}