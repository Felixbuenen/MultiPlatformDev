using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInputSystem
{
  IUIInput UIInput { get; }
  TrickQueue GetSerializedTrickInput(); // serialized trick input

  void Update();

  Vector2 AnalogMoveInput();
  Vector2 AnalogTrickInput(); // raw, unserialized trick input
}

public interface IUIInput
{
  bool GetBackButton();
  bool GetPressButton();
  bool GetUpButton();
  bool GetDownButton();
}