using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using PRN231.AuctionKoi.Repository.Entities;
using PRN231.AuctionKoi.Repository.UnitOfWork;
using PRN231.AuctionKoi.Repository.Utils;
using PRN231.AuctionKoi.Service.ISerivice;
using PRN231.AuctionKoi.Service.Models.Pagination;
using PRN231.AuctionKoi.Service.Models.Proposal;
using PRN231.AuctionKoi.Service.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PRN231.AuctionKoi.Service.Services
{
    public class ProposalService : IProposalService
    {
        public IUnitOfWork _unitOfWork;
        public IMapper _mappper;

        public ProposalService(IUnitOfWork unitOfWork, IMapper mappper)
        {
            _unitOfWork = unitOfWork;
            _mappper = mappper;
        }

        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<PageEntity<ProposalModel>> Get(PaginationParameter paginationParameter)
        {
            Expression<Func<Proposal, bool>> filter = null!;
            Func<IQueryable<Proposal>, IOrderedQueryable<Proposal>> orderBy = null!;
            if (!paginationParameter.Search.IsNullOrEmpty())
            {
                int validInt = 0;
                var checkInt = int.TryParse(paginationParameter.Search, out validInt);
                if (checkInt)
                {
                    filter = x => x.FarmId == validInt;
                }
                else
                {
                    filter = x => x.FarmCode.ToLower().Contains(paginationParameter.Search.ToLower())
                                  || x.FarmName.ToLower().Contains(paginationParameter.Search.ToLower())
                                  || x.Location.ToLower().Contains(paginationParameter.Search.ToLower())
                                  || x.Status.ToLower().Contains(paginationParameter.Search.ToLower())
                                  || x.Description.ToLower().Contains(paginationParameter.Search.ToLower())
                                  || x.Owner.ToLower().Contains(paginationParameter.Search.ToLower())
                                  || x.User.FullName.ToLower().Contains(paginationParameter.Search.ToLower());
                }
            }
                switch(paginationParameter.SortBy)
                {
                    case "farmid":
                        orderBy = !string.IsNullOrEmpty(paginationParameter.Direction)
                                    ? (paginationParameter.Direction.ToLower().Equals("desc")
                                   ? x => x.OrderByDescending(x => x.FarmId)
                                   : x => x.OrderBy(x => x.FarmId)) : x => x.OrderBy(x => x.FarmId);
                        break;
                    case "farmcode":
                        orderBy = !string.IsNullOrEmpty(paginationParameter.Direction)
                                    ? (paginationParameter.Direction.ToLower().Equals("desc")
                                   ? x => x.OrderByDescending(x => x.FarmCode)
                                   : x => x.OrderBy(x => x.FarmCode)) : x => x.OrderBy(x => x.FarmCode);
                        break;
                    case "farmname":
                        orderBy = !string.IsNullOrEmpty(paginationParameter.Direction)
                                    ? (paginationParameter.Direction.ToLower().Equals("desc")
                                   ? x => x.OrderByDescending(x => x.FarmName)
                                   : x => x.OrderBy(x => x.FarmName)) : x => x.OrderBy(x => x.FarmName);
                        break;
                    case "location":
                        orderBy = !string.IsNullOrEmpty(paginationParameter.Direction)
                                    ? (paginationParameter.Direction.ToLower().Equals("desc")
                                   ? x => x.OrderByDescending(x => x.Location)
                                   : x => x.OrderBy(x => x.Location)) : x => x.OrderBy(x => x.Location);
                        break;
                    case "status":
                        orderBy = !string.IsNullOrEmpty(paginationParameter.Direction)
                                    ? (paginationParameter.Direction.ToLower().Equals("desc")
                                   ? x => x.OrderByDescending(x => x.Status)
                                   : x => x.OrderBy(x => x.Status)) : x => x.OrderBy(x => x.Status);
                        break;
                    case "description":
                        orderBy = !string.IsNullOrEmpty(paginationParameter.Direction)
                                    ? (paginationParameter.Direction.ToLower().Equals("desc")
                                   ? x => x.OrderByDescending(x => x.Description)
                                   : x => x.OrderBy(x => x.Description)) : x => x.OrderBy(x => x.Description);
                        break;
                    case "owner":
                        orderBy = !string.IsNullOrEmpty(paginationParameter.Direction)
                                    ? (paginationParameter.Direction.ToLower().Equals("desc")
                                   ? x => x.OrderByDescending(x => x.Owner)
                                   : x => x.OrderBy(x => x.Owner)) : x => x.OrderBy(x => x.Owner);
                        break;
                    case "fullname":
                        orderBy = !string.IsNullOrEmpty(paginationParameter.Direction)
                                    ? (paginationParameter.Direction.ToLower().Equals("desc")
                                   ? x => x.OrderByDescending(x => x.User.FullName)
                                   : x => x.OrderBy(x => x.User.FullName)) : x => x.OrderBy(x => x.User.FullName);
                        break;
                    case "createdate":
                        orderBy = !string.IsNullOrEmpty(paginationParameter.Direction)
                                    ? (paginationParameter.Direction.ToLower().Equals("desc")
                                   ? x => x.OrderByDescending(x => x.CreateDate)
                                   : x => x.OrderBy(x => x.CreateDate)) : x => x.OrderBy(x => x.CreateDate);
                        break;
                    default:
                        orderBy = x => x.OrderBy(x => x.FarmId);
                        break;
                }
            string includeProperties = "User,DetailProposal";
            var result = await _unitOfWork.ProposalRepository.Get(filter, orderBy, includeProperties, paginationParameter.PageIndex, paginationParameter.PageSize);
            var pagin = new PageEntity<ProposalModel>();
            pagin.List = _mappper.Map<IEnumerable<ProposalModel>>(result);
            pagin.TotalRecord = await _unitOfWork.ProposalRepository.Count();
            pagin.TotalPage = PaginHelper.PageCount(pagin.TotalRecord, paginationParameter.PageSize);
            return pagin;

    
        }

        public Task<ProposalModel> GetByID(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ProposalModel> Insert(CreateProposalModel entityinsert)
        {
            throw new NotImplementedException();
        }

        public Task<ProposalModel> Update(UpdateProposalModel entityUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
