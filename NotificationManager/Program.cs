using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace NotificationManager
{
    class Program
    {
        private const string URL = "https://localhost:5001/api";

        static async System.Threading.Tasks.Task Main(string[] args)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(URL);

            // Login
            var jsonLogin = JsonConvert.SerializeObject(new Login { Username = "raph", Password = "Pa$$w0rd" });
            var data = new StringContent(jsonLogin, Encoding.UTF8, "application/json");

            var loginResponse = await client.PostAsync(URL + "/account/login", data);

            string result = loginResponse.Content.ReadAsStringAsync().Result;
            var loggedInUser = JsonConvert.DeserializeObject<User>(result);
            var token = loggedInUser.Token;

            // Get Tasks for today
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var tasksResponse = await client.GetAsync(URL + "/tasks?showall=true");
            string taskResult = tasksResponse.Content.ReadAsStringAsync().Result;

            var tasks = JsonConvert.DeserializeObject<List<Task>>(taskResult);

            foreach (Task task in tasks)
            {
                Console.WriteLine(task.Name);
            }

            // Get Tokens

            // Send Notifications

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
}
