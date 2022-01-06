using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chessmaze
{
    public static class ConsoleMapPrinter
    {
        public static void Print(FieldMap map)
        {
            Assert.NotNull(map, nameof(map));

            for (int y = 0; y < map.Y; y++)
            {
                for (int x = 0; x < map.X; x++)
                {
                    FieldType type = map[x, y].Type;

                    if (type == FieldType.Cluster)
                        Console.ForegroundColor = ConsoleColor.Red;
                    else if (type == FieldType.Obstacle)
                        Console.ForegroundColor = ConsoleColor.Blue;
                    else if (type == FieldType.Start || type == FieldType.End)
                        Console.ForegroundColor = ConsoleColor.Green;
                    else if (type == FieldType.Node)
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    else if (type == FieldType.Wall)
                        Console.ForegroundColor = ConsoleColor.Magenta;


                    Console.Write($" {FieldTypeMapper.MapType(type)} ");
                    Console.ResetColor();
                }

                Console.WriteLine();
            }

            Console.WriteLine();
        }
    }
}
