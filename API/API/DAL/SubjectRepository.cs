using API.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace API.DAL
{
    public class SubjectRepository
    {
        private MyDbContext dbContext;

        public SubjectRepository(MyDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void DeleteSubject(int subjectId)
        {
            var subject = dbContext.Subjects.Find(subjectId);
            dbContext.Subjects.Remove(subject);
            Save();
        }

        public Subject GetSubjectById(int subjectId)
        {
            return dbContext.Subjects.Find(subjectId);
        }

        public IEnumerable<Subject> GetGategories()
        {
            return dbContext.Subjects.ToList();
        }

        public void InsertSubject(Subject subject)
        {
            dbContext.Subjects.Add(subject);
            Save();
        }

        public void UpdateSubject(Subject subject)
        {
            dbContext.Entry(subject).State = EntityState.Modified;
            Save();
        }

        private void Save()
        {
            dbContext.SaveChanges();
        }
    }
}
