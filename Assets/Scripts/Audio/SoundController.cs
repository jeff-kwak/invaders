using Gr8tGames.Audio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : SoundHandler
{
  public SoundDefinition MenuBackgroundMusic;
  public SoundDefinition MenuClick;

  public void PlayMenuBackgroundMusic()
  {
    Play(MenuBackgroundMusic);
  }

  public void PlayMenuClick()
  {
    Play(MenuClick);
  }
}
