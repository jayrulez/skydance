using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace BillBox.Common
{
    public enum ErrorCode
    {
        NoError, UserNotFound, AgentNotFound, SubscriberNotFound, 
        DuplicateEmailAddress, PaymentNotFound,
        DuplicateName, DuplicateOperatingName,  DuplicateUsername, NoResultsFound,
        FKError,
        DbError, DBEntityValidationError, SysError, InvalidPageSize, InvalidPageNumber, Generic1, Generic2

    }

    public enum PagedList
    {
        Default = 0, SubscriberPayments
    }

    public class Util
    {
        /// <summary>
        /// Returns a string value from the webconfig appsetting based on the specified key.
        /// If the key is not found then an null string is returned
        /// </summary>
        /// <param name="Key">the string index for the value required</param>
        /// <returns></returns>
        public static string GetAppSetting(string Key)
        {
            string value = null;

            if (string.IsNullOrEmpty(Key))
                return String.Empty;

            try
            {
                value = WebConfigurationManager.AppSettings[Key];
            }
            catch
            {
                return String.Empty;
            }

            return value;
        }

        public static int GetPageSize(PagedList PageList)
        {
            int pageSize = 0;
            string key;
            switch (PageList)
            {
                case PagedList.SubscriberPayments: key = "PageSize_SubscriberPayments";
                    break;
                default: key = "PageSize_Default";
                    break;
            }

            bool isSuccessful = int.TryParse(GetAppSetting(key), out pageSize);

            return (isSuccessful) ? pageSize : 30;
        }

        public static T SafeOutput<T>(T value)
        {            
            if (value == null)
                return default(T);
            else
                return value;
        }
    

    }
}