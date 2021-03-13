using System;
using UnityEngine;

public delegate void EventBusHandler();
public delegate void EventBusHandler<T>(T arg);
public delegate void EventBusHandler<S, T>(S arg0, T arg1);

[CreateAssetMenu(menuName = "Game/EventBus")]
public class EventBusDefinition : ScriptableObject
{
  public event EventBusHandler OnPlayClicked;
  public event EventBusHandler OnQuitClicked;
  public event EventBusHandler OnSceneTransitionLeaveCompleted;
  public event EventBusHandler OnSceneTransitionEnterCompleted;
  public event EventBusHandler OnCollisionWithLeftWall;
  public event EventBusHandler OnCollisionWithRightWall;
  public event EventBusHandler<GameObject, GameObject> OnMissileHitEnemy;

  public void RaiseOnPlayClicked()
  {
    OnPlayClicked?.Invoke();
  }

  public void RaiseMissileHitEnemy(GameObject missile, GameObject enemy)
  {
    OnMissileHitEnemy?.Invoke(missile, enemy);
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

  public void RaiseOnCollisionWithLeftWall()
  {
    OnCollisionWithLeftWall?.Invoke();
  }

  public void RaiseOnCollisionWithRightWall()
  {
    OnCollisionWithRightWall?.Invoke();
  }
}
