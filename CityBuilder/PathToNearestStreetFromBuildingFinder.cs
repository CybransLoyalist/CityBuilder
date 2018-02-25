using System.Collections.Generic;
using CityBuilder.Buildings;
using CityBuilding;
using SettlersEngine;

namespace CityBuilder
{
    public class PathToNearestStreetFromBuildingFinder
    {
        private readonly IMap _map;
        private readonly BuildingOnMapLocator _buildingOnMapLocator;
        private readonly ClosestStreetFinder _closestStreetFinder;
        private readonly SpatialAStar<ITile> _astar;

        public PathToNearestStreetFromBuildingFinder(
            IMap map,
            BuildingOnMapLocator buildingOnMapLocator,
            ClosestStreetFinder closestStreetFinder)
        {
            _map = map;
            _astar = new SpatialAStar<ITile>(_map.GetTilesArray());
            _buildingOnMapLocator = buildingOnMapLocator;
            _closestStreetFinder = closestStreetFinder;
        }

        public LinkedList<ITile> Find(IBuilding building, IPoint placingPointOnMap)
        {
            _buildingOnMapLocator.BlockBuildingArea(_map, building, placingPointOnMap);

            var closestStreet = _closestStreetFinder.Find(_map, placingPointOnMap);
            var pathToNearestStreet = _astar.Search(placingPointOnMap, closestStreet);

            _map.UnblockAllTiles();

            return pathToNearestStreet;
        }

    }
}