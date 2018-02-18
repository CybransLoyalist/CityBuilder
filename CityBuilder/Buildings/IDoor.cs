using CityBuilding;

namespace CityBuilder.Buildings
{
    public interface IDoor
    {
        ITilePattern TilePattern { get; }
        Direction Direction { get; }
    }
}