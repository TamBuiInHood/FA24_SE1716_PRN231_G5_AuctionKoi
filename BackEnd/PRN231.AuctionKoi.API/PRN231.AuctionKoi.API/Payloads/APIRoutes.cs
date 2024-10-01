namespace PRN231.AuctionKoi.API.Payloads
{
    public static class APIRoutes
    {
        public const string Base = "auction-koi";

        public static class AuctionKoi
        {
            public const string GetAll = Base + "/weatherforecast/";
        }

        public static class Authentication
        {
            public const string Login = Base + "/authentication/login";

            public const string LoginMobile = Base + "/authentication/login-mobile";

            public const string RefreshToken = Base + "/authentication/refresh-token";

        }

        public static class Paymnet
        {
            public const string Get = Base + "/paymnets/";

            public const string GetByID = Base + "/paymnets/{search-id}";

            public const string Update = Base + "/paymnets/{payment-id}";

            public const string Delete = Base + "/paymnets/{payment-id}";

            public const string Create = Base + "/paymnets/";

            public const string GetNoPagin = Base + "/paymnets/";

        }

        public static class Proposal
        {
            public const string Get = Base + "/proposals/";

            public const string Create = Base + "/proposals/create-proposal";

            public const string GetByID = Base + "/proposals/{proposal-id}";

            public const string Update = Base + "/proposals/{proposal-id}";

            public const string Delete = Base + "/proposals/{proposal-id}";

            public const string GetNoPagin = Base + "/proposals/get-no-paging";

            public const string GetUser = Base + "/proposals/user";

        }

        public static class UserAuction
        {
            public const string Get = Base + "/user-auctions/";

            public const string GetByID = Base + "/user-auctions/{bid-id}";

        }

        public static class DetailProposal
        {
            public const string Get = Base + "/detailProposal/";

            public const string Create = Base + "/detailProposal/create-detail-proposal";

            public const string GetByID = Base + "/detailProposal/{detail-proposal-id}";

            public const string Update = Base + "/detailProposal/{detail-proposal-id}";

            public const string Delete = Base + "/detailProposal/{detail-proposal-id}";

            public const string GetNoPagin = Base + "/detailProposal/get-no-paging";

            public const string GetListAuction = Base + "/detailProposal/auction";

            public const string GetListDetailProposalType = Base + "/detailProposal/type";

            public const string GetProposal = Base + "/detailProposal/proposal";

        }


    }
}
