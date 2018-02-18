using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using CityBuilder;
using CityBuilder.Buildings;

namespace CityBuilding
{
    public enum NeighbourMode
    {
        Orthogonal,
        All
    }

    public class Map : IMap
    {
        private readonly ITile[,] _tiles;
        public int Width { get; }
        public int Height { get; }
        public Dictionary<IPoint, IBuilding> LocationsOfBuildings { get; } = new Dictionary<IPoint, IBuilding>();
        private readonly Dictionary<ITile, IPoint> _tilesLocations = new Dictionary<ITile, IPoint>();


        public Map(int height, int width)
        {
            Height = height;
            Width = width;
            _tiles = new ITile[Width, Height];
            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    var tile = new Tile { TileState = TileState.Blocked };
                    _tiles[i, j] = tile;
                    _tilesLocations.Add(tile, new Point(i, j));
                }
            }
        }

        public virtual ITile this[int x, int y] => _tiles[x, y];

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

        public IEnumerable<ITile> GetNeighboursOf(ITile tile, NeighbourMode neighbourMode)
        {
            var result = new List<ITile>();
            IPoint tileLocation = GetLocationOf(tile);

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
            if (neighbourMode == NeighbourMode.All)
            {
                if (tileLocation.X - 1 >= 0 && tileLocation.Y - 1 >= 0)
                {
                    result.Add(this[tileLocation.X - 1, tileLocation.Y - 1]);
                }
                if (tileLocation.X + 1 < Width && tileLocation.Y + 1 < Height)
                {
                    result.Add(this[tileLocation.X + 1, tileLocation.Y + 1]);
                }
                if (tileLocation.X - 1 >= 0 && tileLocation.Y + 1 < Height)
                {
                    result.Add(this[tileLocation.X - 1, tileLocation.Y + 1]);
                }
                if (tileLocation.X + 1 < Width && tileLocation.Y - 1 >= 0)
                {
                    result.Add(this[tileLocation.X + 1, tileLocation.Y - 1]);
                }
            }
            return result;
        }

        public IPoint GetLocationOf(ITile tile)
        {
            return _tilesLocations[tile];
        }

        public IBuilding GetBuildingAtTile(ITile tile)
        {
            foreach (var locationsOfBuilding in LocationsOfBuildings)
            {
                var location = locationsOfBuilding.Key;
                var building = locationsOfBuilding.Value;
                var allTilesOfBuilding = new BuildingTilesOnMapLocator().Locate(this, building, location);
                if (allTilesOfBuilding.Contains(tile))
                {
                    return building;
                }
            }

            return null;
        }
    }
}