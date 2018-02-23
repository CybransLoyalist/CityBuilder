using System;
using System.Collections.Generic;

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
    }
}