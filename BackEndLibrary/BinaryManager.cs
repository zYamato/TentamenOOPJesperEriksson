using System;
using System.IO;
using System.Globalization;
using System.Collections.Generic;
using System.Xml;
using Newtonsoft.Json;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;


namespace BackEndLibrary
{
    public static class BinaryManager
    {
        private static string path = "./Database.dat";
        public static void SaveWarehouse(Warehouse warehouse)
        {
            Stream stream = File.Open(path, FileMode.Create);
            BinaryFormatter bf = new BinaryFormatter();

            bf.Serialize(stream, warehouse);
            Console.Clear();
            stream.Close();
        }


        /// <summary>
        /// Returns a Loaded WareHouse
        /// </summary>
        /// <returns></returns>
        public static Warehouse LoadWarehouse()
        {
            if (!File.Exists(path) || File.ReadAllText(path) == "")
            {
                Console.Clear();
                Console.WriteLine();
                Console.WriteLine("There is no file available to Load, please create a Warehouse profile by saving first.");
                Console.WriteLine();
                return null;
            }

            Stream stream = File.Open(path, FileMode.Open);

            BinaryFormatter bf = new BinaryFormatter();

            var LoadedWarehouse = (Warehouse)bf.Deserialize(stream);
            Console.Clear();
            Console.WriteLine("Warehouse profile has been sucessfully loaded.");
            Console.WriteLine();

            stream.Close();
            return LoadedWarehouse;
        }
    }
}
