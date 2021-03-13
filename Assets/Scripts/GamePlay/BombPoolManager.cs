using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BombPoolManager : MonoBehaviour
{
  public CommandBusDefinition Command;
  public GameObject BombPrefab;

  private List<GameObject> BombPool = new List<GameObject>();

  private void Awake()
  {
    Command.OnBombDrop += Command_OnBombDrop;
  }

  private void OnDestroy()
  {
    Command.OnBombDrop -= Command_OnBombDrop;
  }

  private void Command_OnBombDrop(Vector3 bombDoor)
  {
    var usedBomb = BombPool.FirstOrDefault(bomb => !bomb.activeInHierarchy);
    var bomb = usedBomb ?? MakeNewBomb(bombDoor);
    bomb.SetActive(true);
    bomb.transform.SetPositionAndRotation(bombDoor, Quaternion.identity);
  }

  private GameObject MakeNewBomb(Vector3 bombDoor)
  {
    var bomb = Instantiate(BombPrefab);
    bomb.name = $"bomb-{BombPool.Count}";
    BombPool.Add(bomb);
    return bomb;
  }
}
