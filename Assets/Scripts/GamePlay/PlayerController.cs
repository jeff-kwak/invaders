using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
  public CommandBusDefinition Command;
  public EventBusDefinition Event;
  public GamePlayDefinition GamePlay;

  private Vector3 PlayerDirection = Vector3.zero;

  private void Awake()
  {
    Command.OnPlayerMoveRight += Command_OnPlayerMoveRight;
    Command.OnPlayerMoveLeft += Command_OnPlayerMoveLeft;
    Command.OnPlayerStop += Command_OnPlayerStop;
  }

  private void OnDestroy()
  {
    Command.OnPlayerMoveRight -= Command_OnPlayerMoveRight;
    Command.OnPlayerMoveLeft -= Command_OnPlayerMoveLeft;
    Command.OnPlayerStop -= Command_OnPlayerStop;
  }

  private void Update()
  {
    MovePlayer();
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
}
