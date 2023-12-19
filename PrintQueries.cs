using KrutangerHighSchoolDB.Data;
using KrutangerHighSchoolDB.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Net.NetworkInformation;
using System.Security.Claims;

namespace KrutangerHighSchoolDB
{
    internal class PrintQueries
    {
        private UserInputHandler InputHandler { get; }
        private KrutångerHighSchoolContext Context { get; set; }
        private byte GenderChoice { get; set; }
        private int ClassChoice { get; set; }
        private byte ProfessionChoice { get; set; }
        private int MenuChoice { get; set; }

        public PrintQueries(KrutångerHighSchoolContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
            InputHandler = new(context);
        }

        private void GetMenu(string prompt, string[] menuOptions)
        {
            Menu menu = new()
            {
                Prompt = prompt,
                MenuOptions = menuOptions
            };

            MenuChoice = menu.GetMenuChoice();
        }

        private void ConfirmPersonnelEntries(Personnel newPersonnel)
        {
            var selectedGender = Context.Genders.SingleOrDefault(g => g.GenderId == newPersonnel.FkGenderId);

            var selectedJobTitle = Context.JobTitles.SingleOrDefault(j => j.JobTitleId == newPersonnel.FkJobTitleId);

            string prompt = $"Are you satisfied with the entered values:\n" +
                $"\nFirst Name: {newPersonnel.FirstName}" +
                $"\nSurname: {newPersonnel.Surname}" +
                $"\nSSN: {newPersonnel.Ssn}" +
                $"\nGender: {selectedGender.Gender1}" +
                $"\nJob Title: {selectedJobTitle.JobTitle1}\n";

            string[] menuOptions = { "Yes", "No" };

            GetMenu(prompt, menuOptions);
        }

        private void ConfirmStudentEntries(Student newStudent)
        {
            var selectedGender = Context.Genders.SingleOrDefault(g => g.GenderId == newStudent.FkGenderId);

            var selectedClass = Context.ClassLists.SingleOrDefault(c => c.ClassId == newStudent.FkClassId);

            string prompt = $"Are you satisfied with the entered values:\n" +
                $"\nFirst Name: {newStudent.FirstName}" +
                $"\nSurname: {newStudent.Surname}" +
                $"\nSSN: {newStudent.Ssn}" +
                $"\nGender: {selectedGender.Gender1}" +
                $"\nClass: {selectedClass.ClassName}\n";

            string[] menuOptions = { "Yes", "No" };

            GetMenu(prompt, menuOptions);
        }

        private void ChooseGender()
        {
            string prompt = "Select the gender of the student:";

            string[] menuOptions = { "Male", "Female", "Non Binary" };

            GetMenu(prompt, menuOptions);
        }

        private void ChooseClass()
        {
            string prompt = "Select the class for student enrollment:";

            string[] menuOptions =
                {
                    "Class 7A", "Class 7B", "Class 8A",
                    "Class 8B", "Class 9A", "Class 9B"
                };

            GetMenu(prompt, menuOptions);
        }

        private void ChooseProfession()
        {
            string prompt = "Choose profession for the new staff member:";

            string[] menuOptions =
            {
                "Principal", "Administrator",
                "Teacher", "Janitor", "School Nurse",
                "Special Need Teacher", "Chef"
            };

            GetMenu(prompt, menuOptions);
        }

        public void AddPersonnel()
        {
            string firstName = InputHandler.GetNonEmptyName("Enter a first name: ");

            string validatedFirstName = InputHandler.CapitalizeFirstLetter(firstName);

            string surname = InputHandler.GetNonEmptyName($"First Name: {validatedFirstName}\n" +
              $"\nEnter a surname: ");

            string validatedSurname = InputHandler.CapitalizeFirstLetter(surname);

            string validatedSsn = InputHandler.ValidateSSN($"First Name: {validatedFirstName}" +
                $"\nSurname: {validatedSurname}\n" +
                $"\nEnter SSN (YYYYMMDDXXXX): ");

            ChooseGender();

            GenderChoice = (byte)++MenuChoice;

            ChooseProfession();

            ProfessionChoice = (byte)++MenuChoice;

            var newPersonnel = new Personnel
            {
                FirstName = validatedFirstName,
                Surname = validatedSurname,
                Ssn = validatedSsn,
                FkGenderId = GenderChoice,
                FkJobTitleId = ProfessionChoice
            };

            ConfirmPersonnelEntries(newPersonnel);

            if (MenuChoice == 0)
            {
                Context.Add(newPersonnel);
                Context.SaveChanges();

                Console.Clear();
                Console.WriteLine("\nThe new staff member has been successfully registered.");
            }
            else
            {
                Console.Clear();
                Console.WriteLine("\nThe process of register a new staff member has been canceled.");
            }

            PressAnyKeyMessage();
        }

