using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisumAPI.Models;
using VisumData;

namespace VisumAPI
{
    public class DBClient
    {
        private readonly IMongoCollection<User> _users;
        private readonly IMongoCollection<Project> _projects;
        private readonly IMongoCollection<Well> _well;
        private readonly IMongoCollection<WellData> _wellData;

        public DBClient(IOptions<DBClientSettings> settings)
        {

            var client = new MongoClient(settings.Value.MongodbConnection);
            var database = client.GetDatabase("DemoDB");

            _users = database.GetCollection<User>("User");
            _projects = database.GetCollection<Project>("Project");
            _well = database.GetCollection<Well>("Well");
            _wellData = database.GetCollection<WellData>("WellData");
        }

        public async Task<WellData> GetWellDataById(string id)
        {
            var data = await _wellData.Find(d => d.WellId == id).SortByDescending(d => d.DateTime).FirstOrDefaultAsync();
            return data;
        }

        public async Task<List<Well>> GetWellsByProjectId(string id)
        {
            var wells = await _well.Find(p => p.ProjectId == id).ToListAsync();

            return wells;
        }

        public async Task AddWell(Well well)
        {
            await _well.InsertOneAsync(well);
        }

        public async Task<Well> GetWellById(string id)
        {
            var well = await _well.Find(p => p.Id == id).FirstOrDefaultAsync();

            return well;
        }

        public async Task<WellInfo> GetWellAndProjectById(string id)
        {
            var well = await _well.Find(p => p.Id == id).FirstOrDefaultAsync();
            var project = await _projects.Find(p => p.Id == well.ProjectId).FirstOrDefaultAsync();
            var wellInfo = new WellInfo()
            {
                WellName = well.WellName,
                WellType = well.WellType,
                ProjectName = project.ProjectName,
                Id = well.Id,
                Status = "Active"
            };

            return wellInfo;
        }

        public async Task AddWellData(WellData welldata)
        {
            //TODO datetime
            //welldata.DateTime = DateTime.Now;
            await _wellData.InsertOneAsync(welldata);
        }

        public async Task<User> GetUserById(string id) =>
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

        public async Task AddProject(Project project, ObjectId userId)
        {
            var projectn = new Project
            {
                Country = "USA",
                ProjectName = "Project1",
                Reservoir = "reservoir z",
                Pad = "Pad 1",
                UserId = userId
            };
            await _projects.InsertOneAsync(projectn);
        }

        public async Task<List<Project>> GetProjectsForUser(BsonObjectId userId)
        {
            var projects = await _projects.Find(p => p.UserId == userId).ToListAsync();
            
            return projects;
        }

        public async Task<Project> GetProjectById(string projectId)
        {
            var project = await _projects.Find(p => p.Id == projectId).FirstOrDefaultAsync();

            return project;
        }
    }
}
