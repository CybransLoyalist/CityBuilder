using System.Collections.Generic;
using CityBuilder.Buildings;

namespace CityBuilding
{
    public interface ITilePattern
    {
        IPoint Transformation { get; set; }
        IList<Direction> PossibleDoorDirections { get; set; }
    }
}