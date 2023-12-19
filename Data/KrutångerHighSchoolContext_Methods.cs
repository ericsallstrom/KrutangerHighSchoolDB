using KrutangerHighSchoolDB.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KrutangerHighSchoolDB.Data
{
    internal class KrutångerHighSchoolContext_Methods : DbContext
    {
        private KrutångerHighSchoolContext Context { get; set; }

        public KrutångerHighSchoolContext_Methods()
        {
            Context = new();
        }

        //using (var dbContext = new KrutångerHighSchoolContext())
        //{                       
        //    foreach (var student in dbContext.SeeStudents)
        //    {
        //        Console.WriteLine($"ID:     {student.StudentId}");
        //        Console.WriteLine($"Name:   {student.FirstName} {student.Surname}");
        //        Console.WriteLine($"SSN:    {student.Ssn}");
        //        Console.WriteLine(new string ('-', 18));                    
        //    }
        //}

        public List<Personnel> GetAllPersonnelWithJobTitles()
        {
            return Context.Personnel
                        .Join(Context.JobTitles,
                        p => p.FkJobTitleId,
                        jt => jt.JobTitleId,
                        (p, jt) => new Personnel
                        {
                            PersonnelId = p.PersonnelId,
                            FirstName = p.FirstName,
                            Surname = p.Surname,
                            FkJobTitleId = jt.JobTitleId
                        })
                        .ToList();
            //return Context.SeePersonnel.ToList();
        }
    }
}
