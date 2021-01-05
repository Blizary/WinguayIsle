using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace WaveFunctionCollapse
{
    public class TileContainer
    {
        public TileBase Tile { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public TileContainer(TileBase _tile, int _x, int _y)
        {
            this.Tile = _tile;
            this.X = _x;
            this.Y = _y;
        }
    }

}