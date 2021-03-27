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
    EventBus.OnBonusLifeAwarded += EventBus_OnBonusLifeAwarded;
    EventBus.OnWaveCleared += EventBus_OnWaveCleared;
    EventBus.OnScoreChanged += EventBus_OnScoreChanged;
    WaveAnnouncementText = WaveAnnouncement.GetComponent<TMP_Text>();
  }

  private void OnDestroy()
  {
    EventBus.OnPlayerDead -= EventBus_OnPlayerDead;
    EventBus.OnBonusLifeAwarded -= EventBus_OnBonusLifeAwarded;
    EventBus.OnWaveCleared -= EventBus_OnWaveCleared;
    EventBus.OnScoreChanged -= EventBus_OnScoreChanged;
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

  private void EventBus_OnBonusLifeAwarded(int livesRemaining)
  {
    UpdateLivesRemaining(livesRemaining);
  }

  private void EventBus_OnScoreChanged(int points)
  {
    ScoreText.text = points.ToString("0000000");
  }


  private void UpdateLivesRemaining(int lives)
  {
    LivesRemainingText.text = $"Lives x {Mathf.Clamp(lives - 1, 0, int.MaxValue)}";
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
