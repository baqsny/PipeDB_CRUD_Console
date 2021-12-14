using System;
using System.Collections.Generic;

namespace PipeDB.Core
{
    public class PipeManager
    {
        private List<Pipe> Pipes { get; set; }
        DatabaseManager databaseManager { get; } = new DatabaseManager();
        List<string> pipeIds = new List<string>();
        public PipeManager()
        {
            Pipes = new List<Pipe>();
        }

        public void AddPipe(string name, double pipeOutDiameter, double pipeWallThickness, double pipeKgM)
        {
            var pipe = new Pipe
            {
                Name = name,
                PipeOutDiameter = pipeOutDiameter,
                PipeWallThickness = pipeWallThickness,
                PipeKgM = pipeKgM
            };

            databaseManager.CreatePipeInDatabase(name, pipeOutDiameter, pipeWallThickness, pipeKgM);
        }
        public void DeletePipe(int pipeId)
        {
            pipeIds = databaseManager.ReadPipesIdsFromDatabase();
            foreach (var pipeIdFromList in pipeIds)
            {
                if (pipeId == int.Parse(pipeIdFromList))
                {
                    databaseManager.DeletePipeInDatabase(pipeId);
                    Console.WriteLine("Succeed!");
                    return;
                }
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Pipe with selected id doesn't exist!");
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public void DeleteAllPipes(string confirmation)
        {
            if (confirmation != "Delete all data")
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Input was incorrect!");
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            else
            {
                databaseManager.DeleteAllDataInDatabase();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Succeed!");
                Console.ForegroundColor = ConsoleColor.Gray;
            }

        }

        public void UpdatePipe(int pipeId, string name, double pipeOutDiameter, double pipeWallThickness, double pipeKgM)
        {
            var pipe = new Pipe
            {
                Name = name,
                PipeOutDiameter = pipeOutDiameter,
                PipeWallThickness = pipeWallThickness,
                PipeKgM = pipeKgM
            };

            databaseManager.UpdatePipeInDatabase(pipeId, name, pipeOutDiameter, pipeWallThickness, pipeKgM);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Succeed!");
            Console.ForegroundColor = ConsoleColor.Gray;
        }
        public bool CheckIfDatabaseIsEmpty()
        {
            pipeIds = databaseManager.ReadPipesIdsFromDatabase();
            if (pipeIds.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("There is no data in database! Firstly input data to database!");
                Console.ForegroundColor = ConsoleColor.Gray;
                return true;
            }
            return false;
        }

    }

}

