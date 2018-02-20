using SettlersEngine;

namespace CityBuilding
{
    public interface ITile : IPathNode
    {
        TileState TileState { get; set; }
        bool IsBlocked { get; set; }
    }
}