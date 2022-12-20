using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;

namespace CityDistanceCalculator
{
    public static class FileReaderCSV
    {
        public static uint[,] GetMapFromDesktopCSVHelper(this FileReader fileReader, string fileName)
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

            List<uint[]> distances = new();

            while (csv.Read())
            {
                distances.Add(csv.Record!.Select(s => uint.Parse(s)).ToArray());
            }

            uint[][] jaggedMap = distances.ToArray();

            uint[,] map = new uint[distances.Count, distances.Count];

            for (var line = 0; line < distances.Count; line++)
            {
                for (var column = 0; column < distances.Count; column++)
                {
                    map[line, column] = jaggedMap[line][column];
                }
            }

            return map;
        }

        public static uint[] GetRoutesFromDesktopCSVHelper(this FileReader fileReader, string fileName)
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
