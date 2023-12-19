using KrutangerHighSchoolDB.Data;
using System.Text.RegularExpressions;

namespace KrutangerHighSchoolDB.Navigation
{
    internal class UserInputHandler
    {
        private readonly KrutångerHighSchoolContext _context;

        // Constructor to initialize the class with a database context.
        public UserInputHandler(KrutångerHighSchoolContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        // Method to capitalize the first letter of a string.
        public string CapitalizeFirstLetter(string name)
        {
            return char.ToUpper(name[0]) + name.Substring(1).ToLower();
        }

        // Method to validate that a string contains only letters and allows for å, ä and ö.
        public bool ValidateName(string name)
        {
            return Regex.IsMatch(name, @"^[\p{L}]+$");
        }
        
        // Method to get a non-empty name from the user with validation.
        public string GetNonEmptyName(string prompt)
        {
            string? name;

            while (true)
            {
                Console.Clear();
                Console.CursorVisible = true;

                Console.Write(prompt);
                name = Console.ReadLine()?.Trim();

                if (!string.IsNullOrEmpty(name) && ValidateName(name))
                {
                    break;
                }

                Console.CursorVisible = false;

                Console.WriteLine("\nInvalid entry. The name field cannot be left " +
                    "\nempty and should only contain letters." +
                    "\nPlease try again.");

                Thread.Sleep(3000);
            }

            return name;
        }

        // Method to validate and format a SSN.
        public string ValidateSSN(string prompt)
        {
            string validatedSsn = "";

            while (true)
            {
                Console.Clear();
                Console.CursorVisible = true;

                Console.Write(prompt);
                string ssn = Console.ReadLine()!.Trim();

                Console.CursorVisible = false;

                if (ssn.Length == 12 && ssn.All(char.IsDigit))
                {
                    int year = int.Parse(ssn.Substring(0, 4));
                    int month = int.Parse(ssn.Substring(4, 2));
                    int day = int.Parse(ssn.Substring(6, 2));

                    // Check if year is greater than or equal to 1900 and less than or equal to the current year. Also, ensure 
                    // that the month is between 1 and 12, and the day is within the valid range for the given month and year.
                    if (year >= 1900 && year <= DateTime.Now.Year && month >= 1 && month <= 12 && day >= 1 && day <= DateTime.DaysInMonth(year, month))
                    {
                        if (SSNLastFourDigitsIsUniqe(ssn))
                        {
                            validatedSsn = ssn.Insert(8, "-");
                            break;
                        }
                        else
                        {
                            Console.Write("\nLast four digits of SSN mmust be uniqe.");
                        }
                    }
                }

                Console.Write("\nInvalid SSN. Please enter a valid SSN (YYYYMMDDXXXX).");

                Thread.Sleep(3000);
            }

            return validatedSsn;
        }

        // Method to check if the last four digits of an SSN are unique in the database.
        public bool SSNLastFourDigitsIsUniqe(string ssn)
        {
            string lastFourDigits = ssn.Substring(8, 4);

            bool isUnique = !_context.Students.Any(s => s.Ssn.EndsWith(lastFourDigits)) &&
                !_context.Personnel.Any(p => p.Ssn.EndsWith(lastFourDigits));

            return isUnique;
        }
    }
}
