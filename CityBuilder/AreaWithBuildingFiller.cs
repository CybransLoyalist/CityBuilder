using CityBuilder.Buildings;
using CityBuilding;

namespace CityBuilder
{
    public class AreaWithBuildingFiller
    {
        private readonly RandomBuildingLocationGenerator _randomBuildingLocationGenerator;
        private readonly BuildingOnMapIfPossibleLocator _buildingOnMapIfPossibleLocator;
        private readonly MapFillingParametersCalculator _mapFillingParametersCalculator;
        private readonly IMap _map;

        public AreaWithBuildingFiller(
            IMap map,
            RandomBuildingLocationGenerator randomBuildingLocationGenerator,
            BuildingOnMapIfPossibleLocator buildingOnMapIfPossibleLocator,
            MapFillingParametersCalculator mapFillingParametersCalculator)
        {
            _map = map;
            _randomBuildingLocationGenerator = randomBuildingLocationGenerator;
            _buildingOnMapIfPossibleLocator = buildingOnMapIfPossibleLocator;
            _mapFillingParametersCalculator = mapFillingParametersCalculator;
        }

        public void Fill(EmptyAreaGroup emptyAreaGroup)
        {
            var buildingTypesBySize = BuildingTypesProvider.GetGroupedBySize();

            for (var index = 0; index < buildingTypesBySize.Count; ++index)
            {
                var buildingTypesOfEqualSize = buildingTypesBySize[index];
                var tileAnglesCombinations = new TileAnglesCombinations(emptyAreaGroup.Tiles); 
                while (tileAnglesCombinations.Any())
                {
                    var buildingLocation = _randomBuildingLocationGenerator.Generate(tileAnglesCombinations, buildingTypesOfEqualSize);
                    var building = buildingLocation.Instantiate();
                    var placingPointOnMap = _map.GetLocationOf(buildingLocation.Tile);

                    tileAnglesCombinations.RemoveAngleForTile(buildingLocation.Angle, buildingLocation.Tile);

                    _buildingOnMapIfPossibleLocator.TryLocate(building, placingPointOnMap);
                    if (ShallFinishWithCurrentBuildingType(emptyAreaGroup, index))
                    {
                        break;
                    }
                }

            }

            var mapParameters = new MapFillingParametersCalculator().Calculate(_map);

        }

        private bool ShallFinishWithCurrentBuildingType(EmptyAreaGroup emptyAreaGroup, int index)
        {
            return _mapFillingParametersCalculator.GetMapFillingFactor(emptyAreaGroup.Tiles) > (decimal)(index + 1) / 5;
        }
    }
}