using System.IO;
using System;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;

namespace NotificationSchedule
{
    public class Program
    {
        static void Main(string[] args)
        {
            var defaultApp = FirebaseApp.Create(new AppOptions()
            {
                Credential = GoogleCredential.FromFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "key.json"))
            });

            Console.WriteLine(defaultApp.Name + " initialized");
        }
    }
}
