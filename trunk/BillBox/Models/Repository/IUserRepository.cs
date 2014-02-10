using BillBox.Common;

namespace BillBox.Models.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public interface IUserRepository
    {
        IResponse<User> GetUser(int UserId);
        IResponse<User> GetUser(string Username);
        //IResponse<User> GetUsers();
        //IResponse<User> GetUsers(int PageNumber, int PageSize);

        IResponse<bool> AddUser(User User);
        IResponse<bool> UpdateUser(User User);

        //IResponse<UserLevel> GetUserBranch(int UserId);

        IResponse<UserLevel> GetUserLevel(int UserId);
        IResponse<UserRight> GetUserRights(int UserId);

        IResponse<bool> UpdateUserPassword(int UserId, string Password);
        IResponse<bool> UpdateUserLoginStatus(int UserId, int LoginStatus);
    }
}
