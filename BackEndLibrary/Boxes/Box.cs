using System;
using System.Text;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace BackEndLibrary
{
    [Serializable()]
    public abstract class Box : I3DStogareObject, ISerializable
    {
        //Fields
        private string id;
        private string description;
        private double weight;
        private double volume;
        private double area;
        private bool isFragile;
        private double maxDimensions;
        private double? insuranceValue = null;

        protected Box()
        {
            this.ID = id;
        }

        //Properties
        public string ID{ get => id; set => id = value; }
        public string Description { get => description; set => description = value; }
        public double Weight { get => weight; set => weight = value; }
        public double Volume { get => volume; set => volume = value; }
        public double Area { get => area; set => area = value; }
        public bool IsFragile { get => isFragile; set => isFragile = value; }
        public double MaxDimensions { get => maxDimensions; set => maxDimensions = value; }
        public double? InsuranceValue { get => insuranceValue; set => insuranceValue = value; }
        public enum type
        {
            Cube, Cuboid, Sphere, Blob, Unknown
        }

        internal abstract int CalculateArea();
        internal abstract int CalculateVolume();
        internal abstract int CalculateMaxDimension();

        public override string ToString()
        {
            string str = "ID:" + this.ID;
            var sb = new StringBuilder(str);
            sb.Append(" Description:" + this.Description);
            sb.Append(" Weight:" + this.Weight + "kg");
            sb.Append(" Volume:" + this.Volume + "cm^3");
            sb.Append(" Area:" + this.Area + "cm^2");
            if(this.IsFragile == true)
            {
                sb.Append(" IsFragile?:Yes" );
            }
            else
            {
                sb.Append(" IsFragile?:No");
            }
            
            sb.Append(" Max Dimension:" + this.MaxDimensions + "cm");

            if(this.InsuranceValue != null)
            {
                sb.Append(" Insurance Value: " + this.InsuranceValue);
            }
            return sb.ToString();
        }

        public abstract void GetObjectData(SerializationInfo info, StreamingContext context);

        protected Box(SerializationInfo serializationInfo, StreamingContext streamingContext)
        {
            throw new NotImplementedException();
        }
    }
}
