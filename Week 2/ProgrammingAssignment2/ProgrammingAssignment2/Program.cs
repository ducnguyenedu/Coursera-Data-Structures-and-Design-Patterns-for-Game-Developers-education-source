using AutograderGraphUtils;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using AutograderGraphUtils;

namespace ProgrammingAssignment2
{
    // Don't change ANY of the code in this file; if you
    // do, you'll break the automated grader!

    /// <summary>
    /// Programming Assignment 2
    /// </summary>
    class Program
    {
        // test case to run
        static int testCaseNumber;

        // size of current graph
        static GraphSize currentSize;

        // mapping between game object ids and waypoints
        static Dictionary<int, Waypoint> gameObjectIdWaypoints =
            new Dictionary<int, Waypoint>();

        /// <summary>
        /// Tests the SortedLinkedList and Traveler classes
        /// </summary>
        /// <param name="args">command-line args</param>
        static void Main(string[] args)
        { 
            // Graph test objects
            Graph<Waypoint> correctSmallGraph;
            Graph<Waypoint> correctMediumGraph;
            Graph<Waypoint> correctLargeGraph;

            // SearchNode test objects
            SearchNode<Waypoint> searchNode;

            // SortedLinkedList test objects
            SortedLinkedList<SearchNode<Waypoint>> testList =
                new SortedLinkedList<SearchNode<Waypoint>>();

            // GraphBuilder test object
            GraphBuilder graphBuilder;

            // Traveler test object
            Traveler traveler;

            // set up UnityEngine delegates
            GameObject.AddGetComponentDelegate(typeof(Waypoint), GetWaypointComponent);
            GameObject.AddFindObjectByTagDelegate(FindGameObjectWithTag);
            GameObject.AddFindObjectsByTagDelegate(FindGameObjectsWithTag);

            // correct graphs
            correctSmallGraph = GraphUtils.GetCorrectGraph(GraphSize.Small);
            correctMediumGraph = GraphUtils.GetCorrectGraph(GraphSize.Medium);
            correctLargeGraph = GraphUtils.GetCorrectGraph(GraphSize.Large);

            // loop while there's more input
            string input = Console.ReadLine();
            while (input[0] != 'q')
            {
                // extract test case number from string
                GetInputValueFromString(input);

                // execute selected test case
                switch (testCaseNumber)
                {
                    case 1:
                        // test SortedLinkedList Add method with single node
                        testList.Clear();
                        testList.Add(SearchNodeUtils.GetSearchNode(Vector3.zero, 1, 0));
                        if (testList.Count == 1 &&
                            ListContentsEqual(testList, "0"))
                        {
                            Console.WriteLine("Passed");
                        }
                        else
                        {
                            Console.WriteLine("FAILED");
                        }
                        break;
                    case 2:
                        // test SortedLinkedList Add method with two equal nodes
                        // (comparison is based on distance of search nodes)
                        testList.Clear();
                        testList.Add(SearchNodeUtils.GetSearchNode(Vector3.zero, 1, 0));
                        testList.Add(SearchNodeUtils.GetSearchNode(Vector3.zero, 2, 0));
                        if (testList.Count == 2 &&
                            ListContentsEqual(testList, "0c0"))
                        {
                            Console.WriteLine("Passed");
                        }
                        else
                        {
                            Console.WriteLine("FAILED");
                        }
                        break;
                    case 3:
                        // test SortedLinkedList Add method with multiple unequal nodes
                        // (comparison is based on distance of search nodes)
                        testList.Clear();
                        testList.Add(SearchNodeUtils.GetSearchNode(Vector3.zero, 1, 1));
                        testList.Add(SearchNodeUtils.GetSearchNode(Vector3.zero, 2, 0));
                        testList.Add(SearchNodeUtils.GetSearchNode(Vector3.zero, 3, 2));
                        if (testList.Count == 3 &&
                            ListContentsEqual(testList, "0c1c2"))
                        {
                            Console.WriteLine("Passed");
                        }
                        else
                        {
                            Console.WriteLine("FAILED");
                        }
                        break;
                    case 4:
                        // test SortedLinkedList Reposition method with 
                        // no change in position
                        // (comparison is based on distance of search nodes)
                        testList.Clear();
                        testList.Add(SearchNodeUtils.GetSearchNode(Vector3.zero, 1, 1));
                        testList.Add(SearchNodeUtils.GetSearchNode(Vector3.zero, 2, 0));
                        searchNode = SearchNodeUtils.GetSearchNode(Vector3.zero, 3, 2);
                        testList.Add(searchNode);
                        testList.Reposition(searchNode);
                        if (testList.Count == 3 &&
                            ListContentsEqual(testList, "0c1c2"))
                        {
                            Console.WriteLine("Passed");
                        }
                        else
                        {
                            Console.WriteLine("FAILED");
                        }
                        break;
                    case 5:
                        // test SortedLinkedList Reposition method with 
                        // change in position
                        // (comparison is based on distance of search nodes)
                        testList.Clear();
                        testList.Add(SearchNodeUtils.GetSearchNode(Vector3.zero, 1, 1));
                        testList.Add(SearchNodeUtils.GetSearchNode(Vector3.zero, 2, 0));
                        searchNode = SearchNodeUtils.GetSearchNode(Vector3.zero, 3, 2);
                        testList.Add(searchNode);
                        searchNode.Distance = 0.5f;
                        testList.Reposition(searchNode);
                        if (testList.Count == 3 &&
                            ListContentsEqual(testList, "0c0.5c1"))
                        {
                            Console.WriteLine("Passed");
                        }
                        else
                        {
                            Console.WriteLine("FAILED");
                        }
                        break;
                    case 6:
                        // test GraphBuilder class for small graph
                        currentSize = GraphSize.Small;
                        BuildGameObjectIdWaypointDictionary(currentSize);
                        graphBuilder = new GraphBuilder(new GameObject(int.MaxValue,
                            new Transform(Vector3.zero)));
                        graphBuilder.Awake();
                        if (GraphUtils.WaypointGraphsEqual(correctSmallGraph, 
                            GraphBuilder.Graph))
                        {
                            Console.WriteLine("Passed");
                        }
                        else
                        {
                            Console.WriteLine("FAILED");
                        }
                        break;
                    case 7:
                        // test GraphBuilder class for large graph
                        currentSize = GraphSize.Large;
                        BuildGameObjectIdWaypointDictionary(currentSize);
                        graphBuilder = new GraphBuilder(new GameObject(int.MaxValue,
                            new Transform(Vector3.zero)));
                        graphBuilder.Awake();
                        if (GraphUtils.WaypointGraphsEqual(correctLargeGraph,
                            GraphBuilder.Graph))
                        {
                            Console.WriteLine("Passed");
                        }
                        else
                        {
                            Console.WriteLine("FAILED");
                        }
                        break;
                    case 8:
                        // test Traveler class for small graph
                        currentSize = GraphSize.Small;
                        BuildGameObjectIdWaypointDictionary(currentSize);
                        GraphBuilder.Graph = correctSmallGraph;
                        traveler = new Traveler(new GameObject(int.MaxValue,
                            new Transform(Vector3.zero)));
                        traveler.Start();
                        if (WithinOneHundredth(traveler.PathLength, 9))
                        {
                            Console.WriteLine("Passed");
                        }
                        else
                        {
                            Console.WriteLine("FAILED");
                        }
                        break;
                    case 9:
                         // test Traveler class for medium graph
                        currentSize = GraphSize.Medium;
                        BuildGameObjectIdWaypointDictionary(currentSize);
                        GraphBuilder.Graph = correctMediumGraph;
                        traveler = new Traveler(new GameObject(int.MaxValue,
                            new Transform(Vector3.zero)));
                        traveler.Start();
                        if (WithinOneHundredth(traveler.PathLength, 18.0111f))
                        {
                            Console.WriteLine("Passed");
                        }
                        else
                        {
                            Console.WriteLine("FAILED");
                        }
                        break;
                    case 10:
                        // test Traveler class for large graph
                        currentSize = GraphSize.Large;
                        BuildGameObjectIdWaypointDictionary(currentSize);
                        GraphBuilder.Graph = correctLargeGraph;
                        traveler = new Traveler(new GameObject(int.MaxValue,
                            new Transform(Vector3.zero)));
                        traveler.Start();
                        if (WithinOneHundredth(traveler.PathLength, 19.4451f))
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
                        testList = new SortedLinkedList<SearchNode<Waypoint>>();
                        Console.WriteLine(ListContentsEqual(testList, ""));
                        break;
                    case 12:
                        // testing ListContentsEqual method
                        // 2 more elements in sorted list than expected
                        testList = new SortedLinkedList<SearchNode<Waypoint>>();
                        testList.Add(GetSearchNode(Vector3.zero,0,0));
                        testList.Add(GetSearchNode(Vector3.zero, 0, 0));
                        testList.Add(GetSearchNode(Vector3.zero, 0, 0));
                        Console.WriteLine(ListContentsEqual(testList, "0"));
                        break;
                    case 13:
                        // testing ListContentsEqual method
                        // 1 more element in sorted list than expected
                        testList = new SortedLinkedList<SearchNode<Waypoint>>();
                        testList.Add(GetSearchNode(Vector3.zero, 0, 0));
                        testList.Add(GetSearchNode(Vector3.zero, 0, 0));
                        testList.Add(GetSearchNode(Vector3.zero, 0, 0));
                        Console.WriteLine(ListContentsEqual(testList, "0,0"));
                        break;
                    case 14:
                        // testing ListContentsEqual method
                        // fewer elements in sorted list than expected
                        testList = new SortedLinkedList<SearchNode<Waypoint>>();
                        testList.Add(GetSearchNode(Vector3.zero, 0, 0));
                        Console.WriteLine(ListContentsEqual(testList, "0,0"));
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
        /// <param name="correctList">correct list (as c-separated string)</param>
        /// <returns>true if the list contents are equal, false otherwise</returns>
        static bool ListContentsEqual(SortedLinkedList<SearchNode<Waypoint>> sortedList,
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
            string correctValueString;
            float correctValue;
            LinkedListNode<SearchNode<Waypoint>> currentNode = sortedList.First;
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
                        correctValueString = correctList.Substring(0, cIndex);
                        correctValueString.Replace(',', '.');
                        correctValue = float.Parse(correctValueString,
                            CultureInfo.InvariantCulture);
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
                            correctValueString = correctList;
                            correctValueString.Replace(',', '.');
                            correctValue = float.Parse(correctValueString,
                                CultureInfo.InvariantCulture);
                        }
                    }
                }

                // debugging
                //Console.WriteLine("Sorted: " + sortedList[i].Distance);
                //Console.WriteLine("Correct: " + correctValue);

                // fail for non-match
                if (!WithinOneHundredth(currentNode.Value.Distance, correctValue))
                {
                    return false;
                }

                // move along list
                if (currentNode != null)
                {
                    currentNode = currentNode.Next;
                }
            }

