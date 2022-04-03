using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using WebAPI_Project;
using Xunit;

namespace WebAPITest
{
    public class UnitTest1
    {

        private UsersController _controller;
        private IUserService _service;              
        private Mock<IUserService> moqService;
        private const string AppSettingsJsonFile = "appsettings.json";
        private readonly HttpClient HttpClient;

        public UnitTest1()
        {
            var appConfig = new ConfigurationBuilder().AddJsonFile(AppSettingsJsonFile).Build();

            _service = new UserService(appConfig, HttpClient);
            moqService = new Mock<IUserService>();            
            _controller = new UsersController(moqService.Object);          
        }

        //[Fact]
        //public void GetUserExistsinGitUser()
        //{
        //    //arrange
        //    var expectedUser = GetSampleUser();
        //    moqService.Setup(x => x.GetUserFromGit("Sneha1")).Returns(GetSampleUser);

        //    //act
        //    var actualResult = _service.GetUserFromGit("Sneha1");

        //    //assert           
        //    Assert.True(expectedUser.Equals(actualResult));
        //}


        [Fact]
        public void GetUserListinService()
        {
            //arrange     ttttttttttttt       
            var userList = new List<UserRequest>();
            userList.Add(new UserRequest { gitName = "Sneha1" });
            userList.Add(new UserRequest { gitName = "P" });

            var expectedUserList = GetSampleUserList();
            moqService.Setup(x => x.GetUserListFromGit(userList)).Returns(expectedUserList);

            //act
            var actualResultList = _service.GetUserListFromGit(userList);

            //assert                      
            Assert.True(expectedUserList.Equals(actualResultList));

        }


        //[Fact]
        //public void GetUserListinController()
        //{
        //    //arrange            
        //    var userList = new List<UserRequest>();
        //    userList.Add(new UserRequest { gitName = "Sneha1" });
        //    userList.Add(new UserRequest { gitName = "P" });

        //    var expectedUserList = GetSampleUserList();
        //    moqService.Setup(x => x.GetUserListFromGit(userList)).Returns(expectedUserList);

        //    //act
        //    var actualResultList = _controller.RetrieveUsersList(userList);

        //    //assert                       
        //    Assert.True(expectedUserList.Equals(actualResultList));

        //}


        private Task<List<User>> GetSampleUserList()
        {

            Task<List<User>> task = Task<List<User>>.Factory.StartNew(() =>
            {
                List<User> userList = new List<User>()
            {
                new User() { id = 4414536,name = null, login="Sneha1", followers = 0, public_repos = 0,company=null },
                new User() { id = 125612,name = "Oleg Pudeyev", login="p", followers = 74, public_repos = 146, company=null},
            };
                return userList;
            });

            return task;

        }

        private Task<User> GetSampleUser()
        {

            Task<User> task = Task<User>.Factory.StartNew(() =>
            {
                User aUser = new User()
                {
                    id = 4414536,
                    name = null,
                    login = "Sneha1",
                    followers = 0,
                    public_repos = 0,
                    company = null
                };
                return aUser;
            });

            return task;
         }


     }
}
