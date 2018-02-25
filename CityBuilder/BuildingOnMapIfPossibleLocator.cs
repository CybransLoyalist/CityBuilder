using CityBuilder.Buildings;
using CityBuilding;

namespace CityBuilder
{
    public class BuildingOnMapIfPossibleLocator
    {
        private readonly BuildingOnMapLocator _buildingOnMapLocator;
        private readonly IMap _map;
        private readonly PathToNearestStreetFromBuildingFinder _pathToNearestStreetFromBuildingFinder;

        public BuildingOnMapIfPossibleLocator(
            IMap map,
            BuildingOnMapLocator buildingOnMapLocator,
            PathToNearestStreetFromBuildingFinder pathToNearestStreetFromBuildingFinder)
        {
            _map = map;
            _buildingOnMapLocator = buildingOnMapLocator;
            _pathToNearestStreetFromBuildingFinder = pathToNearestStreetFromBuildingFinder;
        }

        public void TryLocate(
            IBuilding building,
            IPoint placingPointOnMap)
        {
            var canLocate = _buildingOnMapLocator.CanLocate(_map, building, placingPointOnMap);

            if (canLocate)
            {
                var pathToNearestStreet = _pathToNearestStreetFromBuildingFinder.Find(building, placingPointOnMap);

                if (pathToNearestStreet != null)
                {
                    _buildingOnMapLocator.Locate(_map, building, placingPointOnMap);

                    foreach (var tile in pathToNearestStreet)
                    {
                        tile.TileState = TileState.Street;
                    }
                }
            }
        }
    }
}