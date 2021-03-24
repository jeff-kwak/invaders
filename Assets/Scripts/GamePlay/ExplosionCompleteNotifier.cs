using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionCompleteNotifier : MonoBehaviour
{
  public EventBusDefinition Event;

  private void OnParticleSystemStopped()
  {
    Event.RaiseExplosionComplete(gameObject);
  }
}
