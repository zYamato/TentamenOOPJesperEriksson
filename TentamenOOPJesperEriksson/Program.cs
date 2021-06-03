using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Globalization;
using BackEndLibrary;

namespace TentamenOOPJesperEriksson
{
    class Program
    {
        static void Main(string[] args)
        {

            Warehouse warehouse = new Warehouse();
            UserInterface userinterface = new UserInterface(warehouse);
            Console.OutputEncoding = Encoding.Unicode;
            Console.InputEncoding = Encoding.Unicode;
            userinterface.menu();
        }
    }
}
