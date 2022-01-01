using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A bullet
/// </summary>
public class Bullet : MonoBehaviour
{
    // saved for efficiency
    Rigidbody2D rb2d;
    Vector2 forceVector;

    #region Constructor

    // Uncomment the code below after you copy this class into the console
    // app for the automated grader. DON'T uncomment it now; it won't
    // compile in a Unity project

    /// <summary>
    /// Constructor
    /// 
    /// Note: The class in the Unity solution doesn't use a constructor;
    /// this constructor is to support automated grading
    /// </summary>
    /// <param name="gameObject">the game object the script is attached to</param>
    //public Bullet(GameObject gameObject) :
    //    base(gameObject)
    //{
    //}

    #endregion

    #region Property for autograder only

    /// <summary>
    /// Gets the force vector for the bullet
    /// </summary>
    public Vector2 ForceVector
    {
        get { return forceVector; }
    }

    #endregion

    /// <summary>
    /// Initializes object. We don't use Start for this because
    /// we want to initialize objects as they're added to the
    /// pool
    /// </summary>
    public void Initialize()
    {       
        // save Rigidbody2D for efficiency
        rb2d = GetComponent<Rigidbody2D>();

        // set force vector
        // Caution: you MUST use the bullet impulse force from
        // GameConstants
        forceVector = new Vector2(
            GameConstants.BulletImpulseForce, 0);
    }

    /// <summary>
    /// Starts the bullet moving in the given direction
    /// </summary>
    /// <param name="direction">movement direction</param>
    public void StartMoving(BulletDirection direction)
    {
        // apply impulse force to get projectile moving
        if (direction == BulletDirection.Left)
        {
            forceVector.x = -GameConstants.BulletImpulseForce;
        }
        else
        {
            forceVector.x = GameConstants.BulletImpulseForce;
        }
        rb2d.AddForce(forceVector, ForceMode2D.Impulse);
    }

    /// <summary>
    /// Stops the bullet
    /// </summary>
    public void StopMoving()
    {
        rb2d.velocity = Vector2.zero;
    }

    /// <summary>
    /// Update is called every frame
    /// </summary>
    void Update()
    {
        // this is a very ugly fix for a rare problem
        // Once in a while, the game has a bullet
        // that's not moving in the scene. This 
        // would take hours to debug, since everything
        // works fine almost all the time, so instead
        // I've implemented the kluge below

        // if a bullet is active and not moving,
        // return it to the pool
        if (gameObject.activeInHierarchy &&
            rb2d.velocity.magnitude < 0.1f)
        {
            ObjectPool.ReturnBullet(gameObject);
        }
    }

    /// <summary>
    /// Called when the bullet becomes invisible
    /// </summary>
    void OnBecameInvisible()
    {
        // return to the pool
        ObjectPool.ReturnBullet(gameObject);
    }

    /// <summary>
    /// Processes trigger collisions with other game objects
    /// </summary>
    /// <param name="other">information about the other collider</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        // if colliding with a bullet, return both to pool
        if (other.gameObject.CompareTag("Bullet"))
        {
            ObjectPool.ReturnBullet(other.gameObject);
            ObjectPool.ReturnBullet(gameObject);
        }
        else if (other.gameObject.CompareTag("Enemy"))
        {
            // if colliding with enemy return both to 
            // their respective pools
            ObjectPool.ReturnEnemy(other.gameObject);
            ObjectPool.ReturnBullet(gameObject);
        }
    }
}
