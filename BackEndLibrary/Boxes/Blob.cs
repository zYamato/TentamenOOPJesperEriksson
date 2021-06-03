using System;
using System.Runtime.Serialization;

namespace BackEndLibrary
{
    [Serializable()]
    internal class Blob : Box
    {
        private int side = 0;

        public int XSide { get; set; }
        public int YSide { get; set; }
        public int ZSide { get; set; }

        internal Blob(int side, string id, string description, double weight)
        {
            this.XSide = side;
            this.YSide = side;
            this.ZSide = side;
            this.IsFragile = true;
            this.ID = id;
            this.Description = description;
            this.Weight = weight;
            this.MaxDimensions = CalculateMaxDimension();
            this.Area = CalculateArea();
            this.Volume = CalculateVolume();
        }
        internal Blob(int side, string id, string description, double weight, double? insuranceValue)
        {
            this.XSide = side;
            this.YSide = side;
            this.ZSide = side;
            this.IsFragile = true;
            this.ID = id;
            this.Description = description;
            this.Weight = weight;
            this.InsuranceValue = insuranceValue;
            this.MaxDimensions = CalculateMaxDimension();
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
        internal Blob(SerializationInfo info, StreamingContext streamingContext)
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
