using System;
using CityBuilder.Buildings;
using CityBuilding;

namespace CityBuilder
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