using System.Linq;
using CityBuilder.Map;
using CityBuilder.Map.Tiles;

namespace CityBuilder.AreaWithBuildingFilling
{
    public class ClosestStreetFinder
    {
        public virtual IPoint Find(IMap map, IPoint start)
        {
            var streetTiles = map.AllTiles.Where(a => a.TileState == TileState.Street).ToList();

            var closestStreet = map.GetLocationOf(streetTiles.First());
            var minDistance = Point.Distance(start, closestStreet);
            for (int i = 1; i < streetTiles.Count; i++)
            {
                var currStreet = map.GetLocationOf(streetTiles[i]);
                var currDistance = Point.Distance(start, currStreet);
                if (Point.Distance(start, currStreet) < minDistance)
                {
                    closestStreet = currStreet;
                    minDistance = currDistance;
                }
            }

            return closestStreet;
        }
    }
}