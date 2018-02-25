namespace CityBuilder.MapModel.Tiles
{
    public class Tile : ITile
    {
        public TileState TileState { get; set; }
        public bool IsTemporarilyBlocked { get; set; }
        public bool CanBuildingEntranceBePlacedOn()
        {
            return TileState != TileState.Blocked && TileState != TileState.Full;
        }

        public bool IsWalkable()
        {
            return !IsTemporarilyBlocked && TileState != TileState.Blocked &&  TileState != TileState.Full;
        }
    }
}