using System;
using System.Collections.Generic;
using CityBuilding;

namespace CityBuilder.Buildings
{
    public class Door : IDoor
    {
        public Door(ITilePattern tilePattern, Direction direction)
        {
            TilePattern = tilePattern;
            Direction = direction;
        }

        public ITilePattern TilePattern { get; }
        public Direction Direction { get; }
    }
    public abstract class Building : IBuilding
    {
        protected Building(Guid guid, Angle angle)
        {
            Guid = guid;
            Angle = angle;
            Door = GetRandomDoor();
        }

        private IDoor GetRandomDoor()
        {
            var tilePattern = TilePatterns.Random();
            var direction = tilePattern.PossibleDoorDirections.Random();
            return new Door(tilePattern, direction);
        }

        public Guid Guid { get; }
        public abstract IList<ITilePattern> TilePatterns { get; }
        public IDoor Door { get;  }
        public Angle Angle { get;  }
    }
}