            // all elements matched and list sizes matched
            return true;
        }

        #region Delegates for Unity engine

        /// <summary>
        /// Builds the mapping between game objects and waypoints for
        /// the given graph size
        /// </summary>
        /// <param name="size">graph size</param>
        static void BuildGameObjectIdWaypointDictionary(GraphSize size)
        {
            gameObjectIdWaypoints.Clear();
            Waypoint startWaypoint = GraphUtils.GetStartWaypoint(size);
            gameObjectIdWaypoints.Add(startWaypoint.gameObject.id, startWaypoint);
            Waypoint endWaypoint = GraphUtils.GetEndWaypoint(size);
            gameObjectIdWaypoints.Add(endWaypoint.gameObject.id, endWaypoint);
            Waypoint[] waypoints = GraphUtils.GetWaypoints(size);
            foreach (Waypoint waypoint in waypoints)
            {
                gameObjectIdWaypoints.Add(waypoint.gameObject.id, waypoint);
            }
        }

        /// <summary>
        /// Delegate to get the Waypoint component for a game object
        /// </summary>
        /// <param name="gameObject">game object</param>
        /// <returns>Waypoint component</returns>
        static Waypoint GetWaypointComponent(GameObject gameObject)
        {
            if (gameObjectIdWaypoints.ContainsKey(gameObject.id))
            {
                return gameObjectIdWaypoints[gameObject.id];
            }
            else
            {
                // critical failure
                return null;
            }
        }

