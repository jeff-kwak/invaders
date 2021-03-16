using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenuController : MonoBehaviour
{
  public CommandBusDefinition CommandBus;
  public EventBusDefinition EventBus;

  private void Awake()
  {
    EventBus.OnPlayClicked += EventBus_OnPlayClicked;
    EventBus.OnQuitClicked += EventBus_OnQuitClicked;
    EventBus.OnSceneTransitionLeaveCompleted += EventBus_OnSceneTransitionLeaveCompleted;
  }

  private void OnDestroy()
  {
    EventBus.OnPlayClicked -= EventBus_OnPlayClicked;
    EventBus.OnQuitClicked -= EventBus_OnQuitClicked;
  }

  private void Start()
  {
    CommandBus.RequestSceneTransitionEnter();
  }

  private void EventBus_OnQuitClicked()
  {
    Debug.Log("Goodbye!");
    Application.Quit();
#if UNITY_EDITOR
    UnityEditor.EditorApplication.isPlaying = false;
#endif
  }

  private void EventBus_OnPlayClicked()
  {
    Debug.Log("Play again was clicked!");
    CommandBus.RequestSceneTransitionLeave();
  }

  private void EventBus_OnSceneTransitionLeaveCompleted()
  {
    SceneManager.LoadScene((int)SceneIndex.Primary);
  }
}
