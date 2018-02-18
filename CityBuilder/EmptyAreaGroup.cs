using System.Collections.Generic;

namespace CityBuilding
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