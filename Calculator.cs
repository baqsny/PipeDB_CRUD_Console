using PipeDB.Core;
using System;


namespace Pipe_calculator
{
    public class Calculator
    {
        public void AskForAction()
        {
            CheckPipeUserInput checkPipeUserInput = new CheckPipeUserInput();
            CalculatorManager calculatorManager = new CalculatorManager();
            var userInput = default(int);
            do
            {
                Console.WriteLine("Welcome in pipe load calculator!");
                Console.WriteLine("Menu:");
                Console.WriteLine("1. Calculate pipe from database");
                Console.WriteLine("2. Calculate new pipe");
                Console.WriteLine("0. Back to main menu");

                userInput = checkPipeUserInput.CheckUserInputInt();

                switch (userInput)
                {
                    case 1:
                        calculatorManager.CalculatePipeFromDatabase();
                        break;
                    case 2:
                        calculatorManager.CalculateNewPipe();
                        break;
                    default:
                        break;
                }
            } while (userInput != 0);
        }

        public void PipeCalculator(double outerDiameter, double wallThickness, double KgM, bool isPipeFromDatabase)
        {
            double innerDiameter;
            double pipeMass;
            double pipeLoad;
            double fullPipeMass;
            double fullPipeLoad;
            double waterMass;

            if (isPipeFromDatabase == true)
            {
                Console.WriteLine("Loaded from database succesful!");
                Console.WriteLine("Loaded properties of pipe: ");
                Console.WriteLine("Outer diameter: {0} mm", outerDiameter);
                Console.WriteLine("Wall thickness: {0} mm", wallThickness);
                Console.WriteLine("kg/m factor: {0} kg/m", KgM);
            }
            CheckPipeUserInput checkPipeUserInput = new CheckPipeUserInput();

            Console.WriteLine("Input pipe length [mm]: ");
            var length = checkPipeUserInput.CheckUserInputInt();

            innerDiameter = outerDiameter - 2 * wallThickness;
            pipeMass = length / 1000 * KgM;
            pipeLoad = 9.81 * pipeMass;

            waterMass = 3.14 * (innerDiameter / 1000 * innerDiameter / 1000) * (length / 1000) * 997;

            fullPipeMass = pipeMass + waterMass;
            fullPipeLoad = pipeLoad + waterMass * 9.81;
            
            Console.WriteLine(String.Format("Empty pipeline mass: {0:0.000} kg", pipeMass));
            Console.WriteLine(String.Format("Empty pipeline load: {0:0.000} N", pipeLoad));
            Console.WriteLine(String.Format("Full pipeline mass: {0:0.000} kg", fullPipeMass));
            Console.WriteLine(String.Format("Full pipeline load: {0:0.000} N", fullPipeLoad));

            if (isPipeFromDatabase == false)
            {
                Console.WriteLine("Do you want to save new pipe to database? (y/n)");
                var userInput = Console.ReadLine();
                userInput = userInput.ToLower();
                switch (userInput)
                {
                    case "y":
                        CalculatorManager calculatorManager = new CalculatorManager();
                        calculatorManager.SaveNewPipeToDatabase(outerDiameter, wallThickness, KgM);
                        break;
                    case "n":
                        break;
                }
            }
            

        }
       
    }
}
