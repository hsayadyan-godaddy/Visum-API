using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisumData
{
    public class DBClient
    {
        private readonly IMongoCollection<DBUser> _users;


        public DBClient()
        {
            
        }
    }
}
