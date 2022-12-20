using System.Globalization;

namespace CityDistanceCalculator
{
    public class MapConsoleView : IView
    {
        public MapController Controller { get; private set; }

        public MapConsoleView()
        {
            this.Controller = new MapController(GetCities());
        }

        public static int GetNextCityFromInput(int numberOfCities)
        {
            int validInput;

            bool isInputValid;
            bool isValidInput;

            do
            {
                Console.WriteLine("--Type any negative number to STOP--");
                Console.Write($"City (1-{numberOfCities}): ");
                isValidInput = isInputValid = int.TryParse(Console.ReadLine()!.Replace(",", "."), NumberStyles.Integer, CultureInfo.InvariantCulture, out validInput);

                if (!isInputValid)
                {
                    Console.WriteLine("Input is not a valid integer format!");
                    isValidInput = false;
                }

                if (isInputValid && ((validInput == 0) || (validInput > numberOfCities)))
                {
                    Console.WriteLine($"Not a valid integer value. Input should be a integer equal or lower than {numberOfCities}.");
                    isValidInput = false;
                }
            }
            while (!isInputValid || !isValidInput);

            return validInput;
        }

        public static int GetDistanceFromInput(int origin, int destination)
        {
            int distance;

            bool isInputInteger;
            bool isValidDistance;

            do
            {
                Console.Write($"Distance from {origin} to {destination} in Km: ");
                isValidDistance = isInputInteger = int.TryParse(Console.ReadLine()!.Replace(",", "."), NumberStyles.Integer, CultureInfo.InvariantCulture, out distance);

                if (!isInputInteger)
                {
                    Console.WriteLine("Input is not a valid integer format!");
                    isValidDistance = false;
                }

                if (isInputInteger && (distance <= 0))
                {
                    Console.WriteLine("Not a valid distance. Distance should be a integer equal or higher than zero.");
                    isValidDistance = false;
                }
            }
            while (!isInputInteger || !isValidDistance);

            return distance;
        }

        public void Run()
        {
            ViewMap();
            GetMap();
            ViewMap();
            ViewDistance();
        }

        public int GetCities()
        {
            int numberOfCities;

            bool isInputInteger;
            bool isValidNumberOfCities;

            do
            {
                Console.Write($"How many cities in the map (2 or more)? ");
                isValidNumberOfCities = isInputInteger = int.TryParse(Console.ReadLine()!.Replace(",", "."), NumberStyles.Integer, CultureInfo.InvariantCulture, out numberOfCities);

                if (!isInputInteger)
                {
                    Console.WriteLine("Input is not a valid integer format!");
                    isValidNumberOfCities = false;
                }

                if (isInputInteger && (numberOfCities < 2))
                {
                    Console.WriteLine("Not a valid number of cities. The number of cities should be a integer higher than two.");
                    isValidNumberOfCities = false;
                }
            }
            while (!isInputInteger || !isValidNumberOfCities);


            return numberOfCities;
        }

        public void GetMap()
        {
            Console.Clear();
            for (int i = 0; i < Controller.GetMap().Distances2D.GetLength(0); i++)
            {
                for (int j = 0; j < Controller.GetMap().Distances2D.GetLength(1); j++)
                {
                    if (i <= j)
                    {
                        continue;
                    }

                    Controller.GetMap().Distances2D[i, j] = Controller.GetMap().Distances2D[j, i] = GetDistanceFromInput(i + 1, j + 1);
                    ViewMap();
                }
            }
        }

        public int[] GetDistances()
        {
            List<int> route = new();
            int userInput;

            do
            {
                userInput = GetNextCityFromInput(Controller.GetMap().Distances2D.GetLength(0)); ;

                if (userInput > 0)
                {
                    route.Add(userInput);
                    Console.WriteLine($"Route: [{string.Join(", ", route.ToArray())}]");
                }

                if (userInput < 0)
                {
                    if (route.Count == 0)
                    {
                        Console.WriteLine("No integer input was provided. Execution halted.");
                        return Enumerable.Empty<int>().ToArray();
                    }
                }
            }
            while (userInput >= 0);

            return route.ToArray();
        }

        public void ViewMap()
        {
            Console.Clear();
            Console.Write(Controller.GetMap());
        }

        public void ViewDistance()
        {
            Console.WriteLine($"Total distance: {Controller.CalculateDistance(GetDistances())}");
        }
    }
}
