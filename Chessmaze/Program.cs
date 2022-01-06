using System;
using System.Collections.Generic;
using System.Linq;

namespace Chessmaze
{
    class Program
    {
        static void Main(string[] args)
        {
            ShowObstacleSampes();
        }

        private static void ShowObstacleSampes()
        {
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine($"Map Nr: {i}");

                FieldMap testMap = new FieldMap(50, 50);
                ChessmazeAlgorithm.PlaceClusters(testMap);
                ChessmazeAlgorithm.PlaceObstacles(testMap);
                ChessmazeAlgorithm.PlaceStartAndEndPoint(testMap);
                ValidateMap(testMap, i);

                ChessmazeAlgorithm.PlaceNodes(testMap);
                ValidateMap(testMap, i);

                ChessmazeAlgorithm.PlaceRoutes(testMap);
                ValidateMap(testMap, i);

                UnityMapOptimizer.DoubleDiagonalRoutes(testMap);
                ValidateMap(testMap, i);

                UnityMapOptimizer.SurroundNodes(testMap);
                ValidateMap(testMap, i);

                ConsoleMapPrinter.Print(testMap);
                ValidateMap(testMap, i);
            }
        }

        private static void ValidateMap(FieldMap map, int mapIndex)
        {
            FieldInformationSearchResult start = ChessmazeAlgorithm.GetFieldInformation(map, FieldType.Start).FirstOrDefault();

            if (start == null)
                throw new ValidationException($"No Start Node found. mapindex: {mapIndex}");

            FieldInformationSearchResult end = ChessmazeAlgorithm.GetFieldInformation(map, FieldType.End).FirstOrDefault();

            if (end == null)
                throw new ValidationException($"No End Node found. mapindex: {mapIndex}");

            int wallFieldsCount = map.X* 4 - 4;

            if (wallFieldsCount != ChessmazeAlgorithm.GetWalls(map).Count())
                throw new ValidationException($"Wall was overriden!");
        }
    }
}
