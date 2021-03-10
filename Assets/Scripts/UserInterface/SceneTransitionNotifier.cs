using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransitionNotifier : MonoBehaviour
{
  public EventBusDefinition EventBus;

  public void SceneHasLeft()
  {
    Debug.Log("The scene animation is reporting that it has completed");
    EventBus.RaiseOnSceneTransitionLeaveCompleted();
  }

  public void SceneHasEntered()
  {
    Debug.Log("The scene animation is reporting that it has completed");
    EventBus.RaiseOnSceneTransitionEnterCompleted();
  }
}
