namespace ManyWhoCan.Models
{
    public class Teacher
    {
        public int TeacherId { get; set; }
        public string? FullNameTeacher { get; set; }
        public ICollection<Course>? Courses { get; set; } = new List<Course>();
    }
}
