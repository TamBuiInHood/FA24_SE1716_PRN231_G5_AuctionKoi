using Microsoft.AspNetCore.Mvc;
using PRN231.AuctionKoi.API.Payloads;
using PRN231.AuctionKoi.Common.Utils;
using KoiAuction.Service.ISerivice;
using KoiAuction.Service.Responses;
using KoiAuction.BussinessModels.Proposal;

namespace PRN231.AuctionKoi.API.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class ProposalController : ControllerBase
    {
        public IProposalService _proposalService;

        public ProposalController(IProposalService proposalService)
        {
            _proposalService = proposalService;
        }

        [HttpGet(APIRoutes.Proposal.Get, Name = "Get All Proposal")]
        public async Task<IActionResult> GetAllProposal(PaginationParameter paginationParameter)
        {
            try
            {
                var result = await _proposalService.Get(paginationParameter);
                if(result != null)
                {
                    return Ok(new BaseResponse()
                    {
                        StatusCode = StatusCodes.Status200OK,
                        Message = "Get All Proposal Success",
                        Data = result,
                        IsSuccess = true
                    });
                }
                return NotFound(new BaseResponse()
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = "Can Not Get Any Proposal",
                    IsSuccess = false
                });
            }
            catch (Exception ex)
            {

                return BadRequest(new BaseResponse()
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = ex.Message,
                    IsSuccess = false
                });
            }
        }

        [HttpPost(APIRoutes.Proposal.Create, Name ="Create Proposal")]
        public async Task<IActionResult> CreateProposal([FromBody] CreateProposalModel createProposalModel)
        {
            try
            {
                var result = await _proposalService.Insert(createProposalModel);
                if(result != null)
                {
                    return Ok(new BaseResponse()
                    {
                        StatusCode = StatusCodes.Status200OK,
                        Message = "Create Proposal Success",
                        Data = result,
                        IsSuccess = true
                    });
                }
                return NotFound(new BaseResponse()
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = "Can Not Create Proposal",
                    IsSuccess = false
                });
            }
            catch (Exception ex)
            {

                return BadRequest(new BaseResponse()
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = ex.Message,
                    IsSuccess = false
                });
            }
        }

        

    }
}
