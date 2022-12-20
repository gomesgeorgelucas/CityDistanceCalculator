namespace CityDistanceCalculator
{
    public interface IView
    {
        void Run();
        void GetMap();
        int[] GetDistances();
        void ViewDistance();
    }
}
