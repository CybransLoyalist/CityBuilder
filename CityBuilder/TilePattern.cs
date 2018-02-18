using System.Collections.Generic;
using CityBuilder.Buildings;

namespace CityBuilding
{
    public class TilePattern : ITilePattern
    {
        public TilePattern(IPoint point, params Direction[] possibleDoorDirections)
        {
            Transformation = point;
            PossibleDoorDirections = possibleDoorDirections;
        }

        public IPoint Transformation { get; set; }
        public IList<Direction> PossibleDoorDirections { get; set; }
    }
}