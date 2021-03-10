using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrimaryGameController : MonoBehaviour
{
  public EventBusDefinition EventBus;
  public CommandBusDefinition CommandBus;

  private void Awake()
  {
    EventBus.OnSceneTransitionEnterCompleted += EventBus_OnSceneTransitionEnterCompleted;
  }

  private void OnDestroy()
  {
    EventBus.OnSceneTransitionEnterCompleted -= EventBus_OnSceneTransitionEnterCompleted;
  }

  private void Start()
  {
    CommandBus.RequestSceneTransitionEnter();
  }

  private void EventBus_OnSceneTransitionEnterCompleted()
  {
    Debug.Log("Scene tranisition enter complete");
  }
}
