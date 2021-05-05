using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisumData.User
{
    interface IUser
    {

        User GetUser(string username, string password);
    }
}
