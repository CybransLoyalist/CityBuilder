namespace CityBuilding
{
    public class Tile : ITile
    {
        public TileState TileState { get; set; }
        public bool IsBlocked { get; set; }

        public bool IsWalkable()
        {
            return !IsBlocked && TileState != TileState.Blocked &&  TileState != TileState.Full;
        }
    }
}