using System;
using System.Collections.Generic;
using CityBuilding;

namespace CityBuilder.Buildings
{
    public abstract class Building : IBuilding
    {
        protected Building(Guid guid)
        {
            Guid = guid;
            Tiles = new List<ITile>();
        }

        public Guid Guid { get; }
        public IList<ITile> Tiles { get; }
        public abstract ITilePattern TilePattern { get; }
    }
}