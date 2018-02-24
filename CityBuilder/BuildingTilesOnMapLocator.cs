using System;
using System.Collections.Generic;
using System.Linq;
using CityBuilder.Buildings;
using CityBuilding;

namespace CityBuilder
{
    public class BuildingTilesOnMapLocator
    {
        public virtual void Locate(IMap map, IBuilding building, IPoint placingPointOnMap)
        {
            var tilesOfBuilding = new List<ITile>();
            foreach (var tilePattern in building.TilePatterns) //todo refactor with below
            {
                var buildingTilePoint = Transform(placingPointOnMap, tilePattern.Transformation, building.Angle);
                var tile = map[buildingTilePoint.X, buildingTilePoint.Y];

                tile.TileState = tilePattern.IsDoor ? TileState.Street : TileState.Full;
                if (!tilePattern.IsDoor)
                {
                    tilesOfBuilding.Add(tile);

                    map.SetBuildingAtTile(tile, building);
                }
            }

            map.AddBuilding(building, tilesOfBuilding.Where(a => a.TileState != TileState.Street));
        }

        public virtual void LocateVirtual(IMap map, IBuilding building, IPoint placingPointOnMap)
        {
            foreach (var tilePattern in building.TilePatterns) //todo refactor with below
            {
                var buildingTilePoint = Transform(placingPointOnMap, tilePattern.Transformation, building.Angle);
                var tile = map[buildingTilePoint.X, buildingTilePoint.Y];
                tile.IsBlocked = true;
            }
        }

        private static Point Transform(IPoint placingPointOnMap, IPoint transformation, Angle angle)
        {
            var rotatedTransformation = RotatePoint(transformation, angle);

            var x = placingPointOnMap.X + rotatedTransformation.X;
            var y = placingPointOnMap.Y + rotatedTransformation.Y;
            return new Point(x, y);
        }

        private static Point RotatePoint(IPoint pointToRotate, Angle angle)
        {
            switch (angle)
            {
                case Angle.Zero:
                    return new Point(pointToRotate.X, pointToRotate.Y);
                case Angle.Ninety:
                    return new Point(pointToRotate.Y, -pointToRotate.X);
                case Angle.OneHundredEighty:
                    return new Point(-pointToRotate.X, -pointToRotate.Y);
                case Angle.TwoHundredSeventy:
                    return new Point(-pointToRotate.Y, pointToRotate.X);
                default:
                    throw new Exception();
            }
        }

        public bool CanLocate(IMap map, IBuilding building, IPoint placingPointOnMap)
        {
            var tilesOfBuilding = new List<TilePatternLocation>();
            foreach (var tilePattern in building.TilePatterns)
            {
                var buildingTilePoint = Transform(placingPointOnMap, tilePattern.Transformation, building.Angle);
                if (buildingTilePoint.X < 0 || buildingTilePoint.X >= map.Width)
                {
                    return false;
                }
                if (buildingTilePoint.Y < 0 || buildingTilePoint.Y >= map.Height)
                {
                    return false;
                }
                var tile = map[buildingTilePoint.X, buildingTilePoint.Y];
                tilesOfBuilding.Add(new TilePatternLocation(tile, tilePattern));
            }

            var nonDoorTiles = tilesOfBuilding.Where(a => !a.TilePattern.IsDoor);
            var doorTile = tilesOfBuilding.First(a => a.TilePattern.IsDoor);
            return nonDoorTiles.All(a => a.Tile.TileState == TileState.Empty) && (
                       doorTile.Tile.TileState != TileState.Blocked &&
                       doorTile.Tile.TileState != TileState.Full);
        }

        private class TilePatternLocation
        {
            public TilePatternLocation(ITile tile, ITilePattern tilePattern)
            {
                Tile = tile;
                TilePattern = tilePattern;
            }

            public ITilePattern TilePattern { get; }
            public ITile Tile { get; }
        }
    }
}