namespace KrutangerHighSchoolDB
{
    internal class Grades
    {
        public int GradeId { get; set; }
        public char Grade { get; set; }
        public DateTime GradedDate { get; set; }
        public int FKCourseTeacherId { get; set; }
    }
}
