using System;
using System.Collections.Generic;

namespace KrutangerHighSchoolDB.Models;

public partial class ClassList
{
    public int ClassId { get; set; }

    public string? ClassName { get; set; }

    public string? Branch { get; set; }

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
