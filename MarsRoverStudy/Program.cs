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
            string upperCoordinates = "5 5";
            string firstRover = "1 2 N";
            string firstRoverNav = "LMLMLMLMM";
            string secondRover = "3 3 E";
            string secondRoverNav = "MMRMMRMRRM";
            string thirdRover = "3 3 S";
            string thirdRoverNav = "MMRMMRMRRM";
            Dictionary<string, string> listCoordinates = new Dictionary<string, string>();
            listCoordinates.Add(firstRover, firstRoverNav);
            listCoordinates.Add(secondRover, secondRoverNav);
            listCoordinates.Add(thirdRover, thirdRoverNav);
            #endregion

            List<string> results = GetDirection(upperCoordinates, listCoordinates);

            foreach (var item in results)
            {
                Console.WriteLine(item);
            }
            Console.ReadLine();
        }
        /// <summary>
        /// rovers dictionary example: key is 3 3 E value is MMRMMRMRRM
        /// </summary>
        /// <param name="upperCoordinates"></param>
        /// <param name="rovers"></param>
        /// <returns></returns>
        public static List<string> GetDirection(string upperCoordinates, Dictionary<string, string> rovers)
        {
            List<string> results = new List<string>();

            foreach (var rover in rovers)
            {
                try
                {
                    if (rover.Key.Split(' ').Length < 3) { Console.WriteLine("Wrong Params"); throw new Exception("RoverParams are invalid"); }

                    int roverX = Convert.ToInt32(rover.Key.Split(' ')[0]);
                    int roverY = Convert.ToInt32(rover.Key.Split(' ')[1]);
                    string roverLetter = rover.Key.Split(' ')[2];

                    char[] navigation = rover.Value.ToCharArray();
                    foreach (var item in navigation)
                    {
                        Move(ref roverX, ref roverY, ref roverLetter, item.ToString());
                    }
                    results.Add(roverX.ToString() + " " + roverY.ToString() + " " + roverLetter.ToString());
                }
                catch (Exception ex)
                {
                    Console.WriteLine(string.Format("Exception occured on rover-->{0} ex--> {1}", rover, ex.ToString()));
                }
            }
            return results;
        }

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
                    throw new Exception("navigationLetter is not invalid "+ navigationLetter);
            }
        }
    }
}
