using Gr8tGames;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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
    MoveDownOnRight
  }

  private enum Trigger
  {
    Setup,
    SetupComplete,
    SceneTransitionComplete,
    MoveEnemy,
    LeftWallCollision,
    RightWallCollision,
    EnemyHasMovedDown,
    EnemyHasTravelledDropDistance
  }

  private StateMachine<State, Trigger> Machine;
  private Vector3 EnemyDirection = Vector3.left;
  private float accumulatedMoveDistance = 0f;


  private void Awake()
  {
    Machine = new StateMachine<State, Trigger>(State.Start);

    Machine.Configure(State.Start)
      .Permit(Trigger.Setup, State.Ready);

    Machine.Configure(State.Setup)
      .OnEnter(SetupGame)
      .Permit(Trigger.SetupComplete, State.Ready);

    Machine.Configure(State.Ready)
      .OnEnter(RequestSceneTransition)
      .Permit(Trigger.SceneTransitionComplete, State.MoveLeft);

    Machine.Configure(State.MoveLeft)
      .OnEnter(ChangeDirectionToLeft)
      .Permit(Trigger.MoveEnemy, State.MoveLeft)
      .Permit(Trigger.LeftWallCollision, State.MoveDownOnLeft);

    Machine.Configure(State.MoveDownOnLeft)
      .OnEnter(ChangeDirectionToDown)
      .OnExit(() => Debug.Log($"Leaving state {Machine.State}"))
      .Permit(Trigger.MoveEnemy, State.MoveDownOnLeft)
      .Permit(Trigger.EnemyHasMovedDown, State.MoveDownOnLeft)
      .Permit(Trigger.EnemyHasTravelledDropDistance, State.MoveRight);

    Machine.Configure(State.MoveRight)
      .OnEnter(ChangeDirectionToRight)
      .OnExit(() => Debug.Log($"Leaving state {Machine.State}"))
      .Permit(Trigger.MoveEnemy, State.MoveRight)
      .Permit(Trigger.RightWallCollision, State.MoveDownOnRight);

    Machine.Configure(State.MoveDownOnRight)
      .OnEnter(ChangeDirectionToDown)
      .Permit(Trigger.MoveEnemy, State.MoveDownOnRight)
      .Permit(Trigger.EnemyHasMovedDown, State.MoveDownOnRight)
      .Permit(Trigger.EnemyHasTravelledDropDistance, State.MoveLeft);

    EventBus.OnSceneTransitionEnterCompleted += EventBus_OnSceneTransitionEnterCompleted;
    EventBus.OnCollisionWithLeftWall += EventBus_OnCollisionWithLeftWall;
    EventBus.OnCollisionWithRightWall += EventBus_OnCollisionWithRightWall;
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

  private void OnDestroy()
  {
    EventBus.OnSceneTransitionEnterCompleted -= EventBus_OnSceneTransitionEnterCompleted;
    EventBus.OnCollisionWithLeftWall -= EventBus_OnCollisionWithLeftWall;
    EventBus.OnCollisionWithRightWall -= EventBus_OnCollisionWithRightWall;
  }

  private void Start()
  {
    Machine.Fire(Trigger.Setup);
  }

  private void Update()
  {
    AccumulateMoveDistance();
    Machine.Fire(Trigger.MoveEnemy, MoveEnemy);
  }

  private void SetupGame()
  {
    EnemyGrid.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
    Machine.Fire(Trigger.SetupComplete);
  }

  private void RequestSceneTransition()
  {
    CommandBus.RequestSceneTransitionEnter();
  }

  private void AccumulateMoveDistance()
  {
    var delta = GamePlay.EnemySpeed * Time.deltaTime;
    if(accumulatedMoveDistance < GamePlay.EnemyDropDistance && accumulatedMoveDistance + delta >= GamePlay.EnemyDropDistance)
    {
      Machine.Fire(Trigger.EnemyHasTravelledDropDistance);
    }
    accumulatedMoveDistance += delta;
  }

  private void MoveEnemy()
  {
    var pos = EnemyGrid.transform.position + EnemyDirection * GamePlay.EnemySpeed * Time.deltaTime;
    EnemyGrid.transform.SetPositionAndRotation(pos, Quaternion.identity);
  }

  private void OnMove(InputValue inputValue)
  {
    var leftRightInput = inputValue.Get<Vector2>().x;
    
    if(leftRightInput < 0)
    {
      CommandBus.RequestPlayerMoveLeft();
    }
    else if(leftRightInput > 0)
    {
      CommandBus.RequestPlayerMoveRight();
    }
    else
    {
      CommandBus.RequestPlayerStop();
    }    
  }

  private void EventBus_OnSceneTransitionEnterCompleted()
  {
    Debug.Log("Scene tranisition enter complete");
    Machine.Fire(Trigger.SceneTransitionComplete);
  }

  private void EventBus_OnCollisionWithRightWall()
  {
    Debug.Log("collision with right wall");
    Machine.Fire(Trigger.RightWallCollision);
  }

  private void EventBus_OnCollisionWithLeftWall()
  {
    Debug.Log("collision with left wall");
    Machine.Fire(Trigger.LeftWallCollision);
  }
}
