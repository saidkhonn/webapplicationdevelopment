using Microsoft.AspNetCore.Mvc.Rendering;

namespace MVC.Models
{
    public class TeacherSubjectViewModel
    {
        public Teacher Teacher { get; set; }
        public SelectList Subjects { get; set; }
    }
}
