using System.Collections.Generic;
using System.Linq;
using CityBuilder;

namespace CityBuilding
{
    public class EmptyAreaGroupGetter
    {
        public IList<EmptyAreaGroup> Get(IMap map)
        {

            var emptyTiles = map.AllTiles.Where(a => a.TileState == TileState.Empty).ToList();
            return !emptyTiles.Any() ?
                new List<EmptyAreaGroup>() :
                BuildGroups(map, emptyTiles);
        }

        private IList<EmptyAreaGroup> BuildGroups(IMap map, IList<ITile> emptyTiles)
        {
            IList<EmptyAreaGroup> result = new List<EmptyAreaGroup>();
            var currentTile = GetTileNotBelongingToAnyGroup(result, emptyTiles);
            while (currentTile != null)
            {
                ProccessAllNeighboursOfTile(map, result, currentTile);
                currentTile = GetTileNotBelongingToAnyGroup(result, emptyTiles);
            }

            return result;
        }

        private static void ProccessAllNeighboursOfTile(IMap map, IList<EmptyAreaGroup> result, ITile currentTile)
        {
            var currentGroup = new EmptyAreaGroup();
            currentGroup.Add(currentTile);

            AddNeighboursToGroup(map, currentTile, currentGroup);
            result.Add(currentGroup);
        }

        private static ITile GetTileNotBelongingToAnyGroup(IList<EmptyAreaGroup> result, IList<ITile> emptyTiles)
        {
            return emptyTiles.FirstOrDefault(a => !result.Any(g => g.Contains(a)));
        }

        private static void AddNeighboursToGroup(IMap map, ITile currentTile, EmptyAreaGroup currentGroup)
        {
            var neighboursOfCurrentTile = map.GetNeighboursOf(currentTile, NeighbourMode.Orthogonal).Where(a => a.TileState == TileState.Empty);
            foreach (var neighbour in neighboursOfCurrentTile)
            {
                if (!currentGroup.Tiles.Contains(neighbour))
                {
                    currentGroup.Add(neighbour);

                    AddNeighboursToGroup(map, neighbour, currentGroup);
                }
            }
        }
    }
}