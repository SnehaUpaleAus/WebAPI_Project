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
       // Task<User> GetUserFromGit(string userName);
        Task<List<User>> GetUserListFromGit(List<UserRequest> userList);
    }

    public class UserService : IUserService
    {
        private IConfiguration _config;

        private readonly IHttpClientFactory _httpClient;
        public UserService(IConfiguration aConfig, IHttpClientFactory aclient)
        {
            _config = aConfig;
            _httpClient = aclient;
        }
        //public async Task<User> GetUserFromGit(string userName)
        //{
        //    string gitAPIURL = _config.GetValue(typeof(string), "GITAPIUrl") + userName;

        //    _httpClient.DefaultRequestHeaders.Add("User-Agent", "my-user-agent-name");
        //    var request = new HttpRequestMessage(HttpMethod.Get, gitAPIURL);
        //    var authHeasderValue = new AuthenticationHeaderValue("Basic");
        //    request.Headers.Authorization = authHeasderValue;

        //    var response = await _httpClient.GetAsync(gitAPIURL);

        //    var apiResponse = await response.Content.ReadAsStringAsync();
        //    return JsonConvert.DeserializeObject<User>(apiResponse);

        //}



        public async Task<List<User>> GetUserListFromGit(List<UserRequest> userList)
        {
            try
            {
                List<User> gitUserList = new List<User>();

                foreach (var u in userList)
                {
                    string gitAPIURL = _config.GetValue(typeof(string), "GITAPIUrl").ToString() + u.gitName;
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
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
