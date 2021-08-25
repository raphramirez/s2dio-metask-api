using System;

namespace Domain
{
    public class UserTask
    {
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public Guid TaskId { get; set; }
        public Task Task { get; set; }
        public DateTime Date { get; set; }
        public DateTime DateCreated { get; set; }
        public bool IsCompleted { get; set; }
    }
}