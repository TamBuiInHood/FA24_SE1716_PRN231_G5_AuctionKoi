using KoiAuction.BussinessModels.Filters;
using KoiAuction.Common.Constants;
using KoiAuction.Service.Base;
using KoiAuction.Service.ISerivice;
using KoiAuction.Service.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PRN231.AuctionKoi.API.Payloads;
using PRN231.AuctionKoi.Common.Utils;

namespace KoiAuction.API.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class UserAuctionController : ControllerBase
    {
        private readonly IUserAuctionService _userAuctionService;

        public UserAuctionController(IUserAuctionService userAuctionService)
        {
            _userAuctionService = userAuctionService;
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
        public async Task<IActionResult> GetByIdAsync([FromRoute(Name = "bid-id")] string bidId)
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

    }
}
