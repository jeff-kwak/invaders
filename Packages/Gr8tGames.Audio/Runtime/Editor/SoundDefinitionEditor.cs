using UnityEditor;
using UnityEngine;

namespace Gr8tGames.Audio
{

  [CustomEditor(typeof(SoundDefinition), true)]
  public class SoundDefinitionEditor : Editor
  {
    [SerializeField]
    private AudioSource PreviewAudio;

    public void OnEnable()
    {
      PreviewAudio = EditorUtility
        .CreateGameObjectWithHideFlags("Preview Audio Source", HideFlags.HideAndDontSave, typeof(AudioSource))
        .GetComponent<AudioSource>();
    }

    public void OnDisable()
    {
      DestroyImmediate(PreviewAudio.gameObject);
    }

    public override void OnInspectorGUI()
    {
      DrawDefaultInspector();
      EditorGUI.BeginDisabledGroup(serializedObject.isEditingMultipleObjects);
      if(GUILayout.Button("Preview"))
      {
        var sound = (SoundDefinition)target;
        PreviewAudio.clip = sound.Clip;
        PreviewAudio.volume = sound.Volume;
        PreviewAudio.pitch = sound.Pitch;
        PreviewAudio.loop = sound.IsLoop;
        PreviewAudio.Play();
      }
    }
  }
}
