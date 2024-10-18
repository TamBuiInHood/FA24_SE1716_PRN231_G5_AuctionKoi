using KoiAuction.BussinessModels.CheckingProposal;
using KoiAuction.Service.Base;
using KoiAuction.Service.ISerivice;
using KoiAuction.Service.Services;
using Microsoft.AspNetCore.Mvc;
using PRN231.AuctionKoi.API.Payloads;


namespace KoiAuction.API.Controllers;

//[Route("api/[controller]")]
[ApiController]
public class CheckingProposalsController : ControllerBase
{
    public ICheckingProposalService _checkingPropoService;

    public CheckingProposalsController(ICheckingProposalService checkingPropoService)
    {
        _checkingPropoService = checkingPropoService;
    }

    // GET: api/CheckingProposals
    [HttpGet(APIRoutes.CheckingProposal.Get, Name = "Get All CheckingProposal")]
    public async Task<IActionResult> GetCheckingProposals([FromQuery] string? searchKey, [FromQuery] string? orderBy, [FromQuery] int? pageIndex, [FromQuery] int? pageSize)
    {
        try
        {
            var result = await _checkingPropoService.Get(searchKey, orderBy, pageIndex, pageSize);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound("Order not found.");

        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // GET: api/CheckingProposals/5
    [HttpGet(APIRoutes.CheckingProposal.GetByID, Name = "Get CheckingProposal by id")]
    public async Task<IBusinessResult> GetCheckingProposal([FromRoute(Name = "id")] int id)
    {
        var proposal = await _checkingPropoService.GetByID(id);

        //if (proposal == null)
        //{
        //    return NotFound();
        //}

        return proposal;
    }

    // PUT: api/CheckingProposals/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut(APIRoutes.CheckingProposal.Update, Name = "Update Checking Proposal")]
    public async Task<IBusinessResult> PutCheckingProposal([FromRoute(Name = "id")] int id, UpdateCheckingProposalModel updateCheckingProposalModel)
    {
        return await _checkingPropoService.Update(id, updateCheckingProposalModel);
    }

    // POST: api/CheckingProposals
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost(APIRoutes.CheckingProposal.Create, Name = "Create Checking Proposal")]
    public async Task<IBusinessResult> PostCheckingProposal(CreateCheckingProposalModel checkingProposal)
    {
        return await _checkingPropoService.Insert(checkingProposal);

    }

    // DELETE: api/CheckingProposals/5
    [HttpDelete(APIRoutes.CheckingProposal.Delete, Name = "Delete Checking Proposal")]
    public async Task<IBusinessResult> DeleteCheckingProposal([FromRoute(Name = "id")] int id)
    {
        return await _checkingPropoService.Delete(id);
    }

    /*private bool CheckingProposalExists(int id)
    {
        return _context.CheckingProposals.Any(e => e.CheckingProposalId == id);
    }*/

    // GET: api/Proposals
    [HttpGet(APIRoutes.CheckingProposal.GetFish, Name = "Get All Fish")]
    public async Task<IBusinessResult> GetAllUserProposals()
    {
        return await _checkingPropoService.GetFish();
    }
}
