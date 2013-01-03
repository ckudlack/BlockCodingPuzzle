using System;
using System.Collections;
using System.IO;

namespace Stuff
{
    class PuzzleSolver
    {
        public double width;
        public int height;
        public ArrayList arr = new ArrayList();
        public string three = "01";
        public string fourptfive = "001";
        ArrayList[] branches;

        public void Program()
        {
            if (width > 48 || width < 3)
            {
                Console.WriteLine("Width not valid");
                return;
            }
            if (height > 10 || height < 1)
            {
                Console.WriteLine("Height not valid");
                return;
            }
            if ((width % 3) != 4.5 && (width % 3) != 0 && (width % 4.5) != 3 && (width % 4.5) != 0)
            {
                //0 Ways
                Console.WriteLine("0");
                return;
            }
            
            getCombinations(width, "");

            //Console.WriteLine("Combinations: " + arr.Count);

            if (height == 1 || height == 2)
            {
                Console.WriteLine(arr.Count);
                return;
            }

            string[] ways = (string[])arr.ToArray(typeof(string));
            branches = new ArrayList[arr.Count];

            for (int i = 0; i < ways.Length; i++)
            {
                if(branches[i]==null)
                {
                    branches[i] = new ArrayList();
                }

                for (int j = 0; j < ways.Length; j++)
                {
                    if (branches[j] == null)
                    {
                        branches[j] = new ArrayList();
                    }
                    if ((Convert.ToInt32(ways[i], 2) & Convert.ToInt32(ways[j], 2)) == 0)
                    {
                        if (!branches[i].Contains(j))
                        {
                            branches[i].Add(j);
                        }
                        if (!branches[j].Contains(i))
                        {
                            branches[j].Add(i);
                        }
                    }   
                }   
            }

            //Console.WriteLine("Finished branching");

            int modheight = height - 2;

            int[] origCount = new int[branches.Length];
            getCountFirstRow().CopyTo(origCount, 0);

            int[] newCount = new int[branches.Length];

            newCount=buildLevels(origCount);

            modheight--;

            for (int i = 0; i < modheight; i++)
            {
                newCount = buildLevels(newCount);
            }

            long total = 0;
            for (int i = 0; i < newCount.Length; i++)
            {
                total += newCount[i];
            }
            Console.WriteLine(total);
        }

        public int[] getCountFirstRow()
        {
            //takes in the reference array, previous int[] array with branch counts for each row type
            int[] typeCounts = new int[branches.Length];
            for (int i = 0; i < typeCounts.Length; i++)
            {
                foreach(object h in branches[i])
                {
                    typeCounts[(int)h]++;
                }
            }
            return typeCounts;
        }

        public int[] buildLevels(int[] refCount)
        {
            int[] newCount = new int[branches.Length];
            for (int i = 0; i < refCount.Length; i++)
            {
                    foreach (object h in branches[i])
                    {
                        newCount[(int)h] += refCount[i];
                    }
                //Console.WriteLine(i);
            }

            return newCount;
        }

        public void getCombinations(double num, string row)
        {
            if (num <= 0)
            {
                arr.Add(row.Substring(0, row.Length - 1));
                return;
            }
            if ((num % 3 == 0))
            {
                getCombinations(num - 3, row + three);
            }
            if ((num % 4.5 == 0))
            {
                getCombinations(num - 4.5, row + fourptfive);
            }
            if ((num > 3) && (num % 4.5 == 3))
            {
                getCombinations(num - 4.5, row + fourptfive);
            }
            if ((num - 4.5 > 3) && (num % 4.5 == 1.5))
            {
                getCombinations(num - 4.5, row + fourptfive);
            }
            if ((num > 4.5) && (num % 3 == 1.5))
            {
                getCombinations(num - 3, row + three);
            }
            return;
        }

        static void Main(string[] args)
        {
            PuzzleSolver PS = new PuzzleSolver();
            PS.width = Convert.ToInt16(args[0],10);
            PS.height = Convert.ToInt16(args[1], 10);
            PS.Program();
        }
    }
}