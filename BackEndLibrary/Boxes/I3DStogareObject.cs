using System;
using System.Text;
using System.Xml;
using Newtonsoft.Json;
using System.Xml.Serialization;
using System.Runtime.Serialization;
namespace BackEndLibrary
{
    public interface I3DStogareObject : ISerializable
    {

        string ID { get; }
        string Description { get; }
        double Weight { get; }
        double Volume { get; }
        double Area { get; }
        bool IsFragile { get; }
        double MaxDimensions { get; }
        double? InsuranceValue { get; }
    }
}
