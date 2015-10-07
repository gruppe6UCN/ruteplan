using System;
using OsmSharp.Routing;
using OsmSharp.Osm.PBF.Streams;
using System.IO;
using OsmSharp.Routing.Osm.Interpreter;

namespace OsmSharp
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Load Map");
            var router = Router.CreateLiveFrom(
                new PBFOsmStreamSource(new FileInfo("planet_-8.182_60.951_dd3f1a8d.osm.pbf").OpenRead()),
                new OsmRoutingInterpreter());

            Console.WriteLine("First Point");
            var resolved1 = router.Resolve(Vehicle.Car, new Math.Geo.GeoCoordinate(57.01369, 9.98733));
            Console.WriteLine("Second Point");
            var resolved2 = router.Resolve(Vehicle.Car, new Math.Geo.GeoCoordinate(57.02018, 9.88498));


            Console.WriteLine("begin calculate route...");
            // calculate route.
            var route = router.Calculate(Vehicle.Car, resolved1, resolved2);

            Console.WriteLine("save file");
            route.SaveAsGpx(new FileInfo("route.gpx").OpenWrite());
        }
    }
}
