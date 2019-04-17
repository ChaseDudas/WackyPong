using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// An event indicating that a ball has been lost
/// </summary>
public class BallLostEvent : UnityEvent<ScreenSide, int>
{
}
