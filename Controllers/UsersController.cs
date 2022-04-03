using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI_Project
{
    [ApiController]
    [Route("[controller]")]    
    public class UsersController : ControllerBase
    {
        private IUserService _userService;
        private IHttpClientFactory _httpClient;
       
        public UsersController( IUserService aService, IHttpClientFactory client)
        {
            _userService = aService;
            _httpClient = client;
        }


        [HttpGet, Route("Get")]
        public List<string> Get()
        {
            List<string> alist = new List<string>();

            alist.Add("s1");
            alist.Add("s2");

            return alist;

        }

        //[HttpPost, Route("RetrieveUser")]
        //public async Task<ActionResult<List<User>>> RetrieveUser([FromBody] List<UserRequest> userList)
        //{
        //    List<User> gitUserList = new List<User>();

        //    if (userList != null && userList.Count > 0)
        //    {
        //        foreach (var u in userList)
        //        {
        //            var aGitUser = await _userService.GetUserFromGit(u.gitName);

        //            if (aGitUser == null)
        //            {
        //                continue;
        //            }
        //            else
        //            {
        //                if (aGitUser.id > 0)
        //                {
        //                    gitUserList.Add(aGitUser);
        //                }
        //            }
        //        }
        //    }
        //    else
        //    {
        //        return BadRequest("Supplied user list is empty.");
        //    }

        //    return Ok(gitUserList); 
        //}



        [HttpPost, Route("RetrieveUsers")]
        public async Task<ActionResult<List<User>>> RetrieveUsersList([FromBody] List<UserRequest> userList)
        {
            List<User> gitUserList = new List<User>();

            if (userList != null && userList.Count > 0)
            {
                gitUserList = await _userService.GetUserListFromGit(userList);
            }
            else
            {
                return BadRequest("Supplied user list is empty.");
            }

            return Ok(gitUserList);
        }



    }
}
