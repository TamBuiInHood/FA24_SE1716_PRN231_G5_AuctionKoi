using KoiAuction.BussinessModels.DetailProposalModel;
using KoiAuction.BussinessModels.Pagination;
using KoiAuction.BussinessModels.Proposal;
using KoiAuction.BussinessModels.UserAuctionModels;
using KoiAuction.Service.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiAuction.Common
{
    public class ODataResultConverter<T>
    {
        public static IQueryable<T> ConvertToQueryable(IBusinessResult businessResult)
        {
            if (businessResult.Data is PageEntity<T> list)
            {
                return list.List.AsQueryable();
            }

            throw new InvalidOperationException("Invalid business result type");
        }

        public static IQueryable<T> ConvertToNoPaginQueryable(IBusinessResult businessResult)
        {
            if (businessResult.Data is IEnumerable<T> data)
            {
                return data.AsQueryable();
            }

            throw new InvalidOperationException("Invalid business result type");
        }
    }
}
