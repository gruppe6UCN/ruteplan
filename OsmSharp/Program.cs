using System;
using OsmSharp.Routing;
using OsmSharp.Osm.PBF.Streams;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters.Soap;
using OsmSharp.Routing.Osm.Interpreter;

using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace OsmSharp
{
    class MainClass
    {
        public static String fileObj = "map.obj";

        public static void Main(string[] args)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Router));
            TextWriter writer = new StreamWriter(fileObj);


            Console.WriteLine("Load Map");
            Router router;
            router = Router.CreateLiveFrom(
                             new PBFOsmStreamSource(new FileInfo("planet_-8.182_60.951_dd3f1a8d.osm.pbf").OpenRead()),
                             new OsmRoutingInterpreter());

            ser.Serialize(writer, ser);
            writer.Close();

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

        public static void SerializeObject<T>(string filename, T obj)
        {
            Stream stream = File.Open(filename, FileMode.Create);
//            BinaryFormatter binaryFormatter = new BinaryFormatter();
//            binaryFormatter.Serialize(stream, obj);
            SoapFormatter formatter = new SoapFormatter();
            formatter.Serialize(stream, obj);
            stream.Close();
        }

        public static T DeSerializeObject<T> (string filename)
        {
            T objectToBeDeSerialized;
            Stream stream = File.Open(filename, FileMode.Open);
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            objectToBeDeSerialized= (T)binaryFormatter.Deserialize(stream);
            stream.Close();
            return objectToBeDeSerialized;
        }
    }
}
