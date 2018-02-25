using System.Collections.Generic;
using CityBuilder.Buildings;
using CityBuilder.MapModel.Tiles;

namespace CityBuilder.MapModel
{
    public interface IMap
    {
        int Width { get; }
        int Height { get; }
        ITile this[int x, int y] { get; }
        IEnumerable<ITile> AllTiles { get; }
        ITile[,] GetTilesArray();
        IEnumerable<ITile> GetTilesOfBuilding(IBuilding building);
        IEnumerable<IBuilding> GetBuildings();
        IEnumerable<ITile> GetNeighboursOf(ITile tile, NeighbourMode neighbourMode);
        IPoint GetLocationOf(ITile tile);
        IBuilding GetBuildingAtTile(ITile tile);
        void AddBuilding(IBuilding building, IList<ITile> tiles);
        void UnblockAllTemporarilyBlockedTiles();
    }
}