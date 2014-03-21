﻿using BillBox.Models;
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
using System.Web.Routing;

namespace BillBox.Common
{
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
        TextType = 0, IntegerType, AlphabeticType, AlphanumericType, DoubleType, DateType
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
            int dateType = (int)FieldType.DateType;

            return new SelectList(new [] {
                new {ID = textType.ToString(), Name = "Text"},
                new {ID = integerType.ToString(), Name = "Integer"},
                new {ID = alphabeticType.ToString(), Name = "Alphabetic"},
                new {ID = alphanumericType.ToString(), Name = "Alphanumeric"},
                new {ID = doubleType.ToString(), Name = "Decimal"},
                new {ID = dateType.ToString(), Name = "Date"}
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

        public static string GetDbSetting(string key, bool fallback = true)
        {
            Entities dbContext = new Entities();

            var setting = dbContext.Settings.FirstOrDefault(s => s.Name == key);

            if (setting != null)
            {
                return setting.Value;
            }
            else
            {
                if(fallback)
                {
                    return Util.GetAppSetting(key);
                }
            }

            return null;
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
            errorMessage = baseException.Message;
            return handled;
        }

        public static string DisplayForDollar(double amount)
        {
            return "$" + amount.ToString();
        }

        public static string DisplayForDollar(float amount)
        {
            return "$" + amount.ToString();
        }

        public static string DisplayForDollar(string amount)
        {
            return "$" + amount;
        }

        public static void PreparePagerInfo(RequestContext context, dynamic dictionary, string actionName, int pageNumber, int pageSize, int totalRecordCount, object routeValues = null)
        {
            var routeValueDictionary = new RouteValueDictionary(routeValues);
            var helper = new UrlHelper(context);

            if (pageNumber > 1)
            {
                routeValueDictionary.Add("page", pageNumber - 1);
                dictionary.Previous = helper.Action(actionName, routeValueDictionary);
            }

            if ((pageNumber * pageSize) < totalRecordCount)
            {
                if (routeValueDictionary.ContainsKey("page"))
                    routeValueDictionary.Remove("page");

                routeValueDictionary.Add("page", pageNumber + 1);
                dictionary.Next = helper.Action(actionName, routeValueDictionary);
            }

            dictionary.RecordTotal = totalRecordCount;
            dictionary.RecordBegin = ((pageNumber - 1) * pageSize) + 1;
            dictionary.RecordEnd = (pageNumber * pageSize) < totalRecordCount ? (pageNumber * pageSize) : totalRecordCount;
        }
    }
}