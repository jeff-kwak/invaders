using Gr8tGames.Audio;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : SoundHandler
{
  public SoundDefinition MenuBackgroundMusic;
  public SoundDefinition MenuClick;
  public SoundDefinition SlowEnemyMarch;
  public SoundDefinition MediumEnemyMarch;
  public SoundDefinition FastEnemyMarch;
  public SoundDefinition PewPew;
  public SoundDefinition Explosion;
  public SoundDefinition BombDrop;

  private AudioSource enemyMarch;

  public void PlayMenuBackgroundMusic()
  {
    Play(MenuBackgroundMusic);
  }

  public void PlayMenuClick()
  {
    Play(MenuClick);
  }

  public void PlaySlowEnemyMarch()
  {
    Debug.Log("Play slow march");
    if (enemyMarch != null) Stop(enemyMarch);
    enemyMarch = Play(SlowEnemyMarch);
  }

  public void PlayMediumEnemyMarch()
  {
    if (enemyMarch != null) Stop(enemyMarch);
    enemyMarch = Play(MediumEnemyMarch);
  }

  public void PlayFastEnemyMarch()
  {
    if (enemyMarch != null) Stop(enemyMarch);
    enemyMarch = Play(FastEnemyMarch);
  }

  public void StopPlayingEnemyMarch()
  {
    if (enemyMarch != null) Stop(enemyMarch);
  }

  public void PlayPewPew()
  {
    Play(PewPew);
  }

  public void PlayBombDrop()
  {
    Play(BombDrop);
  }

  public void PlayExplosion()
  {
    Play(Explosion);
  }
}
