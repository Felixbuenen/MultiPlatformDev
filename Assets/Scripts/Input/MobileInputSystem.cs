using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileInputSystem : IInputSystem
{
  Queue<int> parsedInput;
  public Queue<int> GetSerializedTrickInput()
  {
    return parsedInput;
  }

  // Use this for initialization
  public MobileInputSystem()
  {
    parsedInput = new Queue<int>();
    for (int i = 0; i < 90; i++) parsedInput.Enqueue(0); // fill queue with 0
  }

  public void Update()
  {
    // DEBUG
    if (Input.touchCount > 0)
    {
      parsedInput.Enqueue(3);
      parsedInput.Dequeue();
    }
    else
    {
      parsedInput.Enqueue(0);
      parsedInput.Dequeue();
    }
  }

  public Vector2 AnalogMoveInput()
  {
    Debug.Log("MOBILE NOT IMPLEMENTED");
    return Vector2.zero;
  }

  public Vector2 AnalogTrickInput()
  {
    Debug.Log("MOBILE NOT IMPLEMENTED");
    return Vector2.zero;
  }

}
