using System.Collections.Generic;

namespace CityBuilding
{
    public class TilePattern : ITilePattern
    {
        public IList<Point> Transformations { get; set; }
    }
}