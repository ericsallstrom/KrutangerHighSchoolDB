using KrutangerHighSchoolDB.Data;
using KrutangerHighSchoolDB.Navigation;

namespace KrutangerHighSchoolDB
{
    internal class App
    {
        // Properties for sorting and checking menu choices.
        private int SortByChoice { get; set; }
        private int SortByOrder { get; set; }
        private int MenuChoice { get; set; }
        private PrintQueries PrintQuery { get; set; }

        // Constructor to initialize the database context and PrintQueries instance.
        public App()
        {
            var context = new KrutångerHighSchoolContext();
            PrintQuery = new PrintQueries(context);
        }

        // Main method to run the application.
        public void RunApp()
        {
            Console.CursorVisible = false;

            while (true)
            {
                RunMainMenu(); // Display the main menu.

                // Execute the selected option.
                switch (MenuChoice)
                {
                    case 0:
                        RetrievePersonnelWithOptions();
                        break;
                    case 1:
                        RetrieveStudentsWithOptions();
                        break;
                    case 2:
                        RetrieveGradesFromLatestMonth();
                        break;
                    case 3:                        
                        RetrieveAllCoursesWithAverageGrade();
                        break;
                    case 4:                        
                        EnrollNewStudent();
                        break;
                    case 5:                        
                        RegisterNewPersonnel();
                        break;
                    case 6:
                        ExitApp();
                        return;
                }
            }
        }

        // Method to register new personnel.
        private void RegisterNewPersonnel()
        {
            Console.Clear();
            string prompt = ("To register a new staff member, please provide the following details: " +
               "\nfirst name, surname, social security number (SSN)" +
               "\nand select appropriate profession from the available options." +
               "\n\nREGISTER NEW PERSONNEL" +
               "\n======================");

            string[] menuOptions = { "Register Personnel", "Back" };

            GetMenu(prompt, menuOptions);

            // Process the user's choice.
            if (MenuChoice == 0)
            {
                PrintQuery.AddPersonnel();
            }
        }

        // Method to enroll a new student.
        private void EnrollNewStudent()
        {
            Console.Clear();
            string prompt = ("To enroll a new student, please provide the following details: " +
                "\nfirst name, surname, social security number (SSN)" +
                "\nand select a class from the available options." +
                "\n\nENROLL NEW STUDENT" +
                "\n====================");

            string[] menuOptions = { "Enroll Student", "Back" };

            GetMenu(prompt, menuOptions);

            if (MenuChoice == 0)
            {
                PrintQuery.AddStudent(prompt);
            }
        }

        // Method to retrieve information about all courses with average
        // grades, as well as the highest and lowest grade for each course.
        private void RetrieveAllCoursesWithAverageGrade()
        {
            Console.Clear();
            Console.WriteLine("Review every course, featuring the average grade, as well as the highest" +
                "\nand lowest grades achieved in every class." +
                "\n\nCOURSE PERFORMANCE OVERVIEW" +
                "\n===========================\n");

            PrintQuery.PrintAllCoursesWithAvgGrade();
        }

        // Method to retrieve grades from latest month.
        private void RetrieveGradesFromLatestMonth()
        {
            Console.Clear();
            Console.WriteLine("Review the grades of each student for every course from the latest month." +
                "\n\nLATEST MONTH GRADE REPORT" +
                "\n=========================\n");

            PrintQuery.PrintGradesLatestMonth();
        }

        // Method to exit the app.
        private static void ExitApp()
        {
            Console.Clear();
            Console.WriteLine("The program is shutting down...");
            Thread.Sleep(2000);
            Console.WriteLine("\nHave a great day and welcome back.");
            Thread.Sleep(3000);
        }

        // Method to display the main menu.
        private void RunMainMenu()
        {
            string prompt = "Welcome to the Krutånger High School Database." +
                "\nExplore and retrieve a wide range of information about students and personnel." +
                "\n\nNavigate using the arrow keys and press Enter to make your selection." +
                "\n\nKRUTÅNGER HIGH SCHOOL - DATABASE" +
                "\n================================";

            string[] menuOptions =
            {
                "Personnel", "Students", "Grades", "Courses",
                "Register New Student", "Register New Personnel", "Exit"
            };

            GetMenu(prompt, menuOptions);
        }

