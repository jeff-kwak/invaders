using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
  public CommandBusDefinition CommandBus;
  public Animator TransitionAnimator;

  private void Awake()
  {
    CommandBus.OnSceneTransitionLeave += CommandBus_OnSceneTransitionLeave;
    CommandBus.OnSceneTransitionEnter += CommandBus_OnSceneTransitionEnter;
  }

  private void OnDestroy()
  {
    CommandBus.OnSceneTransitionLeave -= CommandBus_OnSceneTransitionLeave;
    CommandBus.OnSceneTransitionEnter -= CommandBus_OnSceneTransitionEnter;
  }

  private void CommandBus_OnSceneTransitionLeave()
  {
    TransitionAnimator.SetTrigger("LeaveScene");
  }
  private void CommandBus_OnSceneTransitionEnter()
  {
    TransitionAnimator.SetTrigger("EnterScene");
  }
}
