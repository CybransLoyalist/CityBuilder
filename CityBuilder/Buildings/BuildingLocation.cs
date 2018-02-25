using System;
using CityBuilder.Map.Tiles;
using CityBuilder.Util;

namespace CityBuilder.Buildings
{
    public class BuildingLocation
    {
        public ITile Tile { get; set; }
        public Angle Angle { get; set; }
        public Type Type { get; set; }

        public IBuilding Instantiate()
        {
            return Activator.CreateInstance(Type, Guid.NewGuid(), Angle) as IBuilding;
        }
    }
}