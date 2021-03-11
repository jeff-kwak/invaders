using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/GamePlay")]
public class GamePlayDefinition : ScriptableObject
{
  public float EnemySpeed = 2f; // u/s
  public float EnemyDropDistance = 0.7f; // u
  public float PlayerSpeed = 1f; // u/s 
  public float PlayerLeftLimit = -5f; // u
  public float PlayerRightLimit = 5f; // u
}
