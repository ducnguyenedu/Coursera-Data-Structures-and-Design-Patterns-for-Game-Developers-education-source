using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Manages connections between event listeners and event invokers
/// </summary>
public static class EventManager
{
    #region Fields

    static List<Ship> healthChangedInvokers = new List<Ship>();
    static List<UnityAction<int>> healthChangedListeners =
        new List<UnityAction<int>>();

    static List<Ship> gameOverInvokers = new List<Ship>();
    static List<UnityAction> gameOverListeners =
        new List<UnityAction>();

    #region Needed for autograder only

    static List<IBulletCreatedInvoker> bulletCreatedInvokers = 
        new List<IBulletCreatedInvoker>();
    static List<UnityAction<GameObject>> bulletCreatedListeners =
        new List<UnityAction<GameObject>>();

    static List<IEnemyCreatedInvoker> enemyCreatedInvokers =
        new List<IEnemyCreatedInvoker>();
    static List<UnityAction<GameObject>> enemyCreatedListeners =
        new List<UnityAction<GameObject>>();

    #endregion

    #endregion

    #region Public methods

    /// <summary>
    /// Adds a HealthChanged invoker
    /// </summary>
    /// <param name="invoker">invoker</param>
    public static void AddHealthChangedInvoker(Ship invoker)
    {
        // add listeners to new invoker and add new invoker to list
        foreach (UnityAction<int> listener in healthChangedListeners)
        {
            invoker.AddHealthChangedListener(listener);
        }
        healthChangedInvokers.Add(invoker);
    }

    /// <summary>
    /// Adds a HealthChanged listener
    /// </summary>
    /// <param name="listener">listener</param>
    public static void AddHealthChangedListener(UnityAction<int> listener)
    {
        // add as listener to all invokers and add new listener to list
        foreach (Ship invoker in healthChangedInvokers)
        {
            invoker.AddHealthChangedListener(listener);
        }
        healthChangedListeners.Add(listener);
    }

    /// <summary>
    /// Adds a GameOver invoker
    /// </summary>
    /// <param name="invoker">invoker</param>
    public static void AddGameOverInvoker(Ship invoker)
    {
        // add listeners to new invoker and add new invoker to list
        foreach (UnityAction listener in gameOverListeners)
        {
            invoker.AddGameOverListener(listener);
        }
        gameOverInvokers.Add(invoker);
    }

    /// <summary>
    /// Adds a GameOver listener
    /// </summary>
    /// <param name="listener">listener</param>
    public static void AddGameOverListener(UnityAction listener)
    {
        // add as listener to all invokers and add new listener to list
        foreach (Ship invoker in gameOverInvokers)
        {
            invoker.AddGameOverListener(listener);
        }
        gameOverListeners.Add(listener);
    }

    #region Needed for autograder only

    /// <summary>
    /// Adds a BulletCreated invoker
    /// </summary>
    /// <param name="invoker">invoker</param>
    public static void AddBulletCreatedInvoker(IBulletCreatedInvoker invoker)
    {
        // add listeners to new invoker and add new invoker to list
        foreach (UnityAction<GameObject> listener in bulletCreatedListeners)
        {
            invoker.AddBulletCreatedListener(listener);
        }
        bulletCreatedInvokers.Add(invoker);
    }

    /// <summary>
    /// Adds a BulletCreated listener
    /// </summary>
    /// <param name="listener">listener</param>
    public static void AddBulletCreatedListener(UnityAction<GameObject> listener)
    {
        // add as listener to all invokers and add new listener to list
        foreach (IBulletCreatedInvoker invoker in bulletCreatedInvokers)
        {
            invoker.AddBulletCreatedListener(listener);
        }
        bulletCreatedListeners.Add(listener);
    }

    /// <summary>
    /// Adds an EnemyCreated invoker
    /// </summary>
    /// <param name="invoker">invoker</param>
    public static void AddEnemyCreatedInvoker(IEnemyCreatedInvoker invoker)
    {
        // add listeners to new invoker and add new invoker to list
        foreach (UnityAction<GameObject> listener in enemyCreatedListeners)
        {
            invoker.AddEnemyCreatedListener(listener);
        }
        enemyCreatedInvokers.Add(invoker);
    }

    /// <summary>
    /// Adds an EnemyCreated listener
    /// </summary>
    /// <param name="listener">listener</param>
    public static void AddEnemyCreatedListener(UnityAction<GameObject> listener)
    {
        // add as listener to all invokers and add new listener to list
        foreach (IEnemyCreatedInvoker invoker in enemyCreatedInvokers)
        {
            invoker.AddEnemyCreatedListener(listener);
        }
        enemyCreatedListeners.Add(listener);
    }

    #endregion

    #endregion
}
