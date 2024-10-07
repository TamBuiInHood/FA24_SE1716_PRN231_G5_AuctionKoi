using AutoMapper;
using KoiAuction.BussinessModels.DetailProposalModel;
using KoiAuction.BussinessModels.Order;
using KoiAuction.BussinessModels.PaymentModels;
using KoiAuction.BussinessModels.Proposal;
using KoiAuction.BussinessModels.UserAuctionModels;
using KoiAuction.BussinessModels.UserModels;
using KoiAuction.Repository.Entities;

namespace KoiAuction.Service.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserModel>().ReverseMap();
            CreateMap<Proposal, ProposalModel>()
                .ForMember(dest => dest.UserFullName, opt => opt.MapFrom(x => x.User.FullName))
                //.ForMember(dest => dest.DetailProposals, opt => opt.MapFrom(x => x.DetailProposals))
                .ReverseMap();
            CreateMap<Payment, PaymentModel>()
                //.ForMember(dest => dest.Order, opt => opt.MapFrom(x => x.Order))
                .ReverseMap();
            CreateMap<Proposal, CreateProposalModel>().ReverseMap();

            CreateMap<UserAuction, UserAuctionModel>()
               .ForMember(dest => dest.FishCode, opt => opt.MapFrom(x => x.Fish.FishCode))
               .ForMember(dest => dest.FishName, opt => opt.MapFrom(x => x.Fish.FishName))
               .ForMember(dest => dest.FishTypeName, opt => opt.MapFrom(x => x.Fish.FishType.FishTypeName))
               .ForMember(dest => dest.FarmName, opt => opt.MapFrom(x => x.Fish.Farm!.FarmName))
               .ForMember(dest => dest.UserCode, opt => opt.MapFrom(x => x.User.UserCode))
               .ForMember(dest => dest.FullName, opt => opt.MapFrom(x => x.User.FullName))
               .ForMember(dest => dest.Mail, opt => opt.MapFrom(x => x.User.Mail))
               .ForMember(dest => dest.AuctionId, opt => opt.MapFrom(x => x.Fish.Auction!.AuctionId))
               .ForMember(dest => dest.AuctionCode, opt => opt.MapFrom(x => x.Fish.Auction!.AuctionCode))
               .ReverseMap();

            CreateMap<DetailProposal, DetailProposalModel>()
                .ForMember(dest => dest.FarmName, opt => opt.MapFrom(x => x.Farm.FarmName))
                .ForMember(dest => dest.AuctionName, opt => opt.MapFrom(x => x.Auction.AuctionName))
                .ForMember(dest => dest.FishTypeName, opt => opt.MapFrom(x => x.FishType.FishTypeName))
                .ReverseMap();
            CreateMap<DetailProposal, CreateDetailProposalModel>().ReverseMap();

            CreateMap<UserAuctionModel, UserAuction>()
                .ForMember(dest => dest.Fish, opt => opt.Ignore())
                .ForMember(dest => dest.User, opt => opt.Ignore())
                .ForMember(dest => dest.OrderDetails, opt => opt.Ignore());
            CreateMap<Order, OrderModel>().ReverseMap();
            CreateMap<Order, UpdateOrder>().ReverseMap();
            CreateMap<CreateOrder, Order>()
                 .ForMember(dest => dest.OrderCode, opt => opt.Ignore())
                 .ForMember(dest => dest.Vat, opt => opt.Ignore())
                 .ForMember(dest => dest.TotalPrice, opt => opt.Ignore())
                 .ForMember(dest => dest.OrderDate, opt => opt.Ignore())
                 .ForMember(dest => dest.Status, opt => opt.Ignore())
                 .ForMember(dest => dest.DeliveryDate, opt => opt.Ignore())
                 .ForMember(dest => dest.OrderDetails, opt => opt.Ignore())
                 .ForMember(dest => dest.TotalProduct, opt => opt.Ignore())
                 .ForMember(dest => dest.ShippingCost, opt => opt.Ignore());

        }
    }
}