        // Method to display the menu for seeing students from a specific class.
        private void SeeStudentsFromSpecificClassMenu()
        {
            while (true)
            {
                string prompt = "Retrieve information about students from a specific class." +
                    "\nYou can choose sorting by first name or surname and specify" +
                    "\nin which order they should be presented." +
                    "\n\nRETRIEVE STUDENTS FROM A SPECIFIC CLASS" +
                    "\n=======================================";

                string[] menuOptions =
                {
                    "Class 7A", "Class 7B", "Class 8A",
                    "Class 8B", "Class 9A", "Class 9B",
                    "Back"
                };

                GetMenu(prompt, menuOptions);

                string prompt2 = "Please select your sorting preference for the students' names." +
                    "\n\nSORT BY FIRST NAME OR SURNAME" +
                    "\n=============================";

                if (MenuChoice != 6)
                {
                    int classChoice = ++MenuChoice;
                    SortByChoice = SortByFirstOrLastNameMenu(prompt2);
                    SortByOrder = OrderByMenu();

                    PrintQuery.PrintStudentsFromSpecificClass(SortByChoice, SortByOrder, classChoice);
                }
                else
                {
                    return;
                }
            }
        }

        // Method to display the menu for sorting by first name or surname.
        private int SortByFirstOrLastNameMenu(string prompt)
        {
            string[] menuOptions = { "First Name", "Surname" };

            GetMenu(prompt, menuOptions);

            return MenuChoice;
        }

        // Method to display the menu for sorting order.
        private int OrderByMenu()
        {
            string prompt = "Please specify whether the students' names" +
                "\nshould be sorted in ascending or descending order." +
                "\n\nPRESENTED ORDER" +
                "\n===============";

            string[] menuOptions = { "Ascending Order", "Descending Order" };

            GetMenu(prompt, menuOptions);

            return MenuChoice;
        }

        // Method to retrieve students with various sorting and filtering options.
        private void RetrieveStudentsWithOptions()
        {
            while (true)
            {
                string prompt = "Retrieve information about every student at Krutånger High School," +
              "\nsorted by either first name or surname. Choose to see the information" +
              "\nof all students or students from a specific class. Additionally, you can " +
              "\nspecify whether the sorting should be in ascending or descending order." +
              "\n\nSTUDENT OVERVIEW" +
                "\n================";

                string prompt2 = "Please select your sorting preference for the students' names." +
                       "\n\nSORT BY FIRST NAME OR SURNAME" +
                       "\n=============================";

                string[] menuOptions = { "All Students", "Students From a Specific Class", "Back" };

                GetMenu(prompt, menuOptions);


                if (MenuChoice == 0)
                {
                    SortByChoice = SortByFirstOrLastNameMenu(prompt2); // Let the user choose first name or surname.
                    SortByOrder = OrderByMenu(); // Let the user choose ascending or descending order.

                    PrintQuery.PrintAllStudentsWithOptions(SortByChoice, SortByOrder);
                }
                else if (MenuChoice == 1)
                {
                    SeeStudentsFromSpecificClassMenu();
                }
                else
                {
                    return;
                }
            }
        }

        // Method to retrieve personnel with sorting and filtering options.
        private void RetrievePersonnelWithOptions()
        {
            while (true)
            {
                string prompt = "Retrieve information of every personnel at Krutånger High School" +
                    "\nor filter personnel based on their specific job titles." +
                    "\n\nPERSONNEL OVERVIEW" +
                    "\n==================";

                string[] menuOptions =
                {
                    "All Employees", "Principal", "Administrators",
                    "Teachers", "Janitors", "School Nurse",
                    "Special Need Teachers", "Chef", "Back"
                };

                GetMenu(prompt, menuOptions);

                if (MenuChoice == 0)
                {
                    PrintQuery.PrintAllPersonnel();
                }
                else if (MenuChoice == 8)
                {
                    return;
                }
                else
                {
                    PrintQuery.PrintPersonnelWithSpecificJobTitles(MenuChoice);
                }
            }
        }

        // Method to display the menu and get user's choice.
        public void GetMenu(string prompt, string[] menuOptions)
        {
            Menu menu = new()
            {
                Prompt = prompt,
                MenuOptions = menuOptions
            };

            MenuChoice = menu.GetMenuChoice();
        }
    }
}
