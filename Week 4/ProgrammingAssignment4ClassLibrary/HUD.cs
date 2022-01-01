using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// The HUD
/// </summary>
public class HUD : MonoBehaviour
{
    //[SerializeField]
    Text healthText;

    #region Constructor

    /// <summary>
    /// Constructor
    /// 
    /// Note: The class in the Unity solution doesn't use a constructor;
    /// this constructor is to support automated grading
    /// </summary>
    /// <param name="gameObject">the game object the script is attached to</param>
    public HUD(GameObject gameObject) :
        base(gameObject)
    {
        EventManager.AddHealthChangedListener(HandleHealthChangedEvent);
    }

    #endregion

    /// <summary>
    /// Awake is called before Start
    /// </summary>
 //   void Awake()
	//{
 //       EventManager.AddHealthChangedListener(HandleHealthChangedEvent);
	//}

    /// <summary>
    /// changes health text display
    /// </summary>
    /// <param name="health">new health</param>
    void HandleHealthChangedEvent(int health)
    {
        healthText.text = "Health: " + health.ToString();
    }
}
