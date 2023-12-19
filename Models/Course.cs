using System;
using System.Collections.Generic;

namespace KrutangerHighSchoolDB.Models;

public partial class Course
{
    public int CourseId { get; set; }

    public string? Course1 { get; set; }

    public virtual ICollection<CourseRegistration> CourseRegistrations { get; set; } = new List<CourseRegistration>();

    public virtual ICollection<CourseTeacher> CourseTeachers { get; set; } = new List<CourseTeacher>();
}
