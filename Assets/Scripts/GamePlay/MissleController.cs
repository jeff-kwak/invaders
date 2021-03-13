using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissleController : MonoBehaviour
{
  public EventBusDefinition EventBus;
  public GamePlayDefinition GamePlay;

  private void Update()
  {
    var delta = GamePlay.MissleSpeed * Vector3.up * Time.deltaTime;
    var pos = transform.position + delta;
    transform.SetPositionAndRotation(pos, Quaternion.identity);
  }

  private void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.CompareTag("Enemy"))
    {
      Debug.Log($"Missle has collided with {collision.gameObject.name}");
      EventBus.RaiseMissileHitEnemy(gameObject, collision.gameObject);
    }
    else if(collision.CompareTag("Roof"))
    {
      Debug.Log($"Missile has missled");
      gameObject.SetActive(false);
    }
    else
    {
      Debug.LogError($"Missile {gameObject.name} has collided with {collision.gameObject.name}:{collision.tag} but there is no handler");
    }
  }

}
