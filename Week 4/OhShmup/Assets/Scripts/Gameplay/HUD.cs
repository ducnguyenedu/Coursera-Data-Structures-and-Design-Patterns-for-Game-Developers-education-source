using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// The HUD
/// </summary>
public class HUD : MonoBehaviour
{
    [SerializeField]
    Text healthText;

	/// <summary>
	/// Awake is called before Start
	/// </summary>
	void Awake()
	{
        EventManager.AddHealthChangedListener(HandleHealthChangedEvent);
	}

    /// <summary>
    /// changes health text display
    /// </summary>
    /// <param name="health">new health</param>
    void HandleHealthChangedEvent(int health)
    {
        healthText.text = "Health: " + health.ToString();
    }
}
