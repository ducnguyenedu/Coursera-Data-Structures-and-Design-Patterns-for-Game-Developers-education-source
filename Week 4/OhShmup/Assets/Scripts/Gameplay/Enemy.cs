using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// An enemy
/// </summary>
public class Enemy : MonoBehaviour
{
    Timer shootTimer;

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
    //public Enemy(GameObject gameObject) :
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
        // Caution: you MUST use the enemy impulse force from
        // GameConstants
        forceVector = new Vector2(
            GameConstants.EnemyImpulseForce, 0);

        // set up shoot timer
        shootTimer = gameObject.AddComponent<Timer>();
        shootTimer.Duration = GameConstants.EnemyShootDelaySeconds;
        shootTimer.AddTimerFinishedListener(HandleShootTimerFinished);
    }

    /// <summary>
    /// Starts the enemy moving and starts the shoot timer
    /// </summary>
    public void Activate()
    {
        // apply impulse force to get enemy moving
        rb2d.AddForce(forceVector, ForceMode2D.Impulse);

        shootTimer.Run();
    }

    /// <summary>
    /// Stops the enemy and its shot timer
    /// </summary>
    public void Deactivate()
    {
        rb2d.velocity = Vector2.zero;
        shootTimer.Stop();
    }

    /// <summary>
    /// Called when the enemy becomes invisible
    /// </summary>
    void OnBecameInvisible()
    {
        // don't remove when spawned
        if (transform.position.x < 0)
        {
            // return to the pool
            ObjectPool.ReturnEnemy(gameObject);
        }
    }

    /// <summary>
    /// Shoots a bullet and restarts the shoot timer
    /// </summary>
    void HandleShootTimerFinished()
    {
        shootTimer.Run();

        // shoot bullet
        Vector3 bulletPos = transform.position;
        bulletPos.x += GameConstants.EnemyBulletXOffset;
        bulletPos.y += GameConstants.EnemyBulletYOffset;
        GameObject bullet = ObjectPool.GetBullet();
        bullet.transform.position = bulletPos;
        bullet.SetActive(true);
        bullet.GetComponent<Bullet>().StartMoving(BulletDirection.Left);
    }
}
