using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace TasksManager
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
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            // Create the dailies
            string[] tasksNames =
            {
                "Dishwashing",
                "Cleaning",
                "CR",
                "Trash",
                "Kitchen",
                "Coffee Cleaning",
                "Rice Management",
                "Stock Management",
            };

            var appUsers = new List<AppUser>
            {
                    new AppUser
                    {
                        UserName = "elbert"
                    },
                    new AppUser
                    {
                        UserName = "russel"
                    },
                    new AppUser
                    {
                        UserName = "jhie"
                    },
                    new AppUser
                    {
                        UserName = "genesis"
                    },
                    new AppUser
                    {
                        UserName = "raph"
                    },
                    new AppUser
                    {
                        UserName = "hanz"
                    },
                    new AppUser
                    {
                        UserName = "jude"
                    },
                    new AppUser
                    {
                        UserName = "jade"
                    },
            };

            int currentMonth = DateTime.Now.Month;
            int currenYear = DateTime.Now.Year;
            int daysInMonth = DateTime.DaysInMonth(currenYear, currentMonth);

            var userIndex = 0;

            // Month of Sept
            for (int day = 1; day <= daysInMonth; day++)
            {
                userIndex--;
                for (int i = 0; i < tasksNames.Length; i++)
                {
                    if (userIndex < 0 || userIndex > appUsers.Count - 1) userIndex = 0;

                    var taskToCreate = new Task
                    {
                        Name = tasksNames[i],
                        Description = "Daily tasks.",
                        Date = new DateTime(currenYear, currentMonth, day, 8, 0, 0),
                        CreatedBy = appUsers[7],
                        DateCreated = DateTime.Now,
                        Assignee = appUsers[userIndex],
                        IsCompleted = false,
                    };

                    var jsonCreateTask = JsonConvert.SerializeObject(taskToCreate);
                    var createTaskRequestBody = new StringContent(jsonCreateTask, Encoding.UTF8, "application/json");
                    var createTaskResponse = await client.PostAsync(URL + "/Tasks", createTaskRequestBody);

                    if (!createTaskResponse.IsSuccessStatusCode)
                    {
                        Console.WriteLine("There is an error creating daily.");
                    }

                    userIndex++;
                }
            }
        }
    }

    public class Login
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class AppUser
    {
        public string UserName { get; set; }
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
        public AppUser CreatedBy { get; set; }
        public AppUser Assignee { get; set; }
        public DateTime Date { get; set; }
        public DateTime DateCreated { get; set; }
        public bool IsCompleted { get; set; }
    }
}
