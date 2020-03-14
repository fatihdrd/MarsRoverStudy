using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRoverStudy
{
    class Program
    {
        static void Main(string[] args)
        {
            #region CreateTestData
            string input = "5 5\r\n" +
                            "1 2 N\r\n" +
                            "LMLMLMLMM\r\n" +
                            "3 3 E\r\n" +
                            "MMRMMRMRRM\r\n" +
                               "1 1 W\r\n" +
                            "MMMMMMMMM";

            #endregion
            string[] stringSeparators = new string[] { "\r\n" };
            string[] lines = input.Split(stringSeparators, StringSplitOptions.None);
          
            List<string> results = GetDirection(lines);

            foreach (var item in results)
            {
                Console.WriteLine(item);
            }
            Console.ReadLine();
        }

        /// <summary>
        /// this method takes input as array. Array's first item is upperCoordinates.
        /// </summary>
        /// <param name="lines"></param>
        /// <returns></returns>
        public static List<string> GetDirection(string[] lines)
        {
            List<string> results = new List<string>();
            string[] upperCoordinates = lines[0].Split(' ');
            for (int i = 1; i < lines.Length; i+=2)
            {
                try
                {
                    if (lines[i].Split(' ').Length < 3) { throw new Exception("RoverParams are invalid"); }
                  
                    string rover = lines[i];
                    string roverNavigation = lines[i+1];

                    int roverX = Convert.ToInt32(rover.Split(' ')[0]);
                    int roverY = Convert.ToInt32(rover.Split(' ')[1]);
                    string roverLetter = rover.Split(' ')[2];

                    char[] navigation = roverNavigation.ToCharArray();
                    foreach (var item in navigation)
                    {
                        Move(ref roverX, ref roverY, ref roverLetter, item.ToString());
                    }
                    string message = string.Empty;//if rover is out of the scope, write information message
                    CheckState(roverX, roverY, upperCoordinates,ref message);
                    results.Add(roverX.ToString() + " " + roverY.ToString() + " " + roverLetter.ToString()+ " "+message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(string.Format("Exception occured on rover-->{0} ex--> {1}", lines[i], ex.ToString()));
                }
               
            }
            return results;
        }

        /// <summary>
        /// this method check the state of item. item must less then upperCoordinate and bigger than lower coordinate(0,0)
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="upperCoorditanes"></param>
        /// <param name="message"></param>
        public static  void CheckState(int x,int y,string[] upperCoorditanes,ref string message)
        {
            if (x > Convert.ToInt32(upperCoorditanes[0]))
            {
                message+="rover x coordinate is bigger than upper x coordinate";
            }
            if (x < 0)
            {
                message+="rover x coordinate is  smaller than lower x coordinate";
            }
            if (y > Convert.ToInt32(upperCoorditanes[1]))
            {
                message+="rover y coordinate is bigger than upper y coordinate";
            }
            if (y < 0)
            {
                message+="rover y coordinate is  smaller than lower y coordinate";
            }
        }

        /// <summary>
        /// this method moves the item by params
        /// </summary>
        /// <param name="roverX"></param>
        /// <param name="roverY"></param>
        /// <param name="roverLetter"></param>
        /// <param name="navigationLetter"></param>
        public static void Move(ref int roverX, ref int roverY, ref string roverLetter, string navigationLetter)
        {
            switch (navigationLetter)
            {
                case "L":
                    if (roverLetter == "N")//North+Left =West
                    {
                        roverLetter = "W";
                    }
                    else if (roverLetter == "E")//East+Left =North
                    {
                        roverLetter = "N";
                    }
                    else if (roverLetter == "W")//West+Left=South
                    {
                        roverLetter = "S";
                    }
                    else//South+Left=East
                    {
                        roverLetter = "E";
                    }
                    break;
                case "R":
                    if (roverLetter == "N")//North+Right =East
                    {
                        roverLetter = "E";
                    }
                    else if (roverLetter == "E")//East+Right =South
                    {
                        roverLetter = "S";
                    }
                    else if (roverLetter == "W")//West+Right=North
                    {
                        roverLetter = "N";
                    }
                    else//South+Right=West
                    {
                        roverLetter = "W";
                    }
                    break;
                case "M":
                    if (roverLetter == "N")
                    {
                        roverY = roverY + 1;
                    }
                    else if (roverLetter == "E")
                    {
                        roverX = roverX + 1;
                    }
                    else if (roverLetter == "W")
                    {
                        roverX = roverX - 1;
                    }
                    else
                    {
                        roverY = roverY - 1;
                    }
                    break;
                default:
                    throw new Exception("navigationLetter is invalid "+ navigationLetter);
            }
        }
    }
}
