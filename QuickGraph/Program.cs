using QuickGraph;
using QuickGraph.Algorithms;
using System;
using System.Collections.Generic;
using System.Linq;
using QuickGraph.Algorithms.ShortestPath;
using GLib;

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

            graph.AddVertexRange(vertexs);

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

            graph.AddEdgeRange(edges);

            var dijkstra = new DijkstraShortestPathAlgorithm<Point, Edge<Point, double>>(graph, e => e.Weight);
            dijkstra.Compute(vertexs[1]);
            Console.WriteLine("result is : " + dijkstra.Distances[vertexs[3]]);
        }
    }
}