using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

// class needs to be in gloabl namespace to be visible to Player class
class ThinkingTimer : Timer
{
    #region Constructor

    /// <summary>
    /// Constructor
    /// </summary>
    public ThinkingTimer() : base()
    {
    }

    #endregion

    #region Public methods

    /// <summary>
    /// Immediately invokes the TimerFinishedEvent
    /// </summary>
    public override void Run()
    {
        timerFinished.Invoke();
    }

    #endregion
}