        /// <summary>
        /// Delegate to find a game object by its tag
        /// </summary>
        /// <param name="tag">tag</param>
        /// <returns>corresponding game object</returns>
        static GameObject FindGameObjectWithTag(string tag)
        {
            if (tag == "Start")
            {
                switch (currentSize)
                {
                    case GraphSize.Small:
                        return GraphUtils.GetStartWaypoint(GraphSize.Small).gameObject;
                    case GraphSize.Medium:
                        return GraphUtils.GetStartWaypoint(GraphSize.Medium).gameObject;
                    case GraphSize.Large:
                        return GraphUtils.GetStartWaypoint(GraphSize.Large).gameObject;
                }
            }
            else if (tag == "End")
            {
                switch (currentSize)
                {
                    case GraphSize.Small:
                        return GraphUtils.GetEndWaypoint(GraphSize.Small).gameObject;
                    case GraphSize.Medium:
                        return GraphUtils.GetEndWaypoint(GraphSize.Medium).gameObject;
                    case GraphSize.Large:
                        return GraphUtils.GetEndWaypoint(GraphSize.Large).gameObject;
                }
            }

            // should never get here
            return null;
        }

        /// <summary>
        /// Delegate to find all game objects by tag
        /// </summary>
        /// <param name="tag">tag</param>
        /// <returns>corresponding game objects</returns>
        static GameObject[] FindGameObjectsWithTag(string tag)
        {
            if (tag == "Waypoint")
            {
                switch (currentSize)
                {
                    case GraphSize.Small:
                        return GraphUtils.GetWaypointGameObjects(GraphSize.Small);
                    case GraphSize.Medium:
                        return GraphUtils.GetWaypointGameObjects(GraphSize.Medium);
                    case GraphSize.Large:
                        return GraphUtils.GetWaypointGameObjects(GraphSize.Large);
                }
            }

            // should never get here
            return null;
        }

        #endregion
    }
}
