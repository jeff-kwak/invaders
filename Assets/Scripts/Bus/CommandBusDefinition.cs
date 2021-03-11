using UnityEngine;

public delegate void CommandBusHandler();

[CreateAssetMenu(menuName = "Game/CommandBus")]
public class CommandBusDefinition : ScriptableObject
{
  public event CommandBusHandler OnSceneTransitionLeave;
  public event CommandBusHandler OnSceneTransitionEnter;
  public event CommandBusHandler OnEnemyReset;
  public event CommandBusHandler OnPlayerMoveLeft;
  public event CommandBusHandler OnPlayerMoveRight;
  public event CommandBusHandler OnPlayerStop;

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
}
