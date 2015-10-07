using QuickGraph;
using QuickGraph.Algorithms;
using System;
using System.Collections.Generic;
using System.Linq;
using QuickGraph.Algorithms.ShortestPath;
using GLib;
using QuickGraph.Algorithms.Observers;

namespace QuickGraphExampleFuntime
{
    class Point
    {
        public double X { get; private set;}
        public double Y { get; private set;}

        public Point(double X, double Y) {
            this.X = X;
            this.Y = Y;
        }

        public double DistanceTo(Point p2)
        {
            return Math.Pow(Math.Pow(p2.X - X, 2) + Math.Pow(p2.Y - Y, 2), 0.5);
        }

        public override String ToString() {
            return String.Format("({0},{1})", X, Y);
        }
    }

    class Edge<T, W> : IEdge<T>
    {
        public T Source { get; set; }
        public T Target { get; set; }
        public W Weight { get; set; }

        public Edge(T s, T t, W w)
        {
            Source = s;
            Target = t;
            Weight = w;
        }
    }

    class Program
    {

        static void Main(string[] args)
        {
            //Creates Graph
            var graph = new AdjacencyGraph<Point, Edge<Point, double>>(true);

            List<Point> vertexs = new List<Point>()
            {
                new Point(0, 0),
                new Point(4, 0),
                new Point(4, 3),
                new Point(0, 3)
            };

            List<Edge<Point, double>> edges = new List<Edge<Point, double>>()
            {
                new Edge<Point, double>(vertexs[0], vertexs[1], vertexs[0].DistanceTo(vertexs[1])),
                new Edge<Point, double>(vertexs[1], vertexs[0], vertexs[1].DistanceTo(vertexs[0])),
                new Edge<Point, double>(vertexs[1], vertexs[2], vertexs[1].DistanceTo(vertexs[2])),
                new Edge<Point, double>(vertexs[2], vertexs[1], vertexs[2].DistanceTo(vertexs[1])),
                new Edge<Point, double>(vertexs[2], vertexs[0], vertexs[2].DistanceTo(vertexs[0])),
                new Edge<Point, double>(vertexs[0], vertexs[2], vertexs[0].DistanceTo(vertexs[2])),
                new Edge<Point, double>(vertexs[2], vertexs[3], vertexs[2].DistanceTo(vertexs[3])),
                new Edge<Point, double>(vertexs[3], vertexs[2], vertexs[3].DistanceTo(vertexs[2])),
                new Edge<Point, double>(vertexs[3], vertexs[0], vertexs[3].DistanceTo(vertexs[0])),
                new Edge<Point, double>(vertexs[0], vertexs[3], vertexs[0].DistanceTo(vertexs[3]))
            };

            graph.AddVerticesAndEdgeRange(edges);

            // creating the algorithm instance
            var dijkstra = new DijkstraShortestPathAlgorithm<Point, Edge<Point, double>>(graph, e => e.Weight);

            var source = vertexs[1];
            var target = vertexs[3];

            dijkstra.Compute(source);
            var test = dijkstra.Distances[target];
            Console.WriteLine("result is : " + test);

            // Gets a IEnumerable with the edge(s) for the path
            TryFunc<Point, IEnumerable<Edge<Point, double>>> tryGetPaths = graph.ShortestPathsDijkstra(e => e.Weight, source);
            IEnumerable<Edge<Point, double>> path;
            if (tryGetPaths(target, out path))
                foreach (var edge in path)
                    Console.WriteLine("From {0} To {1} Distance is {2}", edge.Source, edge.Target, edge.Weight);
        }
    }
}