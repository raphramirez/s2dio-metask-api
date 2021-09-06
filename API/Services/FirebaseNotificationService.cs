using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace API.Services
{
    public class FirebaseNotificationService
    {

        public FirebaseNotificationService()
        {
            var defaultApp = FirebaseApp.Create(new AppOptions()
            {
                Credential = GoogleCredential.FromFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "key.json"))
            });

            Console.WriteLine(defaultApp.Name);
            Console.WriteLine("hello world!");
        }

        public async void CreateNotificationAsync(string registrationToken)
        {
            var message = new Message
            {
                Notification = new Notification
                {
                    Title = "Hello World Yes",
                    Body = "This is a notification body."
                },
                Token = registrationToken,
            };

            var response = await FirebaseMessaging.DefaultInstance.SendAsync(message);

            Console.WriteLine(response);
        }
    }
}
