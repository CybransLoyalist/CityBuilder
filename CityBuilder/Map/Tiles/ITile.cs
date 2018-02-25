using AStarAlgorithm;

namespace CityBuilder.Map.Tiles
{
    public interface ITile : IPathNode
    {
        TileState TileState { get; set; }
        bool IsBlocked { get; set; }
        bool CanBuildingEntranceBePlacedOn();
    }
}