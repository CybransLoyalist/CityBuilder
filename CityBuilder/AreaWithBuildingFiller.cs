using System;
using System.Collections.Generic;
using System.Linq;
using CityBuilder.Buildings;
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
            for (var index = 0; index < Building.Types.Count; index++)
            {
                var buildingType = Building.Types[index];
                Dictionary<ITile, List<Angle>> tilesToBeChecked = new Dictionary<ITile, List<Angle>>();
                foreach (var tile in emptyAreaGroup.Tiles.Where(
                    a => a.TileState != TileState.Blocked && a.TileState != TileState.Full))
                {
                    tilesToBeChecked.Add(tile,
                        new List<Angle> {Angle.Ninety, Angle.OneHundredEighty, Angle.TwoHundredSeventy, Angle.Zero});
                }
                while (tilesToBeChecked.Any())
                {
                    var randomTile = tilesToBeChecked.Select(a => a.Key).ToList().Random();
                    var angle = tilesToBeChecked[randomTile].Random();
                    var type = buildingType.Random();
                    var building = Activator.CreateInstance(type, Guid.NewGuid(), angle) as IBuilding;


                    var placingPointOnMap = map.GetLocationOf(randomTile);

                    tilesToBeChecked[randomTile].Remove(angle);
                    if (tilesToBeChecked[randomTile].Count == 0)
                    {
                        tilesToBeChecked.Remove(randomTile);
                    }

                    var canLocate = _buildingTilesOnMapLocator.CanLocate(map, building, placingPointOnMap);

                    if (canLocate)
                    {
                        _buildingTilesOnMapLocator.LocateVirtual(map, building, placingPointOnMap);
                        var astar = new SpatialAStar<ITile>(map.GetTilesArray());

                        var closestStreet = GetClosestStreet(placingPointOnMap, map);

                        var result = astar.Search(placingPointOnMap, closestStreet);
                        if (result != null)
                        {
                            foreach (var tile in map.AllTiles.Where(a => a.IsBlocked))
                            {
                                tile.IsBlocked = false;
                            }
                            _buildingTilesOnMapLocator.Locate(map, building, placingPointOnMap);
                            foreach (var tile in result)
                            {
                                tile.TileState = TileState.Street;
                            }
                        }
                    }


                    var mapFillFactor = (decimal)emptyAreaGroup.Tiles.Count(a => a.TileState == TileState.Full) /
                                        (decimal)emptyAreaGroup.Tiles.Count();

                    if (mapFillFactor > (decimal)(index + 1)/ 4)
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

//    public class RandomBuildingCreator
//    {
//        public IBuilding Create(Angle angle)
//        {
//            var type = Building.Types.Random();
//
//            return Activator.CreateInstance(type, Guid.NewGuid(), angle) as IBuilding;
//        }
//    }
}