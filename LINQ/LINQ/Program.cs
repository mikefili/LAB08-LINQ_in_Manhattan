using System;
using Newtonsoft.Json.Linq;
using System.IO;
using LINQ.classes;

namespace LINQ
{
    public class Program
    {
        static void Main(string[] args)
        {
            HandleJSON();
        }

        public static JObject HandleJSON()
        {
            JObject o1 = JObject.Parse(File.ReadAllText(@"C:\Users\Owner\codefellows\401\labs\Lab08-LINQ_In_Manhattan\data.json"));

            JArray items = (JArray)o1["features"];
            int dataArr = items.Count;

            for (int i = 0; i < dataArr; i++)
            {
                CityClass city = new CityClass()
                {
                    Address = (string)o1["features"][i]["properties"]["address"],
                    City = (string)o1["features"][i]["properties"]["city"],
                    State = (string)o1["features"][i]["properties"]["state"],
                    Zip = (int)o1["features"][i]["properties"]["zip"],
                    Borough = (string)o1["features"][i]["properties"]["borough"],
                    Neighborhood = (string)o1["features"][i]["properties"]["neighborhood"],
                    County = (string)o1["features"][i]["properties"]["county"]
                };
                Console.WriteLine();
                Console.WriteLine($"Address: {city.Address}");
                Console.WriteLine($"{city.City}, {city.State} {city.Zip}");
                Console.WriteLine($"Borough: {city.Borough}");
                Console.WriteLine($"Neighborhood: {city.Neighborhood}");
                Console.WriteLine($"County: {city.County}");
            }
            Console.WriteLine();
            return o1;
        }
    }
}
