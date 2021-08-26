using System;

namespace Application.Profiles
{
    public class AssignedTask
    {
        public string Username { get; set; }
        public DateTime Date { get; set; }
        public DateTime DateCreated { get; set; }
        public bool IsCompleted { get; set; }
    }
}