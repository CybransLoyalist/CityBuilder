using System;
using System.Collections.Generic;

namespace CityBuilding
{
    public interface IBuilding
    {
        Guid Guid { get; }
        IList<ITile> Tiles { get; }
    }
}