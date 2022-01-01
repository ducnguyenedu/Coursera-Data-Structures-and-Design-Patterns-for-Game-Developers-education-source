using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Initializes the game
/// </summary>
public class GameInitializer : MonoBehaviour 
{
    #region Constructor

    /// <summary>
    /// Constructor
    /// 
    /// Note: The class in the Unity solution doesn't use a constructor;
    /// this constructor is to support automated grading
    /// </summary>
    /// <param name="gameObject">the game object the script is attached to</param>
    public GameInitializer(GameObject gameObject) :
        base(gameObject)
    {
        ScreenUtils.Initialize();
    }

    #endregion

    /// <summary>
    /// Awake is called before Start
    /// </summary>
	//void Awake()
 //   {
 //       // initialize utils
 //       ScreenUtils.Initialize();
 //       ObjectPool.Initialize();
 //   }
}
