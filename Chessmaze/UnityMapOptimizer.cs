using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chessmaze
{
    public static class UnityMapOptimizer
    {
        public static void DoubleDiagonalRoutes(FieldMap map)
        {
            for (int x = 0; x < map.X; x++)
            {
                for (int y = 0; y < map.Y; y++)
                {
                    if (map[x, y].IsDiagonal)
                    {
                        if (!map.IsOutsideOfMapBounds(x + 1, y))
                        {
                            FieldInformation field = map[x + 1, y];

                            if (field.Type != FieldType.Start && field.Type != FieldType.End && field.Type != FieldType.Node)
                            {
                                field.Type = FieldType.Road;
                                field.IsDiagonal = false;
                            }
                        }
                    }
                }
            }
        }

        public static void SurroundNodes(FieldMap map)
        {
            List<FieldInformationSearchResult> fieldsToSurround = ChessmazeAlgorithm.GetNodes(map).ToList();

            fieldsToSurround.Add(ChessmazeAlgorithm.GetStartNode(map));
            fieldsToSurround.Add(ChessmazeAlgorithm.GetEndNode(map));

            IEnumerable<FieldInformation> fieldsToOverrride = fieldsToSurround.SelectMany(f => GetSurroundingFields(map, f))
                .Where(f => f.Type != FieldType.Node && f.Type != FieldType.End && f.Type != FieldType.Start);

            foreach (FieldInformation field in fieldsToOverrride)
                field.Type = FieldType.Road;

        }

        private static IEnumerable<FieldInformation> GetSurroundingFields(FieldMap map,FieldInformationSearchResult searchResult)
        {
            List<FieldInformation> results = new();

            int x = searchResult.Position.X;
            int y = searchResult.Position.Y;

            List<int> possibleXs = new() { x - 1, x, x + 1 };
            List<int> possibleYs = new() { y - 1, y, y + 1 };


            foreach (int possibleX in possibleXs)
                foreach (int possibleY in possibleYs)
                    if (!map.IsOutsideOfMapBounds(possibleX, possibleY))
                        results.Add(map[possibleX, possibleY]);

            return results;
        }
    }
}
