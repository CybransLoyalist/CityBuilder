using CityBuilder.MapModel;

namespace CityBuilder.Buildings
{
    public interface ITilePattern
    {
        IPoint Transformation { get; set; }
        bool IsDoor { get; }

    }
}