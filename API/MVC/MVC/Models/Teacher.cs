namespace MVC.Models
{
    public class Teacher
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Subject Subject { get; set; }
        public int TeacherSubjectId { get; set; }
    }
}
