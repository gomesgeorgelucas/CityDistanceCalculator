﻿namespace CityDistanceCalculator
{
    public class FileReader
    {
        public static uint[,] GetMapFromDesktop(string fileName)
        {
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string fullName = System.IO.Path.Combine(desktopPath, fileName);

            if (!File.Exists(fullName))
            {
                Console.WriteLine($"File {fileName} not found in {desktopPath}.");
                return new uint[0, 0];
            }

            string[] fileContent;

            try
            {
                fileContent = System.IO.File.ReadAllLines(fullName);
            }
            catch (Exception)
            {
                Console.WriteLine("Error reading file.");
                fileContent = Array.Empty<string>();
            }

            uint mapSize = (uint)fileContent.Length;

            if (mapSize < 2)
            {
                Console.WriteLine("Not enough cities. Should more de one city.");
                return new uint[0, 0];
            }

            uint[,] map = new uint[mapSize, mapSize];

            for (var line = 0; line < mapSize; line++)
            {
                uint[] lineAsUIntegerArray;
                try
                {
                    lineAsUIntegerArray = fileContent[line].Replace(" ", "").Split(",").Select(s => uint.Parse(s)).ToArray();

                    if (lineAsUIntegerArray.Length < mapSize)
                    {
                        Console.WriteLine("Error reading line. The map is not square.");
                        lineAsUIntegerArray = lineAsUIntegerArray = new uint[mapSize];
                    }

                    if (lineAsUIntegerArray.Length > mapSize)
                    {
                        Console.WriteLine($"Error reading line. The map is not square. Ignoring exceding values on line {line + 1}.");
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Error reading line. Invalid values?");
                    lineAsUIntegerArray = new uint[mapSize];
                }

                for (var column = 0; column < mapSize; column++)
                {
                    map[line, column] = lineAsUIntegerArray[column];
                }
            }

            return map;

        }

        public static uint[] GetRoutesFromDesktop(string fileName)
        {
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string fullName = System.IO.Path.Combine(desktopPath, fileName);

            if (!File.Exists(fullName))
            {
                Console.WriteLine($"File {fileName} not found in {desktopPath}.");
                return new uint[0];
            }

            string[] fileContent;

            try
            {
                fileContent = System.IO.File.ReadAllLines(fullName);
            }
            catch (Exception)
            {
                Console.WriteLine("Error reading file.");
                fileContent = Array.Empty<string>();
            }

            uint routesSize = (uint)fileContent.Length;

            if (routesSize > 1)
            {
                Console.WriteLine("Only one line is allowed.");
                return Array.Empty<uint>();
            }

            uint[] lineAsUIntegerArray;

            try
            {
                lineAsUIntegerArray = fileContent[0].Replace(" ", "").Split(",").Select(s => uint.Parse(s)).ToArray();
            }
            catch (Exception)
            {
                Console.WriteLine("Error reading line. Invalid values?");
                lineAsUIntegerArray = Array.Empty<uint>();
            }

            return lineAsUIntegerArray;
        }
    }
}
