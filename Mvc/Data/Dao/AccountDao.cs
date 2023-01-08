using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mvc.Data.Entity;
using Mvc.Models;

namespace Mvc.Data.Dao
{
    public class AccountDao : IAccountDao
    {
        private List<User> users = new List<User>();

        public AccountDao()
        {
            users.Add(new User { UserName = "kevin", Password = "111", Role = new string[] { "student" } });
            users.Add(new User
            {
                UserName = "annie",
                Password = "123",
                Role = new string[] { "employer", "student" }
            });
            users.Add(new User
            {
                UserName = "andy",
                Password = "123321",
                Role = new string[] { "employer", "admin" }
            });
        }
        public ICollection<User> GetAllUsers()
        {
            return users;
        }
    }
}
