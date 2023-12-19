using KrutangerHighSchoolDB.Data;
using System.Text.RegularExpressions;

namespace KrutangerHighSchoolDB
{
    internal class UserInputHandler
    {
        private readonly KrutångerHighSchoolContext _context;

        public UserInputHandler(KrutångerHighSchoolContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public string CapitalizeFirstLetter(string name)
        {
            return char.ToUpper(name[0]) + name.Substring(1).ToLower();
        }

        public bool ValidateName(string name)
        {
            return Regex.IsMatch(name, @"^[\p{L}]+$");
        }

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

                    if (year >= 1900 && month >= 1 && month <= 12 && day >= 1 && day <= DateTime.DaysInMonth(year, month))
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

        public bool SSNLastFourDigitsIsUniqe(string ssn)
        {
            string lastFourDigits = ssn.Substring(8, 4);

            bool isUnique = !_context.Students.Any(s => s.Ssn.EndsWith(lastFourDigits)) &&
                !_context.Personnel.Any(p => p.Ssn.EndsWith(lastFourDigits));

            return isUnique;
        }
    }
}
