using System;
using System.Collections.Generic;
using CityBuilder.Buildings;
using CityBuilding;

namespace CityBuilder
{
    public class BuildingTilesOnMapLocator
    {
        public virtual IEnumerable<ITile> Locate(IMap map, IBuilding building, Point placingPointOnMap, Angle angle)
        {
            foreach (var transformation in building.TilePattern.Transformations)
            {
                var buildingTilePoint = Transform(placingPointOnMap, transformation, angle);
                yield return map[buildingTilePoint.X, buildingTilePoint.Y];
            }
        }

        private static Point Transform(Point placingPointOnMap, Point transformation, Angle angle)
        {
            var rotatedTransformation = RotatePoint(transformation, angle);

            var x = placingPointOnMap.X + rotatedTransformation.X;
            var y = placingPointOnMap.Y + rotatedTransformation.Y;
            return new Point(x, y);
        }

        private static Point RotatePoint(Point pointToRotate, Angle angle)
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