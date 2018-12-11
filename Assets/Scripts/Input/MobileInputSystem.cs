using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MobileInputSystem : IInputSystem
{
  //Queue<int> parsedInput;
  TrickQueue trickQueue;
  float steerSensitivity = 1.5f;


  public TrickQueue GetSerializedTrickInput()
  {
    //return parsedInput;
    return trickQueue;
  }

  // Use this for initialization
  public MobileInputSystem()
  {
    //parsedInput = new Queue<int>();
    //for (int i = 0; i < 90; i++) parsedInput.Enqueue(0); // fill queue with 0

    trickQueue = new TrickQueue(90);
  }

  public void Update()
  {
    // DEBUG
    /*if (Input.touchCount > 0)
    {
      parsedInput.Enqueue(3);
      parsedInput.Dequeue();
    }
    else
    {
      parsedInput.Enqueue(0);
      parsedInput.Dequeue();
    }*/

    if (Input.touchCount > 0)
    {
      trickQueue.EnqueueDequeue(3);
    }
    else
    {
      trickQueue.EnqueueDequeue(0);
    }
  }

  Vector2 movement = new Vector2();
  public Vector2 AnalogMoveInput()
  {
    //Debug.Log("MOBILE NOT IMPLEMENTED");
    movement = Input.acceleration;
    movement.y = 0;

    return movement * steerSensitivity;
  }

  public Vector2 AnalogTrickInput()
  {
    Debug.Log("MOBILE NOT IMPLEMENTED");
    return Vector2.zero;
  }

}
