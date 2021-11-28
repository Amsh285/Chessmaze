using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chessmaze
{
    public static class ChessmazeAlgorithm
    {
        static ChessmazeAlgorithm()
        {
            rngProvider = new Random(Guid.NewGuid().GetHashCode());
        }

        public static void PlaceClusters(FieldMap map, int num_clusters = 3, int probability_range = 101)
        {
            Assert.NotNull(map, nameof(map));

            int used_clusters = 0;

            for (int y = 0; y < map.Y; y++)
            {
                for (int x = 0; x < map.X; x++)
                {
                    int next = rngProvider.Next(1, probability_range);

                    if (next == 1 && used_clusters < num_clusters)
                    {
                        map[x, y].Type = FieldType.Cluster;
                        ++used_clusters;
                    }
                }
            }

            if (used_clusters < num_clusters)
                PlaceClusters(map, num_clusters - used_clusters, probability_range);
        }

        public static void PlaceObstacles(FieldMap map)
        {
            Assert.NotNull(map, nameof(map));

            IEnumerable<FieldInformationSearchResult> clusters = GetClusters(map);
            ClusterSanityCheck(map, clusters);

            IEnumerable<System.Drawing.Point> obstaclePositions = GetObstaclePositions(clusters.Select(c => c.Position), map);

            foreach (System.Drawing.Point objstaclePosition in obstaclePositions)
            {
                FieldInformation current = map[objstaclePosition.X, objstaclePosition.Y];

                if (current.Type == FieldType.Empty)
                    current.Type = FieldType.Obstacle;
            }
        }

        public static IEnumerable<FieldInformationSearchResult> GetClusters(FieldMap map)
        {
            Assert.NotNull(map, nameof(map));

            for (int x = 0; x < map.X; x++)
            {
                for (int y = 0; y < map.Y; y++)
                {
                    if (map[x, y].Type == FieldType.Cluster)
                        yield return new FieldInformationSearchResult(map, map[x, y], new System.Drawing.Point(x, y));
                }
            }
        }

        public static void PlaceStartAndEndPoint(FieldMap map)
        {
            Assert.NotNull(map, nameof(map));

            float scale = 1.0f / 10.0f;
            int x = (int)(map.X * scale);
            int y = (int)(map.Y * scale);

            System.Drawing.Point startPosition = new System.Drawing.Point(rngProvider.Next(0, x), rngProvider.Next(0, y));
            System.Drawing.Point endPosition = new System.Drawing.Point(map.X - 1 - startPosition.X, map.Y - 1 - startPosition.Y);

            map[startPosition.X, startPosition.Y].Type = FieldType.Start;
            map[endPosition.X, endPosition.Y].Type = FieldType.End;
        }

        public class Quadrant
        {
            public Point Start { get; }
            public Point LeftBottom => new Point(Start.X, Start.Y + Y);
            public Point RightTop => new Point(Start.X + X, Start.Y);
            public Point RightBottom => new Point(Start.X + X, Start.Y + Y);

            public int X { get; }
            public int Y { get; }

            public Quadrant(System.Drawing.Point start, int x, int y)
            {
                Start = start;
                X = x;
                Y = y;
            }

            public override string ToString()
            {
                return $"Start: {Start}, LeftBottom: {LeftBottom}, RightTop: {RightTop}, RightBottom: {RightBottom}";
            }
        }

        public static void PlaceNodes(FieldMap map)
        {
            Assert.NotNull(map, nameof(map));

            float scale = 1.0f / 10.0f;
            int x = (int)(map.X * scale);
            int y = (int)(map.Y * scale);

            for (int row = 0; row < 10; ++row)
            {
                for (int col = 0; col < 10; col++)
                {
                    Quadrant q = new Quadrant(new System.Drawing.Point(col * y, row * x), x, y);

                    int mapX = rngProvider.Next(q.Start.X, q.Start.X + q.X);
                    int mapY = rngProvider.Next(q.Start.Y, q.Start.Y + q.Y);

                    if(map[mapX, mapY].Type != FieldType.Start && map[mapX, mapY].Type != FieldType.End
                        && rngProvider.Next(0,5) == 3)
                        map[mapX, mapY].Type = FieldType.Node;
                }
            }
        }

        public static IEnumerable<System.Drawing.Point> GetObstaclePositions(IEnumerable<System.Drawing.Point> clusterPositions, FieldMap map)
        {
            Assert.NotNull(clusterPositions, nameof(clusterPositions));
            Assert.NotNull(map, nameof(map));

            List<System.Drawing.Point> obstaclePositions = new List<System.Drawing.Point>();

            foreach (System.Drawing.Point clusterPosition in clusterPositions)
            {
                int x = clusterPosition.X, y = clusterPosition.Y;

                // oben links: x - 2 y - 1
                AddObstaclePosition(x - 2, y - 1, map, obstaclePositions);
                // oben rechts: x - 2 y + 1
                AddObstaclePosition(x - 2, y + 1, map, obstaclePositions);
                // links oben: y - 2 x - 1
                AddObstaclePosition(x - 1, y - 2, map, obstaclePositions);
                // links unten: y - 2 x + 1
                AddObstaclePosition(x + 1, y - 2, map, obstaclePositions);
                // unten links: x + 2 y - 1
                AddObstaclePosition(x + 2, y - 1, map, obstaclePositions);
                // unten rechts: x + 2 y + 1
                AddObstaclePosition(x + 2, y + 1, map, obstaclePositions);
                // rechs oben: y + 2 x - 1
                AddObstaclePosition(x - 1, y + 2, map, obstaclePositions);
                // rechts unten: y + 2 x + 1
                AddObstaclePosition(x + 1, y + 2, map, obstaclePositions);
            }

            return obstaclePositions;
        }

        private static void AddObstaclePosition(int x, int y, FieldMap map, List<System.Drawing.Point> obstaclePositions)
        {
            Assert.NotNull(map, nameof(map));

            System.Drawing.Point jumpPosition = new System.Drawing.Point(x, y);

            if (!map.IsOutsideOfMapBounds(jumpPosition.X, jumpPosition.Y))
                obstaclePositions.Add(jumpPosition);
        }

        private static void ClusterSanityCheck(FieldMap map, IEnumerable<FieldInformationSearchResult> clusters)
        {
            foreach (FieldInformationSearchResult cluster in clusters)
                Assert.That(
                    map[cluster.Position.X, cluster.Position.Y].Type == FieldType.Cluster,
                    $"Error Cluster on map[{cluster.Position.X}, {cluster.Position.Y}] expected.");
        }

        private static Random rngProvider;
    }
}
