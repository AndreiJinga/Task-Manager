using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManager.Models
{
    public class UserTask
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public TaskStatus Status { get; set; }
        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}
