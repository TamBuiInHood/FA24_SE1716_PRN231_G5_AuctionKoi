using KoiAuction.API.Payloads.Requests.PaymentRequest;
using KoiAuction.API.Payloads.Requests.UserAuctionRequest;
using KoiAuction.BussinessModels.PaymentModels;
using KoiAuction.BussinessModels.UserAuctionModels;
using KoiAuction.Common.Constants;
using KoiAuction.Common.Utils;
using KoiAuction.Common.Utils.Filters;
using KoiAuction.Service.Base;
using KoiAuction.Service.ISerivice;
using KoiAuction.Service.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PRN231.AuctionKoi.API.Payloads;
using System.Net.WebSockets;
using System.Text;

namespace KoiAuction.API.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class UserAuctionController : ControllerBase
    {
        private readonly IUserAuctionService _userAuctionService;
        private readonly IWebSocketService _webSocketService;

        public UserAuctionController(IUserAuctionService userAuctionService, IWebSocketService webSocketService)
        {
            _userAuctionService = userAuctionService;
            _webSocketService = webSocketService;
        }

        //[Authorize(Roles = )]
        [HttpGet(APIRoutes.UserAuction.Get, Name = "GetUserAuctionsAsync")]
        public async Task<IActionResult> GetAllAsync(PaginationParameter paginationParameter, UserAuctionFilters userAuctionFilters)
        {
            try
            {
                var result = await _userAuctionService.Get(paginationParameter, userAuctionFilters);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //[Authorize(Roles = )]
        [HttpGet(APIRoutes.UserAuction.GetByID, Name = "GetUserAuctionByIdAsync")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] string bidId)
        {
            try
            {
                var result = await _userAuctionService.GetByID(bidId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //[Authorize(Roles = )]
        [HttpGet(APIRoutes.UserAuction.GetByAuctionIdAndFishId, Name = "GetByAuctionIdAndFishIdAsync")]
        public async Task<IActionResult> GetByAuctionIdAndFishIdAsync([FromRoute] string auctionId, [FromRoute] string fishId)
        {
            try
            {
                var result = await _userAuctionService.GetByAuctionIdAndFishId(auctionId, fishId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //[Authorize(Roles = )]
        [HttpGet(APIRoutes.UserAuction.GetUsers, Name = "GetUsers")]
        public async Task<IActionResult> GetListUser()
        {
            try
            {
                var result = await _userAuctionService.GetListUser();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //[Authorize(Roles = )]
        [HttpGet(APIRoutes.UserAuction.GetDetailProposals, Name = "GetDetailProposals")]
        public async Task<IActionResult> GetListDetailProposal()
        {
            try
            {
                var result = await _userAuctionService.GetListDetailProposal();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //[Authorize(Roles = )]
        [HttpGet(APIRoutes.UserAuction.GetAuctions, Name = "GetAuctions")]
        public async Task<IActionResult> GetLisAuction()
        {
            try
            {
                var result = await _userAuctionService.GetListAuction();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //[Authorize(Roles = )]
        [HttpPost(APIRoutes.UserAuction.Create, Name = "CreateUserAuctionAsync")]
        public async Task<IActionResult> CreateAsync([FromBody] UserAuctionRequest reqObj)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var insertEntity = new CreateUserAuctionModel
                {
                    BidCode = CodeHelper.GenerateCode(),
                    CreateDate = DateTime.Now,
                    IsWinner = reqObj.IsWinner,
                    Price = reqObj.Price,
                    UserId = reqObj.UserId,
                    FishId = reqObj.FishId,
                    AuctionId = reqObj.AuctionId
                };

                var result = await _userAuctionService.Insert(insertEntity);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //[Authorize(Roles = )]
        [HttpPut(APIRoutes.UserAuction.Update, Name = "UpdateUserAuctionAsync")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int bidId,
            [FromBody] UserAuctionRequest reqObj)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var updateEntity = new UpdateUserAuctionModel
                {
                    IsWinner = reqObj.IsWinner,
                    Price = reqObj.Price,
                    UserId = reqObj.UserId,
                    FishId = reqObj.FishId,
                    AuctionId = reqObj.AuctionId
                };

                var result = await _userAuctionService.Update(bidId, updateEntity);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //[Authorize(Roles = )]
        [HttpDelete(APIRoutes.UserAuction.Delete, Name = "DeleteUserAuctionAsync")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int bidId)
        {
            try
            {
                var result = await _userAuctionService.Delete(bidId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet(APIRoutes.WebSocket.GetWsByAuctionIdAndFishId, Name = "GetWsByAuctionIdAndFishId")]
        public async Task GetWsByAuctionIdAndFishId(string auctionId, string fishId)
        {
            if (HttpContext.WebSockets.IsWebSocketRequest)
            {
                WebSocket webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();

                _webSocketService.AddClient(auctionId, fishId, webSocket);

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
