﻿using KoiAuction.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiAuction.Repository.IRepositories;

public interface ICheckingProposalRepository
{
    public Task<CheckingProposal> GetCheckingByID(int id);
}

