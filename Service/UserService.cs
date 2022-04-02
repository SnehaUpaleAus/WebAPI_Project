using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace WebAPI_Project
{
    public interface IUserService
    {
        Task<User> GetUserFromGit(string userName);
        Task<List<User>> GetUserListFromGit(List<UserRequest> userList);
    }

    public class UserService : IUserService
    {
        private IConfiguration _config;
        public UserService(IConfiguration aConfig)
        {
            _config = aConfig;
        }
        public async Task<User> GetUserFromGit(string userName)
        {

            string gitAPIURL = _config.GetValue(typeof(string), "GITAPIUrl") + userName;

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("User-Agent", "my-user-agent-name");
                var request = new HttpRequestMessage(HttpMethod.Get, gitAPIURL);
                var authHeasderValue = new AuthenticationHeaderValue("Basic");
                request.Headers.Authorization = authHeasderValue;

                using (var response = await httpClient.GetAsync(gitAPIURL))
                { 
                    var apiResponse = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<User>(apiResponse);
                }
            }
        }


        public async Task<List<User>> GetUserListFromGit(List<UserRequest> userList)
        {
            List<User> gitUserList = new List<User>();
            string gitAPIURL = _config.GetValue(typeof(string), "GITAPIUrl").ToString();

            foreach (var u in userList)
            {
                gitAPIURL = _config.GetValue(typeof(string), "GITAPIUrl").ToString();

                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Add("User-Agent", "my-user-agent-name");
                    var request = new HttpRequestMessage(HttpMethod.Get, gitAPIURL);
                    var authHeasderValue = new AuthenticationHeaderValue("Basic");
                    request.Headers.Authorization = authHeasderValue;

                    using (var response = await httpClient.GetAsync(gitAPIURL))
                    {
                        var apiResponse = await response.Content.ReadAsStringAsync();
                        User resUser = JsonConvert.DeserializeObject<User>(apiResponse);

                        if (resUser == null)
                        {
                            continue;
                        }
                        else
                        {
                            if (resUser.id > 0)
                            {
                                gitUserList.Add(resUser);
                            }
                        }
                    }
                }
            }

            return gitUserList;
        }


    }
}
