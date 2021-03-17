using TMPro;
using UnityEngine;

public class HUDController : MonoBehaviour
{
  public EventBusDefinition EventBus;
  public GamePlayDefinition GamePlay;
  public TMP_Text ScoreText;
  public TMP_Text LivesRemainingText;
  public GameObject WaveAnnouncement;

  private TMP_Text WaveAnnouncementText;
  private int CurrentWave = 1;

  private void Awake()
  {
    EventBus.OnPlayerDead += EventBus_OnPlayerDead;
    EventBus.OnWaveCleared += EventBus_OnWaveCleared;
    WaveAnnouncementText = WaveAnnouncement.GetComponent<TMP_Text>();
  }

  private void OnDestroy()
  {
    EventBus.OnPlayerDead -= EventBus_OnPlayerDead;
  }

  private void Start()
  {
    CurrentWave = 1;
    UpdateLivesRemaining(GamePlay.InitialNumberOfLives);
    ShowWaveAnnouncement();
  }

  private void EventBus_OnPlayerDead(int livesRemaining)
  {
    UpdateLivesRemaining(livesRemaining);
  }

  private void UpdateLivesRemaining(int lives)
  {
    LivesRemainingText.text = $"Lives x {lives - 1}";
  }

  private void EventBus_OnWaveCleared(int waveCleared)
  {
    CurrentWave = waveCleared + 1;
    ShowWaveAnnouncement();
  }


  private void ShowWaveAnnouncement()
  {
    WaveAnnouncement.SetActive(true);
    WaveAnnouncementText.text = $"Wave {CurrentWave}";
    Invoke(nameof(HideWaveAnnouncement), GamePlay.WaveAnnouncementTime);
  }

  private void HideWaveAnnouncement()
  {
    WaveAnnouncement.SetActive(false);
  }
}
