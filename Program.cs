using System.Collections.Generic;
using System;
using System.IO;
using System.Linq;
using System.Diagnostics;

namespace PyramidChallenge
{
    class Program
    {

        static int[][] Pyramid;
        static void Main(string[] args)
        {
            // Stopwatch stopWatch = new Stopwatch();
            // stopWatch.Start();
            if (args.Length > 0)
            {
                int NumberOfLines = File.ReadLines(args[0]).Count();
                Pyramid = new int[NumberOfLines][];
                StreamReader reader = File.OpenText(args[0]);
                for (int i = 0; i<NumberOfLines; i++)
                {
                    // Read all values on line and convert to int array
                    string[] ValuesInLine = reader.ReadLine().Split(" ");
                    int[] ValueArray = Array.ConvertAll(ValuesInLine, int.Parse);
                    Pyramid[i] = ValueArray; // Add the int array to the pyramid
                }
                List<int> MaxPath = TracePath(new List<int>{Pyramid[0][0]}, 0, 0);
                // stopWatch.Stop();

                System.Console.WriteLine("Max sum: {0}", MaxPath.Sum());
                foreach (int i in MaxPath )
                {
                    System.Console.Write(i + " ");
                }
                System.Console.WriteLine();

                // System.Console.WriteLine(stopWatch.Elapsed);
                // Console.WriteLine("RunTime " + elapsedTime);
            }
            else
            {
                System.Console.WriteLine("Provide a .txt file containing a pyramid");
            }
        }


        /// <summary>
        /// Recursively traverses the pyramid by tracing paths from all eligible 
        /// children nodes, while maintaining a list of the path for all traces
        /// down to the bottom of the pyramid and returning the path with the 
        /// highest sum. 
        /// 
        /// <paramref name="row"/> refers to the row/line number of the pyramid
        /// while <paramref name="column"/> refers to the node position on that line.
        /// Example: row=1, column=1  is node with value 124 
        /// </summary>
        /// 
        /// <param name="path">A list of values of the traced path so far</param>
        /// <param name="row">Denotes which line we are currently positioned at</param>
        /// <param name="column">Denotes which node on the line</param>
        /// <returns>Returns the path down to the bottom of the pyramid which has the highest sum.</returns>
        public static List<int> TracePath(List<int> path, int row, int column)
        {
            if (row >= Pyramid.Length-1)
            {
                return path;
            }

            int LeftChild   = Pyramid[row+1][column]; // child below
            int RightChild  = Pyramid[row+1][column+1]; // child below and to the right
            int current = path.Last();
            List<int> LeftPath  = new List<int>{};
            List<int> RightPath = new List<int>{}; 

            if(current % 2 != LeftChild % 2) // if left child is a valid node
            {   
                // Trace a path down through left child
                LeftPath = TracePath(path.Append(LeftChild).ToList(), row+1, column);
            }
            if(current % 2 != RightChild % 2) // if right child is a valid node
            {
                // Trace a path down through right child
                RightPath = TracePath(path.Append(RightChild).ToList(), row+1, column+1);
            }

            // When both if-statements fail, an empty list is returned
            // In other words: When the path hits a dead end, we return an empty path.
            return LeftPath.Sum() > RightPath.Sum() ? LeftPath : RightPath;
        }
    }
}