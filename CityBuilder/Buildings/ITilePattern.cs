using System.Collections.Generic;

namespace CityBuilding
{
    public interface ITilePattern
    {
        IList<Point> Transformations { get; }
    }
}