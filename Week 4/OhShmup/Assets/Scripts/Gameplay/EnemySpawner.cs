using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// An enemy spawner
/// </summary>
public class EnemySpawner : MonoBehaviour
{
    Timer spawnTimer;

    // saved for efficiency
    float verticalBorderSize;
    float horizontalOffset;

	/// <summary>
	/// Use this for initialization
	/// </summary>
	void Start()
	{
        // save for efficiency
        GameObject enemy = ObjectPool.GetEnemy();
        Collider2D collider = enemy.GetComponent<PolygonCollider2D>();
        horizontalOffset = collider.bounds.size.x;
        verticalBorderSize = collider.bounds.size.y * 4;
        ObjectPool.ReturnEnemy(enemy);

        // set up spawn timer
        spawnTimer = gameObject.AddComponent<Timer>();
        spawnTimer.Duration = GameConstants.EnemySpawnDelaySeconds;
        spawnTimer.AddTimerFinishedListener(HandleSpawnTimerFinished);
        spawnTimer.Run();
	}

    /// <summary>
    /// Spawns a new enemy and restarts the spawn timer
    /// </summary>
    void HandleSpawnTimerFinished()
    {
        SpawnEnemy();
        spawnTimer.Run();
    }

    /// <summary>
    /// Stops the enemy spawner
    /// </summary>
    public void Stop()
    {
        spawnTimer.Stop();
    }

    /// <summary>
    /// Spawns an enemy in the game
    /// </summary>
    void SpawnEnemy()
    {
        // get random position
        Vector3 enemyPos = new Vector3(
            ScreenUtils.ScreenRight + horizontalOffset,
            Random.Range(ScreenUtils.ScreenTop - verticalBorderSize,
                ScreenUtils.ScreenBottom + verticalBorderSize),
            0);

        // spawn enemy object
        GameObject enemy = ObjectPool.GetEnemy();
        enemy.transform.position = enemyPos;
        enemy.SetActive(true);

        // kluge because some enemies are going faster when spawned
        enemy.GetComponent<Enemy>().Deactivate();

        enemy.GetComponent<Enemy>().Activate();
    }
}
