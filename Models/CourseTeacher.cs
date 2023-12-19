using System;
using System.Collections.Generic;

namespace KrutangerHighSchoolDB.Models;

public partial class CourseTeacher
{
    public int CourseTeacherId { get; set; }

    public int? FkPersonnelId { get; set; }

    public int? FkCourseId { get; set; }

    public virtual Course? FkCourse { get; set; }

    public virtual Personnel? FkPersonnel { get; set; }

    public virtual ICollection<Grade> Grades { get; set; } = new List<Grade>();
}
