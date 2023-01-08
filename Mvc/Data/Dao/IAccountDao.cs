using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mvc.Data.Entity;
using Mvc.Models;

namespace Mvc.Data.Dao
{
    public interface IAccountDao
    {
        ICollection<User> GetAllUsers();
    }
}
