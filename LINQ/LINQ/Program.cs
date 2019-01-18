﻿using System;
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
            IEnumerable<CityClass> nameless = FilterNameless(list);
            IEnumerable<CityClass> noDuplicates = RemoveDupes(list);
            Console.WriteLine("All Manhattan Neighborhoods: ");
            Write(list);
            Console.WriteLine("-----------------------------");
            Console.WriteLine();
            Console.WriteLine("All Nameless Manhattan Neighborhoods: ");
            Console.WriteLine();
            Write(nameless);
            Console.WriteLine("-----------------------------");
            Console.WriteLine();
            Console.WriteLine("All Manhattan Neighborhoods (No Duplicates): ");
            Console.WriteLine();
            Write(noDuplicates);
        }

        public static JObject GetJObject()
        {
            JObject jObject = JObject.Parse(File.ReadAllText(@"C:/Users/Owner/codefellows/401/labs/Lab08-LINQ_In_Manhattan/data.json"));
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

        public static IEnumerable<CityClass> FilterNameless(List<CityClass> list)
        {
            IEnumerable<CityClass> noNames = from neighborhood in list
                                             where neighborhood.Neighborhood.Length == 0
                                             select neighborhood;
            return noNames;
        }

        public static IEnumerable<CityClass> RemoveDupes(IEnumerable<CityClass> list)
        {
            String[] temp = new string[250];
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
            IEnumerable<CityClass> noDupes = from neighborhood in list
                                             where neighborhood.Duplicate == false
                                             select neighborhood;
            return noDupes;
        }
    }
}