        public void AddStudent(string prompt)
        {
            string firstName = InputHandler.GetNonEmptyName("Enter the student's first name: ");

            string validatedFirstName = InputHandler.CapitalizeFirstLetter(firstName);

            string surname = InputHandler.GetNonEmptyName($"First Name: {validatedFirstName}\n" +
                $"\nEnter the student's surname: ");

            string validatedSurname = InputHandler.CapitalizeFirstLetter(surname);

            string validatedSsn = InputHandler.ValidateSSN($"First Name: {validatedFirstName}" +
                $"\nSurname: {validatedSurname}\n" +
                $"\nEnter the student's SSN (YYYYMMDDXXXX): ");

            ChooseGender();

            GenderChoice = (byte)++MenuChoice;

            ChooseClass();

            ClassChoice = ++MenuChoice;

            var newStudent = new Student
            {
                FirstName = validatedFirstName,
                Surname = validatedSurname,
                Ssn = validatedSsn,
                FkGenderId = GenderChoice,
                FkClassId = ClassChoice,
                SchoolStart = DateTime.Now
            };

            ConfirmStudentEntries(newStudent);

            if (MenuChoice == 0)
            {
                Context.Add(newStudent);
                Context.SaveChanges();

                Console.Clear();
                Console.WriteLine("\nThe new student has been successfully enrolled.");
            }
            else
            {
                Console.Clear();
                Console.WriteLine("\nThe process of enrolling a new student has been canceled.");
            }

            PressAnyKeyMessage();
        }

        public void PrintAllCoursesWithAvgGrade()
        {
            var courseWithAverageGrade = Context.CoursesWithAverageGradeViews
                .OrderBy(c => c.Course).ToList();

            Console.WriteLine("Course\t\t\tAverage Grade\tLowest Grade\tHighest Grade");
            Console.WriteLine($"{new string('-', 69)}");

            foreach (var c in courseWithAverageGrade)
            {
                string formattedCourse = c.Course?.Length > 16 ? $"{c.Course}\t"
                 : c.Course?.Length <= 7 ? $"{c.Course}\t\t\t" : $"{c.Course}\t\t";

                Console.Write($"{formattedCourse}{c.AverageGrade}\t\t{c.LowestGrade}\t\t{c.HighestGrade}");

                Console.WriteLine();
            }

            PressAnyKeyMessage();
        }

        public void PrintGradesLatestMonth()
        {
            var latestGradedStudents = Context.LatestMonthGradesViews
                .OrderByDescending(s => s.GradedDate)
                .ThenBy(s => s.ClassName)
                .ThenBy(s => s.Surname).ToList();

            if (latestGradedStudents == null)
            {
                Console.WriteLine("No grades have been graded in the latest month.");
            }
            else
            {
                Console.WriteLine("First Name\tLast Name\tClass\tCourse\t\t\tGrade\tDate Graded");
                Console.WriteLine($"{new string('-', 83)}");

                foreach (var s in latestGradedStudents)
                {
                    string formattedFirstName = s.FirstName?.Length > 7 ? $"{s.FirstName}\t" : $"{s.FirstName}\t\t";
                    string formattedSurname = s.Surname?.Length > 7 ? $"{s.Surname}\t" : $"{s.Surname}\t\t";
                    string formattedCourse = s.Course?.Length > 16 ? $"{s.Course}\t"
                        : s.Course?.Length <= 7 ? $"{s.Course}\t\t\t" : $"{s.Course}\t\t";

                    Console.Write($"{formattedFirstName}{formattedSurname}{s.ClassName}\t{formattedCourse}{s.Grade}\t{s.GradedDate:dd-MM-yyyy}");

                    Console.WriteLine();
                }
            }

            PressAnyKeyMessage();
        }

        public void PrintStudentsFromSpecificClass(int sortByChoice, int orderByChoice, int classChoice)
        {
            IOrderedQueryable<Student> orderedStudents = sortByChoice switch
            {
                0 => orderByChoice == 0 ? Context.Students.OrderBy(student => student.FirstName) : Context.Students.OrderByDescending(student => student.FirstName),
                1 => orderByChoice == 0 ? Context.Students.OrderBy(student => student.Surname) : Context.Students.OrderByDescending(student => student.Surname),
                _ => throw new ArgumentException("Invalid sortByChoice")
            };

            var studentsFromSpecificClass = (from s in orderedStudents
                                             join cl in Context.ClassLists
                                                on s.FkClassId equals cl.ClassId
                                             where s.FkClassId == classChoice
                                             select new
                                             {
                                                 s.FirstName,
                                                 s.Surname,
                                                 s.Ssn,
                                                 cl.ClassName,
                                                 cl.Branch
                                             }).ToList();

            Console.Clear();
            Console.WriteLine("First Name\tLast Name\tSSN\t\tClass\tBranch");
            Console.WriteLine($"{new string('-', 62)}");

            foreach (var s in studentsFromSpecificClass)
            {
                string formattedFirstName = s.FirstName.Length > 7 ? $"{s.FirstName}\t" : $"{s.FirstName}\t\t";
                string formattedSurname = s.Surname.Length > 7 ? $"{s.Surname}\t" : $"{s.Surname}\t\t";

                Console.Write($"{formattedFirstName}{formattedSurname}{s.Ssn}\t{s.ClassName}\t{s.Branch}");

                Console.WriteLine();
            }

            PressAnyKeyMessage();
        }

