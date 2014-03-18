using BillBox.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Web.Configuration;
using System.Data.Entity.Infrastructure;
using System.Data;
using System.Data.SqlClient;

namespace BillBox.Common
{
    //public static class CustomExtensions
    //{
    //    public static string HiddenFormat(this bool value)
    //    {
    //        return (!value) ? "hidden" : string.Empty;
    //    }

    //    public static MvcHtmlString IfUserHasPermission (this MvcHtmlString mvcHtmlString, string permissionName, IList<string> userPermissions)
    //    {
    //        return (userPermissions.Contains(permissionName)) ? mvcHtmlString : MvcHtmlString.Empty;
    //    }
    //}

    public enum PagedList
    {
        General = 0, Users, Agents, Branches, Subscribers, CaptureFields,
        PaymentHistory, PaymentMethods, PaymentTypeCaptureFields, CollectionsReport
    }

    public enum BillStatus
    {
        Init = 0, Working, Posted
    }

    public enum FieldType
    {
        TextType = 0, IntegerType, AlphabeticType, AlphanumericType, DoubleType
    }

    public class Util
    {
        public static SelectList GetFieldTypes()
        {
            int textType = (int)FieldType.TextType;
            int integerType = (int)FieldType.IntegerType;
            int alphabeticType = (int)FieldType.AlphabeticType;
            int alphanumericType = (int)FieldType.AlphanumericType;
            int doubleType = (int)FieldType.DoubleType;

            return new SelectList(new [] {
                new {ID = textType.ToString(), Name = "Text"},
                new {ID = integerType.ToString(), Name = "Integers"},
                new {ID = alphabeticType.ToString(), Name = "Alphabetic"},
                new {ID = alphanumericType.ToString(), Name = "Alphanumeric"},
                new {ID = doubleType.ToString(), Name = "Decimal"}
            }, "ID", "Name");
        }

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
                case PagedList.CollectionsReport: key = "PageSize_CollectionsReport";
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
            try
            {
                Entities dbContext = new Entities();

                User user = dbContext.Users.Find(userId);
                return user;
            }
            catch (EntityException ex)
            {
                throw ex;
            }
        }

        public static User GetLoggedInUser()
        {
            try
            {
                string username = System.Web.HttpContext.Current.User.Identity.Name;

                Entities dbContext = new Entities();

                var user = dbContext.Users.FirstOrDefault(u => u.Username == username);

                return user;
            }
            catch(Exception ex)
            {
                throw ex;
            }  
        }

       
        public static int GenerateInvoiceNumber()
        {
            return (int)DateTime.Now.Ticks;
        }

        //public static bool CheckPermission(string permission)
        //{
        //    return false;
        //}

        public static bool HandleException(Exception baseException, out string errorMessage)
        {
            bool handled = false;
            errorMessage = string.Empty;

            if (baseException.Message.Contains("Cannot open database") || 
                baseException.Message.Contains("Login failed") || 
                baseException.Message.Contains("network-related"))
            {
               errorMessage = "Database server is not available! Please inform the system admistrator";
                handled = true;
            }            

            return handled;
        }
    }
}