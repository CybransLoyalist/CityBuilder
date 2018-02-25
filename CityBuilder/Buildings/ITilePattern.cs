using CityBuilder.Map;

namespace CityBuilder.Buildings
{
    public interface ITilePattern
    {
        IPoint Transformation { get; set; }
        bool IsDoor { get; set; }

    }
}