using System.ComponentModel.DataAnnotations;

namespace ManyWhoCan.Models
{
    public class Course
    {
        public int CourseId { get; set; }
        public string Title { get; set; }
        public int? TeacherId { get; set; }
        public Teacher? Teacher { get; set; }
        public ICollection<Student>? Students { get; set; } = new List<Student>();
    }
}
