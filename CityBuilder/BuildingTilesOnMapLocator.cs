using System;
using System.Collections.Generic;
using CityBuilder.Buildings;
using CityBuilding;

namespace CityBuilder
{
    public class BuildingTilesOnMapLocator
    {
        public virtual void Locate(IMap map, IBuilding building, IPoint placingPointOnMap)
        {
            var tilesOfBuilding = new List<ITile>();
            foreach (var tilePattern in building.TilePatterns)
            {
                var buildingTilePoint = Transform(placingPointOnMap, tilePattern.Transformation, building.Angle);
                var tile = map[buildingTilePoint.X, buildingTilePoint.Y];
                tilesOfBuilding.Add(tile);
                tile.TileState = tilePattern.IsDoor ? TileState.Door : TileState.Full;
            }
            map.BuildingsTiles.Add(building, tilesOfBuilding);
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
    }
}