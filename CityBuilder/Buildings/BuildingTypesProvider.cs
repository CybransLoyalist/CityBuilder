﻿using System;
using System.Collections.Generic;
using System.Linq;
using CityBuilder.Buildings.Patterns;

namespace CityBuilder.Buildings
{
    public static class BuildingTypesProvider
    {
        public static IList<BuildingType> BuildingTypes { get; } = new List<BuildingType>
        {
            AddNewBuildingType(SingleTileBuilding.Pattern, typeof(SingleTileBuilding)),
            AddNewBuildingType(ShortOrthogonalBuilding.Pattern, typeof(ShortOrthogonalBuilding)),
            AddNewBuildingType(LongOrthogonalBuilding.Pattern, typeof(LongOrthogonalBuilding)),
            AddNewBuildingType(SquareBuilding.Pattern, typeof(SquareBuilding)),
            AddNewBuildingType(CourtyardBuilding.Pattern, typeof(CourtyardBuilding)),
        };

        private static BuildingType AddNewBuildingType(IList<ITilePattern> pattern, Type type)
        {
            return new BuildingType {OccupiedTilesCount = pattern.Count - 1, Type = type};
        }

        public static IList<Tuple<int, IList<Type>>> GetGroupedBySize()
        {
            return BuildingTypes.GroupBy(bt => bt.OccupiedTilesCount).
                OrderByDescending(grouping => grouping.Key).
                Select(grouping => new Tuple<int, IList<Type>>(grouping.Key, grouping.Select(b => b.Type).ToList())).ToList();
        }
    }
}