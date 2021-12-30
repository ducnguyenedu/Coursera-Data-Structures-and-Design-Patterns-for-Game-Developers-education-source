using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Builds the graph
/// </summary>
public class GraphBuilder : MonoBehaviour
{
    static Graph<Waypoint> graph = new Graph<Waypoint>();
    [SerializeField] List<Waypoint> wayPoints = new List<Waypoint>();
    #region Constructor

    // Uncomment the code below after copying this class into the console
    // app for the automated grader. DON'T uncomment it now; it won't
    // compile in a Unity project

    /// <summary>
    /// Constructor
    /// 
    /// Note: The GraphBuilder class in the Unity solution doesn't 
    /// use a constructor; this constructor is to support automated grading
    /// </summary>
    /// <param name="gameObject">the game object the script is attached to</param>
    //public GraphBuilder(GameObject gameObject) :
    //    base(gameObject)
    //{
    //}

    #endregion

    /// <summary>
    /// Awake is called before Start
    ///
    /// Note: Leave this method public to support automated grading
    /// </summary>
    public void Awake()
    {
        Debug.Log("Awake Load");
        Waypoint start = GameObject.FindGameObjectWithTag("Start").GetComponent<Waypoint>();
        Waypoint end = GameObject.FindGameObjectWithTag("End").GetComponent<Waypoint>();
        GameObject[] waypoints = GameObject.FindGameObjectsWithTag("Waypoint");

        // add nodes (all waypoints, including start and end) to graph
        graph = new Graph<Waypoint>();
        graph.AddNode(start);
        graph.AddNode(end);
        foreach (GameObject waypoint in waypoints)
        {
            graph.AddNode(waypoint.GetComponent<Waypoint>());
        }

        // add neighbors for each node in graph
        foreach (GraphNode<Waypoint> firstNode in graph.Nodes)
        {
            foreach (GraphNode<Waypoint> secondNode in graph.Nodes)
            {
                // no self edges
                if (firstNode != secondNode)
                {
                    Vector2 positionDelta = firstNode.Value.Position -
                        secondNode.Value.Position;
                    if (Mathf.Abs(positionDelta.x) < 3.5f &&
                        Mathf.Abs(positionDelta.y) < 3f)
                    {
                        firstNode.AddNeighbor(secondNode, positionDelta.magnitude);
                    }
                }
            }
        }
        
                    
    }
   
    /// <summary>
    /// Gets and sets the graph
    /// 
    /// CAUTION: Set should only be used by the autograder
    /// </summary>
    /// <value>graph</value>
    public static Graph<Waypoint> Graph
    {
        get { return graph; }
        set { graph = value; }
    }
}
