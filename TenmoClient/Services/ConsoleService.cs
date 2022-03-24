using System;

namespace TenmoClient.Services
{
    public class ConsoleService
    {
        /************************************************************
            Print methods
        ************************************************************/

        /// <summary>
        /// Prints an error message to the screen, in red text.
        /// </summary>
        /// <param name="message">Message to print.</param>
        public void PrintError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        /// <summary>
        /// Prints a success message to the screen, in green text.
        /// </summary>
        /// <param name="message">Message to print.</param>
        internal void PrintSuccess(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        /************************************************************
            Prompt methods (get user input)
        ************************************************************/

        /// <summary>
        /// Waits for the user to press a key before continuing. Used 
        /// after displaying some results, so the user can read the results, 
        /// then press a key to dismiss.
        /// </summary>
        /// <param name="message">Message to display. If NULL, 'Press any key to continue' will be shown.</param>
        public void Pause(string message = null)
        {
            if (message == null)
            {
                message = "Press any key to continue:";
            }
            Console.Write(message);
            Console.ReadKey();
        }

        /// <summary>
        /// Display a prompt and read an integer from the keyboard.
        /// </summary>
        /// <param name="message">Prompt to display to the user.</param>
        /// <param name="defaultValue">Optional. Value to be used if the user presses Enter without entering anything.</param>
        /// <returns>Integer entered by the user.</returns>
        public int PromptForInteger(string message, int? defaultValue = null)
        {
            // Prompts for a non-negative integer
            return PromptForInteger(message, 0, int.MaxValue, defaultValue);
        }

        /// <summary>
        /// Display a prompt and read an integer from the keyboard. Validates integer is in a range.
        /// </summary>
        /// <param name="message">Prompt to display to the user.</param>
        /// <param name="minimum">Number entered must be greater than or equal to this number.</param>
        /// <param name="maximum">Number entered must be less than or equal to this number.</param>
        /// <param name="defaultValue">Optional. Value to be used if the user presses Enter without entering anything.</param>
        /// <returns>Integer entered by the user.</returns>
        public int PromptForInteger(string message, int minimum, int maximum, int? defaultValue = null)
        {
            string defaultPrompt = defaultValue.HasValue ? $"[{defaultValue}]: " : ": ";
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write($"{message}{defaultPrompt}");
                Console.ResetColor();
                string input = Console.ReadLine();

                // Did the user take the default value?
                if (input.Trim().Length == 0 && defaultValue.HasValue)
                {
                    return defaultValue.Value;
                }

                if (int.TryParse(input, out int selection) && selection >= minimum && selection <= maximum)
                {
                    return selection;
                }
                PrintError($"Number is out of range, please try again.");
            }
        }

        /// <summary>
        /// Display a prompt and read a string from the keyboard.
        /// </summary>
        /// <param name="message">Prompt to display to the user.</param>
        /// <param name="defaultValue">Optional. Value to be used if the user presses Enter without entering anything.</param>
        /// <returns>String entered by the user.</returns>
        public string PromptForString(string message, string defaultValue = null)
        {
            string defaultPrompt = defaultValue == null ? ": " : $"[{defaultValue}]: ";
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"{message}{defaultPrompt}");
            Console.ResetColor();
            string input = Console.ReadLine();
            // Did the user take the default value?
            if (input.Length == 0 && defaultValue != null)
            {
                return defaultValue;
            }
            return input;
        }

        /// <summary>
        /// Prompts the user for a string, but displays asterisks on the screen, as in when a users types their password.
        /// </summary>
        /// <param name="message">The prompt to display to the user.</param>
        /// <returns>String entered by the user.</returns>
        public string PromptForHiddenString(string message)
        {
            string pass = "";
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"{message}: ");
            Console.ResetColor();

            ConsoleKeyInfo key;
            do
            {
                key = Console.ReadKey(true);

                // Backspace Should Not Work
                if (!char.IsControl(key.KeyChar))
                {
                    pass += key.KeyChar;
                    Console.Write("*");
                }
                else
                {
                    if (key.Key == ConsoleKey.Backspace && pass.Length > 0)
                    {
                        pass = pass.Remove(pass.Length - 1);
                        Console.Write("\b \b");
                    }
                }
            }
            // Stops Receving Keys Once Enter is Pressed
            while (key.Key != ConsoleKey.Enter);
            Console.WriteLine("");
            return pass;
        }

        /// <summary>
        /// Display a prompt and read a date from the keyboard.
        /// </summary>
        /// <param name="message">Prompt to display to the user.</param>
        /// <param name="defaultValue">Optional. Value to be used if the user presses Enter without entering anything.</param>
        /// <returns>Date entered by the user.</returns>
        public DateTime PromptForDate(string message, DateTime? defaultValue = null)
        {
            while (true)
            {
                string defaultPrompt = defaultValue.HasValue ? $"[{defaultValue:d}]: " : ": ";
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write($"{message}{defaultPrompt}");
                Console.ResetColor();
                string input = Console.ReadLine();

                // Did the user take the default value?
                if (input.Trim().Length == 0 && defaultValue.HasValue)
                {
                    return defaultValue.Value;
                }

                if (DateTime.TryParse(input, out DateTime date))
                {
                    return date;
                }
                PrintError($"Invalid date, please try again.");
            }
        }

        /// <summary>
        /// Display a prompt and read a double from the keyboard.
        /// </summary>
        /// <param name="message">Prompt to display to the user.</param>
        /// <param name="defaultValue">Optional. Value to be used if the user presses Enter without entering anything.</param>
        /// <returns>Double entered by the user.</returns>
        public double PromptForDouble(string message, double? defaultValue = null)
        {
            string defaultPrompt = defaultValue.HasValue ? $"[{defaultValue}]: " : ": ";
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write($"{message}{defaultPrompt}");
                Console.ResetColor();
                string input = Console.ReadLine();

                // Did the user take the default value?
                if (input.Trim().Length == 0 && defaultValue.HasValue)
                {
                    return defaultValue.Value;
                }

                if (double.TryParse(input, out double selection))
                {
                    return selection;
                }
                PrintError($"Invalid number, please try again.");
            }
        }

        /// <summary>
        /// Display a prompt and read a decimal from the keyboard.
        /// </summary>
        /// <param name="message">Prompt to display to the user.</param>
        /// <param name="defaultValue">Optional. Value to be used if the user presses Enter without entering anything.</param>
        /// <returns>Decimal entered by the user.</returns>
        public decimal PromptForDecimal(string message, decimal? defaultValue = null)
        {
            string defaultPrompt = defaultValue.HasValue ? $"[{defaultValue}]: " : ": ";
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write($"{message}{defaultPrompt}");
                Console.ResetColor();
                string input = Console.ReadLine();

                // Did the user take the default value?
                if (input.Trim().Length == 0 && defaultValue.HasValue)
                {
                    return defaultValue.Value;
                }

                if (decimal.TryParse(input, out decimal selection))
                {
                    return selection;
                }
                PrintError($"Invalid number, please try again.");
            }
        }
    }
}
