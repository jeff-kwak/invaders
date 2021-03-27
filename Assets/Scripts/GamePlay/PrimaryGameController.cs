using Gr8tGames;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(SoundController))]
[RequireComponent(typeof(ExplosionController))]
public class PrimaryGameController : MonoBehaviour
{
  public EventBusDefinition EventBus;
  public CommandBusDefinition CommandBus;
  public GamePlayDefinition GamePlay;
  public GameObject EnemyGrid;

  private enum State
  {
    Start,
    Setup,
    Ready,
    MoveLeft,
    MoveRight,
    MoveDownOnLeft,
    MoveDownOnRight,
    GameOver
  }

  private enum Trigger
  {
    Setup,
    SetupComplete,
    SceneTransitionEnterComplete,
    MoveEnemy,
    LeftWallCollision,
    RightWallCollision,
    EnemyHasMovedDown,
    EnemyHasTravelledDropDistance,
    GameOver,
    SceneTransitionLeaveComplete,
    WaveCleared
  }

  private SoundController SoundController;
  private ExplosionController Explosion;
  private StateMachine<State, Trigger> Machine;
  private Vector3 EnemyDirection = Vector3.left;
  private float CurrentEnemySpeedBase = 0f;
  private float CurrentEnemySpeed = 0f; // u/s
  private float accumulatedMoveDistance = 0f;
  private int LivesRemaining = 0;
  private const int StartingNumberOfEnemies = 60;
  private int EnemiesRemaining = 0;
  private int WaveNumber = 1;
  private int Score = 0;
  private int BonusLifeScore = 0;

  private void Awake()
  {
    SoundController = GetComponent<SoundController>();
    Explosion = GetComponent<ExplosionController>();

    LivesRemaining = GamePlay.InitialNumberOfLives;

    Machine = new StateMachine<State, Trigger>(State.Start);

    Machine.Configure(State.Start)
      .Permit(Trigger.SceneTransitionEnterComplete, State.Setup);

    Machine.Configure(State.Setup)
      .OnEnter(SetupGame)
      .Permit(Trigger.SetupComplete, State.MoveLeft);

    Machine.Configure(State.MoveLeft)
      .OnEnter(ChangeDirectionToLeft)
      .Permit(Trigger.WaveCleared, State.Setup)
      .Permit(Trigger.GameOver, State.GameOver)
      .Permit(Trigger.MoveEnemy, State.MoveLeft)
      .Permit(Trigger.LeftWallCollision, State.MoveDownOnLeft);

    Machine.Configure(State.MoveDownOnLeft)
      .OnEnter(ChangeDirectionToDown)
      .Permit(Trigger.WaveCleared, State.Setup)
      .Permit(Trigger.GameOver, State.GameOver)
      .Permit(Trigger.MoveEnemy, State.MoveDownOnLeft)
      .Permit(Trigger.EnemyHasMovedDown, State.MoveDownOnLeft)
      .Permit(Trigger.EnemyHasTravelledDropDistance, State.MoveRight);

    Machine.Configure(State.MoveRight)
      .OnEnter(ChangeDirectionToRight)
      .Permit(Trigger.WaveCleared, State.Setup)
      .Permit(Trigger.GameOver, State.GameOver)
      .Permit(Trigger.MoveEnemy, State.MoveRight)
      .Permit(Trigger.RightWallCollision, State.MoveDownOnRight);

    Machine.Configure(State.MoveDownOnRight)
      .OnEnter(ChangeDirectionToDown)
      .Permit(Trigger.WaveCleared, State.Setup)
      .Permit(Trigger.GameOver, State.GameOver)
      .Permit(Trigger.MoveEnemy, State.MoveDownOnRight)
      .Permit(Trigger.EnemyHasMovedDown, State.MoveDownOnRight)
      .Permit(Trigger.EnemyHasTravelledDropDistance, State.MoveLeft);

    Machine.Configure(State.GameOver)
       .OnEnter(OnEnterGameOver);

    EventBus.OnSceneTransitionEnterCompleted += EventBus_OnSceneTransitionEnterCompleted;
    EventBus.OnCollisionWithLeftWall += EventBus_OnCollisionWithLeftWall;
    EventBus.OnCollisionWithRightWall += EventBus_OnCollisionWithRightWall;
    EventBus.OnMissileHitEnemy += EventBus_OnMissileHitEnemy;
    EventBus.OnMissileHitShield += EventBus_OnMissileHitShield;
    EventBus.OnBombHitPlayer += EventBus_OnBombHitPlayer;
    EventBus.OnBombHitShield += EventBus_OnBombHitShield;
    EventBus.OnEnemyHitShield += EventBus_OnEnemyHitShield;
    EventBus.OnSceneTransitionLeaveCompleted += EventBus_OnSceneTransitionLeaveCompleted;
    EventBus.OnEnemyHasReachedTheBase += EventBus_OnEnemyHasReachedTheBase;
    EventBus.OnMissileFired += EventBus_OnMissileFired;

    CommandBus.OnBombDrop += CommandBus_OnBombDrop;
  }

