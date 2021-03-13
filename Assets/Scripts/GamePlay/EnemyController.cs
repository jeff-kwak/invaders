using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
  public CommandBusDefinition Command;
  public EventBusDefinition Event;

  private void Awake()
  {
    Command.OnEnemyReset += Command_OnEnemyReset;
  }

  private void OnDestroy()
  {
    Command.OnEnemyReset -= Command_OnEnemyReset;
  }

  private void Command_OnEnemyReset()
  {
    gameObject.SetActive(true);
  }

  private void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.CompareTag("LeftBorder"))
    {
      Event.RaiseOnCollisionWithLeftWall();
    }
    else if (collision.CompareTag("RightBorder"))
    {
      Event.RaiseOnCollisionWithRightWall();
    }
    else if(collision.CompareTag("Missile"))
    {
      Debug.Log($"{gameObject.name} hit by missile");
    }
    else
    {
      Debug.LogError($"{gameObject.name} entered trigger {collision.gameObject.name}: {collision.tag} but there was no handler");
    }
  }
}
