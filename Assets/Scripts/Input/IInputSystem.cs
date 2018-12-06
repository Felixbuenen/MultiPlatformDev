using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInputSystem
{
  Queue<int> GetSerializedTrickInput(); // serialized trick input

  void Update();

  Vector2 AnalogMoveInput();
  Vector2 AnalogTrickInput(); // raw, unserialized trick input
}
