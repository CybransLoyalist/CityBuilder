using System;
using System.Collections.Generic;
using System.Linq;
using CityBuilder.Buildings;
using CityBuilder.Buildings.Patterns;
using CityBuilder.Extensions;
using CityBuilding;
using SettlersEngine;

namespace CityBuilder
{

    public class AreaWithBuildingFiller
    {
        private readonly BuildingTilesOnMapLocator _buildingTilesOnMapLocator;

        public AreaWithBuildingFiller(
            BuildingTilesOnMapLocator buildingTilesOnMapLocator)
        {
            _buildingTilesOnMapLocator = buildingTilesOnMapLocator;
        }

        public void Fill(IMap map, EmptyAreaGroup emptyAreaGroup)
        {
            var buildingTypesBySize = BuildingTypesProvider.GetGroupedBySize();
            var astar = new SpatialAStar<ITile>(map.GetTilesArray());

            for (var index = 0; index < buildingTypesBySize.Count; ++index)
            {
                var buildingTypesOfEqualSize = buildingTypesBySize[index];
                var tilesAndAnglesToBeChecked = GetTilesAndAnglesToBeCheckedIfNewBuildingCanBePlacedOnThem(emptyAreaGroup);
                while (tilesAndAnglesToBeChecked.Any())
                {
                    var randomTile = tilesAndAnglesToBeChecked.Select(a => a.Key).ToList().Random();
                    var angle = tilesAndAnglesToBeChecked[randomTile].Random();
                    var type = buildingTypesOfEqualSize.Item2.Random();
                    var building = Activator.CreateInstance(type, Guid.NewGuid(), angle) as IBuilding;

                    var placingPointOnMap = map.GetLocationOf(randomTile);

                    tilesAndAnglesToBeChecked[randomTile].Remove(angle);
                    if (tilesAndAnglesToBeChecked[randomTile].Count == 0)
                    {
                        tilesAndAnglesToBeChecked.Remove(randomTile);
                    }

                    var canLocate = _buildingTilesOnMapLocator.CanLocate(map, building, placingPointOnMap);

                    if (canLocate)
                    {
                        _buildingTilesOnMapLocator.BlockBuildingArea(map, building, placingPointOnMap);
       

                        var closestStreet = GetClosestStreet(placingPointOnMap, map);

                        var result = astar.Search(placingPointOnMap, closestStreet);
                        if (result != null)
                        {
                            map.UnblockAllTiles();
                           
                            _buildingTilesOnMapLocator.Locate(map, building, placingPointOnMap);
                            foreach (var tile in result)
                            {
                                tile.TileState = TileState.Street;
                            }
                        }
                    }


                    var mapFillFactor = (decimal)emptyAreaGroup.Tiles.Count(a => a.TileState == TileState.Full) /
                                        (decimal)emptyAreaGroup.Tiles.Count();

                    if (mapFillFactor > (decimal)(index + 1) / 4)
                    {
                        break;
                    }
                }

            }

            var mapFillFactor2 = (decimal)emptyAreaGroup.Tiles.Count(a => a.TileState == TileState.Full) /
                                (decimal)emptyAreaGroup.Tiles.Count();

            var courtyardbuildings = map.GetBuildings().Count(a => a is CourtyardBuilding);
            var longL = map.GetBuildings().Count(a => a is LongOrthogonalBuilding);
            var square = map.GetBuildings().Count(a => a is SquareBuilding);
            var shortL = map.GetBuildings().Count(a => a is ShortOrthogonalBuilding);
            var singleSq = map.GetBuildings().Count(a => a is SingleTileBuilding);
        }

        private static Dictionary<ITile, List<Angle>> GetTilesAndAnglesToBeCheckedIfNewBuildingCanBePlacedOnThem(EmptyAreaGroup emptyAreaGroup)
        {
            Dictionary<ITile, List<Angle>> tilesToBeChecked = new Dictionary<ITile, List<Angle>>();
            foreach (var tile in emptyAreaGroup.Tiles.Where(a => a.CanBuildingEntranceBePlacedOn()))
            {
                tilesToBeChecked.Add(tile,
                    new List<Angle> { Angle.Ninety, Angle.OneHundredEighty, Angle.TwoHundredSeventy, Angle.Zero });
            }

            return tilesToBeChecked;
        }

        private IPoint GetClosestStreet(IPoint start, IMap map)
        {
            var streetTiles = map.AllTiles.Where(a => a.TileState == TileState.Street).ToList();

            var closestStreet = map.GetLocationOf(streetTiles.First());
            var minDistance = Point.Distance(start, closestStreet);
            for (int i = 1; i < streetTiles.Count(); i++)
            {
                var currStreet = map.GetLocationOf(streetTiles[i]);
                var currDistance = Point.Distance(start, currStreet);
                if (Point.Distance(start, currStreet) < minDistance)
                {
                    closestStreet = currStreet;
                    minDistance = currDistance;
                }
            }

            return closestStreet;
        }
    }
}