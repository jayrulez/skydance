using System;
using System.Linq;
using Billbox.Common;
using Billbox.Models.Interfaces;
using System.Web.Configuration;

namespace Billbox.Models.Respositories
{
    /// <summary>
    /// The user repository class
    /// </summary>
    public class UserRepository : IUserRepository
    {
        /// <summary>
        /// Returns a specified user from the UserRepository in a generic Response object
        /// </summary>
        /// <param name="Username">the user's unique name</param>
        /// <returns></returns>
        public IResponse<User> GetUser(string Username)
        {
            IResponse<User> response = new Response<User>();

            try
            {
                using (Entities db = new Entities())
                {
                    var user = db.Users.FirstOrDefault(u => u.Username == Username);
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
        /// <param name="User">The user object to be added to the repository</param>
        /// <returns></returns>
        public IResponse<bool> AddUser(User User)
        {
            IResponse<bool> response = new Response<bool>();

            try
            {
                using (Entities db = new Entities())
                {
                    db.Users.Add(User);
                    var result = db.SaveChanges();

                    if (result == 0)
                        response.Error = ErrorCode.DbError; //to be changed
                    else
                        response.Result = true;
                }
            }
            catch (Exception ex)
            {
                ex = ex.GetBaseException();

                if (ex.Message.Contains("Violation of UNIQUE KEY constraint"))
                {
                    if (ex.Message.Contains(GetAppSetting("UKViolation_EmailAddress")))
                    {
                        response.Error = ErrorCode.DuplicateEmailAddress;
                    }
                    else if (ex.Message.Contains(GetAppSetting("UKViolation_Username")))
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
        /// <param name="User"></param>
        /// <returns></returns>
        public IResponse<bool> UpdateUser(User User)
        {
            IResponse<bool> response = new Response<bool>();

            try
            {
                using (Entities db = new Entities())
                {
                    db.Users.Attach(User);
                    var entry = db.Entry(User);
                    entry.State = System.Data.EntityState.Modified;

                    /*prevent modification on the following fields*/ 
                    entry.Property(e => e.LoginStatus).IsModified = false;
                    entry.Property(e => e.PasswordExpireAt).IsModified = false;
                    entry.Property(e => e.Password).IsModified = false;

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
                    if (ex.Message.Contains(GetAppSetting("UKViolation_EmailAddress")))
                    {
                        response.Error = ErrorCode.DuplicateEmailAddress;
                    }
                    else if (ex.Message.Contains(GetAppSetting("UKViolation_Username")))
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
        /// <param name="UserId">the user's unique identifier</param>
        /// <returns></returns>
        public IResponse<UserLevel> GetUserLevel(int UserId)
        {
            IResponse<UserLevel> response = new Response<UserLevel>();

            try
            {
                using (Entities db = new Entities())
                {
                    var userLevel = db.Users.FirstOrDefault(u => u.UserId == UserId).UserLevel;

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
        /// Return all the rights belonging to the specified user
        /// </summary>
        /// <param name="UserId">the user unique identifier</param>
        /// <returns>Response</returns>
        public IResponse<UserRight> GetUserRights(int UserId)
        {
            IResponse<UserRight> response = new Response<UserRight>();

            try
            {
                using (Entities db = new Entities())
                {
                    var userRights = db.Users.Find(UserId).UserLevel.UserRights.ToList();
                    if (userRights.Count > 0)
                        response.Results = userRights;
                    else
                        response.Error = ErrorCode.NoResultsFound;
                }
            }
            catch(NullReferenceException)
            {
                response.Error = ErrorCode.UserNotFound;
            }
            catch
            {
                response.Error = ErrorCode.DbError;
            }

            return response;
        }

        /// <summary>
        /// Updates a user's password with a new value
        /// </summary>
        /// <param name="UserId">the user's unique identifier</param>
        /// <param name="Password">the new password value</param>
        /// <returns></returns>
        public IResponse<bool> UpdateUserPassword(int UserId, string Password)
        {
            IResponse<bool> response = new Response<bool>();

            try
            {
                using (Entities db = new Entities())
                {
                    var user = db.Users.Find(UserId);
                    if (user != null)
                    {
                        user.Password = Password;

                        int passwordExpirationPeriod;  //number of days
                        bool isSuccessful = Int32.TryParse(GetAppSetting("PasswordExpirationPeriod"), out passwordExpirationPeriod);

                        user.PasswordExpireAt = (isSuccessful) ? DateTime.Now.AddDays(passwordExpirationPeriod) : DateTime.Now.AddDays(30);

                        int result = db.SaveChanges();

                        if (result > 0)
                            response.Result = true;
                        else
                            response.Error = ErrorCode.DbError;
                    }
                    else
                    {
                        response.Error = ErrorCode.UserNotFound;
                    }
                }
            }
            catch 
            {
                response.Error = ErrorCode.DbError;
            }

            return response;
        }

        /// <summary>
        /// Update the user login status
        /// </summary>
        /// <param name="UserId">the user unique identifier</param>
        /// <param name="LoginStatus">the user login status</param>
        /// <returns></returns>
        public IResponse<bool> UpdateUserLoginStatus(int UserId, int LoginStatus)
        {
            IResponse<bool> response = new Response<bool>();

            try
            {
                using (Entities db = new Entities())
                {
                    var user = db.Users.Find(UserId);

                    if (user != null)
                    {
                        user.LoginStatus = LoginStatus;

                        int result = db.SaveChanges();

                        if (result > 0)
                            response.Result = true;
                        else
                            response.Error = ErrorCode.DbError;
                    }
                    else
                    {
                        response.Error = ErrorCode.UserNotFound;
                    }
                }
            }
            catch
            {
                response.Error = ErrorCode.DbError;
            }

            return response;
        }
        
        /// <summary>
        /// Returns a string value from the webconfig appsetting based on the specified key.
        /// If the key is not found then an null string is returned
        /// </summary>
        /// <param name="Key">the string index for the value required</param>
        /// <returns></returns>
        private string GetAppSetting(string Key)
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