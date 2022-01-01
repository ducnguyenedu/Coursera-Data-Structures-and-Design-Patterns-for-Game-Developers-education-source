using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Events;

// Need this interface in the global namespace for Unity
// script to see it

/// <summary>
/// Interface for an invoker of the EnemyCreated event
/// </summary>
public interface IEnemyCreatedInvoker
{
    /// <summary>
    /// Adds the given listener for the EnemyCreated event
    /// </summary>
    /// <param name="listener">listener</param>
    void AddEnemyCreatedListener(UnityAction<GameObject> listener);
}
