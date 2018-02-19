using System.Collections.Generic;
using System.Drawing;
using CityBuilder.Buildings;

namespace CityBuilding
{

    public interface IMap
    {
        int Width { get;  }
        int Height { get;  }
        ITile this[int x, int y] { get; }
        Dictionary<IPoint, IBuilding> LocationsOfBuildings { get; }
        IEnumerable<ITile> Tiles { get; }
        IDictionary<IBuilding, IEnumerable<ITile>> BuildingsTiles { get; set; }
        IEnumerable<ITile> GetNeighboursOf(ITile tile, NeighbourMode neighbourMode);
        IPoint GetLocationOf(ITile tile);
        IBuilding GetBuildingAtTile(ITile tile);
    }
}