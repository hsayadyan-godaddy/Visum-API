using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisumAPI.Models;

namespace VisumAPI
{
    public class DBClient
    {
        private readonly IMongoCollection<User> _users;
        private readonly IMongoCollection<Project> _projects;

        public DBClient(IOptions<DBClientSettings> settings)
        {

            var client = new MongoClient(settings.Value.MongodbConnection);
            var database = client.GetDatabase("DemoDB");

            _users = database.GetCollection<User>("User");
            _projects = database.GetCollection<Project>("Project");
        }

        public async Task<User> GetUserById(BsonObjectId id) =>
            await _users.Find(u => u.Id == id).FirstOrDefaultAsync();

        public async Task<User> GetUserByUserNameAndpass(string username, string pass)
        {
            //var userNew = new User
            //{
            //    Email = "haykaz.sayadyan@hall",
            //    Password = "strongpassword",
            //    PasswordHash = "hashpassword",
            //    UserName = "Haykaz",
            //    Name = "Haykaz Sayadyan"
            //};

            //await _users.InsertOneAsync(userNew);


            var filter1 = Builders<User>.Filter.Eq("UserName", username);
            var filter2 = Builders<User>.Filter.Eq("Password", pass);
            var user =  await _users.Find(f=>f.UserName == username && f.Password == pass).FirstOrDefaultAsync();
            return  user;
        }



        public async Task<ProjectList> GetProjectsForUser(BsonObjectId userId)
        {
            var user = await GetUserById(userId);
            var customer = new Customer { UserName = user.UserName };
            var projects = await _projects.Find(p => p.UserId == userId).ToListAsync();
            var projectList = new ProjectList
            {
                Customer = customer,
                Projects = projects
            };
            return projectList;
        }
       
    }
}
