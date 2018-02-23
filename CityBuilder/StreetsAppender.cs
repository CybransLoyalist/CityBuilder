using System.Collections.Generic;
using System.Linq;
using CityBuilder;

namespace CityBuilding
{
    public class StreetsAppender
    {
        public virtual IEnumerable<ITile> AppendStreets(IMap map, IList<EmptyAreaGroup> emptyAreas)
        {
            var result = new List<ITile>();
            foreach (var emptyAreaGroup in emptyAreas)
            {
                foreach (var tile in emptyAreaGroup.Tiles.Where(a => a.TileState == TileState.Empty))
                {
                    foreach (var neighbour in map.GetNeighboursOf(tile, NeighbourMode.All))
                    {
                        if (!emptyAreaGroup.Tiles.Contains(neighbour))
                        {
                            neighbour.TileState = TileState.Street;
                            result.Add(neighbour);
                        }
                    }
                }
            }

            foreach (var emptyAreaGroup in emptyAreas)
            {
                emptyAreaGroup.Tiles = emptyAreaGroup.Tiles.Where(a => a.TileState == TileState.Empty).ToList();
            }
            return result;
        }
    }
}