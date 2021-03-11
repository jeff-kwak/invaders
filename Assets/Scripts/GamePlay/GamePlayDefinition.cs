using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/GamePlay")]
public class GamePlayDefinition : ScriptableObject
{
  public float EnemySpeed = 2f; // 2u/s
  public float EnemyDropDistance = 0.7f; // u
}
