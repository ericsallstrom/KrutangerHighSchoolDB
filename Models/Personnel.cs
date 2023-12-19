using System;
using System.Collections.Generic;

namespace KrutangerHighSchoolDB.Models;

public partial class Personnel
{
    public int PersonnelId { get; set; }

    public string? FirstName { get; set; }

    public string? Surname { get; set; }

    public string? Ssn { get; set; }

    public byte? FkJobTitleId { get; set; }

    public byte? FkGenderId { get; set; }

    public string? PhoneNr { get; set; }

    public string? Email { get; set; }

    public virtual ICollection<CourseTeacher> CourseTeachers { get; set; } = new List<CourseTeacher>();

    public virtual Gender? FkGender { get; set; }

    public virtual JobTitle? FkJobTitle { get; set; }
}
