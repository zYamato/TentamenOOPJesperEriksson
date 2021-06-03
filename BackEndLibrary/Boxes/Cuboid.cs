using System;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace BackEndLibrary
{
    [Serializable()]
    internal class Cuboid : Box
    {
        private int xSide = 0;
        private int ySide = 0;
        private int zSide = 0;

        public int XSide { get; set; }
        public int YSide { get; set; }
        public int ZSide { get; set; }

        internal Cuboid(int XSide, int YSide, int ZSide, string id, string description, double weight, bool isFragile)
        {
            this.XSide = xSide;
            this.YSide = ySide;
            this.ZSide = zSide;
            this.ID = id;
            this.Description = description;
            this.Weight = weight;
            this.IsFragile = IsFragile;
            this.Area = CalculateArea();
            this.Volume = CalculateVolume();
            this.MaxDimensions = CalculateMaxDimension();
        }
        internal Cuboid(int XSide, int YSide, int ZSide, string id, string description, double weight, bool isFragile, double? insuranceValue)
        {
            this.XSide = xSide;
            this.YSide = ySide;
            this.ZSide = zSide;
            this.ID = id;
            this.Description = description;
            this.Weight = weight;
            this.IsFragile = IsFragile;
            this.InsuranceValue = insuranceValue;
            this.Area = CalculateArea();
            this.Volume = CalculateVolume();
            this.MaxDimensions = CalculateMaxDimension();
        }

        internal override int CalculateArea()
        {
            int[] Sides = {XSide,YSide,ZSide};

            Array.Sort(Sides);

            int area = Sides[0] * Sides[1];

            return area;
        }

        internal override int CalculateVolume()
        {
            int volume = XSide * YSide * ZSide;

            return volume;
        }
        internal override int CalculateMaxDimension()
        {
            int[] Sides = { XSide, YSide, ZSide };
            Array.Sort(Sides);

            int MaxDimension = Sides[0];
            return MaxDimension;
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

        internal Cuboid(SerializationInfo info, StreamingContext streamingContext)
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
            if (this.InsuranceValue != null)
            {
                this.InsuranceValue = (double?)info.GetValue("InsuranceValue", typeof(double?));
            }
        }
    }
}
