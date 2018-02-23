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

        public ITile[,] GetTilesArray()
        {
            return _tiles;
        }

        public void AddBuilding(IBuilding building, IEnumerable<ITile> tilesOfBuilding)
        {
            BuildingsTiles.Add(building, tilesOfBuilding);
        }

        public void SetBuildingAtTile(ITile tile, IBuilding building)
        {
            TileBuildings.Add(tile, building);
        }

        public virtual ITile this[int x, int y] => _tiles[x, y];

        public IEnumerable<ITile> AllTiles
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

        public IEnumerable<ITile> GetTilesOfBuilding(IBuilding building)
        {
            return BuildingsTiles[building];
        }

        public IEnumerable<IBuilding> GetBuildings()
        {
            return BuildingsTiles.Keys;
        }

        public IDictionary<IBuilding, IEnumerable<ITile>> BuildingsTiles { get; set; } = new Dictionary<IBuilding, IEnumerable<ITile>>();

        public IDictionary<ITile, IBuilding> TileBuildings { get; } = new Dictionary<ITile, IBuilding>();

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
            return TileBuildings[tile];
        }
    }
}