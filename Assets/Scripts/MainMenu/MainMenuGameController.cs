using System;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(SoundController))]
public class MainMenuGameController : MonoBehaviour
{
  public EventBusDefinition EventBus;
  public CommandBusDefinition CommandBus;
  public SceneIndex SceneOnPlay = SceneIndex.Primary;

  private SoundController SoundController;

  private void Awake()
  {
    SoundController = GetComponent<SoundController>();
    EventBus.OnPlayClicked += EventBus_OnPlayClicked;
    EventBus.OnQuitClicked += EventBus_OnQuitClicked;
    EventBus.OnSceneTransitionLeaveCompleted += EventBus_OnSceneTransitionLeaveCompleted;
  }

  private void OnDestroy()
  {
    EventBus.OnPlayClicked -= EventBus_OnPlayClicked;
    EventBus.OnQuitClicked -= EventBus_OnQuitClicked;
    EventBus.OnSceneTransitionLeaveCompleted -= EventBus_OnSceneTransitionLeaveCompleted;
  }

  private void Start()
  {
    SoundController.PlayMenuBackgroundMusic();
  }

  private void EventBus_OnQuitClicked()
  {
    Debug.Log($"{nameof(MainMenuGameController)}: Detected OnQuit Clicked");
    SoundController.PlayMenuClick();
#if UNITY_EDITOR
    UnityEditor.EditorApplication.isPlaying = false;
#elif (UNITY_STANDALONE)
    Application.Quit();
#elif UNITY_WEBGL
    Application.OpenURL("about:blank");
#endif
  }


  private void EventBus_OnPlayClicked()
  {
    Debug.Log($"{nameof(MainMenuGameController)}: Detected OnPlay Clicked");
    SoundController.PlayMenuClick();
    CommandBus.RequestSceneTransitionLeave();
  }

  private void EventBus_OnSceneTransitionLeaveCompleted()
  {
    SceneManager.LoadScene((int)SceneOnPlay);
  }
}
