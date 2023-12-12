namespace KrutangerHighSchoolDB
{
    internal abstract class Person
    {
        public string? FirstName { get; set; }
        public string? Surname { get; set; }
        public string? SSN { get; set; }
        public int FKGenderId { get; set; }
        public string? PhoneNr { get; set; }
        public string? Email { get; set; }
    }
}
