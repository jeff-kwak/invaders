using Gr8tGames;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
  public CommandBusDefinition Command;
  public EventBusDefinition Event;
  public GamePlayDefinition GamePlay;

  public GameObject MisslePrefab;
  public Transform MissileSpawnPoint;

  private enum FireState
  {
    Ready,
    Cooldown
  }

  private enum FireTrigger
  {
    Fire,
    CooldownComplete
  }

  private StateMachine<FireState, FireTrigger> FireStateMachine;

  private Vector3 PlayerDirection = Vector3.zero;
  private float FireTimer = 0;
  private float FireCooldownTime = float.MaxValue;
  private List<GameObject> MissilePool = new List<GameObject>();

  private void Awake()
  {
    FireStateMachine = new StateMachine<FireState, FireTrigger>(FireState.Ready);

    FireStateMachine.Configure(FireState.Ready)
      .Permit(FireTrigger.Fire, FireState.Cooldown);

    FireStateMachine.Configure(FireState.Cooldown)
      .OnEnter(EnterCooldown)
      .Permit(FireTrigger.CooldownComplete, FireState.Ready);

    FireCooldownTime = GamePlay.PlayerFireLimit > 0 ? 1f / GamePlay.PlayerFireLimit : float.MaxValue;

    Command.OnPlayerMoveRight += Command_OnPlayerMoveRight;
    Command.OnPlayerMoveLeft += Command_OnPlayerMoveLeft;
    Command.OnPlayerStop += Command_OnPlayerStop;
    Command.OnPlayerFire += Command_OnPlayerFire;
    Command.OnPlayerReset += Command_OnPlayerReset;
  }

  private void OnDestroy()
  {
    Command.OnPlayerMoveRight -= Command_OnPlayerMoveRight;
    Command.OnPlayerMoveLeft -= Command_OnPlayerMoveLeft;
    Command.OnPlayerStop -= Command_OnPlayerStop;
    Command.OnPlayerFire -= Command_OnPlayerFire;
    Command.OnPlayerReset -= Command_OnPlayerReset;
  }

  private void Update()
  {
    UpdateFireTimer();
    MovePlayer();
  }

  private void UpdateFireTimer()
  {
    if(FireTimer < FireCooldownTime && FireTimer + Time.deltaTime >= FireCooldownTime)
    {
      FireStateMachine.Fire(FireTrigger.CooldownComplete);
    }

    FireTimer += Time.deltaTime;
  }

  private void MovePlayer()
  {
    var delta = PlayerDirection * GamePlay.PlayerSpeed * Time.deltaTime;
    var pos = transform.position + delta;
    if (pos.x <= GamePlay.PlayerRightLimit && pos.x >= GamePlay.PlayerLeftLimit)
    {
      transform.SetPositionAndRotation(pos, Quaternion.identity);
    }
  }

  private void EnterCooldown()
  {
    FireTimer = 0;
  }

  private void Command_OnPlayerMoveLeft()
  {
    PlayerDirection = Vector3.left;
  }

  private void Command_OnPlayerMoveRight()
  {
    PlayerDirection = Vector3.right;
  }
  private void Command_OnPlayerStop()
  {
    PlayerDirection = Vector3.zero;
  }

  private void Command_OnPlayerFire()
  {
    FireStateMachine.Fire(FireTrigger.Fire, SpawnMissle);
  }

  private void Command_OnPlayerReset()
  {
    gameObject.SetActive(true);
    var pos = new Vector3(0, transform.position.y, 0);
    transform.SetPositionAndRotation(pos, Quaternion.identity);
    FireStateMachine.Fire(FireTrigger.CooldownComplete);
  }

  private void SpawnMissle()
  {
    Debug.Log("Missle fired!");
    var usedMissle = MissilePool.FirstOrDefault(m => !m.activeInHierarchy);
    var missile = usedMissle ?? MakeNewMissile();
    missile.transform.SetPositionAndRotation(MissileSpawnPoint.position, Quaternion.identity);
    missile.SetActive(true);
  }

  private GameObject MakeNewMissile()
  {
    var missile = Instantiate(MisslePrefab, MissileSpawnPoint.position, Quaternion.identity);
    MissilePool.Add(missile);
    return missile;
  }
}