  private void OnDestroy()
  {
    EventBus.OnSceneTransitionEnterCompleted -= EventBus_OnSceneTransitionEnterCompleted;
    EventBus.OnCollisionWithLeftWall -= EventBus_OnCollisionWithLeftWall;
    EventBus.OnCollisionWithRightWall -= EventBus_OnCollisionWithRightWall;
    EventBus.OnMissileHitEnemy -= EventBus_OnMissileHitEnemy;
    EventBus.OnBombHitPlayer -= EventBus_OnBombHitPlayer;
    EventBus.OnBombHitShield -= EventBus_OnBombHitShield;
    EventBus.OnEnemyHitShield -= EventBus_OnEnemyHitShield;
    EventBus.OnSceneTransitionLeaveCompleted -= EventBus_OnSceneTransitionLeaveCompleted;
    EventBus.OnEnemyHasReachedTheBase -= EventBus_OnEnemyHasReachedTheBase;
    EventBus.OnMissileFired -= EventBus_OnMissileFired;
    CommandBus.OnBombDrop -= CommandBus_OnBombDrop;
  }

  private void Start()
  {
    CommandBus.RequestSceneTransitionEnter();
  }

  private void Update()
  {
    AccumulateMoveDistance();
    Machine.Fire(Trigger.MoveEnemy, MoveEnemy);
  }

  private void ChangeDirectionToRight()
  {
    Debug.Log($"Changing direction to right: {Machine.State}");
    EnemyDirection = Vector3.right;
  }

  private void ChangeDirectionToDown()
  {
    Debug.Log($"Changing direction to down: {Machine.State}");
    accumulatedMoveDistance = 0;
    EnemyDirection = Vector3.down;
  }

  private void ChangeDirectionToLeft()
  {
    Debug.Log($"Changing direction to left: {Machine.State}");
    EnemyDirection = Vector3.left;
  }

  private void SetupGame()
  {
    Debug.Log("SetupGame");
    Invoke(nameof(DelayedSetup), GamePlay.WaveAnnouncementTime);
  }

  private void DelayedSetup()
  {
    Debug.Log("Delayed setup enter");
    SoundController.PlaySlowEnemyMarch();
    CurrentEnemySpeedBase = GamePlay.EnemySpeed * (1f + (WaveNumber - 1) * GamePlay.LevelMultiplierIncrement);
    CurrentEnemySpeed = CurrentEnemySpeedBase;
    BonusLifeScore += GamePlay.BonusLifePoints;
    EnemyGrid.SetActive(true);
    EnemyGrid.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
    EnemiesRemaining = StartingNumberOfEnemies;
    CommandBus.RequestEnemyReset();
    Machine.Fire(Trigger.SetupComplete);
  }

  private void AccumulateMoveDistance()
  {
    var delta = GamePlay.EnemySpeed * Time.deltaTime;
    if (accumulatedMoveDistance < GamePlay.EnemyDropDistance && accumulatedMoveDistance + delta >= GamePlay.EnemyDropDistance)
    {
      Machine.Fire(Trigger.EnemyHasTravelledDropDistance);
    }
    accumulatedMoveDistance += delta;
  }

  private void MoveEnemy()
  {
    var pos = EnemyGrid.transform.position + EnemyDirection * CurrentEnemySpeed * Time.deltaTime;
    EnemyGrid.transform.SetPositionAndRotation(pos, Quaternion.identity);
  }

  private void ResumeAfterRespawn()
  {
    CommandBus.RequestPlayerReset();
  }

  // Message from the Input System
  private void OnMove(InputValue inputValue)
  {
    var leftRightInput = inputValue.Get<Vector2>().x;

    if (leftRightInput < 0)
    {
      CommandBus.RequestPlayerMoveLeft();
    }
    else if (leftRightInput > 0)
    {
      CommandBus.RequestPlayerMoveRight();
    }
    else
    {
      CommandBus.RequestPlayerStop();
    }
  }

  // Message from the Input System
  private void OnFire(InputValue inputValue)
  {
    CommandBus.RequestPlayerFire();
  }

