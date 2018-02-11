
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace CityBuilding
{
    public enum TileState
    {
        Blocked,
        Empty,
        Full
    }

    public interface ITile
    {
        TileState TileState { get; set; }
    }

    public interface IBuilding
    {
        Guid Guid { get; }
        IList<ITile> Tiles { get; }
    }

    public class Building : IBuilding
    {
        public Building(Guid guid)
        {
            Guid = guid;
            Tiles = new List<ITile>();
        }

        public Guid Guid { get; }
        public IList<ITile> Tiles { get; }
    }

    public interface IMap
    {
        int Width { get;  }
        int Height { get;  }
        ITile this[int x, int y] { get; }
        IList<IBuilding> Buildings { get; }
        IEnumerable<ITile> Tiles { get; }
        IEnumerable<ITile> GetNeighboursOf(ITile tile);
        Point GetLocationOf(ITile tile);
    }

    public class Tile : ITile
    {
        public TileState TileState { get; set; }
    }

    public class Map : IMap
    {
        private readonly ITile[,] _tiles;
        public int Width { get; }
        public int Height { get; }
        public IList<IBuilding> Buildings { get; } = new List<IBuilding>();
        private Dictionary<ITile, Point> _tilesLocations = new Dictionary<ITile, Point>();

        public IEnumerable<ITile> Tiles
        {
            get
            {
                for (int i = 0; i < Width; i++)
                {
                    for (int j = 0; j < Height; j++)
                    {
                        yield return _tiles[i, j];
                    }
                }
            }
        }

        public IEnumerable<ITile> GetNeighboursOf(ITile tile)
        {
            var result = new List<ITile>();
            Point tileLocation = GetLocationOf(tile);

            if (tileLocation.X - 1 >= 0)
            {
                result.Add(this[tileLocation.X - 1, tileLocation.Y]);
            }
            if (tileLocation.X + 1 < Width)
            {
                result.Add(this[tileLocation.X + 1, tileLocation.Y]);
            }
            if (tileLocation.Y - 1 >= 0)
            {
                result.Add(this[tileLocation.X , tileLocation.Y - 1]);
            }
            if (tileLocation.Y + 1 < Height)
            {
                result.Add(this[tileLocation.X , tileLocation.Y + 1]);
            }
            return result;
        }

        public Point GetLocationOf(ITile tile)
        {
            return _tilesLocations[tile];
        }

        public Map(int height, int width)
        {
            Height = height;
            Width = width;
            _tiles = new ITile[Width,Height];
            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    var tile = new Tile {TileState = TileState.Blocked};
                    _tiles[i, j] = tile;
                    _tilesLocations.Add(tile, new Point(i,j));
                }
            }
        }

        public virtual ITile this[int x, int y] => _tiles[x, y];
    }

    public class EmptyAreaGroup
    {
        public IList<ITile> Tiles { get; set; }

        public EmptyAreaGroup()
        {
            Tiles = new List<ITile>();
        }
    }
    public class CityBuilder
    {
        public virtual void FillMap(IMap initialMap)
        {
            var building = new Building(Guid.NewGuid());
//            var tile = initialMap[1, 1];
//            building.Tiles.Add(tile);
//            tile.TileState = TileState.Full;
//
//            initialMap.Buildings.Add(building);

            var emptyAreas = GetEmptyAreas(initialMap);
        }

        public IList<EmptyAreaGroup> GetEmptyAreas(IMap map)
        {
            IList<EmptyAreaGroup> result = new List<EmptyAreaGroup>();

            var emptyTiles = map.Tiles.Where(a => a.TileState == TileState.Empty);
            if (!emptyTiles.Any())
            {
                return result;
            }

            var currentTile = emptyTiles.FirstOrDefault(a => !result.Any(g => g.Tiles.Contains(a)));
            while (currentTile != null)
            {
                var currentGroup = new EmptyAreaGroup();
                currentGroup.Tiles.Add(currentTile);

                NewMethod(map, currentTile, currentGroup);
                result.Add(currentGroup);

                currentTile = emptyTiles.FirstOrDefault(a => !result.Any(g => g.Tiles.Contains(a)));
            }

            return result;

        }

        private static void NewMethod(IMap map, ITile currentTile, EmptyAreaGroup currentGroup)
        {
            var neighboursOfCurrentTile = map.GetNeighboursOf(currentTile).Where(a => a.TileState == TileState.Empty);
            foreach (var neighbour in neighboursOfCurrentTile)
            {
                if (!currentGroup.Tiles.Contains(neighbour))
                {
                    currentGroup.Tiles.Add(neighbour);

                    NewMethod(map, neighbour, currentGroup);
                }
                //currentGroup.Tiles.Add(neighbour);
            }
        }
    }
}
