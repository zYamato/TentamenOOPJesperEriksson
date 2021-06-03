using System;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace BackEndLibrary
{
    [Serializable()]
    internal class Sphere : Box
    {
        private int radius = 0;

        public int Radius { get; set; }

        internal Sphere(int Radius, string id, string description, double weight, bool isFragile)
        {
            this.Radius = radius;
            this.ID = id;
            this.Description = description;
            this.Weight = weight;
            this.IsFragile = IsFragile;
            this.MaxDimensions = CalculateMaxDimension();
            this.Volume = CalculateVolume();
            this.Area = CalculateArea();
        }
        internal Sphere(int Radius, string id, string description, double weight, bool isFragile, double? insuranceValue)
        {
            this.Radius = radius;
            this.ID = id;
            this.Description = description;
            this.Weight = weight;
            this.IsFragile = IsFragile;
            this.InsuranceValue = insuranceValue;
            this.MaxDimensions = CalculateMaxDimension();
            this.Volume = CalculateVolume();
            this.Area = CalculateArea();
        }

        internal override int CalculateArea()
        {
            int area = radius * radius;

            return area;
        }

        internal override int CalculateVolume()
        {
            int volume = radius * radius * radius;
            return volume;
        }

        internal override int CalculateMaxDimension()
        {
            int Diameter = Radius * 2;
            return Diameter;
        }
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Radius", this.Radius);
            info.AddValue("ID", this.ID);
            info.AddValue("Description", this.Description);
            info.AddValue("Weight", this.Weight);
            info.AddValue("Volume", this.Volume);
            info.AddValue("Area", this.Area);
            info.AddValue("IsFragile", this.IsFragile);
            info.AddValue("MaxDimensions", this.MaxDimensions);
            info.AddValue("InsuranceValue", this.InsuranceValue);
        }

        internal Sphere(SerializationInfo info, StreamingContext streamingContext)
        {
            this.Radius = (int)info.GetValue("Radius", typeof(int));
            this.ID = (string)info.GetValue("ID", typeof(string));
            this.Description = (string)info.GetValue("Description", typeof(string));
            this.Weight = (double)info.GetValue("Weight", typeof(double));
            this.Volume = (double)info.GetValue("Volume", typeof(double));
            this.Area = (double)info.GetValue("Area", typeof(double));
            this.IsFragile = (bool)info.GetValue("IsFragile", typeof(bool));
            this.MaxDimensions = (double)info.GetValue("MaxDimensions", typeof(double));
            if (this.InsuranceValue != null)
            {
                this.InsuranceValue = (double?)info.GetValue("InsuranceValue", typeof(double?));
            }
        }
    }
}
