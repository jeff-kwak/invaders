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
  public event EventBusHandler<GameObject, GameObject> OnMissileHitShield;
  public event EventBusHandler<GameObject, GameObject> OnBombHitPlayer;
  public event EventBusHandler<GameObject, GameObject> OnBombHitShield;
  public event EventBusHandler<GameObject, GameObject> OnEnemyHitShield;
  public event EventBusHandler<int> OnPlayerDead;
  public event EventBusHandler OnGameOver;
  public event EventBusHandler<int> OnWaveCleared;
  public event EventBusHandler<int> OnScoreChanged;
  public event EventBusHandler OnEnemyHasReachedTheBase;

  public void RaiseOnPlayClicked()
  {
    OnPlayClicked?.Invoke();
  }

  public void RaiseMissileHitEnemy(GameObject missile, GameObject enemy)
  {
    OnMissileHitEnemy?.Invoke(missile, enemy);
  }

  public void RaiseOnBombHitPlayer(GameObject bomb, GameObject player)
  {
    OnBombHitPlayer?.Invoke(bomb, player);
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

  public void RaiseEnemyHasReachedTheBase()
  {
    OnEnemyHasReachedTheBase?.Invoke();
  }

  public void RaiseOnCollisionWithRightWall()
  {
    OnCollisionWithRightWall?.Invoke();
  }

  public void RaiseOnBombHitShield(GameObject bomb, GameObject grid)
  {
    OnBombHitShield?.Invoke(bomb, grid);
  }

  public void RaiseOnMissileHitShield(GameObject missile, GameObject grid)
  {
    OnMissileHitShield?.Invoke(missile, grid);
  }

  internal void RaiseOnEnemyHitShield(GameObject enemy, GameObject grid)
  {
    OnEnemyHitShield?.Invoke(enemy, grid);
  }

  internal void RaisePlayerDead(int livesRemaining)
  {
    OnPlayerDead?.Invoke(livesRemaining);
  }

  internal void RaiseGameOver()
  {
    OnGameOver?.Invoke();
  }

  internal void RaiseWaveCleared(int waveNumber)
  {
    OnWaveCleared?.Invoke(waveNumber);
  }

  internal void RaiseScoreChanged(int points)
  {
    OnScoreChanged?.Invoke(points);
  }
}
