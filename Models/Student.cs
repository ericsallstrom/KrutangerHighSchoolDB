using System;
using System.Collections.Generic;

namespace KrutangerHighSchoolDB.Models;

public partial class Student
{
    public int StudentId { get; set; }

    public string? FirstName { get; set; }

    public string? Surname { get; set; }

    public string? Ssn { get; set; }

    public byte? FkGenderId { get; set; }

    public string? PhoneNr { get; set; }

    public string? Email { get; set; }

    public int? FkClassId { get; set; }

    public DateTime? SchoolStart { get; set; }

    public virtual ICollection<CourseRegistration> CourseRegistrations { get; set; } = new List<CourseRegistration>();

    public virtual ClassList? FkClass { get; set; }

    public virtual Gender? FkGender { get; set; }
}
