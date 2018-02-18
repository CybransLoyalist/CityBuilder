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
        IList<IBuilding> Buildings { get; }
        IEnumerable<ITile> Tiles { get; }
        IEnumerable<ITile> GetNeighboursOf(ITile tile, NeighbourMode neighbourMode);
        IPoint GetLocationOf(ITile tile);
    }
}