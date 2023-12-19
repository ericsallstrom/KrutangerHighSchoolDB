using System;
using System.Collections.Generic;

namespace KrutangerHighSchoolDB.Models;

public partial class GradeType
{
    public byte GradeId { get; set; }

    public string? Grade { get; set; }

    public virtual ICollection<Grade> Grades { get; set; } = new List<Grade>();
}
