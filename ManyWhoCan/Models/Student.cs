namespace ManyWhoCan.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        public string? FullNameStudent { get; set; }
        public ICollection<Course>? Courses { get; set; } = new List<Course>();
    }
}
