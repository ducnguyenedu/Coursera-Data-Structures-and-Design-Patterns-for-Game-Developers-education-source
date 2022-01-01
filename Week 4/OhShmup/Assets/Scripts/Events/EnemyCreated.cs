using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// An event that indicates that an enemy has been created
/// </summary>
public class EnemyCreated : UnityEvent<GameObject>
{
}
