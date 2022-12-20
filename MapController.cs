namespace CityDistanceCalculator
{
    public class MapController
    {
        private MapModel Map;

        public MapController(int mapSize)
        {
            this.Map = new MapModel(mapSize);
        }

        public MapModel GetMap()
        {
            return this.Map;
        }

        public int CalculateDistance(int[] routes)
        {
            if (routes.Length < 2)
            {
                return default;
            }

            int distance = default;

            for (int i = 0; i < (routes.Length - 1); i++)
            {
                distance += GetDistance(routes[i], routes[i + 1]);
            }

            return distance;
        }

        private int GetDistance(int origin, int destination)
        {
            if (origin > (int)Map.Distances2D.GetLength(0)
                || destination > (int)Map.Distances2D.GetLength(1)
                || origin < 0
                || destination < 0)
            {
                throw new InvalidDataException($"{ConsoleViewMessages.PtBr["InvalidDataException"]}");
            }

            if (origin == destination)
            {
                return default;
            }

            if (origin < destination)
            {
                return this.Map.Distances2D[destination - 1, origin - 1];
            }

            return this.Map.Distances2D[origin - 1, destination - 1]; ;
        }

        public void FillLowerMatrix()
        {
            for (int i = 0; i < Map.Distances2D.GetLength(0); i++)
            {
                for (int j = 0; j < Map.Distances2D.GetLength(1); j++)
                {
                    if (i <= j)
                    {
                        continue;
                    }

                    Map.Distances2D[i, j] = (int)new Random().Next(30);
                }
            }
        }
    }
}
