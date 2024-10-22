using KoiAuction.BussinessModels.CheckingProposal;
using KoiAuction.Service.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiAuction.Service.ISerivice;

public interface ICheckingProposalService
{
    /*Task<IBusinessResult> Get(PaginationParameter paginationParameter);*/

    Task<IBusinessResult> Get(string? searchKey, string? orderBy, int? pageIndex = null, int? pageSize = null);

    Task<IBusinessResult> GetByID(int id);

    Task<IBusinessResult> Insert(CreateCheckingProposalModel entityinsert);
    Task<IBusinessResult> Update(int id, UpdateCheckingProposalModel entityUpdate);
    /*Task<IBusinessResult> Test(UpdateCheckingProposalModel entityUpdate);*/
    Task<IBusinessResult> Delete(int id);

    Task<IBusinessResult> GetFish();
}

