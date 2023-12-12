namespace KrutangerHighSchoolDB
{
    internal class CourseRegistration
    {
        public int CourseRegId { get; set; }
        public int FKStudentId { get; set; }
        public int FKCourseId { get; set; }
    }
}
