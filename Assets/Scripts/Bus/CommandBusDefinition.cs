using System;
using UnityEngine;

public delegate void CommandBusHandler();
public delegate void CommandBusHandler<T>(T arg);

[CreateAssetMenu(menuName = "Game/CommandBus")]
public class CommandBusDefinition : ScriptableObject
{
  public event CommandBusHandler OnSceneTransitionLeave;
  public event CommandBusHandler OnSceneTransitionEnter;
  public event CommandBusHandler OnEnemyReset;
  public event CommandBusHandler OnPlayerMoveLeft;
  public event CommandBusHandler OnPlayerMoveRight;
  public event CommandBusHandler OnPlayerStop;
  public event CommandBusHandler OnPlayerFire;
  public event CommandBusHandler<Vector3> OnBombDrop;
  public event CommandBusHandler OnPlayerReset;

  public void RequestSceneTransitionLeave()
  {
    OnSceneTransitionLeave?.Invoke();
  }

  public void RequestSceneTransitionEnter()
  {
    OnSceneTransitionEnter?.Invoke();
  }

  public void RequestEnemyReset()
  {
    OnEnemyReset?.Invoke();
  }

  public void RequestPlayerMoveLeft()
  {
    OnPlayerMoveLeft?.Invoke();
  }

  public void RequestPlayerMoveRight()
  {
    OnPlayerMoveRight?.Invoke();
  }

  public void RequestPlayerStop()
  {
    OnPlayerStop?.Invoke();
  }

  public void RequestPlayerFire()
  {
    OnPlayerFire?.Invoke();
  }

  public void RequestBombDrop(Vector3 pos)
  {
    OnBombDrop?.Invoke(pos);
  }

  internal void RequestPlayerReset()
  {
    OnPlayerReset?.Invoke();
  }
}
