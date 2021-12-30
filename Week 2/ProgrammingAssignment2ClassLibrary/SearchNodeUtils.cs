using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ProgrammingAssignment2
{
    /// <summary>
    /// Provides SearchNode utils
    /// </summary>
    public static class SearchNodeUtils
    {

        /// <summary>
        /// Gets a search node with the given position and id
        /// </summary>
        /// <param name="position">position</param>
        /// <param name="waypointId">waypoint id</param>
        /// <param name="distance">distance</param>
        /// <returns>search node</returns>
        public static SearchNode<Waypoint> GetSearchNode(Vector3 position, 
            int waypointId, float distance)
        {
            GameObject gameObject = new GameObject(waypointId, 
                new Transform(position));
            Waypoint waypoint = new Waypoint(gameObject, waypointId);
            GraphNode<Waypoint> graphNode = new GraphNode<Waypoint>(waypoint);
            SearchNode<Waypoint> searchNode = new SearchNode<Waypoint>(graphNode);
            searchNode.Distance = distance;
            return searchNode;
        }
    }
}
