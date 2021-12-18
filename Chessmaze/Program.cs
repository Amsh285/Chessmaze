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
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine($"Map Nr: {i}");

                FieldMap test = new FieldMap(30, 30);
                ChessmazeAlgorithm.PlaceClusters(test);
                ChessmazeAlgorithm.PlaceObstacles(test);
                ChessmazeAlgorithm.PlaceStartAndEndPoint(test);
                ValidateMap(test, i);

                ChessmazeAlgorithm.PlaceNodes(test);
                ValidateMap(test, i);

                ChessmazeAlgorithm.PlaceRoutes(test);

                ConsoleMapPrinter.Print(test);
                ValidateMap(test, i);
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
        }
    }
}
