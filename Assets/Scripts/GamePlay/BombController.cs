using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour
{
  public EventBusDefinition EventBus;
  public GamePlayDefinition GamePlay;

  private void Update()
  {
    var delta = GamePlay.BombSpeed * Vector3.down * Time.deltaTime;
    var pos = transform.position + delta;
    transform.SetPositionAndRotation(pos, Quaternion.identity);
  }

  private void OnTriggerEnter2D(Collider2D collision)
  {
    if (CollidesWithFilteredObject(collision.tag)) return;

    if (collision.CompareTag("Player"))
    {
      Debug.Log($"Player has collided with {collision.gameObject.name}:{collision.tag}");
      EventBus.RaiseOnBombHitPlayer(gameObject, collision.gameObject);
    }
    else if (collision.CompareTag("Ground"))
    {
      Debug.Log($"Bomb has missed");
      gameObject.SetActive(false);
    }
    else
    {
      Debug.LogError($"Bomb {gameObject.name} has collided with {collision.gameObject.name}:{collision.tag} but there is no handler");
    }
  }

  private bool CollidesWithFilteredObject(string tag)
  {
    return tag == "Enemy" || tag == "Bomb" || tag == "LeftBorder" || tag == "RightBorder";
  }
}
