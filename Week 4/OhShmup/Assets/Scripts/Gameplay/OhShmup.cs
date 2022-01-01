using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Game manager
/// </summary>
public class OhShmup : MonoBehaviour
{
	/// <summary>
	/// Awake is called before Start
	/// </summary>
	void Awake()
	{
        EventManager.AddGameOverListener(HandleGameOverEvent);	
	}

    /// <summary>
    /// Moves to game over scene
    /// </summary>
    void HandleGameOverEvent()
    {
        // stop enemy spawner
        EnemySpawner spawner = Camera.main.GetComponent<EnemySpawner>();
        spawner.Stop();

        // return all enemies and bullets to pools
        GameObject[] bullets = GameObject.FindGameObjectsWithTag("Bullet");
        for (int i = bullets.Length - 1; i >= 0; i--)
        {
            ObjectPool.ReturnBullet(bullets[i]);
        }
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = enemies.Length - 1; i >= 0; i--)
        {
            ObjectPool.ReturnEnemy(enemies[i]);
        }

        SceneManager.LoadScene("gameover");
    }
}
