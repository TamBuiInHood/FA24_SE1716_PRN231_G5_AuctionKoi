﻿using KoiAuction.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiAuction.Repository.IRepositories
{
    public interface IDetailProposalRepository
    {
        public Task<bool> DeleteDetailProposal(int id);
        public Task<List<Proposal>> ListProposals();

        public Task<List<FishType>> ListFishTypes();
        public Task<List<Auction>> ListAuctions();
    }
}