using System.Collections.Generic;
using CityBuilder.Map.Tiles;

namespace CityBuilder.Map
{
    public class EmptyAreaGroup
    {
        public IList<ITile> Tiles { get; set; }

        public EmptyAreaGroup()
        {
            Tiles = new List<ITile>();
        }

        public void Add(ITile tile)
        {
            Tiles.Add(tile);
        }

        public bool Contains(ITile tile)
        {
            return Tiles.Contains(tile);
        }
    }
}