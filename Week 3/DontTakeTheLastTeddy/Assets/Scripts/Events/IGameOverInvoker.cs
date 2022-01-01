using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine.Events;

// Need this interface in the global namespace for Unity
// script to see it

/// <summary>
/// Interface for an invoker of the GameOver event
/// </summary>
public interface IGameOverInvoker
{
    /// <summary>
    /// Adds the given listener for the GameOver event
    /// </summary>
    /// <param name="listener">listener</param>
    void AddGameOverListener(UnityAction<PlayerName, Difficulty, Difficulty> listener);
}
