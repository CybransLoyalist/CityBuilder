using System.Collections.Generic;
using AStarAlgorithm;
using CityBuilder.Buildings;
using CityBuilder.Map;
using CityBuilder.Map.Tiles;
using IPoint = CityBuilder.Map.IPoint;

namespace CityBuilder.AreaWithBuildingFilling
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
            var pathToNearestStreet = _astar.Search(
                new AStarAlgorithm.Point(placingPointOnMap.X, placingPointOnMap.Y),
                new AStarAlgorithm.Point(closestStreet.X, closestStreet.Y),
                NeighbourClassification.ByWall);

            _map.UnblockAllTiles();

            return pathToNearestStreet;
        }

    }
}