using System;
using System.Collections.Generic;

namespace KrutangerHighSchoolDB.Models;

public partial class Grade
{
    public int GradedId { get; set; }

    public int? FkCourseRegId { get; set; }

    public byte? FkGradeId { get; set; }

    public DateTime? GradedDate { get; set; }

    public int? FkCourseTeacherId { get; set; }

    public virtual CourseRegistration? FkCourseReg { get; set; }

    public virtual CourseTeacher? FkCourseTeacher { get; set; }

    public virtual GradeType? FkGrade { get; set; }
}
