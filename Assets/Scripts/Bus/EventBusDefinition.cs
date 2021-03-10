using UnityEngine;

public delegate void EventBusHandler();

[CreateAssetMenu(menuName = "Game/EventBus")]
public class EventBusDefinition : ScriptableObject
{
  public event EventBusHandler OnPlayClicked;
  public event EventBusHandler OnQuitClicked;
  public event EventBusHandler OnSceneTransitionLeaveCompleted;
  public event EventBusHandler OnSceneTransitionEnterCompleted;

  public void RaiseOnPlayClicked()
  {
    OnPlayClicked?.Invoke();
  }

  public void RaiseOnQuitClicked()
  {
    OnQuitClicked?.Invoke();
  }

  public void RaiseOnSceneTransitionLeaveCompleted()
  {
    OnSceneTransitionLeaveCompleted?.Invoke();
  }

  public void RaiseOnSceneTransitionEnterCompleted()
  {
    OnSceneTransitionEnterCompleted?.Invoke();
  }
}
