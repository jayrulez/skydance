using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Billbox.Common;

namespace Billbox.Models
{
    public class UserRepository
    {
        public Response<User> GetUser(string username)
        {
            Response<User> response = new Response<User>();

            try
            {
                using (Entities db = new Entities())
                {
                    var user = db.Users.FirstOrDefault( u => u.Username == username);
                    if (user == null)
                        response.Error = ErrorCode.UserNotFound;
                    else
                        response.Result = user;
                }
            }
            catch
            {
                response.Error = ErrorCode.SysError;
            }

            return response;
        }

        public Response<Boolean> AddUser(User user)
        {
            Response<Boolean> result = new Response<bool>();

            try 
	        {	        
		        using(Entities db = new Entities())
                {
                    db.Users.Add(user);
                    var dbResult = db.SaveChanges();

                    if (dbResult == 0)
                        result.Error = ErrorCode.Generic1;
                }
	        }            
	        catch (Exception)
	        {		
		        result.Error = ErrorCode.SysError;
	        }

            return result;
        }
    }
}