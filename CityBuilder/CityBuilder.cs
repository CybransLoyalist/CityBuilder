
using System;
using System.Collections.Generic;
using System.Linq;

namespace CityBuilding
{
    public class CityBuilder
    {
        private readonly EmptyAreaGroupGetter _emptyAreaGroupGetter;

        public CityBuilder(EmptyAreaGroupGetter emptyAreaGroupGetter)
        {
            _emptyAreaGroupGetter = emptyAreaGroupGetter;
        }

        public virtual void FillMap(IMap initialMap)
        {
            var building = new Building(Guid.NewGuid());


            var emptyAreas = _emptyAreaGroupGetter.Get(initialMap);

            var streets = CreateStreets(initialMap, emptyAreas);
        }

        private IEnumerable<ITile> CreateStreets(IMap map, IList<EmptyAreaGroup> emptyAreas)
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
