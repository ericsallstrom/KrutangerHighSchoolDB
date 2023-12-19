using KrutangerHighSchoolDB.Data;
using KrutangerHighSchoolDB.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Net.NetworkInformation;
using System.Security.Claims;

namespace KrutangerHighSchoolDB.Navigation
{
    internal class PrintQueries
    {
        // Properties for handling user input, choices and database context.
        private UserInputHandler InputHandler { get; }
        private KrutångerHighSchoolContext Context { get; set; }
        private byte GenderChoice { get; set; }
        private int ClassChoice { get; set; }
        private byte ProfessionChoice { get; set; }
        private int MenuChoice { get; set; }

        // Constructor initializes the class with a database context.
        public PrintQueries(KrutångerHighSchoolContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
            InputHandler = new(context);
        }

        // Method to display a menu and get the user's choice.
        private void GetMenu(string prompt, string[] menuOptions)
        {
            Menu menu = new()
            {
                Prompt = prompt,
                MenuOptions = menuOptions
            };

            MenuChoice = menu.GetMenuChoice();
        }

        // Method to confirm personnel entries with the user.
        private void ConfirmPersonnelEntries(Personnel newPersonnel)
        {
            // Retrieve associated gender and job title from the database.
            var selectedGender = Context.Genders.SingleOrDefault(g => g.GenderId == newPersonnel.FkGenderId);
            var selectedJobTitle = Context.JobTitles.SingleOrDefault(j => j.JobTitleId == newPersonnel.FkJobTitleId);

            // Display the entered values and prompt for confirmation.
            string prompt = $"Are you satisfied with the entered values:\n" +
                $"\nFirst Name: {newPersonnel.FirstName}" +
                $"\nSurname: {newPersonnel.Surname}" +
                $"\nSSN: {newPersonnel.Ssn}" +
                $"\nGender: {selectedGender.Gender1}" +
                $"\nJob Title: {selectedJobTitle.JobTitle1}\n";

            string[] menuOptions = { "Yes", "No" };

            GetMenu(prompt, menuOptions);
        }

        // Method to confirm student entries with the user.
        private void ConfirmStudentEntries(Student newStudent)
        {
            // Retrieve associated gender and class from the database.
            var selectedGender = Context.Genders.SingleOrDefault(g => g.GenderId == newStudent.FkGenderId);
            var selectedClass = Context.ClassLists.SingleOrDefault(c => c.ClassId == newStudent.FkClassId);

            // Display the entered values and prompt for confirmation.
            string prompt = $"Are you satisfied with the entered values:\n" +
                $"\nFirst Name: {newStudent.FirstName}" +
                $"\nSurname: {newStudent.Surname}" +
                $"\nSSN: {newStudent.Ssn}" +
                $"\nGender: {selectedGender.Gender1}" +
                $"\nClass: {selectedClass.ClassName}\n";

            string[] menuOptions = { "Yes", "No" };

            GetMenu(prompt, menuOptions);
        }
        // Method to prompt the user to choose a gender.
        private void ChooseGender()
        {
            string prompt = "Select the gender of the student:";

            string[] menuOptions = { "Male", "Female", "Non Binary" };

            GetMenu(prompt, menuOptions);
        }

        // Method to prompt the user to choose a class.
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

        // Method to prompt the user to choose a profession.
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

