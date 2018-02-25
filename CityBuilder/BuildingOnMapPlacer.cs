using CityBuilder.Buildings;
using CityBuilding;

namespace CityBuilder
{
    public class BuildingOnMapPlacer //todo this class is unnecessary
    {
        private readonly BuildingOnMapLocator _buildingOnMapLocator;

        public BuildingOnMapPlacer(BuildingOnMapLocator buildingOnMapLocator)
        {
            _buildingOnMapLocator = buildingOnMapLocator;
        }

        public virtual void PlaceBuildingOn(IMap map, IBuilding building, Point placingPointOnMap, Angle angle)
        {
            _buildingOnMapLocator.Locate(map, building, placingPointOnMap);
        }
    }
}