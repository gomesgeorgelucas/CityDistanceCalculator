using System.Globalization;
using System.Text;

namespace CityDistanceCalculator
{
    public class MapConsoleView
    {
        private static MapController? Controller { get; set; }

        public static void PrintHeader()
        {
            Console.Write("-------------------------------------------------\n");
            Console.Write("Welcome to the CS City Distance Calculator! \n");
            Console.WriteLine("-------------------------------------------------\n");
        }

        public static void PrintFooter()
        {
            Console.Write("\n-------------------------------------------------\n");
            Console.Write("Thanks!\n");
            Console.Write("-------------------------------------------------\n");
        }

        private static int GetNextCityFromInput(uint numberOfCities)
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

        private static uint GetDistanceFromInput(uint origin, uint destination)
        {
            uint distance;

            bool isInputInteger;
            bool isValidDistance;

            do
            {
                Console.Write($"Distance from {origin} to {destination} in Km: ");
                isValidDistance = isInputInteger = uint.TryParse(Console.ReadLine()!.Replace(",", "."), NumberStyles.Integer, CultureInfo.InvariantCulture, out distance);

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

        public static void Run(bool fromFile)
        {
            PrintHeader();

            uint[,] map;
            uint[] routes;

            if (fromFile)
            {
                try
                {
                    map = FileReader.GetMapFromDesktopCSVHelper("matriz.txt");
                    ViewMap(map, (uint)map.GetLength(0));
                    routes = FileReader.GetRoutesFromDesktopCSVHelper("caminho.txt");
                    ViewRoutes(routes);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    Console.WriteLine("Error gettin map or routes from file, switching to interactive input.");
                    map = GetMap();
                    routes = GetDistances((uint)map.GetLength(0));
                }
            }
            else
            {
                map = GetMap();
                routes = GetDistances((uint)map.GetLength(0));
            }

            Controller = new MapController(map, routes);

            ViewDistance();

            PrintFooter();
        }

        private static uint GetMapSize()
        {
            uint numberOfCities;

            bool isInputInteger;
            bool isValidNumberOfCities;

            do
            {
                Console.Write($"How many cities in the map (2 or more)? ");
                isValidNumberOfCities = isInputInteger = uint.TryParse(Console.ReadLine()!.Replace(",", "."), NumberStyles.Integer, CultureInfo.InvariantCulture, out numberOfCities);

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

        private static uint[,] GetMap()
        {
            uint mapSize = GetMapSize();

            uint[,] map = new uint[mapSize, mapSize];

            ViewMap(map, mapSize);

            for (var i = 0; i < mapSize; i++)
            {
                for (var j = 0; j < mapSize; j++)
                {
                    if (i <= j)
                    {
                        continue;
                    }

                    map[i, j] = map[j, i] = GetDistanceFromInput((uint)i + 1, (uint)j + 1);
                    ViewMap(map, mapSize);
                }
            }

            return map;
        }

        private static uint[] GetDistances(uint mapSize)
        {
            List<uint> route = new();
            int userInput;

            do
            {
                userInput = GetNextCityFromInput(mapSize); ;

                if (userInput > 0)
                {
                    route.Add((uint)userInput);
                    Console.WriteLine($"Route: [{string.Join(", ", route.ToArray())}]");
                }

                if (userInput < 0)
                {
                    if (route.Count == 0)
                    {
                        Console.WriteLine("No integer input was provided. Execution halted.");
                        return Enumerable.Empty<uint>().ToArray();
                    }
                }
            }
            while (userInput >= 0);

            return route.ToArray();
        }

        private static void ViewMap(uint[,] map, uint mapSize)
        {
            if (map == null)
            {
                Console.Write("Null array.");

                return;
            }

            StringBuilder sb = new();

            sb.AppendLine($"{new string(' ', 5)}{string.Join(new string(' ', 4), Enumerable.Range(1, (int)mapSize))}");

            for (var i = 0; i < mapSize; i++)
            {
                sb.Append($" {i + 1} ");
                for (var j = 0; j < mapSize; j++)
                {
                    sb.Append($" {map[i, j]: 00} ");
                }
                sb.Append(Environment.NewLine);
                sb.Append(Environment.NewLine);
            }


            sb.Append(Environment.NewLine);
            Console.Write(sb.ToString());
        }

        private static void ViewRoutes(uint[] map)
        {
            if (map == null)
            {
                Console.Write("Null array.");

                return;
            }

            StringBuilder sb = new();

            sb.Append("[");

            foreach (var city in map)
            {
                sb.Append($"{city}, ");
            }

            sb.Append(";");

            sb.Replace(", ;", "");

            sb.Append("]");

            sb.Append(Environment.NewLine);
            sb.Append(Environment.NewLine);
            Console.Write(sb.ToString());
        }

        private static void ViewDistance()
        {
            if (Controller == null)
            {
                Console.WriteLine($"Total distance: Unavailable.");

                return;
            }

            Console.WriteLine($"Total distance: {Controller.GetTotalDistance()}km");
        }
    }
}