        // Method to add a new personnel to the database.
        public void AddPersonnel()
        {
            // Get user input for personnel details.
            string firstName = InputHandler.GetNonEmptyName("Enter a first name: ");
            string validatedFirstName = InputHandler.CapitalizeFirstLetter(firstName);
            string surname = InputHandler.GetNonEmptyName($"First Name: {validatedFirstName}\n" +
              $"\nEnter a surname: ");
            string validatedSurname = InputHandler.CapitalizeFirstLetter(surname);
            string validatedSsn = InputHandler.ValidateSSN($"First Name: {validatedFirstName}" +
                $"\nSurname: {validatedSurname}\n" +
                $"\nEnter SSN (YYYYMMDDXXXX): ");

            // Prompt the user to choose gender and profession.
            ChooseGender();
            GenderChoice = (byte)++MenuChoice;
            ChooseProfession();
            ProfessionChoice = (byte)++MenuChoice;

            // Create a new personnel object.
            var newPersonnel = new Personnel
            {
                FirstName = validatedFirstName,
                Surname = validatedSurname,
                Ssn = validatedSsn,
                FkGenderId = GenderChoice,
                FkJobTitleId = ProfessionChoice
            };

            // Confirm entered values with the user.
            ConfirmPersonnelEntries(newPersonnel);

            // Add personnel to the database if confirmed.
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

        // Method to add a new student to the database.
        public void AddStudent(string prompt)
        {
            // Get user input for student details.
            string firstName = InputHandler.GetNonEmptyName("Enter the student's first name: ");
            string validatedFirstName = InputHandler.CapitalizeFirstLetter(firstName);
            string surname = InputHandler.GetNonEmptyName($"First Name: {validatedFirstName}\n" +
                $"\nEnter the student's surname: ");
            string validatedSurname = InputHandler.CapitalizeFirstLetter(surname);
            string validatedSsn = InputHandler.ValidateSSN($"First Name: {validatedFirstName}" +
                $"\nSurname: {validatedSurname}\n" +
                $"\nEnter the student's SSN (YYYYMMDDXXXX): ");

            // Prompt user to choose gender and class.
            ChooseGender();
            GenderChoice = (byte)++MenuChoice;
            ChooseClass();
            ClassChoice = ++MenuChoice;

            // Create a new student object.
            var newStudent = new Student
            {
                FirstName = validatedFirstName,
                Surname = validatedSurname,
                Ssn = validatedSsn,
                FkGenderId = GenderChoice,
                FkClassId = ClassChoice,
                SchoolStart = DateTime.Now
            };

            // Confirm entered values with the user.
            ConfirmStudentEntries(newStudent);

            // Add student to the database if confirmed.            
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

        // Method to print all courses with average grades as well as the lowest and highest grade for each course.
        public void PrintAllCoursesWithAvgGrade()
        {             
            // Retrieve and display courses with average grade as well as lowest and highest grade for each course.
            var courseWithAverageGrade = Context.CoursesWithAverageGradeViews
                .OrderBy(c => c.Course).ToList();

            // Display grade information in formatted table.
            Console.WriteLine("Course\t\t\tAverage Grade\tLowest Grade\tHighest Grade");
            Console.WriteLine($"{new string('-', 69)}");

            foreach (var c in courseWithAverageGrade)
            {
                // Format name of course for better alignment.
                string formattedCourse = c.Course?.Length > 16 ? $"{c.Course}\t"
                 : c.Course?.Length <= 7 ? $"{c.Course}\t\t\t" : $"{c.Course}\t\t";

                Console.Write($"{formattedCourse}{c.AverageGrade}\t\t{c.LowestGrade}\t\t{c.HighestGrade}");

                Console.WriteLine();
            }

            PressAnyKeyMessage();
        }

        // Method to print grades of students in the latest month.
        public void PrintGradesLatestMonth()
        {
            // Retrieve and display graded students in the latest month from the database.
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
                // Display information about student and their grade in each course in formatted table.
                Console.WriteLine("First Name\tLast Name\tClass\tCourse\t\t\tGrade\tDate Graded");
                Console.WriteLine($"{new string('-', 83)}");

                foreach (var s in latestGradedStudents)
                {
                    // Format first name, surname and name of course for better alignment.
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

        // Method to print students from a specific class.
        public void PrintStudentsFromSpecificClass(int sortByChoice, int orderByChoice, int classChoice)
        {
            // Order students based on user choices and retrieve students from a specific class.
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

            // Display student information in formatted table.
            Console.Clear();
            Console.WriteLine("First Name\tLast Name\tSSN\t\tClass\tBranch");
            Console.WriteLine($"{new string('-', 62)}");

            foreach (var s in studentsFromSpecificClass)
            {
                // Format first name and surname for better alignment.
                string formattedFirstName = s.FirstName.Length > 7 ? $"{s.FirstName}\t" : $"{s.FirstName}\t\t";
                string formattedSurname = s.Surname.Length > 7 ? $"{s.Surname}\t" : $"{s.Surname}\t\t";

                Console.Write($"{formattedFirstName}{formattedSurname}{s.Ssn}\t{s.ClassName}\t{s.Branch}");

                Console.WriteLine();
            }

            PressAnyKeyMessage();
        }

        // Method to print all students with sorting and ordering options.
        public void PrintAllStudentsWithOptions(int sortByChoice, int orderByChoice)
        {
            // Order students based on user choices and retrieve all students.
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

            // Display student information in formatted table.
            Console.Clear();
            Console.WriteLine("First Name\tLast Name\tSSN\t\tClass\tBranch\t\t\tDate of Enrollment");
            Console.WriteLine($"{new string('-', 98)}");

            foreach (var s in students)
            {
                // Format first name and surname for better alignment.
                string formattedFirstName = s.FirstName.Length > 7 ? $"{s.FirstName}\t" : $"{s.FirstName}\t\t";
                string formattedSurname = s.Surname.Length > 7 ? $"{s.Surname}\t" : $"{s.Surname}\t\t";

                Console.Write($"{formattedFirstName}{formattedSurname}{s.Ssn}\t{s.ClassName}\t{s.Branch}\t\t{s.SchoolStart:dd-MM-yyyy}");

                Console.WriteLine();
            }

            PressAnyKeyMessage();
        }

        // Method to print personnel with specific job titles.
        public void PrintPersonnelWithSpecificJobTitles(int menuChoice)
        {
            // Retrieve and display personnel with a specific job title from the database.
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

            // Display personnel information in formatted table.
            Console.Clear();
            Console.WriteLine("First Name\tLast Name\tJob Title");
            Console.WriteLine($"{new string('-', 41)}");

            foreach (var p in personnel)
            {
                // Format first name and surname for better alignment.
                string formattedFirstName = p.FirstName.Length > 7 ? $"{p.FirstName}\t" : $"{p.FirstName}\t\t";
                string formattedSurname = p.Surname.Length > 7 ? $"{p.Surname}\t" : $"{p.Surname}\t\t";

                Console.Write($"{formattedFirstName}{formattedSurname}{p.JobTitle}");

                Console.WriteLine();
            }

            PressAnyKeyMessage();
        }

        // Method to print all personnel information.
        public void PrintAllPersonnel()
        {
            // Retrieve and display personnel information, including related job titles from the database.
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

            // Display personnel information in formatted table.
            Console.Clear();
            Console.WriteLine("ID\tFirst Name\tLast Name\tJob Title");
            Console.WriteLine($"{new string('-', 50)}");

            foreach (var p in personnel)
            {
                // Format first name and surname for better alignment.
                string formattedFirstName = p.FirstName.Length > 7 ? $"{p.FirstName}\t" : $"{p.FirstName}\t\t";
                string formattedSurname = p.Surname.Length > 7 ? $"{p.Surname}\t" : $"{p.Surname}\t\t";

                Console.Write($"{p.PersonnelId}\t{formattedFirstName}{formattedSurname}{p.JobTitle}");

                Console.WriteLine();
            }

            PressAnyKeyMessage();
        }

        // Method to display a message and wait for any key press.
        private static void PressAnyKeyMessage()
        {
            Console.WriteLine("\nPress any key to go back.");
            Console.ReadKey();
        }
    }
}
