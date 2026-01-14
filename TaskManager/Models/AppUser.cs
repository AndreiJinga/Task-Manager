using System.Collections.Generic;

namespace TaskManager.Models
{
    public class AppUser
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<UserTask> Tasks { get; set; } = new List<UserTask>();
    }
}
