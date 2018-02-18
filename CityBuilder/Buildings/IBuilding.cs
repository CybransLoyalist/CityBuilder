using System;
using System.Collections.Generic;
using CityBuilding;

namespace CityBuilder.Buildings
{
    public interface IBuilding
    {
        Guid Guid { get; }
        IList<ITile> Tiles { get; }
        ITilePattern TilePattern { get; }
    }
}