  private void EventBus_OnSceneTransitionEnterCompleted()
  {
    Debug.Log("Scene tranisition enter complete");
    Machine.Fire(Trigger.SceneTransitionEnterComplete);
  }

  private void EventBus_OnCollisionWithRightWall()
  {
    Machine.Fire(Trigger.RightWallCollision);
  }

  private void EventBus_OnCollisionWithLeftWall()
  {
    Machine.Fire(Trigger.LeftWallCollision);
  }

  private void EventBus_OnMissileHitEnemy(GameObject missile, GameObject enemy)
  {
    SoundController.PlayExplosion();
    Explosion.Explode(missile.transform.position);
    enemy.SetActive(false);
    missile.SetActive(false);
    
    var worthPoints = enemy.GetComponent<WorthPoints>();
    Score += worthPoints != null ? worthPoints.PointValue : 0;
    EventBus.RaiseScoreChanged(Score);
    AwardBonusLife(Score);
    EnemiesRemaining -= 1;
    if(EnemiesRemaining == 0)
    {
      SoundController.StopPlayingEnemyMarch();
      Machine.Fire(Trigger.WaveCleared, () => EventBus.RaiseWaveCleared(WaveNumber));
      WaveNumber++;
    }
    else if(EnemiesRemaining == GamePlay.SecondStepEnemyCount)
    {
      SoundController.PlayFastEnemyMarch();
      CurrentEnemySpeed = CurrentEnemySpeedBase * GamePlay.SecondStepSpeedMultiplier;
    }
    else if(EnemiesRemaining == GamePlay.FirstStepEnemyCount)
    {
      SoundController.PlayMediumEnemyMarch();
      CurrentEnemySpeed = CurrentEnemySpeedBase * GamePlay.FirstStepSpeedMultiplier;
    }
  }

  private void EventBus_OnBombHitPlayer(GameObject bomb, GameObject player)
  {
    player.SetActive(false);
    bomb.SetActive(false);
    Explosion.Explode(player.transform.position);
    SoundController.PlayExplosion();
    LivesRemaining--;
    EventBus.RaisePlayerDead(LivesRemaining);
    if (LivesRemaining <= 0)
    {
      Machine.Fire(Trigger.GameOver);
    }
    else
    {
      Invoke(nameof(ResumeAfterRespawn), GamePlay.RespawnTime);
    }
  }


  private void EventBus_OnEnemyHasReachedTheBase()
  {
    Machine.Fire(Trigger.GameOver);
  }

  private void OnEnterGameOver()
  {
    Debug.Log("Game Over!");
    EventBus.RaiseGameOver();
    CommandBus.RequestSceneTransitionLeave();
  }

  private void EventBus_OnBombHitShield(GameObject bomb, GameObject grid)
  {
    SoundController.PlayExplosion();
    EraseShieldCell(grid, bomb.transform.position + new Vector3(0, -0.05f, 0));
    bomb.SetActive(false);
    Explosion.Explode(bomb.transform.position);
  }

  private void EventBus_OnMissileHitShield(GameObject missile, GameObject grid)
  {
    SoundController.PlayExplosion();
    EraseShieldCell(grid, missile.transform.position + new Vector3(0, 0.05f, 0));
    missile.SetActive(false);
    Explosion.Explode(missile.transform.position);
  }

  private void EventBus_OnEnemyHitShield(GameObject enemy, GameObject grid)
  {
    SoundController.PlayExplosion();
    Explosion.Explode(enemy.transform.position);
    EraseShieldCell(grid, enemy.transform.position);
  }

  private void EventBus_OnSceneTransitionLeaveCompleted()
  {
    SceneManager.LoadScene((int)SceneIndex.GameOver);
  }

  private void EraseShieldCell(GameObject gridGameObject, Vector3 worldPosition)
  {
    var tilemap = gridGameObject.GetComponent<Tilemap>();
    var cell = tilemap?.WorldToCell(worldPosition);
    tilemap?.SetTile(cell ?? Vector3Int.zero, null);
  }

  private void CommandBus_OnBombDrop(Vector3 pos)
  {
    SoundController.PlayBombDrop();
  }

  private void EventBus_OnMissileFired()
  {
    SoundController.PlayPewPew();
  }

  private void AwardBonusLife(int score)
  {
    if(score >= BonusLifeScore)
    {
      LivesRemaining += 1;
      Debug.Log($"Bonus life awarded. Lives remaining: {LivesRemaining}");
      EventBus.RaiseBonusLifeAwarded(LivesRemaining);
      SoundController.PlayBonusLife();
      BonusLifeScore += GamePlay.BonusLifePoints;
    }
  }
}
