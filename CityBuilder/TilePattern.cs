using System.Collections.Generic;
using CityBuilder.Buildings;

namespace CityBuilding
{
    public class TilePattern : ITilePattern
    {
        public TilePattern(IPoint point) : this(point, false)
        {
        }

        public TilePattern(IPoint point, bool isDoor)
        {

            Transformation = point;
            IsDoor = isDoor;
        }

        public IPoint Transformation { get; set; }
        public bool IsDoor { get; set; }
    }
}