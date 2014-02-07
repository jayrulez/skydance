using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BillBox.Models;

namespace BillBox.Util
{
    public class AuthUtil
    {
        public static User GetUser(string username)
        {
            Entities dbContext = new Entities();

            User user = dbContext.Users.FirstOrDefault(u=> u.Username == username);

            return user;
        }
    }
}