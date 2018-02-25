using CityBuilder.MapModel;

namespace CityBuilder.Buildings
{
    public class DoorTilePattern : TilePattern
    {
        public DoorTilePattern() : base(new Point(0,0))
        {
        }

        public override bool IsDoor => true;
    }
}