using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using BackEndLibrary;

namespace TentamenOOPJesperEriksson
{
    public class UserInterface
    {
        private Warehouse warehouse;

        public UserInterface(Warehouse warehouse)
        {
            this.warehouse = warehouse;
        }

        public void menu()
        {
            while (true)
            {
                Console.WriteLine("Main Menu!");
                Console.WriteLine("[1] Create a box");
                Console.WriteLine("[2] Move a box");
                Console.WriteLine("[3] Search for a box");
                Console.WriteLine("[4] Retrive a box");
                Console.WriteLine("[5] Display single location");
                Console.WriteLine("[6] Display Wearhouse");
                Console.WriteLine("[7] Clone Box");
                Console.WriteLine("[8] Exit");
                ConsoleKey input = Console.ReadKey().Key;

                switch (input)
                {
                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:
                        Console.Clear();
                        PromptCreateBox();
                        break;

                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:
                        Console.Clear();
                        PromptMoveBox();
                        break;

                    case ConsoleKey.D3:
                    case ConsoleKey.NumPad3:
                        Console.Clear();
                        PromptSearchBox();
                        break;

                    case ConsoleKey.D4:
                    case ConsoleKey.NumPad4:
                        Console.Clear();
                        PromptRemoveBox();
                        break;

                    case ConsoleKey.D5:
                    case ConsoleKey.NumPad5:
                        Console.Clear();
                        PromptWarehouseLocation();
                        break;
                    case ConsoleKey.D6:
                    case ConsoleKey.NumPad6:
                        Console.Clear();
                        this.warehouse.DisplayWearhouse();
                        break;
                    case ConsoleKey.D7:
                    case ConsoleKey.NumPad7:
                        Console.Clear();
                        PromptClone();
                        break;

                    case ConsoleKey.D8:
                    case ConsoleKey.NumPad8:
                    case ConsoleKey.Escape:
                        Console.Clear();
                        Console.WriteLine("Are you sure you want to exit? y/n");
                        ConsoleKey exitYN = Console.ReadKey().Key;

                        if (exitYN == ConsoleKey.Y || exitYN == ConsoleKey.Escape)
                        {
                            Environment.Exit(0);
                        }
                        break;
                }
            }
        }
        public void PromptCreateBox()
        {
            //default values
            int XSide = 0;
            int YSide = 0;
            int ZSide = 0;
            int radius = 0;
            string id = "";
            string description = "";
            double weight = 0;
            bool isFragile = false;
            bool isDone = false;

            //prompts what kind of box it is and how big it sizes are.
            do
            {
                Console.WriteLine("What kind of box would you like to create? (Cube, Cubeoid, Sphere or Blob)");
                string prompt = Console.ReadLine().ToUpper();

                if (prompt == "Cube".ToUpper())
                {
                    XSide = PromptXSide();
                    id = PromptId();
                    description = PromptDesc();
                    weight = PromptWeight();
                    isFragile = PromptIsFragile();
                    PromptLocation(this.warehouse.CreateCube(XSide, id, description, weight, isFragile));
                    BinaryManager.SaveWarehouse(this.warehouse);
                    isDone = true;
                }

                else if (prompt == "Cuboid".ToUpper())
                {
                    XSide = PromptXSide();
                    YSide = PromptYSide();
                    ZSide = PromptZSide();
                    id = PromptId();
                    description = PromptDesc();
                    weight = PromptWeight();
                    isFragile = PromptIsFragile();
                    PromptLocation(this.warehouse.CreateCuboid(XSide, YSide, ZSide, id, description, weight, isFragile));
                    BinaryManager.SaveWarehouse(this.warehouse);
                    isDone = true;
                }

                else if (prompt == "Sphere".ToUpper())
                {
                    radius = PromptRadius();
                    id = PromptId();
                    description = PromptDesc();
                    weight = PromptWeight();
                    isFragile = PromptIsFragile();
                    PromptLocation(this.warehouse.CreateSphere(radius, id, description, weight, isFragile));
                    BinaryManager.SaveWarehouse(this.warehouse);
                    isDone = true;
                }

                if (prompt == "Blob".ToUpper())
                {
                    XSide = PromptXSide();
                    id = PromptId();
                    description = PromptDesc();
                    weight = PromptWeight();
                    PromptLocation(this.warehouse.CreateBlob(XSide, id, description, weight));
                    BinaryManager.SaveWarehouse(this.warehouse);
                    isDone = true;
                }

            } while (isDone == false);
            
            
        }
        public int PromptXSide()
        {
            return CollectNumberInput("How wide is the box?");
        }
        public int PromptYSide()
        {
            return CollectNumberInput("How tall is the box?");
        }
        public int PromptZSide()
        {
            return CollectNumberInput("How deep is the box?");
        }
        public int PromptRadius()
        {
            return CollectNumberInput("What is the radius of the sphere?");
        }
        public string PromptId() 
        {
            string id = CollectNumberInput("What is the id of the box?").ToString();
            if (this.warehouse.IDValidation(id) == true)
            {
                return id;
            }
            else
            {
                Console.WriteLine("There is already a package with that id in the warehouse.");
                return PromptId();
            }
            
        }
        public string PromptDesc()
        {
            return CollectStringInput("What is the description of the box?");
        }
        public string PromptInsuranceValue()
        {
            bool hasInsuranceValue = false;
            Console.WriteLine("Does the box have insurance value? (Y/N)");
            ConsoleKey input = Console.ReadKey().Key;
            Console.Clear();

            switch (input)
            {
                case ConsoleKey.Y:
                    hasInsuranceValue = true;
                    break;

                case ConsoleKey.N:
                    hasInsuranceValue = false;
                    break;
            }
            if(hasInsuranceValue == true)
            {
                return CollectNumberInput("How much is the insurance value of the box?").ToString();
            }
            return null;

        }
        public double PromptWeight()
        {
            return CollectNumberInput("How heavy is the box?");
        }
        public bool PromptIsFragile()
        {
            bool isFragile = false;
            Console.WriteLine("Is the box Fragile? (Y/N)");
            ConsoleKey input = Console.ReadKey().Key;
            Console.Clear();

            switch (input)
            {
                case ConsoleKey.Y:
                    isFragile = true;
                    break;

                case ConsoleKey.N:
                    isFragile = false;
                    break;
            }
            return isFragile;



        }
        public int CollectNumberInput(string question)
        {
            bool validInput = false;
            string promptSide;
            do
            {
                Console.WriteLine(question);
                promptSide = Console.ReadLine().ToUpper();
                if(Validation.CheckInvalidChars(promptSide,"num") == true)
                {
                    validInput = true;
                }

            } while (!validInput);
            try
            {
                Validation.CheckLength(promptSide);
                int XSide = int.Parse(promptSide);
                return XSide;
            }
            catch(Exception error)
            {
                Console.WriteLine(error);
            }
            return -1;
            

        }
        public string CollectStringInput(string question)
        {
            bool validInput = false;
            string promptID;
            do
            {
                Console.WriteLine(question);
                promptID = Console.ReadLine().ToUpper();
                if (Validation.CheckInvalidChars(promptID, "letter") == true)
                {
                    validInput = true;
                }



            } while (!validInput);
            string str = promptID;
            validInput = true;
            return str;

        }
        public void PromptLocation(Box item)
        {
            string input = CollectStringInput("Would you like to store the box manually (manu) or automatically (auto)").ToUpper();

            switch (input)
            {
                case "AUTOMATICALLY":
                case "AUTO":
                    this.warehouse.StoreAuto(item);
                    return;
                case "MANUALLY":
                case "MANU":
                    int level = CollectNumberInput("At what level would you like to store the box?");
                    int position = CollectNumberInput("At what position out of 100 would you like to store your box?");
                    this.warehouse.StoreManually(item,position,level);
                    return;
                default:
                    Console.WriteLine("That is not a valid input.");
                    PromptLocation(item);
                    break;
            }
        }
        public void PromptSearchBox()
        {
            int idPrompt = CollectNumberInput(("What is the id of the box?"));
            string id = idPrompt.ToString();
            int[] index = this.warehouse.SearchBox(id);
            if(index[0] != -1)
            {

                Console.WriteLine("Available at level " + (index[0]) + " and at position " + (index[1]));
            }
            else
            {
                Console.WriteLine("There is no package with that id.");
            }


        }
        public void PromptRemoveBox()
        {
            string id = CollectNumberInput("What box with what id do you want to remove from the warehouse?").ToString();
            int result = this.warehouse.RetriveBox(id);
            if(result == 1)
            {
                Console.WriteLine("Box removed");
            }
            else
            {
                Console.WriteLine("There is no box with that id");
            }
        }
        public void PromptMoveBox()
        {
            string id = CollectNumberInput("What box would you like to move? (id)").ToString();
            int row = CollectNumberInput("What level do you want to move the box to?");
            int col = CollectNumberInput("What position do you want to move the box to?");
            bool result = this.warehouse.MoveBox(id,row-1,col-1);
            if(result == true)
            {
                Console.WriteLine("Box has been moved to level " + row + " and to position " + col);
            }
            else
            {
                Console.WriteLine("Box cant be moved to that position.");
            }
            
        }
        public void CollectClone(int row,int col)
        {
            if (row < 0 || col < 0 || row > 2 || col > 99)
            {
                Console.WriteLine("Position does not exist.");
            }
            WarehouseLocation location = this.warehouse.FetchWarehouseClone(row,col);
            if(location.ToString() == "")
            {
                Console.WriteLine("An empty location has been cloned");
            }
            else
            {
                Console.WriteLine(location.ToString() + "has been cloned");
            }

            
        }
        public void PromptClone()
        {
            int row = CollectNumberInput("At what level is the box you want to clone?");
            int col = CollectNumberInput("At what position is the box you want to clone?");
            CollectClone(row, col);
        }
        public void PromptWarehouseLocation()
        {
            int levelIndex = CollectNumberInput("What level would you like to display?");
            int locationIndex = CollectNumberInput("What position on level " + levelIndex + " would you like to display?");
            if(warehouse[levelIndex-1, locationIndex-1] == "")
            {
                Console.WriteLine("Empty Location");
            }
            else
            {
                Console.WriteLine(warehouse[levelIndex-1, locationIndex-1]);
            }
        }
    }
}
