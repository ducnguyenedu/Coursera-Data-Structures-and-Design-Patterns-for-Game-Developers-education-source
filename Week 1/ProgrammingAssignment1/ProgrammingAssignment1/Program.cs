using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

namespace ProgrammingAssignment1
{
    /// <summary>
    /// Programming Assignment 1
    /// </summary>
    class Program
    {
        // test case to run
        static int testCaseNumber;

        /// <summary>
        /// Tests the Target and SortedList classes
        /// </summary>
        /// <param name="args">command-line args</param>
        static void Main(string[] args)
        {
            // loop while there's more input
            string input = Console.ReadLine();
            while (input[0] != 'q')
            {
                // Target test objects
                GameObject targetGameObject0;
                Target target0;
                GameObject targetGameObject1;
                Target target1;
                GameObject targetGameObject2;
                Target target2;

                // SortedList test object
                SortedList<Target> testList;

                // extract test case number from string
                GetInputValueFromString(input);

                // execute selected test case
                switch (testCaseNumber)
                {
                    case 1:
                        // test Target constructor, UpdateDistance method, and
                        // Distance property
                        targetGameObject0 = new GameObject(int.MaxValue,
                            new Transform(new Vector3(1, 1, 0)));
                        target0 = new Target(targetGameObject0, Vector3.zero);
                        if (WithinOneHundredth(target0.Distance, 1.414f))
                        {
                            Console.WriteLine("Passed");
                        }
                        else
                        {
                            Console.WriteLine("FAILED");
                        }
                        break;
                    case 2:
                        // test Target constructor and GameObject property
                        targetGameObject0 = new GameObject(int.MaxValue,
                            new Transform(Vector3.zero));
                        target0 = new Target(targetGameObject0, Vector3.zero);
                        if (targetGameObject0.Equals(target0.GameObject))
                        {
                            Console.WriteLine("Passed");
                        }
                        else
                        {
                            Console.WriteLine("FAILED");
                        }
                        break;
                    case 3:
                        // test Target CompareTo method where target0 precedes
                        // target1
                        targetGameObject0 = new GameObject(int.MaxValue,
                            new Transform(Vector3.zero));
                        target0 = new Target(targetGameObject0, Vector3.zero);
                        targetGameObject1 = new GameObject(int.MaxValue,
                            new Transform(new Vector3(1, 1, 1)));
                        target1 = new Target(targetGameObject1, Vector3.zero);
                        if (target0.CompareTo(target1) > 0)
                        {
                            Console.WriteLine("Passed");
                        }
                        else
                        {
                            Console.WriteLine("FAILED");
                        }
                        break;
                    case 4:
                        // test Target CompareTo method where target0 follows
                        // target1
                        targetGameObject0 = new GameObject(int.MaxValue,
                            new Transform(new Vector3(1, 1, 1)));
                        target0 = new Target(targetGameObject0, Vector3.zero);
                        targetGameObject1 = new GameObject(int.MaxValue,
                            new Transform(Vector3.zero));
                        target1 = new Target(targetGameObject1, Vector3.zero);
                        if (target0.CompareTo(target1) < 0)
                        {
                            Console.WriteLine("Passed");
                        }
                        else
                        {
                            Console.WriteLine("FAILED");
                        }
                        break;
                    case 5:
                        // test Target CompareTo method where target0 is in the
                        // same sort position as target1
                        targetGameObject0 = new GameObject(int.MaxValue,
                            new Transform(Vector3.zero));
                        target0 = new Target(targetGameObject0, Vector3.zero);
                        targetGameObject1 = new GameObject(int.MaxValue,
                            new Transform(Vector3.zero));
                        target1 = new Target(targetGameObject1, Vector3.zero);
                        if (target0.CompareTo(target1) == 0)
                        {
                            Console.WriteLine("Passed");
                        }
                        else
                        {
                            Console.WriteLine("FAILED");
                        }
                        break;
                    case 6:
                        // test SortedList constructor, Count and
                        // [] properties
                        testList = new SortedList<Target>();
                        targetGameObject0 = new GameObject(int.MaxValue,
                            new Transform(Vector3.zero));
                        target0 = new Target(targetGameObject0, Vector3.zero);
                        testList.Add(target0);
                        if (testList.Count == 1 &&
                            testList[0].Equals(target0))
                        {
                            Console.WriteLine("Passed");
                        }
                        else
                        {
                            Console.WriteLine("FAILED");
                        }
                        break;
                    case 7:
                        // test SortedList Add method where first item added
                        // precedes second item added
                        targetGameObject0 = new GameObject(int.MaxValue,
                            new Transform(Vector3.zero));
                        target0 = new Target(targetGameObject0, Vector3.zero);
                        targetGameObject1 = new GameObject(int.MaxValue,
                            new Transform(new Vector3(1, 1, 1)));
                        target1 = new Target(targetGameObject1, Vector3.zero);
                        testList = new SortedList<Target>();
                        testList.Add(target0);
                        testList.Add(target1);
                        if (ListContentsEqual(testList, "1.732051c0"))
                        {
                            Console.WriteLine("Passed");
                        }
                        else
                        {
                            Console.WriteLine("FAILED");
                        }
                        break;
                    case 8:
                        // test SortedList Add method where first item added
                        // precedes second item added
                        targetGameObject0 = new GameObject(int.MaxValue,
                            new Transform(Vector3.zero));
                        target0 = new Target(targetGameObject0, Vector3.zero);
                        targetGameObject1 = new GameObject(int.MaxValue,
                            new Transform(new Vector3(1, 1, 1)));
                        target1 = new Target(targetGameObject1, Vector3.zero);
                        testList = new SortedList<Target>();
                        testList.Add(target1);
                        testList.Add(target0);
                        if (ListContentsEqual(testList, "1.732051c0"))
                        {
                            Console.WriteLine("Passed");
                        }
                        else
                        {
                            Console.WriteLine("FAILED");
                        }
                        break;
                    case 9:
                        // test SortedList Add method where first item added
                        // is in same position as second item added
                        targetGameObject0 = new GameObject(int.MaxValue,
                            new Transform(Vector3.zero));
                        target0 = new Target(targetGameObject0, Vector3.zero);
                        targetGameObject1 = new GameObject(int.MaxValue,
                            new Transform(Vector3.zero));
                        target1 = new Target(targetGameObject1, Vector3.zero);
                        testList = new SortedList<Target>();
                        testList.Add(target0);
                        testList.Add(target1);
                        if (ListContentsEqual(testList, "0c0"))
                        {
                            Console.WriteLine("Passed");
                        }
                        else
                        {
                            Console.WriteLine("FAILED");
                        }
                        break;
                    case 10:
                        // Test SortedList with combination of Add and
                        // RemoveAt calls
                        targetGameObject0 = new GameObject(int.MaxValue,
                            new Transform(Vector3.zero));
                        target0 = new Target(targetGameObject0, Vector3.zero);
                        targetGameObject1 = new GameObject(int.MaxValue,
                            new Transform(new Vector3(1, 1, 1)));
                        target1 = new Target(targetGameObject1, Vector3.zero);
                        targetGameObject2 = new GameObject(int.MaxValue,
                            new Transform(new Vector3(2, 2, 2)));
                        target2 = new Target(targetGameObject2, Vector3.zero);
                        testList = new SortedList<Target>();
                        testList.Add(target2);
                        testList.Add(target0);
                        testList.Add(target1);
                        testList.RemoveAt(0);
                        testList.RemoveAt(1);
                        if (ListContentsEqual(testList, "1.732051"))
                        {
                            Console.WriteLine("Passed");
                        }
                        else
                        {
                            Console.WriteLine("FAILED");
                        }
                        break;

                        /*
                    // The following cases were for testing the ListContentsEqual
                    // method and are never run by the autograder
                    case 11:
                        // testing ListContentsEqual method
                        // empty sorted list
                        testList = new SortedList<Target>();
                        Console.WriteLine(ListContentsEqual(testList, ""));
                        break;
                    case 12:
                        // testing ListContentsEqual method
                        // 2 more elements in sorted list than expected
                        testList = new SortedList<Target>();
                        targetGameObject0 = new GameObject(
                            new Transform(Vector3.zero));
                        target0 = new Target(targetGameObject0, Vector3.zero);
                        targetGameObject1 = new GameObject(
                            new Transform(new Vector3(1, 1, 1)));
                        target1 = new Target(targetGameObject1, Vector3.zero);
                        targetGameObject2 = new GameObject(
                            new Transform(new Vector3(2, 2, 2)));
                        target2 = new Target(targetGameObject2, Vector3.zero);
                        testList = new SortedList<Target>();
                        testList.Add(target0);
                        testList.Add(target1);
                        testList.Add(target2);
                        Console.WriteLine(ListContentsEqual(testList, "3.464102"));
                        break;
                    case 13:
                        // testing ListContentsEqual method
                        // 1 more element in sorted list than expected
                        testList = new SortedList<Target>();
                        targetGameObject0 = new GameObject(
                            new Transform(Vector3.zero));
                        target0 = new Target(targetGameObject0, Vector3.zero);
                        targetGameObject1 = new GameObject(
                            new Transform(new Vector3(1, 1, 1)));
                        target1 = new Target(targetGameObject1, Vector3.zero);
                        targetGameObject2 = new GameObject(
                            new Transform(new Vector3(2, 2, 2)));
                        target2 = new Target(targetGameObject2, Vector3.zero);
                        testList = new SortedList<Target>();
                        testList.Add(target0);
                        testList.Add(target1);
                        testList.Add(target2);
                        Console.WriteLine(ListContentsEqual(testList, "3.464102,1.732051"));
                        break;
                    case 14:
                        // testing ListContentsEqual method
                        // fewer elements in sorted list than expected
                        testList = new SortedList<Target>();
                        targetGameObject0 = new GameObject(
                            new Transform(Vector3.zero));
                        target0 = new Target(targetGameObject0, Vector3.zero);
                        targetGameObject1 = new GameObject(
                            new Transform(new Vector3(1, 1, 1)));
                        target1 = new Target(targetGameObject1, Vector3.zero);
                        testList = new SortedList<Target>();
                        testList.Add(target1);
                        Console.WriteLine(ListContentsEqual(testList, "1.732051,0"));
                        break;
                        */
                }

                input = Console.ReadLine();
            }
        }

