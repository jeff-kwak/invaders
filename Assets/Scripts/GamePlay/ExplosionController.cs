using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionController : MonoBehaviour
{
  public EventBusDefinition Event;
  public GameObject ExplosionPrefab;

  private Queue<GameObject> ExplosionQueue = new Queue<GameObject>();

  private void Awake()
  {
    Event.OnExplosionComplete += Event_OnExplosionComplete;
  }

  private void OnDestroy()
  {
    Event.OnExplosionComplete -= Event_OnExplosionComplete;
  }

  private void Event_OnExplosionComplete(GameObject explosion)
  {
    explosion.SetActive(false);
    ExplosionQueue.Enqueue(explosion);
  }

  public void Explode(Vector3 pos)
  {
    var explosion = GetExplosionFromQueue();
    explosion.transform.SetPositionAndRotation(pos, Quaternion.identity);
    explosion.SetActive(true);
  }

  private GameObject GetExplosionFromQueue()
  {
    if(ExplosionQueue.Count == 0)
    {
      ExplosionQueue.Enqueue(MakeExplosion());
    }
    return ExplosionQueue.Dequeue();
  }

  private GameObject MakeExplosion()
  {
    var explosion = Instantiate(ExplosionPrefab);
    explosion.SetActive(false);
    return explosion;
  }
}
