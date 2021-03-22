using System.Collections.Generic;
using UnityEngine;

namespace Gr8tGames.Audio
{
  public class SoundHandler : MonoBehaviour
  {
    private GameObject SoundPrefab;
    private Dictionary<SoundDefinition, List<GameObject>> SoundPool;

    private void Awake()
    {
      SoundPrefab = new GameObject("SoundPrefab", typeof(AudioSource));
      SoundPool = new Dictionary<SoundDefinition, List<GameObject>>();
    }

    public AudioSource Play(SoundDefinition sound, Vector3? position = null)
    {
      var pos = position ?? Vector3.zero;
      var soundGameObject = GetSoundFromPool(sound);
      soundGameObject.SetActive(true);
      soundGameObject.transform.SetPositionAndRotation(pos, Quaternion.identity);
      var audio = soundGameObject.GetComponent<AudioSource>();
      audio.volume = sound.Volume;
      audio.pitch = sound.Pitch;
      audio.spatialBlend = sound.SpatialBend;
      audio.outputAudioMixerGroup = sound.Output;
      audio.loop = sound.IsLoop;
      if (sound.IsLoop || !sound.IsOneShot)
      {
        audio.clip = sound.Clip;
        audio.Play();
      }
      else
      {
        audio.PlayOneShot(sound.Clip);
      }
      
      return audio;
    }

    public void Stop(AudioSource audio)
    {
      audio.Stop();
      audio.gameObject.SetActive(false);
    }

    private GameObject GetSoundFromPool(SoundDefinition definition)
    {
      if (!SoundPool.ContainsKey(definition))
      {
        SoundPool[definition] = new List<GameObject>();
      }
      var sounds = SoundPool[definition];
      foreach (var sound in sounds)
      {
        var audio = sound.GetComponent<AudioSource>();
        if (!audio.isPlaying) return sound;
      }

      return MakeSound(sounds);
    }

    private GameObject MakeSound(List<GameObject> sounds)
    {
      var newSound = Instantiate(SoundPrefab);
      newSound.SetActive(false);
      sounds.Add(newSound);
      return newSound;
    }
  }
}
