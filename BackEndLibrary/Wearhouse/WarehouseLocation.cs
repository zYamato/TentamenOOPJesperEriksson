using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace BackEndLibrary
{
    [Serializable()]
    public class WarehouseLocation : IEnumerable<WarehouseLocation>, ISerializable
    {
        private List<Box> contents;
        private Warehouse warehouse;
        private const int height = 300;
        private const int width = 200;
        private const int depth = 200;
        private const double maxWeight = 1000;
        private static double currentWeight = 0;
        private static double currentVolume = 0;
        private int row;
        private int col;
        // Kubik centimeter uträkning.
        private readonly int maxVolume = (height * width * depth);

        public WarehouseLocation(Warehouse warehouse)
        {
            this.contents = new List<Box>();
            this.CurrentVolume = currentVolume;
            this.CurrentWeight = currentWeight;
            this.warehouse = warehouse;
        }

        public double MaxVolume
        {
            get => maxVolume;
        }

        public double MaxWeight
        {
           get => maxWeight;
        }
            
        public int Height 
        { 
            get => height;
        } 
        public int Width
        {
            get => width;
        }
        public int Depth
        {
            get => depth;
        }
        public double CurrentWeight
        {
            get => currentWeight;
            set => currentWeight = value;
        }
        public double CurrentVolume
        {
            get => currentVolume;
            set => currentVolume = value;
        }
        public List<Box> Contents
        {
            get => contents;
        }
        public int Row
        {
            get => row;
            set => row = value;
        }
        public int Col
        {
            get => col;
            set => col = value;
        }

        public IEnumerator<WarehouseLocation> GetEnumerator()
        {
            foreach (WarehouseLocation warehouseLocation in warehouse.Location)
            {
                yield return warehouseLocation;
            }
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
        public override string ToString()
        {
            var sb = new StringBuilder();
            foreach(var item in this.contents)
            {
               sb.Append("\n " + "{" + item.ToString() + "}");
            }
            return sb.ToString();
        }
        public WarehouseLocation Content()
        {
            WarehouseLocation newLocation = new WarehouseLocation(warehouse);
            newLocation.contents = this.Contents;
            newLocation.CurrentVolume = this.CurrentVolume;
            newLocation.CurrentWeight = this.CurrentWeight;

            return newLocation;
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Contents", contents);
            info.AddValue("CurrentWeight", currentWeight);
            info.AddValue("CurrentVolume", currentVolume);
            info.AddValue("Row", row);
            info.AddValue("Col", col);
        }

        protected WarehouseLocation(SerializationInfo info, StreamingContext streamingContext)
        {
            contents = (List<Box>)info.GetValue("Contents", typeof(List<Box>));
            currentWeight = (double)info.GetValue("CurrentWeight", typeof(double));
            currentVolume = (double)info.GetValue("CurrentVolume", typeof(double));
            row = (int)info.GetValue("Row", typeof(int));
            col = (int)info.GetValue("Col", typeof(int));

        }
    }
}