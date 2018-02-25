using System;
using CityBuilder.MapModel.Tiles;
using CityBuilder.Util;

namespace CityBuilder.Buildings
{
    public class BuildingLocation
    {

        public BuildingLocation(Type type, ITile tile, Angle angle)
        {
            if (!type.IsSubclassOf(typeof(Building)))
            {
                throw new ArgumentException("Type must be a subclass of Building");
            }
            Type = type;
            Tile = tile;
            Angle = angle;
        }

        public ITile Tile { get; set; }
        public Angle Angle { get; set; }
        public Type Type { get; set; }

        public IBuilding Instantiate()
        {
            return Activator.CreateInstance(Type, Guid.NewGuid(), Angle) as IBuilding;
        }
    }
}