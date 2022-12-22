using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;

namespace CityDistanceCalculator
{
    sealed partial class FileReader
    {
        public static uint[,] GetMapFromDesktopCSVHelper(string fileName)
        {
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string fullName = System.IO.Path.Combine(desktopPath, fileName);

            if (!File.Exists(fullName))
            {
                Console.WriteLine($"File {fileName} not found in {desktopPath}.");
                return new uint[0, 0];
            }

            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = false,
            };

            using var reader = new StreamReader(fullName);
            using var csv = new CsvParser(reader, config);

            csv.Read();

            int size = csv.Record!.Length;

            uint[,] distances = new uint[size, size];

            var line = 0;

            do
            {
                uint[] lineAsArray = csv.Record!.Select(s => uint.Parse(s)).ToArray();

                for (var column = 0; column < size; column++)
                {
                    distances[line, column] = lineAsArray[column];
                }

                line++;
            }
            while (csv.Read());

            return distances;
        }

        public static uint[] GetRoutesFromDesktopCSVHelper(string fileName)
        {
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string fullName = System.IO.Path.Combine(desktopPath, fileName);

            if (!File.Exists(fullName))
            {
                Console.WriteLine($"File {fileName} not found in {desktopPath}.");
                return Array.Empty<uint>();
            }

            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = false,
            };

            using var reader = new StreamReader(fullName);
            using var csv = new CsvParser(reader, config);

            csv.Read();

            if (csv.Record == null)
            {
                return Array.Empty<uint>();
            }

            return csv.Record!.Select(s => uint.Parse(s)).ToArray();
        }
    }
}
