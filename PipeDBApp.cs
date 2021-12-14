using System;
using PipeDB.Core;
using Pipe_calculator;
using System.Collections.Generic;
using System.IO;

namespace PipeDB_CRUD_Console
{
    public class PipeDBApp
    {
        public PipeManager pipeManager { get; set; } = new PipeManager();
        private DatabaseManager databaseManager = new DatabaseManager();
        private CheckPipeUserInput checkPipeUserInput = new CheckPipeUserInput();
        private List<string> pipeIds = new List<string>();

        public void IntroducePipeDBApp()
        {
            Console.WriteLine("Welcome to Pipe load CALCULATOR created by Mateusz Sawicki!");
        }

        private void AddPipe()
        {
            Console.WriteLine("Input pipe name: ");
            var userPipeName = Console.ReadLine();
            userPipeName = checkPipeUserInput.CheckLengthUserInputPipeName(userPipeName);

            Console.WriteLine("Input pipe outer diameter: ");
            var userPipeOuterDiameter = default(string);
            userPipeOuterDiameter = checkPipeUserInput.CheckUserInputDouble(userPipeOuterDiameter);
            var userPipeOuterDiameterDouble = double.Parse(userPipeOuterDiameter);

            Console.WriteLine("Input pipe wall thickness: ");
            var userPipeWallThickness = default(string);
            userPipeWallThickness = checkPipeUserInput.CheckUserInputDouble(userPipeWallThickness);
            var userPipeWallThicknessDouble = double.Parse(userPipeWallThickness);

            Console.WriteLine("Input pipe kg/m factor: ");
            var userPipeKgM = default(string);
            userPipeKgM = checkPipeUserInput.CheckUserInputDouble(userPipeKgM);
            var userPipeKgMDouble = double.Parse(userPipeKgM);

            pipeManager.AddPipe(userPipeName, userPipeOuterDiameterDouble, userPipeWallThicknessDouble, userPipeKgMDouble);
        }

        private void DeletePipe()
        {
            if (pipeManager.CheckIfDatabaseIsEmpty())
            {
                return;
            }
            ListAllPipes();
            Console.WriteLine("Input pipe ID to delete: ");
            var userPipeId = checkPipeUserInput.CheckUserInputInt();
            pipeManager.DeletePipe(userPipeId);
        }

        private void DeleteAllPipes()
        {
            Console.Write("Input: ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("'Delete all data'");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write(" .To back to menu press any key: ");
            var userInput = Console.ReadLine();
            pipeManager.DeleteAllPipes(userInput);
        }

        private void UpdatePipe()
        {
            if (pipeManager.CheckIfDatabaseIsEmpty())
            {
                return;
            }
            ListAllPipes();
            Console.WriteLine("Input pipe ID to update: ");
            var userPipeId = checkPipeUserInput.CheckUserInputInt();

            if (pipeIds.Contains(userPipeId.ToString()))
            {
                Console.WriteLine("Input NEW pipe name: ");
                var userPipeName = Console.ReadLine();
                userPipeName = checkPipeUserInput.CheckLengthUserInputPipeName(userPipeName);

                Console.WriteLine("Input NEW pipe outer diameter: ");
                var userPipeOuterDiameter = default(string);
                userPipeOuterDiameter = checkPipeUserInput.CheckUserInputDouble(userPipeOuterDiameter);
                var userPipeOuterDiameterDouble = double.Parse(userPipeOuterDiameter);

                Console.WriteLine("Input NEW pipe wall thickness: ");
                var userPipeWallThickness = default(string);
                userPipeWallThickness = checkPipeUserInput.CheckUserInputDouble(userPipeWallThickness);
                var userPipeWallThicknessDouble = double.Parse(userPipeWallThickness);

                Console.WriteLine("Input NEW pipe kg/m factor: ");
                var userPipeKgM = default(string);
                userPipeKgM = checkPipeUserInput.CheckUserInputDouble(userPipeKgM);
                var userPipeKgMDouble = double.Parse(userPipeKgM);

                pipeManager.UpdatePipe(userPipeId, userPipeName, userPipeOuterDiameterDouble, userPipeWallThicknessDouble, userPipeKgMDouble);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Pipe with selected id doesn't exist!");
                Console.ForegroundColor = ConsoleColor.Gray;
            }
        }

        private void ListAllPipes()
        {
            pipeManager.CheckIfDatabaseIsEmpty();
            databaseManager.ListAllPipesFromDatabase();
        }

        public void AskForAction()
        {
            databaseManager.CreateTableInDatabase();
            var userInput = default(int);
            do
            {
                Console.WriteLine("Menu: ");
                Console.WriteLine("1. New pipe");
                Console.WriteLine("2. Show all pipe");
                Console.WriteLine("3. Update pipe");
                Console.WriteLine("4. Delete pipe");
                Console.WriteLine("5. Delete all pipes");
                Console.WriteLine("6. Calculate pipe load");
                Console.WriteLine("7. Clear console");
                Console.WriteLine("0. Exit program");
                Console.WriteLine("Enter the number of your choice: ");

                userInput = checkPipeUserInput.CheckUserInputInt();

                switch (userInput)
                {
                    case 1:
                        AddPipe();
                        break;
                    case 2:
                        ListAllPipes();
                        break;
                    case 3:
                        UpdatePipe();
                        break;
                    case 4:
                        DeletePipe();
                        break;
                    case 5:
                        DeleteAllPipes();
                        break;
                    case 6:
                        Calculator calculator = new Calculator();
                        calculator.AskForAction();
                        break;
                    case 7:
                        break;
                    case 0:
                        Console.WriteLine("Press any key to exit!");
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Choose option from list! Try again: ");
                        Console.ForegroundColor = ConsoleColor.Gray;
                        break;
                }

            } while (userInput != 0);
        }
    }
}
