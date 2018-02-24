﻿namespace CityBuilding
{
    public class Tile : ITile
    {
        public TileState TileState { get; set; }
        public bool IsBlocked { get; set; }
        public bool CanBuildingEntranceBePlacedOn()
        {
            return TileState != TileState.Blocked && TileState != TileState.Full;
        }

        public bool IsWalkable()
        {
            return !IsBlocked && TileState != TileState.Blocked &&  TileState != TileState.Full;
        }
    }
}