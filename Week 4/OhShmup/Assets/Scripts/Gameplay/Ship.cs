using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// The player ship
/// </summary>
public class Ship : MonoBehaviour
{
    int health = 100;

    // first frame input support
    bool previousFrameShootInput = false;

    // saved for efficiency
    float colliderHalfHeight;

    // events fired by class
    HealthChanged healthChangedEvent = new HealthChanged();
    GameOver gameOverEvent = new GameOver();

	/// <summary>
	/// Use this for initialization
	/// </summary>
	void Start()
	{
        // save for efficiency
        BoxCollider2D collider = GetComponent<BoxCollider2D>();
        colliderHalfHeight = collider.size.y / 2;

        // add as event invoker for events
        EventManager.AddHealthChangedInvoker(this);
        EventManager.AddGameOverInvoker(this);
    }
	
	/// <summary>
	/// Update is called once per frame
	/// </summary>
	void Update()
	{
        // move based on input
        Vector3 position = transform.position;
        float verticalInput = Input.GetAxis("Vertical");
        if (verticalInput != 0)
        {
            position.y += verticalInput * 
                GameConstants.ShipMoveUnitsPerSecond *
                Time.deltaTime;
        }

        // move character to new position and clamp in screen
        transform.position = position;
        ClampInScreen();

        // check for shooting input
        if (Input.GetAxis("Shoot") > 0)
        {
            // only shoot on first input frame
            if (!previousFrameShootInput)
            {
                previousFrameShootInput = true;

                // shoot bullet
                Vector3 bulletPos = transform.position;
                bulletPos.x += GameConstants.ShipBulletOffset;
                GameObject bullet = ObjectPool.GetBullet();
                bullet.transform.position = bulletPos;
                bullet.SetActive(true);
                bullet.GetComponent<Bullet>().StartMoving(BulletDirection.Right);
            }
        }
        else
        {
            // no shoot input
            previousFrameShootInput = false;
        }
	}

    /// <summary>
    /// Processes trigger collisions with other game objects
    /// </summary>
    /// <param name="other">information about the other collider</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        // if colliding with a bullet, return bullet to pool
        // and take damage
        if (other.gameObject.CompareTag("Bullet"))
        {
            ObjectPool.ReturnBullet(other.gameObject);
            TakeDamage(GameConstants.ShipBulletCollisionDamage);
        }
        else if (other.gameObject.CompareTag("Enemy"))
        {
            // if colliding with an enemy, return enemy to pool
            // and take damage
            ObjectPool.ReturnEnemy(other.gameObject);
            TakeDamage(GameConstants.ShipEnemyCollisionDamage);
        }
    }

    /// <summary>
    /// Clamps the ship in the screen
    /// </summary>
    void ClampInScreen()
    {
        // clamp position as necessary
        Vector3 position = transform.position;
        if (position.y + colliderHalfHeight > ScreenUtils.ScreenTop)
        {
            position.y = ScreenUtils.ScreenTop - colliderHalfHeight;
        }
        else if (position.y - colliderHalfHeight < ScreenUtils.ScreenBottom)
        {
            position.y = ScreenUtils.ScreenBottom + colliderHalfHeight;
        }
        transform.position = position;
    }

    /// <summary>
    /// Takes the given amount of damage
    /// </summary>
    /// <param name="damage">damage</param>
    void TakeDamage(int damage)
    {
        health -= damage;
        if (health > 0)
        {
            healthChangedEvent.Invoke(health);
        }
        else
        {
            gameOverEvent.Invoke();
        }
    }

    /// <summary>
    /// Adds the given listener for the HealthChanged event
    /// </summary>
    /// <param name="listener">listener</param>
    public void AddHealthChangedListener(UnityAction<int> listener)
    {
        healthChangedEvent.AddListener(listener);
    }

    /// <summary>
    /// Adds the given listener for the GameOver event
    /// </summary>
    /// <param name="listener">listener</param>
    public void AddGameOverListener(UnityAction listener)
    {
        gameOverEvent.AddListener(listener);
    }
}
