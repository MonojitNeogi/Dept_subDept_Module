namespace Dept_subDept_Module.Models
{
    public class Reminder
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime ReminderDateTime { get; set; }
        public bool IsSent { get; set; }
        public string RecipientEmail { get; set; }
    }
}
