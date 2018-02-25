using CityBuilder.Buildings;
using CityBuilder.MapModel;
using CityBuilder.Util;

namespace CityBuilder.AreaWithBuildingFilling
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