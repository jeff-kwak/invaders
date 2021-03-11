using UnityEngine;

public delegate void CommandBusHandler();

[CreateAssetMenu(menuName = "Game/CommandBus")]
public class CommandBusDefinition : ScriptableObject
{
  public event CommandBusHandler OnSceneTransitionLeave;
  public event CommandBusHandler OnSceneTransitionEnter;
  public event CommandBusHandler OnEnemyReset;

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
}
