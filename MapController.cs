namespace CityDistanceCalculator
{
    public class MapController
    {
        private uint[,]? DistancesMap2D { get; set; }

        private uint[]? Routes { get; set; }

        public MapController(uint[,] map, uint[] routes)
        {
            DistancesMap2D = map;
            Routes = routes;
        }

        public uint GetTotalDistance()
        {
            if (Routes == null)
            {
                return default;
            }

            if (Routes.Length < 2)
            {
                return default;
            }

            uint distance = default;

            for (var i = 0; i < (Routes.Length - 1); i++)
            {
                distance += GetSegmentDistance(Routes[i], Routes[i + 1]);
            }

            return distance;
        }

        private uint GetSegmentDistance(uint origin, uint destination)
        {
            if (DistancesMap2D == null)
            {
                return default;
            }

            if (origin > DistancesMap2D.GetLength(0)
                || destination > DistancesMap2D.GetLength(1)
                || origin < 0
                || destination < 0)
            {
                Console.WriteLine($"Error getting distance. Invalid origin or destination data.");
                return default;
            }

            if (origin == destination)
            {
                return default;
            }

            if (origin < destination)
            {
                return DistancesMap2D[destination - 1, origin - 1];
            }

            return DistancesMap2D[origin - 1, destination - 1]; ;
        }

        /// <summary>
        /// Fills lower matrix with random values.
        /// </summary>
        public void FillLowerMatrix()
        {
            if (DistancesMap2D == null)
            {
                return;
            }

            for (var i = 0; i < DistancesMap2D.GetLength(0); i++)
            {
                for (var j = 0; j < DistancesMap2D.GetLength(1); j++)
                {
                    if (i <= j)
                    {
                        continue;
                    }

                    DistancesMap2D[i, j] = (uint)new Random().Next(30);
                }
            }
        }
    }
}
