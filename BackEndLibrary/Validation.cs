using System;
using System.Collections.Generic;
using System.Text;

namespace BackEndLibrary
{
    public static class Validation
    {
        public static void ContainsInvalidCharsNums(string str)
        {
            string letters = "ABCDEFGHIJKLMNOPQRSTUVXYZ!#¤%&/()=?`^¨'*<>|+";

            foreach (char c in letters)
            {
                if (str.Contains(c.ToString()))
                {
                    throw new Exception("The side can not contain letters.");

                }
            }

        }
        public static void ContainsInvalidCharsLetters(string str)
        {
            string chars = "!#¤%&/()=?`^¨'*<>|+";

            foreach (char c in chars)
            {
                if (str.Contains(c.ToString()))
                {
                    throw new Exception("The id can only contain letters.");

                }
            }

        }
        public static bool CheckInvalidChars(string str, string type)
        {
            try
            {
                if (type == "num")
                {
                    ContainsInvalidCharsNums(str);
                    return true;
                }
                else
                {
                    ContainsInvalidCharsLetters(str);
                    return true;
                }

            }
            catch (Exception error)
            {
                Console.WriteLine(error);
                return false;
            }
        }
        public static bool CheckLength(string str)
        {
            if (str.Length > 10)
            {
                Console.WriteLine("The Id cannot be that long.");
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
