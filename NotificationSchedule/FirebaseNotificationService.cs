using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Notifications
{
    public class FirebaseNotificationService
    {
        public FirebaseNotificationService()
        {
            var defaultApp = FirebaseApp.Create(new AppOptions()
            {
                Credential = GoogleCredential.FromFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "key.json"))
            });

            Console.WriteLine(defaultApp.Name + " initialized");
        }

        public async void CreateNotificationAsync(string registrationToken, string title, string body)
        {
            var message = new Message
            {
                Notification = new Notification
                {
                    Title = title,
                    Body = body
                },
                Token = registrationToken,
            };

            var response = await FirebaseMessaging.DefaultInstance.SendAsync(message);

            Console.WriteLine(response);
        }
    }
}
