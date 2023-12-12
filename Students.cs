namespace KrutangerHighSchoolDB
{
    internal class Students : Person
    {
        public int StudentId { get; set; }
        public int FKClassId { get; set; }
        public DateTime SchoolStart { get; set; }
    }
}
