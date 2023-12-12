namespace KrutangerHighSchoolDB
{
    internal class App
    {
        private Menu? Menu { get; set; }
        private int MenuChoice { get; set; }

        public void RunApp()
        {
            RunMainMenu();

            switch (MenuChoice)
            {
                case 0:
                    SeeStaff();
                    break;
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;
                case 5:
                    break;
                case 6:
                    Console.WriteLine("\nThe program has now terminated. Have a great day!");
                    break;
            }
        }

        private void RunMainMenu()
        {
            string prompt = "Welcome to the Krutånger High School database." +
                          "\nHere, you can retrieve various data for both personnel and students." +
                        "\n\nUse the arrow keys to navigate through the options and press Enter to make a selection." +
                        "\n\nKRUTÅNGAR HIGH SCHOOL - DATABASE" +
                          "\n================================";

            string[] menuOptions =
            {
                "See Personnel", "See Students", "See Grades", "See Courses",
                "Register New Student", "Register New Employee", "Exit"
            };

            GetMenu(prompt, menuOptions);
        }

        private void SeeStudents()
        {
            
        }

        private void SeeStaff()
        {
            string prompt = "RETRIEVE STAFF" +
                          "\n==============";

            string[] menuOptions =
            {
                "See All Employees", "See Teachers",
                "Back To Main Menu"
            };

            GetMenu(prompt, menuOptions);

            switch (MenuChoice)
            {
                case 0:
                    break;
                case 1:
                    break;
                case 2:
                    RunMainMenu();
                    break;
                case 3:
                    break;
                case 4:
                    break;
            }
        }

        private void GetMenu(string prompt, string[] menuOptions)
        {
            Menu = new Menu(prompt, menuOptions);
            MenuChoice = Menu.GetMenuChoice();
        }
    }
}
