using UnityEngine;

public abstract class PlayerState
{
  private static int currentID = 0;
  protected static int GetNewID()
  {
    return currentID++;
  }

  protected PlayerStats playerStats;

  public PlayerState(PlayerStats playerStats)
  {
    this.playerStats = playerStats;
  }

  public abstract void Start();
  public abstract void Stop();

  public abstract void HandleInput();
  public abstract void Update(float dt);

  public virtual void OnCollisionEnter(Collision c) { }
  public virtual void OnCollisionExit() { }
}
