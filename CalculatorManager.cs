using System;
using System.Collections.Generic;
using PipeDB.Core;
using PipeDB_CRUD_Console;

namespace Pipe_calculator
{
    public class CalculatorManager
    {
        CheckPipeUserInput checkPipeUserInput = new CheckPipeUserInput();
        Calculator calculator = new Calculator();
        PipeManager pipeManager = new PipeManager();

        public void CalculatePipeFromDatabase()
        {
            if (pipeManager.CheckIfDatabaseIsEmpty())
            {
                return;
            }
            DatabaseManager databaseManager = new DatabaseManager();
            databaseManager.ListAllPipesFromDatabase();

            Console.WriteLine("Select pipe ID to calculate: ");
            var userPipeId = checkPipeUserInput.CheckUserInputInt();

            List<string> pipeIds = databaseManager.ReadPipesIdsFromDatabase();

            if (pipeIds.Contains(userPipeId.ToString()))
            {
                var pipe = new Pipe();
                pipe = databaseManager.ReadPipeFromDatabase(userPipeId);
                
                calculator.PipeCalculator(pipe.PipeOutDiameter, pipe.PipeWallThickness, pipe.PipeKgM, true);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Pipe with selected id doesn't exist!");
                Console.ForegroundColor = ConsoleColor.Gray;
            }



        }
        public void CalculateNewPipe()
        {
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

            calculator.PipeCalculator(userPipeOuterDiameterDouble, userPipeWallThicknessDouble, userPipeKgMDouble, false);
        }

        public void SaveNewPipeToDatabase(double pipeOutDiameter, double pipeWallThickness, double pipeKgM)
        {
            DatabaseManager databaseManager = new DatabaseManager();
            Console.WriteLine("Input pipe name: ");
            var pipeName = Console.ReadLine();
            databaseManager.CreatePipeInDatabase(pipeName, pipeOutDiameter, pipeWallThickness, pipeKgM);
        }

    }
}
