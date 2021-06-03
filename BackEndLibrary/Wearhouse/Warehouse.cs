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
    [Serializable()]
    public class Warehouse
    {
        public WarehouseLocation[,] Location;
        public Warehouse()
        {
            this.Location = InitializeLocation();
        }

        public Box CreateCube(int XSide, string ID, string Description, double weight, bool isFragile)
        {
            return new Cube(XSide, ID, Description, weight, isFragile);
        }
        public Box CreateCuboid(int XSide, int YSide, int ZSide, string ID, string Description, double weight, bool isFragile)
        {
            return new Cuboid(XSide, YSide, ZSide, ID, Description, weight, isFragile);
        }
        public Box CreateSphere(int radius, string ID, string Description, double weight, bool isFragile)
        {
            return new Sphere(radius, ID, Description, weight, isFragile);
        }
        public Box CreateBlob(int XSide, string ID, string Description, double weight)
        {
            return new Blob(XSide, ID, Description, weight);
        }
        public int RetriveBox(string id)
        {
            int[] index = SearchBox(id);
            int row = index[0];
            int column = index[1];

            if (row == -1)
            {
                return -1;
            }
            else
            {
                for (int i = 0; i < Location[row, column].Contents.Count; i++)
                {
                    if (Location[row, column].Contents[i].ID == id)
                    {
                        Location[row, column].Contents.RemoveAt(i);
                        BinaryManager.SaveWarehouse(this);
                        return 1;
                    }
                }
                return -1;
            }

        }
        public int[] SearchBox(string id)
        {
            for (int i = 0; i < Location.GetLength(0); i++)
            {
                for (int j = 0; j < Location.GetLength(1); j++)
                {
                    if (Location[i, j].Contents.Count != 0)
                    {
                        for (int c = 0; c < Location[i, j].Contents.Count; c++)
                        {
                            if (Location[i, j].Contents[c].ID == id)
                            {
                                int[] newArray = { i, j };
                                return newArray;
                            }
                        }
                    }
                }
            }
            int[] failArray = { -1 };
            return failArray;
        }
        public bool MoveBox(string id, int row, int col)
        {
            int[] currentBoxLocation = SearchBox(id);
            int currentBoxRow = currentBoxLocation[0];
            int currentBoxCol = currentBoxLocation[1];

            foreach (var box in Location[currentBoxRow, currentBoxCol].Contents)
            {
                if (box.ID == id)
                {
                    Box currentBox = box;
                    foreach (var item in Location[row, col].Contents)
                    {
                        if (item.IsFragile == true || Location[row, col].CurrentVolume +
                            box.Volume > Location[row, col].MaxVolume || Location[row, col].CurrentWeight +
                            box.Weight > Location[row, col].MaxWeight)
                        {
                            return false;
                        }
                    }
                    RetriveBox(id);
                    Location[row, col].Contents.Add(currentBox);
                    BinaryManager.SaveWarehouse(this);
                    return true;
                }
            }
            return false;



        }
        public void DisplayWearhouse()
        {
            for (int i = 0; i < Location.GetLength(0); i++)
            {
                for (int j = 0; j < Location.GetLength(1); j++)
                {
                    if (Location[i, j].ToString() == "")
                    {
                        Console.WriteLine("Level " + (Location[i, j].Row + 1) + " Position " + (Location[i, j].Col + 1) + " " + " Empty Location");
                    }
                    else
                    {
                        Console.WriteLine("Level " + (Location[i, j].Row + 1) + " Postition " + (Location[i, j].Col + 1) + " " + Location[i, j].ToString());
                    }
                }

            }
        }
        public WarehouseLocation[,] InitializeLocation()
        {
            if(BinaryManager.LoadWarehouse() == null)
            {

                Location = new WarehouseLocation[3, 100];
                for (int i = 0; i < Location.GetLength(0); i++)
                {
                    for (int j = 0; j < Location.GetLength(1); j++)
                    {
                        Location[i, j] = new WarehouseLocation(this) { Row = i, Col = j };
                    }
                }
                BinaryManager.SaveWarehouse(this);
                return Location;
            }
            else
            {

               return BinaryManager.LoadWarehouse().Location;
            }
        }
        public void StoreAuto(Box item)
        {
            foreach (var locations in this.Location)
            {
                if (locations.CurrentWeight + item.Weight < locations.MaxWeight && locations.CurrentVolume +
                    item.Volume < locations.MaxVolume)
                {
                    int checkIsFragile = 0;
                    foreach (var items in locations.Contents)
                    {
                        if (items.IsFragile == true)
                        {
                            checkIsFragile = 1;
                        }
                    }
                    if (checkIsFragile == 1)
                    {
                        continue;
                    }
                    else
                    {
                        locations.CurrentWeight += item.Weight;
                        locations.Contents.Add(item);
                        break;
                    }
                }
            
            }

        }
        public void StoreManually(Box item, int position, int level)
        {
            WarehouseLocation location = this.Location[level - 1, position - 1];
            if(position > 100 || level > 3)
            {
                Console.WriteLine("This location does not exist in the warehouse.");
                return;
            }
            if (location.CurrentWeight + item.Weight < location.MaxWeight && location.CurrentVolume +
                item.Volume < location.MaxVolume )
            {
                foreach (var items in location.Contents)
                {
                    if (items.IsFragile == true)
                    {
                        Console.WriteLine("There is a fragile item in this position.");
                        return;
                    }
                }
                this.Location[level - 1, position - 1].Contents.Add(item);
            }
            else
            {
                Console.WriteLine("There is no space left in this position.");
                return;
            }



        }
        public bool IDValidation(string id)
        {
            for(int i = 0; i < Location.GetLength(0); i++)
            {
                for (int j = 0; j < Location.GetLength(1); j++)
                {
                    for(int c = 0; c < Location[i,j].Contents.Count; c++)
                    {
                        if(Location[i,j].Contents[c].ID == id)
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }
        public WarehouseLocation FetchWarehouseClone(int row, int col)
        {
            return Location[row, col].Content();
        }
        public string this[int levelIndex, int locationIndex]
        {
            get
            {
                if(levelIndex < 0 || levelIndex > 2 || locationIndex < 0 || locationIndex > 99)
                {
                    return null;
                }
                else
                {
                    return Location[levelIndex, locationIndex].Content().ToString();
                }
            }
        }
        public void SerializeObject(WarehouseLocation location)
        {
            Stream stream = File.Open("Database.dat",
                FileMode.Create);

            BinaryFormatter bf = new BinaryFormatter();

            bf.Serialize(stream, location);
            stream.Close();

        }
        internal WarehouseLocation DeserializeObject()
        {
            Stream stream = File.Open("Database.dat", FileMode.Open);

            BinaryFormatter bf = new BinaryFormatter();

            WarehouseLocation location = (WarehouseLocation)bf.Deserialize(stream);
            stream.Close();
            return location;
        }
    }
}
