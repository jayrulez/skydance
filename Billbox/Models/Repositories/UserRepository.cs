﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using Billbox.Common;

namespace Billbox.Models
{
    /// <summary>
    /// The user repository class
    /// </summary>
    public class UserRepository
    {
        /// <summary>
        /// Returns a specified user from the UserRepository in a generic Response object
        /// </summary>
        /// <param name="username">the user's unique name</param>
        /// <returns></returns>
        public Response<User> GetUser(string username)
        {
            Response<User> response = new Response<User>();

            try
            {
                using (Entities db = new Entities())
                {
                    var user = db.Users.FirstOrDefault(u => u.Username == username);
                    if (user == null)
                        response.Error = ErrorCode.UserNotFound;
                    else
                        response.Result = user;
                }
            }
            catch
            {
                response.Error = ErrorCode.DbError;
            }

            return response;
        }
        
        /// <summary>
        /// Add a user to the user repository
        /// </summary>
        /// <param name="user">The user object to be added to the repository</param>
        /// <returns></returns>
        public Response<Boolean> AddUser(User user)
        {
            Response<Boolean> response = new Response<bool>();

            try
            {
                using (Entities db = new Entities())
                {
                    db.Users.Add(user);
                    var result = db.SaveChanges();

                    if (result == 0)
                        response.Error = ErrorCode.Generic1; //to be changed
                    else
                        response.Result = true;
                }
            }            
            catch (Exception ex)
            {
                ex = ex.GetBaseException();

                if (ex.Message.Contains("Violation of UNIQUE KEY constraint"))
                {
                    if (ex.Message.Contains(ConfigurationManager.AppSettings["UKViolation_EmailAddress"].ToString()))
                    {
                        response.Error = ErrorCode.DuplicateEmailAddress;
                    }
                    else if (ex.Message.Contains(ConfigurationManager.AppSettings["UKViolation_Username"].ToString()))
                    {
                        response.Error = ErrorCode.DuplicateUsername;
                    }
                    else
                    {
                        response.Error = ErrorCode.DbError;
                    }
                }
                else if (ex.Message.Contains("The INSERT statement conflicted with the FOREIGN KEY constraint"))
                {
                    response.Error = ErrorCode.FKError;
                }
                else if (ex.Message.Contains("Validation failed for one or more entities"))
                {
                    response.Error = ErrorCode.DBEntityValidationError;
                }
                else
                {
                    response.Error = ErrorCode.DbError;
                }
            }

            return response;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Response<Boolean> UpdateUser(User user)
        {
            Response<Boolean> response = new Response<bool>();

            try
            {
                using (Entities db = new Entities())
                {
                    db.Users.Attach(user);
                    var entry = db.Entry(user);
                    entry.State = System.Data.EntityState.Modified;

                    /*prevent accidental userid modification*/
                    //entry.Property(e => e.UserId).IsModified = true;

                    entry.Property(e => e.Name).IsModified = false;

                    var result = db.SaveChanges();

                    if (result > 0)
                        response.Result = true;
                    else
                        response.Error = ErrorCode.UserNotFound;
                }
            }
            catch (Exception ex)
            {
                ex = ex.GetBaseException();

                if (ex.Message.Contains("Violation of UNIQUE KEY constraint"))
                {
                    if (ex.Message.Contains(ConfigurationManager.AppSettings["UKViolation_EmailAddress"].ToString()))
                    {
                        response.Error = ErrorCode.DuplicateEmailAddress;
                    }
                    else if (ex.Message.Contains(ConfigurationManager.AppSettings["UKViolation_Username"].ToString()))
                    {
                        response.Error = ErrorCode.DuplicateUsername;
                    }
                    else
                    {
                        response.Error = ErrorCode.DbError;
                    }
                }
                else if (ex.Message.Contains("The INSERT statement conflicted with the FOREIGN KEY constraint"))
                {
                    response.Error = ErrorCode.FKError;
                }
                else if (ex.Message.Contains("Validation failed for one or more entities"))
                {
                    response.Error = ErrorCode.DBEntityValidationError;
                }
                else
                {
                    response.Error = ErrorCode.DbError;
                }
            }

            return response;
        }
        
        /// <summary>
        /// Returns the user level
        /// </summary>
        /// <param name="userId">the user's unique identifier</param>
        /// <returns></returns>
        public Response<UserLevel> GetUserLevel(int userId)
        {
            Response<UserLevel> response = new Response<UserLevel>();

            try
            {
                using (Entities db = new Entities())
                {
                    var userLevel = db.Users.FirstOrDefault(u => u.UserId == userId).UserLevel;

                    if (userLevel == null)
                        response.Error = ErrorCode.UserNotFound;
                    else
                        response.Result = userLevel;
                }
            }
            catch
            {
                response.Error = ErrorCode.DbError;
            }

            return response;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Response<UserLevel> GetUserRights(int userId)
        {
            Response<UserLevel> response = new Response<UserLevel>();

            try
            {
                using (Entities db = new Entities())
                {
                    
                }
            }
            catch
            {
                response.Error = ErrorCode.DbError;
            }

            return response;
        }
    }
}