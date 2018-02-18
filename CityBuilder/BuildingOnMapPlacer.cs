using CityBuilder.Buildings;
using CityBuilding;

namespace CityBuilder
{
    public class BuildingOnMapPlacer
    {
        private readonly BuildingTilesOnMapLocator _buildingTilesOnMapLocator;

        public BuildingOnMapPlacer(BuildingTilesOnMapLocator buildingTilesOnMapLocator)
        {
            _buildingTilesOnMapLocator = buildingTilesOnMapLocator;
        }

        public virtual void PlaceBuildingOn(IMap map, IBuilding building, Point placingPointOnMap, Angle angle)
        {
            map.LocationsOfBuildings.Add(placingPointOnMap, building);

            var buildingTiles = _buildingTilesOnMapLocator.Locate(map, building, placingPointOnMap);
            foreach (var buildingTile in buildingTiles)
            {
                buildingTile.TileState = TileState.Full;
               // building.Tiles.Add(buildingTile);
            }
        }
    }
}