using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Events;

public class Traveler : MonoBehaviour
{
    [SerializeField]
    GameObject prefabExplosion;

    LinkedList<Waypoint> path;
    LinkedListNode<Waypoint> currentTarget;

    Rigidbody2D rb2d;
    const float ImpulseForceMagnitude = 2.0f;

    float pathLength = 0;

    PathFoundEvent pathFoundEvent = new PathFoundEvent();
    PathTraversalCompleteEvent pathTraversalCompleteEvent = new PathTraversalCompleteEvent();

    public float PathLength => pathLength;

    public void Start()
    {
        GraphBuilder builder = new GraphBuilder();
        builder.Awake();
        EventManager.AddPathFoundInvoker(this);
        EventManager.AddPathTraversalCompleteInvoker(this);

        // find the shortest path from start to end
        Waypoint start = GameObject.FindGameObjectWithTag("Start").GetComponent<Waypoint>();
        Waypoint end = GameObject.FindGameObjectWithTag("End").GetComponent<Waypoint>();
        path = Search(start, end, GraphBuilder.Graph);

        // move to start node and follow path (already at start node)
        rb2d = GetComponent<Rigidbody2D>();
        transform.position = start.transform.position;
        currentTarget = path.First;
        GoToNextPathWaypoint();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // only change for target waypoint
        if (other.gameObject == currentTarget.Value.gameObject)
        {
            GoToNextPathWaypoint();
        }
    }

    public void AddPathFoundListener(UnityAction<float> listener)
    {
        pathFoundEvent.AddListener(listener);
    }

    public void AddPathTraversalCompleteListener(UnityAction listener)
    {
        pathTraversalCompleteEvent.AddListener(listener);
    }

    public LinkedList<Waypoint> Search(Waypoint start, Waypoint end,
        Graph<Waypoint> graph)
    {
        SortedLinkedList<SearchNode<Waypoint>> searchList =
            new SortedLinkedList<SearchNode<Waypoint>>();

        Dictionary<GraphNode<Waypoint>, SearchNode<Waypoint>> mapping =
            new Dictionary<GraphNode<Waypoint>, SearchNode<Waypoint>>();

        Debug.Log("Count "+graph.Count);
        GraphNode<Waypoint> startNode = graph.Find(start);
        GraphNode<Waypoint> endNode = graph.Find(end);

        foreach (GraphNode<Waypoint> node in graph.Nodes)
        {
            SearchNode<Waypoint> searchNode = new SearchNode<Waypoint>(node);

            if (node == startNode)
            {
                searchNode.Distance = 0;
            }

            searchList.Add(searchNode);

            mapping.Add(node, searchNode);
        }

        string debug = ConvertSearchListToString(searchList);
        Debug.Log(debug);
        // While the search list isn't empty
        while (searchList.Count > 0)
        {
            SearchNode<Waypoint> currentSearchNode = searchList.First.Value;

            searchList.RemoveFirst();

            GraphNode<Waypoint> currentGraphNode = currentSearchNode.GraphNode;

            mapping.Remove(currentGraphNode);

            if (currentGraphNode == endNode)
            {

                pathFoundEvent.Invoke(currentSearchNode.Distance);
                return BuildWaypointPath(currentSearchNode);
            }

            foreach (GraphNode<Waypoint> neighbor in currentGraphNode.Neighbors)
            {
                if (mapping.ContainsKey(neighbor))
                {
                    float currentDistance = currentSearchNode.Distance +
                        currentGraphNode.GetEdgeWeight(neighbor);

                    SearchNode<Waypoint> neighborSearchNode = mapping[neighbor];

                    if (currentDistance < neighborSearchNode.Distance)
                    {
                        neighborSearchNode.Distance = currentDistance;
                        neighborSearchNode.Previous = currentSearchNode;
                        searchList.Reposition(neighborSearchNode);
                        debug = ConvertSearchListToString(searchList);
                        Debug.Log(debug);
                    }

                }
            }
        }

        // didn't find a path from start to end nodes
        return null;
    }

    LinkedList<Waypoint> BuildWaypointPath(SearchNode<Waypoint> endNode)
    {
        LinkedList<Waypoint> path = new LinkedList<Waypoint>();
        path.AddFirst(endNode.GraphNode.Value);
        pathLength = endNode.Distance;
        SearchNode<Waypoint> previous = endNode.Previous;
        while (previous != null)
        {
            path.AddFirst(previous.GraphNode.Value);
            previous = previous.Previous;
        }

        return path;
    }

    string ConvertSearchListToString(SortedLinkedList<SearchNode<Waypoint>> searchList)
    {
        StringBuilder listString = new StringBuilder();
        LinkedListNode<SearchNode<Waypoint>> currentNode = searchList.First;
        while (currentNode != null)
        {
            listString.Append("[");
            listString.Append(currentNode.Value.GraphNode.Value.Id + " ");
            listString.Append(currentNode.Value.Distance + "] ");
            currentNode = currentNode.Next;
        }
        return listString.ToString();
    }

    void GoToNextPathWaypoint()
    {
        currentTarget = currentTarget.Next;
        if (currentTarget == null)
        {
            // reached end, blow up waypoints on path
            rb2d.velocity = Vector2.zero;
            pathTraversalCompleteEvent.Invoke();
            BlowUpWaypoints();
        }
        else
        {
            Vector2 direction = new Vector2(
                currentTarget.Value.transform.position.x - transform.position.x,
                currentTarget.Value.transform.position.y - transform.position.y);
            direction.Normalize();
            rb2d.velocity = Vector2.zero;
            rb2d.AddForce(direction * ImpulseForceMagnitude,
                ForceMode2D.Impulse);
        }
    }

    void BlowUpWaypoints()
    {
        // take start and end nodes out of path
        path.RemoveFirst();
        path.RemoveLast();

        // blow up waypoints on path
        LinkedListNode<Waypoint> currentWaypoint = path.First;
        while (currentWaypoint != null)
        {
            Instantiate(prefabExplosion, currentWaypoint.Value.transform.position,
                Quaternion.identity);
            Destroy(currentWaypoint.Value.gameObject);
            currentWaypoint = currentWaypoint.Next;
        }
    }
}