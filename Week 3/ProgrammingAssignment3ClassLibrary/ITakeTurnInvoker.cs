using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine.Events;

// Need this interface in the global namespace for Unity
// script to see it

/// <summary>
/// Interface for an invoker of the TakeTurn event
/// </summary>
public interface ITakeTurnInvoker
{
    /// <summary>
    /// Adds the given listener for the TakeTurn event
    /// </summary>
    /// <param name="listener">listener</param>
    void AddTakeTurnListener(UnityAction<PlayerName, Configuration> listener);
}
