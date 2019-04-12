using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// An event indicating that the freezer effect has been activated
/// </summary>
public class FreezerEffectActivatedEvent : UnityEvent<ScreenSide, float>
{
}
