using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Events;

// Need this interface in the global namespace for Unity
// script to see it

/// <summary>
/// Interface for an invoker of the BulletCreated event
/// </summary>
public interface IBulletCreatedInvoker
{
    /// <summary>
    /// Adds the given listener for the BulletCreated event
    /// </summary>
    /// <param name="listener">listener</param>
    void AddBulletCreatedListener(UnityAction<GameObject> listener);
}