        public void PrintAllStudentsWithOptions(int sortByChoice, int orderByChoice)
        {
            IOrderedQueryable<Student> orderedStudents = sortByChoice switch
            {
                0 => orderByChoice == 0 ? Context.Students.OrderBy(student => student.FirstName) : Context.Students.OrderByDescending(student => student.FirstName),
                1 => orderByChoice == 0 ? Context.Students.OrderBy(student => student.Surname) : Context.Students.OrderByDescending(student => student.Surname),
                _ => throw new ArgumentException("Invalid sortByChoice")
            };

            var students = (from s in orderedStudents
                            join cl in Context.ClassLists
                                on s.FkClassId equals cl.ClassId
                            select new
                            {
                                s.FirstName,
                                s.Surname,
                                s.Ssn,
                                cl.ClassName,
                                cl.Branch,
                                s.SchoolStart
                            }).ToList();

            Console.Clear();
            Console.WriteLine("First Name\tLast Name\tSSN\t\tClass\tBranch\t\t\tDate of Enrollment");
            Console.WriteLine($"{new string('-', 98)}");

            foreach (var s in students)
            {
                string formattedFirstName = s.FirstName.Length > 7 ? $"{s.FirstName}\t" : $"{s.FirstName}\t\t";
                string formattedSurname = s.Surname.Length > 7 ? $"{s.Surname}\t" : $"{s.Surname}\t\t";

                Console.Write($"{formattedFirstName}{formattedSurname}{s.Ssn}\t{s.ClassName}\t{s.Branch}\t\t{s.SchoolStart:dd-MM-yyyy}");

                Console.WriteLine();
            }

            PressAnyKeyMessage();
        }

        public void PrintPersonnelWithSpecificJobTitles(int menuChoice)
        {
            var personnel = (from p in Context.Personnel
                             join jt in Context.JobTitles
                                 on p.FkJobTitleId equals jt.JobTitleId
                             where p.FkJobTitleId == menuChoice
                             orderby p.Surname
                             select new
                             {
                                 p.FirstName,
                                 p.Surname,
                                 JobTitle = jt.JobTitle1
                             }).ToList();

            Console.Clear();
            Console.WriteLine("First Name\tLast Name\tJob Title");
            Console.WriteLine($"{new string('-', 41)}");

            foreach (var p in personnel)
            {
                string formattedFirstName = p.FirstName.Length > 7 ? $"{p.FirstName}\t" : $"{p.FirstName}\t\t";
                string formattedSurname = p.Surname.Length > 7 ? $"{p.Surname}\t" : $"{p.Surname}\t\t";

                Console.Write($"{formattedFirstName}{formattedSurname}{p.JobTitle}");

                Console.WriteLine();
            }

            PressAnyKeyMessage();
        }

        public void PrintAllPersonnel()
        {
            var personnel = (from p in Context.Personnel
                             join jt in Context.JobTitles
                                  on p.FkJobTitleId equals jt.JobTitleId
                             orderby p.PersonnelId
                             select new
                             {
                                 p.PersonnelId,
                                 p.FirstName,
                                 p.Surname,
                                 JobTitle = jt.JobTitle1
                             }).ToList();

            Console.Clear();
            Console.WriteLine("ID\tFirst Name\tLast Name\tJob Title");
            Console.WriteLine($"{new string('-', 50)}");


            foreach (var p in personnel)
            {
                string formattedFirstName = p.FirstName.Length > 7 ? $"{p.FirstName}\t" : $"{p.FirstName}\t\t";
                string formattedSurname = p.Surname.Length > 7 ? $"{p.Surname}\t" : $"{p.Surname}\t\t";

                Console.Write($"{p.PersonnelId}\t{formattedFirstName}{formattedSurname}{p.JobTitle}");

                Console.WriteLine();
            }

            PressAnyKeyMessage();
        }

        private static void PressAnyKeyMessage()
        {
            Console.WriteLine("\nPress any key to go back.");
            Console.ReadKey();
        }
    }
}
