using System;
using System.Collections.Generic;

namespace PipeDB.Core
{
    public class CheckPipeUserInput
    {
        public string CheckUserInputDouble(string userInput)
        {
            userInput = Console.ReadLine();
            while(true)
            {
                if (!double.TryParse(userInput, out _))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Incorrect input! Try again: ");
                    Console.ForegroundColor = ConsoleColor.Gray;
      
                    userInput = Console.ReadLine();
                }
                else
                {
                    userInput = CheckLengthUserInputDouble(userInput);
                    break;
                }
            }
            return userInput;
        }

        public int CheckUserInputInt()
        {
            var userInput = Console.ReadLine();
            int userInputInt;
            while (!int.TryParse(userInput, out userInputInt))
                {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Incorrect input! Try again: ");
                Console.ForegroundColor = ConsoleColor.Gray;
                userInput = Console.ReadLine();
                }
            return userInputInt;
        }
        private string CheckLengthUserInputDouble(string userInput)
        {
            while ((userInput.Length > 8) || (userInput.Length <= 0))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Value must have 1-7 characters! Try again: ");
                Console.ForegroundColor = ConsoleColor.Gray;
                
                userInput = CheckUserInputDouble(userInput);
            }
            return userInput;
        }

        public string CheckLengthUserInputPipeName(string userInput)
        {
            while ((userInput.Length > 20) || (userInput.Length <= 0))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("Name must have 1-20 characters! Try again: ");
                Console.ForegroundColor = ConsoleColor.Gray;
                userInput = Console.ReadLine();
            }
            return userInput;
        }

    }
}