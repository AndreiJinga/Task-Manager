namespace TaskManager.DTOs
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<TaskDTO> Tasks { get; set; } = new List<TaskDTO>();
    }
}
