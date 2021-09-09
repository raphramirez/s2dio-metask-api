using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace NotificationManager
{
    class Program
    {
        private const string URL = "https://localhost:5001/api";

        private static FirebaseNotificationService firebaseNotification;

        static async System.Threading.Tasks.Task Main(string[] args)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(URL);

            firebaseNotification = new FirebaseNotificationService();

            // Login
            var jsonLogin = JsonConvert.SerializeObject(new Login { Username = "raph", Password = "Pa$$w0rd" });
            var data = new StringContent(jsonLogin, Encoding.UTF8, "application/json");
            var loginResponse = await client.PostAsync(URL + "/account/login", data);
            string result = loginResponse.Content.ReadAsStringAsync().Result;
            var loggedInUser = JsonConvert.DeserializeObject<User>(result);
            var token = loggedInUser.Token;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            // Get Tasks for today
            var tasksResponse = await client.GetAsync(URL + "/tasks?showall=true");
            string taskResult = tasksResponse.Content.ReadAsStringAsync().Result;

            var tasks = JsonConvert.DeserializeObject<List<Task>>(taskResult);

            var groupedTasksByUser = tasks
                .GroupBy(u => u.Assignee.Username)
                .Select(group => group.ToList())
                .ToList();

            foreach (var list in groupedTasksByUser)
            {
                var taskTitles = new StringBuilder();
                string currentUsername = null;
                foreach (var task in list)
                {
                    taskTitles.AppendLine($"- {task.Name}");
                    currentUsername = task.Assignee.Username;
                }

                var userTokens = (await GetUserTokens(client, currentUsername))
                    ?.Select(t => t.Token).ToList();

                if (userTokens.Count > 0)
                {
                    await FirebaseNotificationService.CreateNotificationAsync(
                        userTokens,
                        "Today's task",
                        taskTitles.ToString()
                    );
                }
            }
        }

        public static async Task<List<NotificationToken>> GetUserTokens(HttpClient client, string username)
        {
            // Get Tokens
            var jsonUserInfo = JsonConvert.SerializeObject(new UsernameDto { Username = username });
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"{URL}/account/tokens"),
                Content = new StringContent(jsonUserInfo, Encoding.UTF8, "application/json"),
            };
            var tokensResponse = await client.SendAsync(request).ConfigureAwait(false);
            tokensResponse.EnsureSuccessStatusCode();
            var responseBody = await tokensResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
            var tokens = JsonConvert.DeserializeObject<List<NotificationToken>>(responseBody);

            return tokens;
        }
    }

    public class Login
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class User
    {
        public string Username { get; set; }
        public string Token { get; set; }
        public string Image { get; set; }
    }

    public class Task
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Application.Profiles.Profile CreatedBy { get; set; }
        public Application.Profiles.Profile Assignee { get; set; }
        public DateTime Date { get; set; }
        public DateTime DateCreated { get; set; }
        public bool IsCompleted { get; set; }
    }

    public class UsernameDto
    {
        public string Username { get; set; }
    }

    public class NotificationToken
    {
        public string Username { get; set; }
        public string Token { get; set; }
    }
}
