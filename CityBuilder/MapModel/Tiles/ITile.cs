using AStarAlgorithm;

namespace CityBuilder.MapModel.Tiles
{
    public interface ITile : IPathNode
    {
        TileState TileState { get; set; }
        bool IsTemporarilyBlocked { get; set; }
        bool CanBuildingEntranceBePlacedOn();
    }
}