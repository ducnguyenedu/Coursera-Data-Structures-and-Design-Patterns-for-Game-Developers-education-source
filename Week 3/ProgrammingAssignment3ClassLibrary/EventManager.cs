using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Th event manager
/// </summary>
public static class EventManager
{
    #region Fields

    // I used something called interfaces here to make the autograder
    // work. You can explore those on your own if you'd like, but
    // the code I provided should work fine using them

    static List<ITakeTurnInvoker> takeTurnInvokers =
        new List<ITakeTurnInvoker>();
    static List<UnityAction<PlayerName, Configuration>> takeTurnListeners =
        new List<UnityAction<PlayerName, Configuration>>();

    static List<ITurnOverInvoker> turnOverInvokers = new List<ITurnOverInvoker>();
    static List<UnityAction<PlayerName, Configuration>> turnOverListeners =
        new List<UnityAction<PlayerName, Configuration>>();

    static List<IGameOverInvoker> gameOverInvokers = 
        new List<IGameOverInvoker>();
    static List<UnityAction<PlayerName, Difficulty, Difficulty>> gameOverListeners =
        new List<UnityAction<PlayerName, Difficulty, Difficulty>>();

    static List<IGameStartingInvoker> gameStartingInvokers = 
        new List<IGameStartingInvoker>();
    static List<UnityAction> gameStartingListeners =
        new List<UnityAction>();
    
    #endregion

    #region Methods

    /// <summary>
    /// Adds the parameter as a TakeTurn event invoker
    /// </summary>
    /// <param name="invoker">invoker</param>
    public static void AddTakeTurnInvoker(ITakeTurnInvoker invoker)
    {
        takeTurnInvokers.Add(invoker);
        foreach (UnityAction<PlayerName, Configuration> listener in takeTurnListeners)
        {
            invoker.AddTakeTurnListener(listener);
        }
    }

    /// <summary>
    /// Adds the parameter as a TakeTurn event listener
    /// </summary>
    /// <param name="listener">listener</param>
    public static void AddTakeTurnListener(UnityAction<PlayerName, Configuration> listener)
    {
        takeTurnListeners.Add(listener);
        foreach(ITakeTurnInvoker invoker in takeTurnInvokers)
        {
            invoker.AddTakeTurnListener(listener);
        }
    }

    /// <summary>
    /// Adds the parameter as a TurnOver event invoker
    /// </summary>
    /// <param name="invoker">invoker</param>
    public static void AddTurnOverInvoker(ITurnOverInvoker invoker)
    {
        turnOverInvokers.Add(invoker);
        foreach (UnityAction<PlayerName, Configuration> listener in turnOverListeners)
        {
            invoker.AddTurnOverListener(listener);
        }
    }

    /// <summary>
    /// Adds the parameter as a TurnOver event listener
    /// </summary>
    /// <param name="listener">listener</param>
    public static void AddTurnOverListener(
        UnityAction<PlayerName, Configuration> listener)
    {
        turnOverListeners.Add(listener);
        foreach (ITurnOverInvoker invoker in turnOverInvokers)
        {
            invoker.AddTurnOverListener(listener);
        }
    }

    /// <summary>
    /// Adds the parameter as a GameOver event invoker
    /// </summary>
    /// <param name="invoker">invoker</param>
    public static void AddGameOverInvoker(IGameOverInvoker invoker)
    {
        gameOverInvokers.Add(invoker);
        foreach (UnityAction<PlayerName, Difficulty, Difficulty> listener in gameOverListeners)
        {
            invoker.AddGameOverListener(listener);
        }
    }

    /// <summary>
    /// Adds the parameter as a GameOver event listener
    /// </summary>
    /// <param name="listener">listener</param>
    public static void AddGameOverListener(UnityAction<PlayerName, Difficulty, Difficulty> listener)
    {
        gameOverListeners.Add(listener);
        foreach(IGameOverInvoker invoker in gameOverInvokers)
        {
            invoker.AddGameOverListener(listener);
        }
    }

    /// <summary>
    /// Adds the parameter as a GameStarting event invoker
    /// </summary>
    /// <param name="invoker">invoker</param>
    public static void AddGameStartingInvoker(IGameStartingInvoker invoker)
    {
        gameStartingInvokers.Add(invoker);
        foreach (UnityAction listener in gameStartingListeners)
        {
            invoker.AddGameStartingListener(listener);
        }
    }

    /// <summary>
    /// Adds the parameter as a GameStarting event listener
    /// </summary>
    /// <param name="listener">listener</param>
    public static void AddGameStartingListener(UnityAction listener)
    {
        gameStartingListeners.Add(listener);
        foreach(IGameStartingInvoker invoker in gameStartingInvokers)
        {
            invoker.AddGameStartingListener(listener);
        }
    }

    #endregion
}
