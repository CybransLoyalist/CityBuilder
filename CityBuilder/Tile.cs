namespace CityBuilding
{
    public class Tile : ITile
    {
        public TileState TileState { get; set; }

        public bool IsWalkable()
        {
            return TileState != TileState.Blocked && TileState != TileState.Door && TileState != TileState.Full;
        }
    }
}