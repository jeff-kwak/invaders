using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/GamePlay")]
public class GamePlayDefinition : ScriptableObject
{
  public float EnemySpeed = 0.7f; // u/s
  public float EnemyDropDistance = 0.2f; // u
  public float PlayerSpeed = 1f; // u/s 
  public float PlayerLeftLimit = -5f; // u
  public float PlayerRightLimit = 5f; // u
  public float PlayerFireLimit = 3f; // shots/s
  public float MissleSpeed = 3f; // u/s
  public float BombSpeed = 3f; // u/s
  public float MinTimeBetweenBombs = 2f; // seconds
  public float MaxTimeBetweenBombs = 10f; // bomb/s
  public float RespawnTime = 3f; // seconds
  public int InitialNumberOfLives = 3;
}
