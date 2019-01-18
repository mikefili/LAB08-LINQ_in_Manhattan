using System;
using Newtonsoft.Json.Linq;
using System.IO;
using LINQ.classes;
using System.Collections.Generic;
using System.Linq;

namespace LINQ
{
    public class Program
    {
        static void Main(string[] args)
        {
            List<CityClass> list = HandleJSON(GetJObject());

            //Console.WriteLine("All Manhattan Neighborhoods: ");
            //Write(list);
            //Console.WriteLine("-----------------------------");
            //Console.WriteLine();

            //IEnumerable<CityClass> nameless = RemoveNameless(list);
            //Console.WriteLine("All Named Manhattan Neighborhoods: ");
            //Write(nameless);
            //Console.WriteLine("-----------------------------");
            //Console.WriteLine();

            IEnumerable<CityClass> noDuplicates = RemoveDupes(list);
            Console.WriteLine("All Manhattan Neighborhoods (No Duplicates): ");
            Write(noDuplicates);
            Console.WriteLine("-----------------------------");
            Console.WriteLine();

            //IEnumerable<CityClass> bothFilters = RemoveNamelessAndDupes(list);
            //Console.WriteLine("All Named Manhattan Neighborhoods (No Duplicates): ");
            //Write(bothFilters);
        }

        /// <summary>
        /// Generate JObject with NewtonSoft
        /// </summary>
        /// <returns>JObject</returns>
        public static JObject GetJObject()
        {
            JObject jObject = JObject.Parse(File.ReadAllText(@"C:/Users/Owner/codefellows/401/labs/Lab08-LINQ_In_Manhattan/data.json"));
            return jObject;
        }

        /// <summary>
        /// Generate list of neighborhoods from JSON object
        /// </summary>
        /// <param name="jObject">jObject</param>
        /// <returns>List of neighborhood objects</returns>
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

        /// <summary>
        /// Writes out formatted neighborhood addresses
        /// </summary>
        /// <param name="list">List of neighborhoods</param>
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

        /// <summary>
        /// Writes out formatted neighborhood addresses
        /// </summary>
        /// <param name="list">List of neighborhoods</param>
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

        /// <summary>
        /// Removes neighborhoods with no name
        /// </summary>
        /// <param name="list">List of neighborhoods</param>
        /// <returns>IEnumerable list of named neighborhoods</returns>
        public static IEnumerable<CityClass> RemoveNameless(List<CityClass> list)
        {
            IEnumerable<CityClass> noNames = from neighborhood in list
                                             where neighborhood.Neighborhood.Length != 0
                                             select neighborhood;
            return noNames;
        }

        /// <summary>
        /// Removes duplicate neighborhoods
        /// </summary>
        /// <param name="list">List of neighborhoods</param>
        /// <returns>IEnumerable list of non-duplicate neighborhoods</returns>
        public static IEnumerable<CityClass> RemoveDupes(IEnumerable<CityClass> list)
        {
            String[] temp = new string[list.Count()];
            int counter = 0;
            foreach (CityClass neighborhood in list)
            {
                if (!temp.Contains(neighborhood.Neighborhood))
                {
                    counter++;
                    temp[counter] = neighborhood.Neighborhood;
                }
                else
                {
                    neighborhood.Duplicate = true;
                }
            }
            IEnumerable<CityClass> noDupes = list.Where(neighborhood => neighborhood.Neighborhood.Length == 0);
            return noDupes;
        }

        /// <summary>
        /// Removes nameless and duplicate neighborhoods
        /// </summary>
        /// <param name="list">List of neighborhoods</param>
        /// <returns>IEnumerable list of named, non-duplicate neighborhoods</returns>
        public static IEnumerable<CityClass> RemoveNamelessAndDupes(IEnumerable<CityClass> list)
        {
            String[] temp = new string[list.Count()];
            int counter = 0;
            foreach (CityClass neighborhood in list)
            {
                if (!temp.Contains(neighborhood.Neighborhood))
                {
                    counter++;
                    temp[counter] = neighborhood.Neighborhood;
                }
                else
                {
                    neighborhood.Duplicate = true;
                }
            }
            IEnumerable<CityClass> noDupesOrNameless = list.Where(neighborhood => neighborhood.Neighborhood.Length != 0 && neighborhood.Duplicate == false);
            return noDupesOrNameless;
        }
    }
}
