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
    }

    public class UserService : IUserService
    {
        public UserService()
        {
                
        }
        public async Task<User> GetUserFromGit(string userName)
        {
            string gitAPIURL = "https://api.github.com/users/" + userName;

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



    }
}
