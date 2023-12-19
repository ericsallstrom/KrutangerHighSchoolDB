using System;
using System.Collections.Generic;

namespace KrutangerHighSchoolDB.Models;

public partial class Gender
{
    public byte GenderId { get; set; }

    public string? Gender1 { get; set; }

    public virtual ICollection<Personnel> Personnel { get; set; } = new List<Personnel>();

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
