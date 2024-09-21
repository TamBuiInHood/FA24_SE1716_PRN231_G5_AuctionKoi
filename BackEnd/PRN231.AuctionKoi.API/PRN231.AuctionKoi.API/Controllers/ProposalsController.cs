using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KoiAuction.Service.Base;
using KoiAuction.Service.ISerivice;
using KoiAuction.Service.Models.Pagination;
using KoiAuction.Service.Models.Proposal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRN231.AuctionKoi.Common.Utils;
using PRN231.AuctionKoi.Repository.Entities;

namespace KoiAuction.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProposalsController : ControllerBase
    {
        //private readonly AuctionKoiOfficialContext _context;

        private readonly IProposalService _proposalService;

        public ProposalsController(IProposalService proposalService)
        {
            _proposalService = proposalService;
        }

        // GET: api/Proposals
        [HttpGet]
        public async Task<IBusinessResult> GetProposals(PaginationParameter paginationParameter)
        {
            return await _proposalService.Get(paginationParameter);
        }

        // GET: api/Proposals/5
        [HttpGet("{id}")]
        public async Task<IBusinessResult> GetProposal(int id)
        {
            var proposal = await _proposalService.GetByID(id);

            //if (proposal == null)
            //{
            //    return NotFound();
            //}

            return proposal;
        }

        // PUT: api/Proposals/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IBusinessResult> PutProposal(UpdateProposalModel updateProposalModel)
        {
            //if (id != proposal.FarmId)
            //{
            //    return new BusinessResult();
            //}

            //_context.Entry(proposal).State = EntityState.Modified;

            //try
            //{
            //    await _context.SaveChangesAsync();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!ProposalExists(id))
            //    {
            //        return NotFound();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}

            //return NoContent();
            return await _proposalService.Update(updateProposalModel);
        }

        // POST: api/Proposals
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IBusinessResult> PostProposal(CreateProposalModel createProposalModel)
        {
            //_context.Proposals.Add(proposal);
            //await _context.SaveChangesAsync();

            //return CreatedAtAction("GetProposal", new { id = proposal.FarmId }, proposal);
            return  await _proposalService.Insert(createProposalModel);
        }

        // DELETE: api/Proposals/5
        [HttpDelete("{id}")]
        public async Task<IBusinessResult> DeleteProposal(int id)
        {
            //var proposal = await _context.Proposals.FindAsync(id);
            //if (proposal == null)
            //{
            //    return NotFound();
            //}

            //_context.Proposals.Remove(proposal);
            //await _context.SaveChangesAsync();

            //return NoContent();
            return await (_proposalService.Delete(id));
        }

        //private bool ProposalExists(int id)
        //{
        //    return _context.Proposals.Any(e => e.FarmId == id);
        //}
    }
}
