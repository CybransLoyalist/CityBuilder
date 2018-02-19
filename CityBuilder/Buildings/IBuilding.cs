using System;
using System.Collections.Generic;
using System.ComponentModel;
using CityBuilding;

namespace CityBuilder.Buildings
{
    public interface IBuilding
    {
        Guid Guid { get; }
        IList<ITilePattern> TilePatterns { get; }
        Angle Angle { get; }
    }
}