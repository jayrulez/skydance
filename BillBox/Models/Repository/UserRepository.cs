using System;
using System.Linq;
using System.Data.Entity;

using BillBox.Common;
using System.Collections.Generic;
using System.Data;

namespace BillBox.Models.Repository
{
    /// <summary>
    /// The user repository class
    /// </summary>
    public class UserRepository : BaseRepository, IUserRepository
    {
        /// <summary>
        /// Returns a Response object with a User specified by UserId from the User Repository
        /// </summary>
        /// <param name="UserId">the intended User Id</param>
        /// <returns></returns>
        public IResponse<User> GetUser(int UserId, bool PopulateRelatedFields = false)
        {
            IResponse<User> response = new Response<User>();

            try
            {
                using (this.dbContext)
                {
                    var user = dbContext.Users.Find(UserId);
                    if (user == null)
                    {
                        response.Error = ErrorCode.UserNotFound;
                    }
                    else
                    {
                        if (PopulateRelatedFields)
                        {
                            dbContext.Entry(user).Reference(u => u.UserLevel).Load();
                            dbContext.Entry(user).Reference(u => u.AgentBranch).Load();
                            dbContext.Entry(user).Reference(u => u.Parish).Load();
                            dbContext.Entry(user).Reference(u => u.Agent).Load();
                        }
                        
                        response.Result = user;
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
        /// Returns a Response object with a User specified by Username from the User Repository
        /// </summary>
        /// <param name="Username">the intended User Username</param>
        /// <returns></returns>
        public IResponse<User> GetUser(string Username, bool PopulateRelatedFields = false)
        {
            IResponse<User> response = new Response<User>();

            try
            {
                using (this.dbContext)
                {
                    var user = dbContext.Users.FirstOrDefault(u => u.Username == Username);
                    if (user == null)
                    {
                        response.Error = ErrorCode.UserNotFound;
                    }
                    else
                    {
                        if (PopulateRelatedFields)
                        {
                            dbContext.Entry(user).Reference(u => u.UserLevel).Load();
                            dbContext.Entry(user).Reference(u => u.AgentBranch).Load();
                            dbContext.Entry(user).Reference(u => u.Parish).Load();
                            dbContext.Entry(user).Reference(u => u.Agent).Load();
                        }

                        response.Result = user;
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
        /// 
        /// </summary>
        /// <returns></returns>
        public IResponse<User> GetUsers(bool PopulateRelatedFields = false)
        {
            IResponse<User> response = new Response<User>();

            try
            {
                using (dbContext)
                {
                    var users = new List<User>();

                    if (PopulateRelatedFields)
                    {
                        users = dbContext.Users
                            .Include(u => u.UserLevel)
                            .Include(u => u.AgentBranch)
                            .Include(u => u.Parish)
                            .Include(u => u.Agent)
                            .ToList();
                    }
                    else
                    {
                        users = dbContext.Users.ToList();
                    }

                    if (users.Count > 0)
                    {
                        response.Results = users;
                    }
                    else
                    {
                        response.Error = ErrorCode.UserNotFound;
                    }
                }
            }
            catch (Exception)
            {
                response.Error = ErrorCode.DbError;
            }

            return response;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="PageNumber"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        public IResponse<User> GetUsers(int PageNumber, int PageSize, bool PopulateRelatedFields = false)
        {
            IResponse<User> response = new Response<User>();

            try
            {
                using (dbContext)
                {
                    var users = new List<User>();

                    if (PopulateRelatedFields)
                    {
                        users = dbContext.Users
                            .Include(u => u.UserLevel)
                            .Include(u => u.AgentBranch)
                            .Include(u => u.Parish)
                            .Include(u => u.Agent)
                            .ToList()
                            .Skip((PageNumber - 1) * PageSize)
                            .Take(PageSize)
                            .ToList();
                    }
                    else
                    {
                        users = dbContext.Users
                            .ToList()
                            .Skip((PageNumber - 1) * PageSize)
                            .Take(PageSize)
                            .ToList();
                    }

                    if (users.Count > 0)
                    {
                        response.Results = users;
                    }
                    else
                    {
                        response.Error = ErrorCode.UserNotFound;
                    }
                }
            }
            catch (Exception)
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
                using (this.dbContext)
                {
                    this.dbContext.Users.Add(User);
                    var result = this.dbContext.SaveChanges();

                    /*Check the number of rows affected by the operation. 0 means the operation failed*/
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
                    if (ex.Message.Contains(Util.GetAppSetting("UKViolation_UserEmail")))
                    {
                        response.Error = ErrorCode.DuplicateEmailAddress;
                    }
                    else if (ex.Message.Contains(Util.GetAppSetting("UKViolation_Username")))
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
                using (this.dbContext)
                {
                    this.dbContext.Users.Attach(User);
                    var entry = this.dbContext.Entry(User);
                    entry.State = EntityState.Modified;

                    /*prevent modification on the following fields*/ 
                    entry.Property(e => e.PasswordExpireAt).IsModified = false;
                    entry.Property(e => e.Password).IsModified = false;

                    var result = this.dbContext.SaveChanges();

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
                    if (ex.Message.Contains(Util.GetAppSetting("UKViolation_UserEmail")))
                    {
                        response.Error = ErrorCode.DuplicateEmailAddress;
                    }
                    else if (ex.Message.Contains(Util.GetAppSetting("UKViolation_Username")))
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
                using (this.dbContext)
                {
                    var userLevel = this.dbContext.Users.FirstOrDefault(u => u.UserId == UserId).UserLevel;

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
                using (this.dbContext)
                {
                    var userRights = this.dbContext.Users.Find(UserId).UserLevel.UserRights.ToList();
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
                using (this.dbContext)
                {
                    var user = this.dbContext.Users.Find(UserId);
                    if (user != null)
                    {
                        user.Password = Password;

                        int passwordExpirationPeriod;  //number of days
                        bool isSuccessful = Int32.TryParse(Util.GetAppSetting("PasswordExpirationPeriod"), out passwordExpirationPeriod);

                        user.PasswordExpireAt = (isSuccessful) ? DateTime.Now.AddDays(passwordExpirationPeriod) : DateTime.Now.AddDays(30);

                        int result = this.dbContext.SaveChanges();

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
                using (this.dbContext)
                {
                    var user = this.dbContext.Users.Find(UserId);

                    if (user != null)
                    {
                        int result = this.dbContext.SaveChanges();

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
    }
}