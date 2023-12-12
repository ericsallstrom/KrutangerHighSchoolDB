namespace KrutangerHighSchoolDB
{
    internal class App
    {
        private Menu? Menu { get; set; }
        private int MenuChoice { get; set; }

        public void RunApp()
        {
            Console.CursorVisible = false;

            while (true)
            {
                RunMainMenu();

                switch (MenuChoice)
                {
                    case 0:
                        SeePersonnel();
                        break;
                    case 1:
                        SeeAllStudents();
                        break;
                    case 2:
                        // Present all students from a specific class.
                        SeeStudentsFromSpecificClass();
                        break;
                    case 3:
                        // Present all grades that been set the latest month.
                        break;
                    case 4:
                        // Present all courses and average grade for each course, plus the highest and lowest score of every course.
                        break;
                    case 5:
                        // Add new students.
                        break;
                    case 6:
                        // Add new personnel.
                        break;
                    case 7:
                        Console.WriteLine("\nThe program has now terminated. Have a great day!");
                        return;
                }
            }
        }

        private void RunMainMenu()
        {
            string prompt = "Welcome to the Krutånger High School database." +
                          "\nHere, you can retrieve various data for both students and personnel." +
                        "\n\nUse the arrow keys to navigate through the options and press Enter to make a selection." +
                        "\n\nKRUTÅNGAR HIGH SCHOOL - DATABASE" +
                          "\n================================";

            string[] menuOptions =
            {
                "Personnel", "Students", "Students From a Specific Class", "Grades",
                "Courses", "Register New Student", "Register New Employee", "Exit"
            };

            GetMenu(prompt, menuOptions);
        }

        private void PresentedOrder()
        {
            string prompt = "Now, you should specify whether the students name" +
                         "\nshould be sorted in ascending or descending order." +
                       "\n\nPRESENTED ORDER" +
                         "\n===============";

            string[] menuOptions =
            {
                "Ascending Order", "Descending Order", "Back"
            };

            GetMenu(prompt, menuOptions);

            switch (MenuChoice)
            {
                case 0:
                    // Present firstname/surname in ascending order
                    break;
                case 1:
                    // Present firstname/surname in descending order
                    break;
                case 2:
                    // Returns to main menu
                    return;
            }
        }
        private void SeeStudentsFromSpecificClass()
        {
            string prompt = "You can retrieve every student from a specific class." +
                          "\nAdditionally, you can choose if they should be sorted" +
                          "\nby either first name or surname, and specify whether" +
                          "\nthe sorting should be in ascending or descending order." +
                        "\n\nRETRIEVE STUDENTS FROM A SPECIFIC CLASS" +
                          "\n=======================================";

            string[] menuOptions =
            {
                "Class 7A", "Class 7B", "Class 8A",
                "Class 8B", "Class 9A", "Class 9B",
                "Back"
            };

            GetMenu(prompt, menuOptions);

            string secondPrompt = "Now, choose whether the students should be sorted by first name or surname.";

            switch (MenuChoice)
            {
                case 0:
                    // Class 7A
                    SortByFirstOrLastName(secondPrompt);
                    break;
                case 1:
                    // Class 7B
                    break;
                case 2:
                    // Class 8A
                    break;
                case 3:
                    // Class 8B
                    break;
                case 4:
                    // Class 9A
                    break;
                case 5:
                    // Class 9B
                    break;
                case 6:
                    return;
            }
        }

        private void SortByFirstOrLastName(string prompt)
        {
            string[] menuOptions =
            {
                "First Name", "Surname", "Back"
            };

            GetMenu(prompt, menuOptions);

            switch (MenuChoice)
            {
                case 0:
                    PresentedOrder();
                    break;
                case 1:
                    //PresentedOrder();
                    break;
                case 2:
                    // Returns to main menu
                    return;
            }
        }

        private void SeeAllStudents()
        {
            string prompt = "You can choose to retrieve every student at Krutånger High School," +
                          "\nsorted by either first name or surname. Additionally, you can specify" +
                          "\nwhether the sorting should be in ascending or descending order." +
                        "\n\nRETRIEVE ALL STUDENTS" +
                          "\n=====================";

            SortByFirstOrLastName(prompt);
        }

        private void SeePersonnel()
        {
            string prompt = "Here, you can see every employee at Krutånger High" +
                          "\nSchool or employees with a specific job title." +
                        "\n\nRETRIEVE STAFF" +
                          "\n==============";

            string[] menuOptions =
            {
                "All Employees", "Principal", "Administrators",
                "Teachers", "Special Education Teachers", "School Nurse",
                "Janitors", "Chef", "Back"
            };

            GetMenu(prompt, menuOptions);

            switch (MenuChoice)
            {
                case 0:
                    // Present all employees
                    break;
                case 1:
                    // Present the principal
                    break;
                case 2:
                    // Present all admins
                    break;
                case 3:
                    // Present all teachers
                    break;
                case 4:
                    // Present all special education teachers
                    break;
                case 5:
                    // Present school nurse
                    break;
                case 6:
                    // Present all janitors
                    break;
                case 7:
                    // Present school chef
                    break;
                case 8:
                    // Returns to main menu
                    return;
            }
        }

        private void GetMenu(string prompt, string[] menuOptions)
        {
            Menu = new Menu(prompt, menuOptions);
            MenuChoice = Menu.GetMenuChoice();
        }
    }
}
