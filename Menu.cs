using System.Threading.Channels;

namespace KrutangerHighSchoolDB
{
    internal class Menu
    {
        private string Prompt { get; set; }
        private string[] MenuOptions { get; set; }
        private ConsoleKeyInfo KeyInfo { get; set; }
        private ConsoleKey KeyPressed { get; set; }
        private int SelectedIndex { get; set; }        

        public Menu(string prompt, string[] menuOptions)
        {
            Prompt = prompt;
            MenuOptions = menuOptions;
            SelectedIndex = 0;            
        }

        public int GetMenuChoice()
        {            
            do
            {
                DisplayMenu();

                KeyInfo = Console.ReadKey(true);
                KeyPressed = KeyInfo.Key;

                switch (KeyPressed)
                {
                    case ConsoleKey.UpArrow:
                        SelectedIndex = (SelectedIndex - 1 + MenuOptions.Length) % MenuOptions.Length;
                        break;
                    case ConsoleKey.DownArrow:
                        SelectedIndex = (SelectedIndex + 1 + MenuOptions.Length) % MenuOptions.Length;
                        break;                           
                }
            } while (KeyPressed != ConsoleKey.Enter);

            return SelectedIndex;
        }        

        private void DisplayMenu()
        {
            Console.Clear();
            Console.WriteLine(Prompt);

            for (int i = 0; i < MenuOptions.Length; i++)
            {
                string currentOption = MenuOptions[i];
                string prefix;

                if (SelectedIndex == i)
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
            Console.ResetColor();
        }
    }
}
