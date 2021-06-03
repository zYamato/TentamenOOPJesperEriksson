using System;
using System.Text;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;


namespace BackEndLibrary
{
    [Serializable()]
    internal class Cube : Box, ISerializable
    {
        private int side = 0;

        public int XSide { get => side; set => side = value; }
        public int YSide { get => side; set => side = value; }
        public int ZSide { get => side; set => side = value; }

        internal Cube(int side, string id, string description, double weight, bool isFragile)
        {
            this.XSide = side;
            this.YSide = side;
            this.ZSide = side;
            this.ID = id;
            this.Description = description;
            this.Weight = weight;
            this.IsFragile = isFragile;
            this.MaxDimensions = CalculateMaxDimension();
            this.Area = CalculateArea();
            this.Volume = CalculateVolume();
        }
        internal Cube(int Side, string id, string description, double weight, double volume, double area, bool isFragile, double maxDimensions, double? insuranceValue)
        {
            this.XSide = side;
            this.YSide = side;
            this.ZSide = side;
            this.ID = id;
            this.Description = description;
            this.Weight = weight;
            this.IsFragile = isFragile;
            this.InsuranceValue = insuranceValue;
            this.MaxDimensions = CalculateMaxDimension(); ;
            this.Area = CalculateArea();
            this.Volume = CalculateVolume();
        }

        internal override int CalculateArea()
        {
            int area = side * side;

            return area;
        }

        internal override int CalculateVolume()
        {
            int volume = side * side * side;

            return volume;
        }
        internal override int CalculateMaxDimension()
        {
            return side;
        }
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("XSide", this.XSide);
            info.AddValue("YSide", this.YSide);
            info.AddValue("ZSide", this.ZSide);
            info.AddValue("ID", this.ID);
            info.AddValue("Description", this.Description);
            info.AddValue("Weight", this.Weight);
            info.AddValue("Volume", this.Volume);
            info.AddValue("Area", this.Area);
            info.AddValue("IsFragile", this.IsFragile);
            info.AddValue("MaxDimensions", this.MaxDimensions);
            info.AddValue("InsuranceValue", this.InsuranceValue);
            
        }

        internal Cube(SerializationInfo info, StreamingContext streamingContext)
        {
            this.XSide = (int)info.GetValue("XSide", typeof(int));
            this.YSide = (int)info.GetValue("YSide", typeof(int));
            this.ZSide = (int)info.GetValue("ZSide", typeof(int));
            this.ID = (string)info.GetValue("ID", typeof(string));
            this.Description = (string)info.GetValue("Description", typeof(string));
            this.Weight = (double)info.GetValue("Weight", typeof(double));
            this.Volume = (double)info.GetValue("Volume", typeof(double));
            this.Area = (double)info.GetValue("Area", typeof(double));
            this.IsFragile = (bool)info.GetValue("IsFragile", typeof(bool));
            this.MaxDimensions = (double)info.GetValue("MaxDimensions", typeof(double));
            if(this.InsuranceValue != null)
            {
                this.InsuranceValue = (double?)info.GetValue("InsuranceValue", typeof(double?));
            }
        }
    }
}
