using System.Collections.Generic;
using System.Linq;
using CityBuilder.Extensions;
using CityBuilder.MapModel.Tiles;
using CityBuilder.Util;

namespace CityBuilder.AreaWithBuildingFilling
{
    public class TileAnglesCombinations
    {
        private readonly Dictionary<ITile, List<Angle>> _items;
        public TileAnglesCombinations(IEnumerable<ITile> tiles)
        {
            _items = new Dictionary<ITile, List<Angle>>();
            var allAngles = new List<Angle> {Angle.Ninety, Angle.OneHundredEighty, Angle.TwoHundredSeventy, Angle.Zero};
            foreach (var tile in tiles.Where(a => a.CanBuildingEntranceBePlacedOn()))
            {
                _items.Add(tile, allAngles.ToList());
            }
        }

        public bool Any()
        {
            return _items.Any();
        }

        public virtual ITile GetRandomTile()
        {
            return _items.Select(a => a.Key).ToList().Random();
        }

        public virtual Angle GetRandomAngleForTile(ITile tile)
        {
            return _items[tile].Random();
        }

        public void RemoveAngleForTile(Angle angle, ITile tile)
        {
            _items[tile].Remove(angle);
            if (_items[tile].Count == 0)
            {
                _items.Remove(tile);
            }
        }
    }
}