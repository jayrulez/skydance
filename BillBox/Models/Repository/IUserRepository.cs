using BillBox.Common;

namespace BillBox.Models.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Returns a Response object with a User specified by UserId from the User Repository
        /// </summary>
        /// <param name="UserId">the intended User Id</param>
        /// <param name="PopulateRelatedFields">Indicate if the Navigation fields should be populated, such that User.Reference table becomes available after the db context is disposed. Default to false</param>
        /// <returns></returns>
        IResponse<User> GetUser(int UserId, bool PopulateRelatedFields = false);

        /// <summary>
        /// Returns a Response object with a User specified by Username from the User Repository
        /// </summary>
        /// <param name="Username">the intended User Username</param>
        /// <param name="PopulateRelatedFields">Indicate if the Navigation fields should be populated, such that User.Reference table becomes available after the db context is disposed. Default to false</param>
        /// <returns></returns>
        IResponse<User> GetUser(string Username, bool PopulateRelatedFields = false);
        
        /// <summary>
        /// Returns a Response object with a list of all the users from the User Repository
        /// </summary>
        /// <returns></returns>
        IResponse<User> GetUsers(bool PopulateRelatedFields = false);

        /// <summary>
        /// Returns a Response object with a paged list of users from the User Repository
        /// </summary>
        /// <param name="PageNumber">the page number to fetch</param>
        /// <param name="PageSize">the size of the page to fetch</param>
        /// <returns></returns>
        IResponse<User> GetUsers(int PageNumber, int PageSize, bool PopulateRelatedFields = false);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="User"></param>
        /// <returns></returns>
        IResponse<bool> AddUser(User User);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="User"></param>
        /// <returns></returns>
        IResponse<bool> UpdateUser(User User);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>   
        IResponse<UserLevel> GetUserLevel(int UserId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        IResponse<UserRight> GetUserRights(int UserId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        IResponse<bool> UpdateUserPassword(int UserId, string Password);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="LoginStatus"></param>
        /// <returns></returns>
        IResponse<bool> UpdateUserLoginStatus(int UserId, int LoginStatus);

    }
}
