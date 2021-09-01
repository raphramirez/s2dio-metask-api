using System;

namespace Application.Tasks
{
    public class TaskParams
    {
        public bool ShowAll { get; set; }
        public bool MyTasks { get; set; }
        public DateTime Date { get; set; } = DateTime.Today;
    }
}
