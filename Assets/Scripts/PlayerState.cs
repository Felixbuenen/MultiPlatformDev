using UnityEngine;

public abstract class PlayerState
{
  private static int currentID = 0;
  protected static int GetNewID()
  {
    return currentID++;
  }

  public abstract void Start();
  public abstract void Stop();

  public abstract void HandleInput();
  public abstract void Update();
}
