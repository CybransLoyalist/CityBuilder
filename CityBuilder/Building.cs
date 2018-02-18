using System;
using System.Collections.Generic;

namespace CityBuilding
{
    public class Building : IBuilding
    {
        public Building(Guid guid)
        {
            Guid = guid;
            Tiles = new List<ITile>();
        }

        public Guid Guid { get; }
        public IList<ITile> Tiles { get; }
    }
}