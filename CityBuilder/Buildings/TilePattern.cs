using CityBuilder.MapModel;

namespace CityBuilder.Buildings
{
    public class TilePattern : ITilePattern
    {

        public TilePattern(IPoint point)
        {

            Transformation = point;
        }

        public IPoint Transformation { get; set; }
        public virtual bool IsDoor => false;
    }
}