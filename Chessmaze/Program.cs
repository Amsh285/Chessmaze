using System;

namespace Chessmaze
{
    class Program
    {
        static void Main(string[] args)
        {
            FieldMap map = new FieldMap(10, 5);
            ConsoleMapPrinter.Print(map);

            ChessmazeAlgorithm.PlaceClusters(map);
            //ShowClusterSamples();
            ShowObstacleSampes();
        }

        private static void ShowClusterSamples()
        {
            for (int i = 0; i < 10; i++)
            {
                FieldMap test = new FieldMap(25, 25);
                ChessmazeAlgorithm.PlaceClusters(test);
                ConsoleMapPrinter.Print(test);
            }
        }

        private static void ShowObstacleSampes()
        {
            for (int i = 0; i < 5; i++)
            {
                FieldMap test = new FieldMap(30, 30);
                ChessmazeAlgorithm.PlaceClusters(test);
                ChessmazeAlgorithm.PlaceObstacles(test);
                ChessmazeAlgorithm.PlaceStartAndEndPoint(test);

                ChessmazeAlgorithm.PlaceNodes(test);

                ConsoleMapPrinter.Print(test);
            }
        }
    }
}
