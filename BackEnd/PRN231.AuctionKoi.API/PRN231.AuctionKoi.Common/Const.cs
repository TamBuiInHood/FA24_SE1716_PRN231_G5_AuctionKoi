using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiAuction.Common
{
    public static class Const
    {
        public static string APIEndPoint = "https://localhost:7094/auction-koi/";
        #region Error Codes

        public static int ERROR_EXCEPTION = -4;

        #endregion

        #region Success Codes

        public static int SUCCESS_CREATE_CODE = 1;
        public static string SUCCESS_CREATE_MSG = "Save data success";
        public static int SUCCESS_READ_CODE = 1;
        public static string SUCCESS_READ_MSG = "Get data success";
        public static int SUCCESS_UPDATE_CODE = 1;
        public static string SUCCESS_UPDATE_MSG = "Update data success";
        public static int SUCCESS_DELETE_CODE = 1;
        public static string SUCCESS_DELETE_MSG = "Delete data success";
        public static int SUCCESS_CHECK_CODE = 1;
        public static string SUCCESS_CHECK_MSG = "Check data is valid";


        #endregion

        #region Fail code

        public static int FAIL_CREATE_CODE = -1;
        public static string FAIL_CREATE_MSG = "Save data fail";
        public static int FAIL_READ_CODE = -1;
        public static string FAIL_READ_MSG = "Get data fail";
        public static int FAIL_UPDATE_CODE = -1;
        public static string FAIL_UPDATE_MSG = "Update data fail";
        public static int FAIL_DELETE_CODE = -1;
        public static string FAIL_DELETE_MSG = "Delete data fail";
        public static int FAIL_CHECK_ID_CODE = -1;
        public static string FAIL_CHECK_ID_MSG = "Invalid ID format";
        public static int FAIL_CHECK_DATE_FILTER_CODE = -1;
        public static string FAIL_CHECK_DATE_FILTER_MSG = "Date 'To' must greater than Date 'From'";
        public static int FAIL_CHECK_NUMBER_FILTER_CODE = -1;
        public static string FAIL_CHECK_NUMBER_FILTER_MSG = "Number 'To' must greater than Number 'From'";
        #endregion

        #region Warning Code

        public static int WARNING_NO_DATA_CODE = 4;
        public static string WARNING_NO_DATA_MSG = "No data";
        public static int WARNING_INVALID_ID_CODE = 4;
        public static string WARNING_INVALID_ID_MSG = "Invalid ID format";
        public static int WARNING_INVALID_USER_ID_CODE = 4;
        public static string WARNING_INVALID_USER_ID_MSG = "Invalid User ID format";
        public static int WARNING_INVALID_DETAIL_PROPOSAL_ID_CODE = 4;
        public static string WARNING_INVALID_DETAIL_PROPOSAL_ID_MSG = "Invalid Detail Proposal ID format";
        public static int WARNING_EXIST_WINNER_CODE = 4;
        public static string WARNING_EXIST_WINNER_MSG = "Winner already have";
        public static int WARNING_WRONG_ROLE_CODE = 4;
        public static string WARNING_WRONG_ROLE_MSG = "Users are not authorized to make bids";
        public static int WARNING_INVALID_DATE_FILTER_CODE = 4;
        public static string WARNING_INVALID_DATE_FILTER_MSG = "Date 'To' must greater than Date 'From'";
        public static int WARNING_INVALID_LOGIN_CODE = 4;
        public static string WARNING_INVALID_LOGIN_MSG = "UserName or Password is wromg";
        public static int WARNING_INVALID_USER_AUCTION_CODE = 4;
        public static string WARNING_INVALID_USER_AUCTION_MSG = "Fish or Auction does not exist";
        public static int WARNING_AUCTION_IN_ACTIVE_CODE = 4;
        public static string WARNING_AUCTION_IN_ACTIVE_MSG = "The auction is not active";
        public static int WARNING_INVALID_AUCTION_PRICE_CODE = 4;
        public static string WARNING_INVALID_AUCTION_PRICE_MSG = "Your bid is below the minimum required.";
        #endregion
    }
}
