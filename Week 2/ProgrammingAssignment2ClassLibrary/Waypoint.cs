using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A waypoint
/// </summary>
public class Waypoint : MonoBehaviour
{
    // CAUTION: Don't make any changes to this class as you develop your solution
    // The version used by the automated grader is slightly different (but
    // compatible with this one), and you can't change the autograder version

    //[SerializeField]
    int id;

    #region Constructor

    /// <summary>
    /// Constructor
    /// 
    /// Note: The class in the Unity solution doesn't expose a constructor;
    /// this constructor is to support automated grading
    /// </summary>
    public Waypoint(GameObject gameObject, int id) :
        base(gameObject)
    {
        this.id = id;
    }

    #endregion

    /// <summary>
    /// Changes waypoint to green
    /// </summary>
    /// <param name="other">other collider</param>
    //void OnTriggerEnter2D(Collider2D other)
    //{
    //    SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
    //    spriteRenderer.color = Color.green;
    //}

    /// <summary>
    /// Gets the position of the waypoint
    /// </summary>
    /// <value>position</value>
    public Vector2 Position
    {
        get 
        { 
            return new Vector2(transform.position.x,
                transform.position.y); 
        }
    }

    /// <summary>
    /// Gets the unique id for the waypoint
    /// </summary>
    /// <value>unique id</value>
    public int Id
    {
        get { return id; }
    }

    /// <summary>
    /// Standard equals comparison
    /// </summary>
    /// <param name="obj">the object to compare to</param>
    /// <returns>true if the objects are equal, false otherwise</returns>
    public override bool Equals(object obj)
    {
        return Equals(obj as Waypoint);
    }

    /// <summary>
    /// Type-specific equals comparison
    /// </summary>
    /// <param name="waypoint">the waypoint to compare to</param>
    /// <returns>true if the waypoints are equal, false otherwise</returns>
    public bool Equals(Waypoint waypoint)
    {
        // comparison to a null waypoint returns false
        if (waypoint == null)
        {
            return false;
        }

        // compare ids
        return (Id == waypoint.Id);
    }

    /// <summary>
    /// Calculates a hash code for the waypoint
    /// </summary>
    /// <returns>the hash code</returns>
    public override int GetHashCode()
    {
        return id;
    }

    /// <summary>
    /// Converts waypoint to string
    /// </summary>
    /// <returns>string representation</returns>
    public override string ToString()
    {
        return id.ToString();
    }
}
