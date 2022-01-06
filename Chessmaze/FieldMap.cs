using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chessmaze
{
    public sealed class FieldMap
    {

        public int X { get; }
        public int Y { get; }

        public FieldMap(int x, int y)
        {
            X = x + 2; // plus two for walls
            Y = y + 2;

            map = new FieldInformation[X, Y];
            InitializeMap();
        }

        public void InitializeMap()
        {
            for (int x = 0; x < X; x++)
            {
                for (int y = 0; y < Y; y++)
                    this[x, y] = new FieldInformation();
            }

            for(int y = 0; y < Y; y++)
            {
                this[0, y].Type = FieldType.Wall;
                this[X-1, y].Type = FieldType.Wall;
                this[y, 0].Type = FieldType.Wall;
                this[y, Y-1].Type = FieldType.Wall;
            }
        }

        public FieldInformation this[int x, int y]
        {
            get
            {
                ValidateBounds(x, y);

                return map[x, y];
            }
            set
            {
                ValidateBounds(x, y);

                map[x, y] = value;
            }
        }

        public bool IsOutsideOfMapBounds(int x, int y)
        {
            return x >= X || x < 0 || y >= Y || y < 0;
        }

        private void ValidateBounds(int x, int y)
        {
            if (IsOutsideOfMapBounds(x, y))
                throw new InvalidOperationException($"x: {x}, y: {y} is out of Bounds. Dimensions are: X:{X}, Y:{Y}");
        }

        private readonly FieldInformation[,] map;
    }
}
