using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
  public CommandBusDefinition Command;
  public EventBusDefinition Event;
  public GamePlayDefinition GamePlay;

  private float BombTimer = float.MaxValue;

  private void Awake()
  {
    Command.OnEnemyReset += Command_OnEnemyReset;
  }

  private void OnDestroy()
  {
    Command.OnEnemyReset -= Command_OnEnemyReset;
  }

  private void Start()
  {
    EnemyReset();
  }

  private void Command_OnEnemyReset()
  {
    EnemyReset();
  }

  private void EnemyReset()
  {
    Debug.Log("enemy reset");
    StopAllCoroutines();
    gameObject.SetActive(true);
    BombTimer = Random.Range(GamePlay.MinTimeBetweenBombs, GamePlay.MaxTimeBetweenBombs);
    Debug.Log($"{gameObject.name}:{gameObject.tag} is preparing to fire every {BombTimer}s");
    StartCoroutine(DropBombsAtWill());
  }

  private IEnumerator DropBombsAtWill()
  {
    while (true)
    {
      yield return new WaitForSeconds(BombTimer);

      var hit = Physics2D.Raycast(transform.position, Vector2.down);
      if (hit.collider != null && !hit.collider.CompareTag("Enemy"))
      {
        Command.RequestBombDrop(transform.position);
      }  
    }
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
    else if(collision.CompareTag("Bomb"))
    {
      // no-operation
    }
    else
    {
      Debug.LogError($"{gameObject.name} entered trigger {collision.gameObject.name}: {collision.tag} but there was no handler");
    }
  }
}
