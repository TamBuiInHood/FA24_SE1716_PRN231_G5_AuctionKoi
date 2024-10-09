using KoiAuction.Service.ISerivice;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PRN231.AuctionKoi.API.Payloads;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace KoiAuction.API.Controllers
{
    [ApiController]
    public class WebSocketController : ControllerBase
    {
        private readonly IUserAuctionService _userAuctionService;
        private readonly IWebSocketService _webSocketService;

        public WebSocketController(IUserAuctionService userAuctionService, IWebSocketService webSocketService)
        {
            _userAuctionService = userAuctionService;
            _webSocketService = webSocketService;
        }

        [HttpGet(APIRoutes.WebSocket.GetWsByAuctionIdAndFishId, Name = "GetWsByAuctionIdAndFishId")]
        public async Task Get(string auctionId, string fishId)
        {
            if (HttpContext.WebSockets.IsWebSocketRequest)
            {
                WebSocket webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();

                _webSocketService.AddClient(auctionId, fishId, webSocket);

                // Bắt đầu lắng nghe và xử lý tin nhắn
                await HandleWebSocketMessages(webSocket, auctionId, fishId);
            }
            else
            {
                HttpContext.Response.StatusCode = 400;
            }
        }

        private async Task HandleWebSocketMessages(WebSocket webSocket, string auctionId, string fishId)
        {
            var buffer = new byte[1024 * 4];
            try
            {
                WebSocketReceiveResult result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

                while (!result.CloseStatus.HasValue)
                {
                    var message = Encoding.UTF8.GetString(buffer, 0, result.Count);
                    var receivedMessage = JsonConvert.DeserializeObject<dynamic>(message);

                    if (receivedMessage.action == "join")
                    {
                        Console.WriteLine($"Client joined auction {auctionId}, fish {fishId}");
                    }
                    else if (receivedMessage.action == "update user auctions")
                    {
                        // Lấy danh sách đấu giá dựa trên auctionId và fishId
                        var userAuctions = await _userAuctionService.GetByAuctionIdAndFishId(auctionId, fishId);

                        var messageToSend = new
                        {
                            action = "update",
                            data = userAuctions.Data
                        };

                        await _webSocketService.BroadcastToClients(auctionId, fishId, messageToSend);
                    }

                    result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                // Đảm bảo xóa client khi ngắt kết nối
                _webSocketService.RemoveClient(auctionId, fishId, webSocket);
                await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Connection closed", CancellationToken.None);
            }
        }
    }
}