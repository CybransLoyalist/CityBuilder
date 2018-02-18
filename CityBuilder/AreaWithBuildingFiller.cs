using System;
using System.Collections.Generic;
using System.Linq;
using CityBuilder.Buildings;
using CityBuilding;

namespace CityBuilder
{
    public static class RandomEnum
    {
        public static T Get<T>()
        {
            var values = Enum.GetValues(typeof(T));
            Random random = new Random();
            T result = (T) values.GetValue(random.Next(values.Length));
            return result;
        }
    }

    public static class ListExtensions
    {
        public static T Random<T>(this IList<T> list)
        {
            var max = list.Count - 1;
            var index = new Random().Next(max);
            return list[index];
        }
    }

    public class AreaWithBuildingFiller
    {
        public void Fill(IMap map, EmptyAreaGroup emptyAreaGroup)
        {
            var angle = RandomEnum.Get<Angle>();
            var building = new ShortOrthogonalBuilding(Guid.NewGuid(), angle);

            var placingPointOnMap = map.GetLocationOf(emptyAreaGroup.Tiles.Random());

            var tiles = new BuildingTilesOnMapLocator().Locate(map, building, placingPointOnMap);
            ;
            foreach (var tile in tiles)
            {
                tile.TileState = TileState.Full;
            }
            map.LocationsOfBuildings.Add(placingPointOnMap, building);
        }
    }
}