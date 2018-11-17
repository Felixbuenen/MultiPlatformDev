using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
  void Start()
  {
    Velocity = Vector3.zero;
    Speed = 10.0f;
  }

  public Vector3 Velocity
  {
    get;
    set;
  }
  public Vector3 Position
  {
    get { return transform.position; }
    set { transform.position = value; }
  }

  public float Speed
  {
    get;
    set;
  }
}
