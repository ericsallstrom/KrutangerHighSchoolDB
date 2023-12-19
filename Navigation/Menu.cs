using System.Text;
using System.Threading.Channels;

namespace KrutangerHighSchoolDB
{
    internal class Menu
    {
        // Private fields that define keys for navigation.
        private const ConsoleKey KeyUp = ConsoleKey.UpArrow;
        private const ConsoleKey KeyDown = ConsoleKey.DownArrow;
        private const ConsoleKey KeySelect = ConsoleKey.Enter;

        // Properties for presenting, controlling and retrieving menu information.
        public string Prompt { get; set; }
        public string[] MenuOptions { get; set; }
        private ConsoleKeyInfo KeyInfo { get; set; }
        private ConsoleKey KeyPressed { get; set; }
        private int SelectedIndex { get; set; }

        // Constructor to initialize properties.
        public Menu()
        {
            Prompt = string.Empty;
            MenuOptions = Array.Empty<string>();
            SelectedIndex = 0;
        }

        // Method to get user's menu choice.
        public int GetMenuChoice()
        {
            do
            {                
                DisplayMenu(); // Display the menu on each iteration.

                KeyInfo = Console.ReadKey();
                KeyPressed = KeyInfo.Key;

                // Update the selected index based on which key is pressed.
                switch (KeyPressed)
                {
                    case KeyUp:
                        SelectedIndex = (SelectedIndex - 1 + MenuOptions.Length) % MenuOptions.Length;
                        break;
                    case KeyDown:
                        SelectedIndex = (SelectedIndex + 1 + MenuOptions.Length) % MenuOptions.Length;
                        break;
                }
            } while (KeyPressed != KeySelect); // Continue until Enter key is pressed.

            return SelectedIndex;
        }

        // Method to handle the color of each menu option based on selection.
        private void OptionColor(string currentOption, int optionIndex)
        {
            string prefix;

            // Determine whether a menu option is highlighted or not.
            if (SelectedIndex == optionIndex)
            {
                prefix = "->";
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.White;
            }
            else
            {
                prefix = " ";
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.Black;
            }
            Console.WriteLine($"{prefix} {currentOption}");
        }

        // Method to display the menu.
        private void DisplayMenu()
        {
            Console.Clear(); // Clear the console before displaying the menu.
            StringBuilder menuBuilder = new();
            menuBuilder.AppendLine(Prompt); // Display the menu prompt.

            Console.Write(menuBuilder);

            // Loop through each menu option and display it.
            for (int i = 0; i < MenuOptions.Length; i++)
            {
                string currentOption = MenuOptions[i];

                OptionColor(currentOption, i); // Apply color based on selection.                
            }

            Console.ResetColor(); // Reset console colors after displaying the menu.
        }
    }
}
