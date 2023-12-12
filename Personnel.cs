namespace KrutangerHighSchoolDB
{
    internal class Personnel : Person
    {
        public int PersonnelId { get; set; }
        public int FKJobTitleId { get; set; }
    }
}