        /// <summary>
        /// Extracts the test case number from the given input string
        /// </summary>
        /// <param name="input">input string</param>
        static void GetInputValueFromString(string input)
        {
            testCaseNumber = int.Parse(input);
        }

        /// <summary>
        /// Tells whether or not the two amounts are within one hundredth of each other
        /// </summary>
        /// <param name="firstAmount">first amount</param>
        /// <param name="secondAmount">second amount</param>
        /// <returns>true if they are, false if they aren't</returns>
        static bool WithinOneHundredth(float firstAmount, float secondAmount)
        {
            return Math.Abs(firstAmount - secondAmount) <= 0.01f;
        }

        /// <summary>
        /// Compares the contents of the two lists and returns true if the 
        /// contents of the lists are equal and false otherwise
        /// </summary>
        /// <param name="sortedList">sorted list</param>
        /// <param name="correctList">correct list (as csv string)</param>
        /// <returns>true if the list contents are equal, false otherwise</returns>
        static bool ListContentsEqual(SortedList<Target> sortedList,
            string correctList)
        {
            // check for malformed sorted list. This method is never called 
            // with a correct list that's empty
            if (sortedList.Count == 0)
            {
                return false;
            }

            // compare each element of the lists
            int cIndex;
            float correctValue;
            for (int i = 0; i < sortedList.Count; i++)
            {
                // csv string requires special processing for elements
                // that aren't last in the list
                if (i < sortedList.Count - 1)
                {
                    cIndex = correctList.IndexOf('c');

                    // make sure correct list isn't exhausted
                    if (cIndex != -1)
                    {
                        correctValue = float.Parse(correctList.Substring(0, cIndex));
                        correctList = correctList.Substring(cIndex + 1);
                    }
                    else
                    {
                        // more elements in sorted list than expected
                        return false;
                    }
                }
                else
                {
                    // last element of sorted list, check for correct list
                    // exhausted
                    if (correctList.Length == 0)
                    {
                        // more elements in sorted list than expected
                        return false;
                    }
                    else
                    {
                        // make sure we don't have 2 or more elements left in correct
                        // list
                        cIndex = correctList.IndexOf('c');
                        if (cIndex != -1)
                        {
                            // more values expected in sorted list
                            return false;
                        }
                        else
                        {
                            correctValue = float.Parse(correctList);
                        }
                    }
                }

                // debugging
                //Console.WriteLine("Sorted: " + sortedList[i].Distance);
                //Console.WriteLine("Correct: " + correctValue);

                // fail for non-match
                if (!WithinOneHundredth(sortedList[i].Distance, correctValue))
                {
                    return false;
                }
            }

            // all elements matched and list sizes matched
            return true;
        }
    }
}
