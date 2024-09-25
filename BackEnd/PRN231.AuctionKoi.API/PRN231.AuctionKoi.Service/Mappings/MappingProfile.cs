using AutoMapper;
using KoiAuction.BussinessModels.PaymentModels;
using KoiAuction.BussinessModels.Proposal;
using PRN231.AuctionKoi.Repository.Entities;

namespace KoiAuction.Service.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Proposal, ProposalModel>()
                .ForMember(dest => dest.UserFullName, opt => opt.MapFrom(x => x.User.FullName))
                //.ForMember(dest => dest.DetailProposals, opt => opt.MapFrom(x => x.DetailProposals))
                .ReverseMap();
            CreateMap<Payment, PaymentModel>()
                //.ForMember(dest => dest.Order, opt => opt.MapFrom(x => x.Order))
                .ReverseMap();
            CreateMap<Proposal, CreateProposalModel>().ReverseMap();
        }
    }
}
