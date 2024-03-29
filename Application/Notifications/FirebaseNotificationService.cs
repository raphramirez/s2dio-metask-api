﻿using FirebaseAdmin;
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
            var defaultApplication = FirebaseApp.Create(new AppOptions()
            {
                Credential = GoogleCredential.FromFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "key.json"))
            });
        }

        public static async Task<BatchResponse> CreateNotificationAsync(List<string> registrationTokens, string title, string body)
        {
            var message = new MulticastMessage
            {
                Notification = new Notification
                {
                    Title = title,
                    Body = body
                },
                Tokens = registrationTokens,
            };

            return await FirebaseMessaging.DefaultInstance.SendMulticastAsync(message);
        }
    }
}
