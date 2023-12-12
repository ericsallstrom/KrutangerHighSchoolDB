namespace KrutangerHighSchoolDB
{
    internal class CourseTeacher
    {
        public int CourseTeacherId { get; set; }
        public int FKPersonnelId { get; set; }
        public int FKCourseId { get; set; }
    }
}
