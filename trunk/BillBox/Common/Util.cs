using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace BillBox.Common
{
    public enum ErrorCode
    {
        NoError, UserNotFound, DuplicateEmailAddress, DuplicateUsername, NoResultsFound,
        FKError,
        DbError, DBEntityValidationError, SysError, Generic1, Generic2

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
                return null;

            try
            {
                value = WebConfigurationManager.AppSettings[Key];
            }
            catch
            {
                return null;
            }

            return value;
        }
    }
}