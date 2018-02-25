using System;
using System.Collections.Generic;
using CityBuilder.Extensions;

namespace CityBuilder
{
    public class RandomBuildingLocationGenerator
    {
        public BuildingLocation Generate(
            TileAnglesCombinations tileAnglesCombinations, 
            Tuple<int, IList<Type>> buildingTypesOfEqualSize)
        {
            var randomTile = tileAnglesCombinations.GetRandomTile();
            var angle = tileAnglesCombinations.GetRandomAngleForTile(randomTile); 
            var type = buildingTypesOfEqualSize.Item2.Random();

            return new BuildingLocation
            {
                Angle = angle,
                Tile = randomTile,
                Type = type
            };
        }
    }
}