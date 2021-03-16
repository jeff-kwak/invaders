using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUDController : MonoBehaviour
{
  public EventBusDefinition EventBus;
  public GamePlayDefinition GamePlay;
  public TMP_Text ScoreText;
  public TMP_Text LivesRemainingText;

  private void Awake()
  {
    EventBus.OnPlayerDead += EventBus_OnPlayerDead;
  }

  private void OnDestroy()
  {
    EventBus.OnPlayerDead -= EventBus_OnPlayerDead;
  }

  private void Start()
  {
    UpdateLivesRemaining(GamePlay.InitialNumberOfLives);
  }

  private void EventBus_OnPlayerDead(int livesRemaining)
  {
    UpdateLivesRemaining(livesRemaining);
  }

  private void UpdateLivesRemaining(int lives)
  {
    LivesRemainingText.text = $"Lives x {lives - 1}";
  }
}
