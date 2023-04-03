using API.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace API.DAL
{
    public class TeacherRepository
    {
        private MyDbContext dbContext;

        public TeacherRepository(MyDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void DeleteTeacher(int teacherId)
        {
            var teacher = dbContext.Teachers.Find(teacherId);
            dbContext.Teachers.Remove(teacher);
            Save();
        }

        public Teacher GetTeacherById(int teacherId)
        {
            var teacher = dbContext.Teachers.Find(teacherId);
            dbContext.Entry(teacher).Reference(s => s.Subject).Load();
            return teacher;
        }

        public IEnumerable<Teacher> GetTeachers()
        {
            return dbContext.Teachers.Include(s => s.Subject).ToList();
        }

        public void InsertTeacher(Teacher teacher)
        {
            teacher.Subject = dbContext.Subjects.Find(teacher.Subject.Id);
            dbContext.Teachers.Add(teacher);
            Save();
        }

        public void UpdateTeacher(Teacher teacher)
        {
            teacher.Subject = dbContext.Subjects.Find(teacher.Subject.Id);
            dbContext.Entry(teacher).State = EntityState.Modified;
            Save();
        }

        private void Save()
        {
            dbContext.SaveChanges();
        }
    }
}
