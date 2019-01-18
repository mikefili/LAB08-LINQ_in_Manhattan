using System;
using Newtonsoft.Json.Linq;
using System.IO;
using LINQ.classes;
using System.Collections.Generic;

namespace LINQ
{
    public class Program
    {
        public static string dataPath = "C:/Users/Owner/codefellows/401/labs/Lab08-LINQ_In_Manhattan/data.json";

        static void Main(string[] args)
        {
            List<CityClass> list = HandleJSON(GetJObject(dataPath));
            Write(list);
        }

        public static JObject GetJObject(string path)
        {
            JObject jObject = JObject.Parse(File.ReadAllText(@path));
            return jObject;
        }

        public static List<CityClass> HandleJSON(JObject jObject)
        {
            var list = jObject["features"];
            List<CityClass> neighborhoods = new List<CityClass>();
            
            foreach (var item in list)
            {
                CityClass city = new CityClass()
                {
                    Address = (string)item["properties"]["address"],
                    City = (string)item["properties"]["city"],
                    State = (string)item["properties"]["state"],
                    Zip = (int)item["properties"]["zip"],
                    Borough = (string)item["properties"]["borough"],
                    Neighborhood = (string)item["properties"]["neighborhood"],
                    County = (string)item["properties"]["county"]
                };
                neighborhoods.Add(city);
            }
            return neighborhoods;
        }

        public static void Write(List<CityClass> list)
        {
            foreach (CityClass neighborhood in list)
            {
                Console.WriteLine();
                Console.WriteLine($"Address: {neighborhood.Address}");
                Console.WriteLine($"{neighborhood.City}, {neighborhood.State} {neighborhood.Zip}");
                Console.WriteLine($"Borough: {neighborhood.Borough}");
                Console.WriteLine($"Neighborhood: {neighborhood.Neighborhood}");
                Console.WriteLine($"County: {neighborhood.County}");
            }
            Console.WriteLine();
        }

        public static void Write(IEnumerable<CityClass> list)
        {
            foreach (CityClass neighborhood in list)
            {
                Console.WriteLine();
                Console.WriteLine($"Address: {neighborhood.Address}");
                Console.WriteLine($"{neighborhood.City}, {neighborhood.State} {neighborhood.Zip}");
                Console.WriteLine($"Borough: {neighborhood.Borough}");
                Console.WriteLine($"Neighborhood: {neighborhood.Neighborhood}");
                Console.WriteLine($"County: {neighborhood.County}");
            }
            Console.WriteLine();
        }

        //public static JObject filterWithoutNeighborhood(List<CityClass> name)
        //{

        //}

    }
}
