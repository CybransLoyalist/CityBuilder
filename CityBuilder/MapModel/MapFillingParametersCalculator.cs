using System;
using System.Collections.Generic;
using System.Linq;
using CityBuilder.Buildings;
using CityBuilder.MapModel.Tiles;

namespace CityBuilder.MapModel
{
    public class MapFillingParametersCalculator
    {
        public virtual MapFillingParameters Calculate(IMap map)
        {
            var mapFillFactor = GetMapFillingFactor(map.AllTiles.ToList());

            var buildingsTypesCount = new Dictionary<Type, int>();
            foreach (var buildingType in BuildingTypesProvider.BuildingTypes)
            {
                var count = map.GetBuildings().Count(a => a.GetType() == buildingType.Type);
                buildingsTypesCount.Add(buildingType.Type, count);
            }

            return new MapFillingParameters
            {
                MapFillFactor = mapFillFactor,
                BuildingTypesCounts = buildingsTypesCount
            };
        }

        public decimal GetMapFillingFactor(IList<ITile> tiles)
        {
            return (decimal) tiles.Count(a => a.TileState == TileState.Full) / tiles.Count;
        }
    }
}