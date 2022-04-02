using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI_Project.Controllers
{
    [ApiController]
    [Route("[controller]")]    
    public class UsersController : ControllerBase
    {
        private IUserService _userService;
        private readonly ILogger<UsersController> _logger;

        public UsersController( IUserService aService, ILogger<UsersController> logger)
        {
            _userService = aService;
            _logger = logger;

        }


        [HttpGet, Route("Get")]
        public List<string> Get()
        {
            List<string> alist = new List<string>();

            alist.Add("Smeja1");
            alist.Add("Smeja2");

            return alist;

        }

        [HttpPost, Route("RetrieveUsers")]
        public async Task<ActionResult<List<User>>> RetrieveUsers([FromBody] List<UserRequest> userList)
        {
            List<User> gitUserList = new List<User>();

            if (userList != null && userList.Count > 0)
            {
                foreach (var u in userList)
                {
                    var aGitUser = await _userService.GetUserFromGit(u.gitName);

                    if (aGitUser == null)
                    {
                        continue;
                    }
                    else
                    {
                        if (aGitUser.id > 0)
                        {
                            gitUserList.Add(aGitUser);
                        }
                    }
                }
            }
            else
            {
                return BadRequest("Supplied user list is empty.");
            }

            return gitUserList;
        }

    }
}
