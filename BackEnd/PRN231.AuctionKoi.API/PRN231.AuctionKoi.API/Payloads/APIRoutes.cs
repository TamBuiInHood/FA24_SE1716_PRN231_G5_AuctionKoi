﻿namespace PRN231.AuctionKoi.API.Payloads
{
    public static class APIRoutes
    {
        public const string Base = "aution-koi";

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

            public const string GetByID = Base + "/paymnets/{payment-id}";

            public const string Update = Base + "/paymnets/{payment-id}";

            public const string Delete = Base + "/paymnets/{payment-id}";

            public const string GetNoPagin = Base + "/paymnets/";

        }

        public static class Proposal
        {
            public const string Get = Base + "/proposals/";

            public const string Create = Base + "/proposals/create-proposal";

            public const string GetByID = Base + "/proposals/{proposal-id}";

            public const string Update = Base + "/proposals/{proposal-id}";

            public const string Delete = Base + "/proposals/{proposal-id}";

            public const string GetNoPagin = Base + "/proposals/";

        }

    }
}
