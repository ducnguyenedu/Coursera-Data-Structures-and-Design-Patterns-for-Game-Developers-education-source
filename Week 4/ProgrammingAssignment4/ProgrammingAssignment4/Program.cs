using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

namespace ProgrammingAssignment4
{
    // Don't change ANY of the code in this file; if you
    // do, you'll break the automated grader!

    /// <summary>
    /// Programming Assignment 4
    /// </summary>
    class Program
    {
        // test case to run
        static int testCaseNumber;

        // unique game object id support for bullets and enemies
        static int lastGameObjectId = -1;

        // mapping between game object ids and bullets
        static Dictionary<int, Bullet> gameObjectIdBullets =
            new Dictionary<int, Bullet>();

        // mapping between game object ids and enemies
        static Dictionary<int, Enemy> gameObjectIdEnemies =
            new Dictionary<int, Enemy>();

        /// <summary>
        /// Tests ObjectPool, Bullet, and Enemy classes
        /// </summary>
        /// <param name="args">command-line args</param>
        static void Main(string[] args)
        {
            // add listeners for creation events
            EventManager.AddBulletCreatedListener(HandleBulletCreatedEvent);
            EventManager.AddEnemyCreatedListener(HandleEnemyCreatedEvent);

            // set up UnityEngine delegates
            Resources.AddLoadDelegate(typeof(GameObject),
                LoadGameObject);
            GameObject.AddGetComponentDelegate(typeof(Bullet), GetBulletComponent);
            GameObject.AddGetComponentDelegate(typeof(Enemy), GetEnemyComponent);

            // initialize game objects
            ScreenUtils.Initialize();
            HUD hud = new HUD(new GameObject(int.MaxValue,
                new Transform(Vector3.zero)));
            Ship ship = new Ship(new GameObject(int.MaxValue,
                new Transform(Vector3.zero)));
            OhShmup ohShmup = new OhShmup(new GameObject(int.MaxValue,
                new Transform(Vector3.zero)));

            // test objects
            ObjectPool objectPool = new ObjectPool(new GameObject(int.MaxValue,
                new Transform(Vector3.zero)));
            GameObject bulletGameObject;
            GameObject enemyGameObject;

            // test object information
            int initialBulletPoolCount;
            int initialBulletPoolCapacity;
            int initialEnemyPoolCount;
            int initialEnemyPoolCapacity;

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
                        // test ObjectPool Initialize method
                        ObjectPool.Initialize();
                        if ((objectPool.GetPoolCount(PooledObjectName.Bullet) ==
                            GameConstants.InitialBulletPoolCapacity) &&
                            (objectPool.GetPoolCount(PooledObjectName.Enemy) ==
                            GameConstants.InitialEnemyPoolCapacity))
                        {
                            Console.WriteLine("Passed");
                        }
                        else
                        {
                            Console.WriteLine("FAILED");
                        }
                        break;
                    case 2:
                        // test ObjectPool EmptyPools method
                        ObjectPool.Initialize();
                        ObjectPool.EmptyPools();
                        if ((objectPool.GetPoolCount(PooledObjectName.Bullet) == 0) &&
                            (objectPool.GetPoolCount(PooledObjectName.Enemy) == 0))
                        {
                            Console.WriteLine("Passed");
                        }
                        else
                        {
                            Console.WriteLine("FAILED");
                        }
                        break;
                    case 3:
                        // test ObjectPool GetBullet method for bullet already
                        // in pool
                        ObjectPool.Initialize();
                        initialBulletPoolCount = 
                            objectPool.GetPoolCount(PooledObjectName.Bullet);
                        bulletGameObject = ObjectPool.GetBullet();
                        if (bulletGameObject != null &&
                            bulletGameObject.GetComponent<Bullet>() != null &&
                            (objectPool.GetPoolCount(PooledObjectName.Bullet) ==
                            initialBulletPoolCount - 1))
                        {
                            Console.WriteLine("Passed");
                        }
                        else
                        {
                            Console.WriteLine("FAILED");
                        }
                        break;
                    case 4:
                        // test ObjectPool GetBullet method when
                        // bullet pool has to grow
                        ObjectPool.Initialize();
                        initialBulletPoolCapacity =
                            objectPool.GetPoolCapacity(PooledObjectName.Bullet);
                        for (int i = 0; i < objectPool.GetPoolCapacity(PooledObjectName.Bullet); i++)
                        {
                            bulletGameObject = ObjectPool.GetBullet();
                        }
                        bulletGameObject = ObjectPool.GetBullet();
                        if (bulletGameObject != null &&
                            bulletGameObject.GetComponent<Bullet>() != null &&
                            (objectPool.GetPoolCapacity(PooledObjectName.Bullet) >
                            initialBulletPoolCapacity))
                        {
                            Console.WriteLine("Passed");
                        }
                        else
                        {
                            Console.WriteLine("FAILED");
                        }
                        break;
                    case 5:
                        // test ObjectPool GetEnemy method for enemy already
                        // in pool
                        ObjectPool.Initialize();
                        initialEnemyPoolCount =
                            objectPool.GetPoolCount(PooledObjectName.Enemy);
                        enemyGameObject = ObjectPool.GetEnemy();
                        if (enemyGameObject != null &&
                            enemyGameObject.GetComponent<Enemy>() != null &&
                            (objectPool.GetPoolCount(PooledObjectName.Enemy) ==
                            initialEnemyPoolCount - 1))
                        {
                            Console.WriteLine("Passed");
                        }
                        else
                        {
                            Console.WriteLine("FAILED");
                        }
                        break;
                    case 6:
                        // test ObjectPool GetEnemy method when
                        // enemy pool has to grow
                        ObjectPool.Initialize();
                        initialEnemyPoolCapacity =
                            objectPool.GetPoolCapacity(PooledObjectName.Enemy);
                        for (int i = 0; i < objectPool.GetPoolCapacity(PooledObjectName.Enemy); i++)
                        {
                            enemyGameObject = ObjectPool.GetEnemy();
                        }
                        enemyGameObject = ObjectPool.GetEnemy();
                        if (enemyGameObject != null &&
                            enemyGameObject.GetComponent<Enemy>() != null &&
                            (objectPool.GetPoolCapacity(PooledObjectName.Enemy) >
                            initialEnemyPoolCapacity))
                        {
                            Console.WriteLine("Passed");
                        }
                        else
                        {
                            Console.WriteLine("FAILED");
                        }
                        break;
                    case 7:
                        // test ObjectPool ReturnBullet method
                        ObjectPool.Initialize();
                        initialBulletPoolCount =
                            objectPool.GetPoolCount(PooledObjectName.Bullet);
                        bulletGameObject = ObjectPool.GetBullet();
                        ObjectPool.ReturnBullet(bulletGameObject);
                        if (objectPool.GetPoolCount(PooledObjectName.Bullet) ==
                            initialBulletPoolCount)
                        {
                            Console.WriteLine("Passed");
                        }
                        else
                        {
                            Console.WriteLine("FAILED");
                        }
                        break;
                    case 8:
                        // test ObjectPool ReturnEnemy method
                        ObjectPool.Initialize();
                        initialEnemyPoolCount =
                            objectPool.GetPoolCount(PooledObjectName.Enemy);
                        enemyGameObject = ObjectPool.GetEnemy();
                        ObjectPool.ReturnEnemy(enemyGameObject);
                        if (objectPool.GetPoolCount(PooledObjectName.Enemy) ==
                            initialEnemyPoolCount)
                        {
                            Console.WriteLine("Passed");
                        }
                        else
                        {
                            Console.WriteLine("FAILED");
                        }
                        break;
                    case 9:
                        // test Bullet Initialize method
                        ObjectPool.Initialize();
                        bulletGameObject = ObjectPool.GetBullet();
                        Bullet bullet = bulletGameObject.GetComponent<Bullet>();
                        bullet.Initialize();
                        if (WithinOneHundredth(bullet.ForceVector.x, 
                            GameConstants.BulletImpulseForce) &&
                            WithinOneHundredth(bullet.ForceVector.y, 0))
                        {
                            Console.WriteLine("Passed");
                        }
                        else
                        {
                            Console.WriteLine("FAILED");
                        }
                        break;
                    case 10:
                        // test Enemy Initialize method
                        ObjectPool.Initialize();
                        enemyGameObject = ObjectPool.GetEnemy();
                        Enemy enemy = enemyGameObject.GetComponent<Enemy>();
                        enemy.Initialize();
                        if (WithinOneHundredth(enemy.ForceVector.x,
                            GameConstants.EnemyImpulseForce) &&
                            WithinOneHundredth(enemy.ForceVector.y, 0))
                        {
                            Console.WriteLine("Passed");
                        }
                        else
                        {
                            Console.WriteLine("FAILED");
                        }
                        break;
                        break;
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

        #region Delegates for Unity engine

        /// <summary>
        /// Delegate to load a game object
        /// </summary>
        /// <param name="path">game object name</param>
        static GameObject LoadGameObject(string path)
        {
            if (path == "Bullet")
            {
                Bullet bullet = new Bullet(new GameObject(0,
                    new Transform(Vector3.zero)));
                return bullet.gameObject;
            }
            else if (path == "Enemy")
            {
                Enemy enemy = new Enemy(new GameObject(1,
                    new Transform(Vector3.zero)));
                return enemy.gameObject;
            }
            else
            {
                // critical failure
                return null;
            }
        }

        /// <summary>
        /// Delegate to get the Bullet component for a game object
        /// </summary>
        /// <param name="gameObject">game object</param>
        /// <returns>Bullet component</returns>
        static Bullet GetBulletComponent(GameObject gameObject)
        {
            if (gameObjectIdBullets.ContainsKey(gameObject.id))
            {
                return gameObjectIdBullets[gameObject.id];
            }
            else
            {
                // critical failure
                return null;
            }
        }

        /// <summary>
        /// Delegate to get the Enemy component for a game object
        /// </summary>
        /// <param name="gameObject">game object</param>
        /// <returns>Enemy component</returns>
        static Enemy GetEnemyComponent(GameObject gameObject)
        {
            if (gameObjectIdEnemies.ContainsKey(gameObject.id))
            {
                return gameObjectIdEnemies[gameObject.id];
            }
            else
            {
                // critical failure
                return null;
            }
        }

        /// <summary>
        /// Handles the bullet created event by adding a mapping between
        /// the game object and its Bullet component
        /// </summary>
        /// <param name="bulletGameObject">bullet game object</param>
        static void HandleBulletCreatedEvent(GameObject bulletGameObject)
        {
            if (!gameObjectIdBullets.ContainsKey(bulletGameObject.id))
            {
                // assign unique id to game object
                bulletGameObject.id = lastGameObjectId + 1;
                lastGameObjectId = bulletGameObject.id;

                // new bullet game objects don't have a Bullet component
                gameObjectIdBullets.Add(bulletGameObject.id,
                    new Bullet(bulletGameObject));              
            }
        }

        /// <summary>
        /// Handles the enemy created event by adding a mapping between
        /// the game object and its Enemy component
        /// </summary>
        /// <param name="enemyGameObject">enemy game object</param>
        static void HandleEnemyCreatedEvent(GameObject enemyGameObject)
        {
            if (!gameObjectIdEnemies.ContainsKey(enemyGameObject.id))
            {
                // assign unique id to game object
                enemyGameObject.id = lastGameObjectId + 1;
                lastGameObjectId = enemyGameObject.id;

                // new enemy game objects don't have an Enemy component
                gameObjectIdEnemies.Add(enemyGameObject.id,
                    new Enemy(enemyGameObject));
            }
        }

        #endregion
    }
}
