using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine.Events;

// Need this interface in the global namespace for Unity
// script to see it

/// <summary>
/// Interface for an invoker of the TurnOver event
/// </summary>
public interface ITurnOverInvoker
{
    /// <summary>
    /// Adds the given listener for the TurnOver event
    /// </summary>
    /// <param name="listener">listener</param>
    void AddTurnOverListener(UnityAction<PlayerName, Configuration> listener);
}
