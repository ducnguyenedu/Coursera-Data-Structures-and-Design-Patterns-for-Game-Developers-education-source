using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine.Events;

// Need this interface in the global namespace for Unity
// script to see it

/// <summary>
/// Interface for an invoker of the GameStarting event
/// </summary>
public interface IGameStartingInvoker
{
    /// <summary>
    /// Adds the given listener for the GameStarting event
    /// </summary>
    /// <param name="listener">listener</param>
    void AddGameStartingListener(UnityAction listener);
}