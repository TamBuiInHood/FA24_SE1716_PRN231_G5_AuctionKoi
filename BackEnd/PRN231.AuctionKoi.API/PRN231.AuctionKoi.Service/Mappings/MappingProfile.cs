using AutoMapper;
using PRN231.AuctionKoi.Repository.Entities;
using PRN231.AuctionKoi.Service.Models.Proposal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PRN231.AuctionKoi.Service.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Proposal, ProposalModel>()
                .ForMember(dest => dest.UserFullName, opt => opt.MapFrom(x => x.User.FullName))
                .ForMember(dest => dest.DetailProposals, opt => opt.MapFrom(x => x.DetailProposals))
                .ReverseMap();
        }
    }
}
