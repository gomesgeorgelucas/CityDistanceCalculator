using System.Text;

namespace CityDistanceCalculator
{
    public class MapModel
    {
        public int[,] Distances2D { get; init; }

        public MapModel(int size)
        {
            Distances2D = new int[size, size];
        }

        public override string ToString()
        {
            if (Distances2D == null)
            {
                return "Null array.";
            }

            StringBuilder sb = new();

            sb.AppendLine($"{new string(' ', 5)}{string.Join(new string(' ', 4), Enumerable.Range(1, Distances2D.GetLength(0)))}");

            for (int i = 0; i < Distances2D.GetLength(0); i++)
            {
                sb.Append($" {i + 1} ");
                for (int j = 0; j < Distances2D.GetLength(1); j++)
                {
                    sb.Append($" {Distances2D[i, j]: 00} ");
                }
                sb.Append(Environment.NewLine);
                sb.Append(Environment.NewLine);
            }


            sb.Append(Environment.NewLine);

            return sb.ToString();
        }
    }
}

