using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUDWithIssuesCore.Models
{
    public static class StudentDb
    {
        public static async Task<Student> Add(Student p, SchoolContext db)
        {
            //Add student to context
            db.Students.Add(p);
            await db.SaveChangesAsync();
            return p;
        }

        public static async Task<List<Student>> GetStudentsAsync(SchoolContext context)
        {
            return await (from s in context.Students
                    select s).ToListAsync();
        }

        public static async Task<Student> GetStudentAsync(SchoolContext context, int id)
        {
            Student p2 = await (context
                            .Students
                            .Where(s => s.StudentId == id))
                            .SingleOrDefaultAsync();
            return p2;
        }

        public static async void Delete(SchoolContext context, Student p)
        {
            // Should delete
            context.Entry(p).State = EntityState.Deleted;
            await context.SaveChangesAsync();
        }

        public static async Task<Student> Update(SchoolContext context, Student p)
        {
            // should update not delete....
            //Mark the object as deleted
            //context.Students.Remove(p);
            context.Entry(p).State = EntityState.Modified;

            //Send delete query to database
            await context.SaveChangesAsync();

            return p;
        }
    }
}
