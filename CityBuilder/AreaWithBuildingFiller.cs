using System;
using System.Collections.Generic;
using System.Linq;
using CityBuilder.Buildings;
using CityBuilder.Extensions;
using CityBuilder.Util;
using CityBuilding;
using SettlersEngine;

namespace CityBuilder
{
    public class PathToStreetDrawer
    {
        public void DrawPathToEdge(IMap map, IPoint placingPointOnMap)
        {
            var astar = new SpatialAStar<ITile>(map.GetTilesArray());
            var result = astar.Search(placingPointOnMap, map.GetLocationOf(map[20, 20]));
            foreach (var tile in result)
            {
                tile.TileState = TileState.Street;
            }
        }
    }
    public class AreaWithBuildingFiller
    {
        private readonly BuildingTilesOnMapLocator _buildingTilesOnMapLocator;
        private readonly PathToStreetDrawer _pathToStreetDrawer;

        public AreaWithBuildingFiller(
            BuildingTilesOnMapLocator buildingTilesOnMapLocator,
            PathToStreetDrawer pathToStreetDrawer)
        {
            _buildingTilesOnMapLocator = buildingTilesOnMapLocator;
            _pathToStreetDrawer = pathToStreetDrawer;
        }

        public void Fill(IMap map, EmptyAreaGroup emptyAreaGroup)
        {
            IPoint placingPointOnMap;
            IBuilding building;
            bool canLocate;
            Dictionary<ITile, List<Angle>> tilesToBeChecked = new Dictionary<ITile, List<Angle>>();


            foreach (var tile in emptyAreaGroup.Tiles)
            {
                tilesToBeChecked.Add(tile,
                    new List<Angle> {Angle.Ninety, Angle.OneHundredEighty, Angle.TwoHundredSeventy, Angle.Zero});
            }

            do
            {
                var randomTile = tilesToBeChecked.Select(a => a.Key).ToList().Random();
                var angle = tilesToBeChecked[randomTile].Random();
                building = new LongOrthogonalBuilding(Guid.NewGuid(), angle);
                placingPointOnMap = map.GetLocationOf(randomTile);

                tilesToBeChecked[randomTile].Remove(angle);
                if (tilesToBeChecked[randomTile].Count == 0)
                {
                    tilesToBeChecked.Remove(randomTile);
                }

                canLocate = _buildingTilesOnMapLocator.CanLocate(map, building, placingPointOnMap);
            } while (!canLocate && tilesToBeChecked.Any());

            if (canLocate)
            {
                _buildingTilesOnMapLocator.Locate(map, building, placingPointOnMap);
                _pathToStreetDrawer.DrawPathToEdge(map, placingPointOnMap);
                map.LocationsOfBuildings.Add(placingPointOnMap, building);
            }
        }
    }
}