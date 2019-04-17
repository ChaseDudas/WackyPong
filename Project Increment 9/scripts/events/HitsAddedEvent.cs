using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// An event indicating that hits have been added
/// </summary>
public class HitsAddedEvent : UnityEvent<ScreenSide, int>
{
}
