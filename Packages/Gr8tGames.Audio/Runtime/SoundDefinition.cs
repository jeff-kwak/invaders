using UnityEngine;
using UnityEngine.Audio;

namespace Gr8tGames.Audio
{
  [CreateAssetMenu(menuName = "Sound/SoundDefinition")]
  public class SoundDefinition : ScriptableObject
  {
    public AudioClip[] Clips;
    public AudioMixerGroup Output;
    
    [MinMaxSlider(0, 1f)]
    public Vector2 VolumeRange = new Vector2(1f, 1f);
    
    [MinMaxSlider(0.05f, 2f)]
    public Vector2 PitchRange = new Vector2(1f, 1f);

    [Range(0, 1.0f)]
    [Tooltip("A 0 means the sound is not effected by 3D space")]
    public float SpatialBend = 0.5f;

    public bool IsLoop = false;

    public bool IsOneShot = false;

    public float Volume => Random.Range(VolumeRange.x, VolumeRange.y);
    public float Pitch => Random.Range(PitchRange.x, PitchRange.y);

    public AudioClip Clip => Clips?.Length > 0 ? Clips[Random.Range(0, Clips.Length)] : default;
  }
}
