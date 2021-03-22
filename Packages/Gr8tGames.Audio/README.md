# Gr8tGames Audio

Provides simple sound features that let you define a particular sound, preview the sound in the inspector, and play the sound using an object pool at a location within the Scene.

## Use

1. Use the create asset menu "Sound > Sound Definition" to create a new sound definition.
2. The properties mirror the standard properties on an `AudioSource` component.
3. Providing more than one clip in the list of `AudioClips` will allow the `SoundHandler` to randomly pick a clip to play.
4. Play the sound by calling `Play` on an instance of the `SoundHandler`.

## Other projects used
Included in this project, because UPM doesn't seem to fully support importing projects that have dependencies on projects with Git URLs. Is "Simple Min Max Slider" from [GucioDevs Simple Min Max Slider](https://github.com/GucioDevs/SimpleMinMaxSlider)

