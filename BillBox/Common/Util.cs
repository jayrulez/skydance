using BillBox.Models;
using System;
using System.Web.Configuration;

namespace BillBox.Common
{

    public enum PagedList
    {
        General = 0, Users, Agents, Branches, Subscribers, CaptureFields,
        PaymentHistory, PaymentMethods, PaymentTypeCaptureFields
    }

    public class Util
    {

        /// <summary>
        /// Returns a string value from the webconfig appsetting based on the specified key.
        /// If the key is not found then null is returned
        /// </summary>
        /// <param name="Key">the string index for the value required</param>
        /// <returns></returns>
        public static string GetAppSetting(string Key)
        {
            string value = null;

            try
            {
                if (string.IsNullOrEmpty(Key))
                    throw new Exception();

                value = WebConfigurationManager.AppSettings[Key];
            }
            catch
            {
                return null;
            }

            return value;
        }

        /// <summary>
        /// Get the size of the specified page list
        /// </summary>
        /// <param name="PageList">The page for which the size is required</param>
        /// <returns></returns>
        public static int GetPageSize(PagedList PageList)
        {
            int pageSize = 0;
            string key;
            switch (PageList)
            {
                case PagedList.General: key = "PageSize_General";
                    break;
                case PagedList.Users: key = "PageSize_Users";
                    break;
                case PagedList.Agents: key = "PageSize_Agents";
                    break;
                case PagedList.Branches: key = "PageSize_Branches";
                    break;
                case PagedList.Subscribers: key = "PageSize_Subscribers";
                    break;
                case PagedList.CaptureFields: key = "PageSize_CaptureFields";
                    break;
                case PagedList.PaymentHistory: key = "PageSize_PaymentHistory";
                    break;
                case PagedList.PaymentMethods: key = "PageSize_PaymentTypes";
                    break;
                case PagedList.PaymentTypeCaptureFields: key = "PageSize_PaymentTypeCaptureFields";
                    break;
                default: key = "PageSize_General";
                    break;
            }

            bool isSuccessful = int.TryParse(GetAppSetting(key), out pageSize);

            return (isSuccessful) ? pageSize : 25;
        }


        public static int GetPasswordExpirationDays()
        {
            int numberOfDays = 0;

            bool isSuccessful = int.TryParse(GetAppSetting("PasswordExpiryDays"), out numberOfDays);

            return (isSuccessful) ? numberOfDays : 30;
        }

        public static User GetUserById(int userId)
        {
            Entities dbContext = new Entities();

            User user = dbContext.Users.Find(userId);

            return user;
        }

        public static User GetLoggedInUser()
        {
            int userId = 1;

            return Util.GetUserById(userId);
        }

        public static int GenerateInvoiceNumber()
        {
            return 0;
        }
    }
}