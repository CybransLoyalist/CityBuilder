﻿using System;
using System.Drawing;
using System.Linq;
using CityBuilding;

namespace CityBuilderGUI
{
    public class BrushForTileCreator
    {
        public virtual Brush Create(ITile tile, IMap map)
        {
            Color color;
            switch (tile.TileState)
            {
                case TileState.Blocked:
                    color = Color.Gray;
                    break;
                case TileState.Empty:
                    color = Color.LightYellow;
                    break;
                case TileState.Street:
                    color = Color.SaddleBrown;
                    break;
                case TileState.Full:
                    var guid = map.Buildings.First(b => b.Tiles.Any(t => t == tile)).Guid;
                    color = Color.FromArgb(guid.GetHashCode());
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return new SolidBrush(color);
        }
    }
}